using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace ClubMap.Filters
{
    public class HMACAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        private static readonly Dictionary<string, string> AllowedApps = new Dictionary<string, string>();
        private const UInt64 RequestMaxAgeInSeconds = 300; //5 mins
        private const string AuthenticationScheme = "amx";

        public bool AllowMultiple { get; private set; }

        public HMACAuthenticationAttribute()
        {
            if (AllowedApps.Count == 0)
            {
                AllowedApps.Add("4d53bce03ec34c0a911182d4c228ee6c", "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=");
            }
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            if (request.Headers.Contains("Amx"))
            {
                var rawAuthzHeader = request.Headers.GetValues("Amx").FirstOrDefault();
                var authorizationHeaderValues = GetAuthorizationHeaderValues(rawAuthzHeader);
                if (authorizationHeaderValues != null)
                {
                    var APPId = authorizationHeaderValues[0];
                    var incomingBase64Signature = authorizationHeaderValues[1];
                    var nonce = authorizationHeaderValues[2];
                    var requestTimeStamp = authorizationHeaderValues[3];
                    var isValid = IsValidRequest(request, APPId, incomingBase64Signature, nonce, requestTimeStamp);
                    if (isValid.Result)
                    {
                        var currentPrincipal = new GenericPrincipal(new GenericIdentity(APPId), null);
                        context.Principal = currentPrincipal;
                        Debug.WriteLine("Authorized access");
                    }
                    else
                    {
                        context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                    }
                }
                else
                {
                    context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
                }
            }
            else
            {
                context.ErrorResult = new UnauthorizedResult(new AuthenticationHeaderValue[0], context.Request);
            }
            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            context.Result = new ResultWithChallenge(context.Result);
            return Task.FromResult(0);
        }

        private static string[] GetAuthorizationHeaderValues(string rawAuthzHeader)
        {
            var credArray = rawAuthzHeader.Replace("amx ", "").Split(':');
            return credArray.Length == 4 ? credArray : null;
        }
        private static async Task<bool> IsValidRequest(HttpRequestMessage req, string APPId, string incomingBase64Signature, string nonce, string requestTimeStamp)
        {
            var requestContentBase64String = "";
            var requestUri = HttpUtility.UrlEncode(req.RequestUri.AbsoluteUri.ToLower());
            var requestHttpMethod = req.Method.Method;

            if (!AllowedApps.ContainsKey(APPId))
            {
                return false;
            }

            var sharedKey = AllowedApps[APPId];

            if (IsReplayRequest(nonce, requestTimeStamp))
            {
                return false;
            }

            var hash = await ComputeHash(req.Content);

            if (hash != null)
            {
                requestContentBase64String = Convert.ToBase64String(hash);
            }

            var data = String.Format("{0}{1}{2}{3}{4}{5}", APPId, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);

            var secretKeyBytes = Convert.FromBase64String(sharedKey);

            var signature = Encoding.UTF8.GetBytes(data);

            using (var hmac = new HMACSHA256(secretKeyBytes))
            {
                var signatureBytes = hmac.ComputeHash(signature);

                return (incomingBase64Signature.Equals(Convert.ToBase64String(signatureBytes), StringComparison.Ordinal));
            }
        }
        private static bool IsReplayRequest(string nonce, string requestTimeStamp)
        {
            if (MemoryCache.Default.Contains(nonce))
            {
                return true;
            }

            var epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            var currentTs = DateTime.UtcNow - epochStart;

            var serverTotalSeconds = Convert.ToUInt64(currentTs.TotalSeconds);
            var requestTotalSeconds = Convert.ToUInt64(requestTimeStamp);

            if ((serverTotalSeconds - requestTotalSeconds) > RequestMaxAgeInSeconds)
            {
                return true;
            }

            MemoryCache.Default.Add(nonce, requestTimeStamp, DateTimeOffset.UtcNow.AddSeconds(RequestMaxAgeInSeconds));

            return false;
        }

        private static async Task<byte[]> ComputeHash(HttpContent httpContent)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hash = null;
                var content = await httpContent.ReadAsByteArrayAsync();
                if (content.Length != 0)
                {
                    hash = md5.ComputeHash(content);
                }
                return hash;
            }
        }
    }
    public class ResultWithChallenge : IHttpActionResult
    {
        private const string AuthenticationScheme = "amx";
        private readonly IHttpActionResult _next;

        public ResultWithChallenge(IHttpActionResult next)
        {
            _next = next;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await _next.ExecuteAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(AuthenticationScheme));
            }

            return response;
        }
    }
}