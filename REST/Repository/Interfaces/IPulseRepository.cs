using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using REST.Models;

namespace REST.Repository.Interfaces
{
    public interface IPulseRepository
    {
        IQueryable<PulseDTO> GetAll(string userId);
        Task<Pulse> GetById(int id);
        Task Add(Pulse pulse, string userId);
        IQueryable<DateDTO> GetMeasurementsDates(string userId);
        IQueryable<PulseDTO> GetMeasurements(string userId, DateTime dateTime);
    }
}
