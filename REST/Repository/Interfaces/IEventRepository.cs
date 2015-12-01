using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using REST.Models;

namespace REST.Repository.Interfaces
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetEvents();
        Task<Event> GetEvent(int eventId);
        Task JoinToEvent(string userId, Event @event);
        Task AddNewEvent(Event @event);
        Task SetEventAsSuccess(int eventId);
    }
}
