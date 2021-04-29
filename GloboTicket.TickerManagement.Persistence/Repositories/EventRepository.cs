using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GloboTicket.TickerManagement.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(GloboTicketDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> IsEventNameAndDateUnique(string eventName, DateTime eventDate)
        {
            var matches = _dbContext.Events.Any(e => e.Name.Equals(eventName) && e.Date.Date.Equals(eventDate.Date));
            return Task.FromResult(matches);
        }
    }
}