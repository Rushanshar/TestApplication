
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DL.Entities;
using DL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DL.Repositories
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SubscriberRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<long>> GetByClientAsync(long clientId)
        {
            return await _dbContext.Subscribers.Where(q => q.ClientId == clientId).Select(q => q.SubscriberId).ToListAsync();
        }

        public async Task AddAsync(long subscriberId, long clientForSubscribeId)
        {
            var existing = await GetExisting(subscriberId, clientForSubscribeId);

            if (existing == null)
            {
                await _dbContext.Subscribers.AddAsync(new Subscriber {ClientId = clientForSubscribeId, SubscriberId = subscriberId});
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(long subscriberId, long clientForUnsubscribeId)
        {
            var existing = await GetExisting(subscriberId, clientForUnsubscribeId);

            if (existing != null)
            {
                _dbContext.Subscribers.Remove(existing);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<long>> GetTopClientsIdsAsync(int take)
        {
            return await _dbContext.Subscribers.GroupBy(q => q.ClientId).OrderBy(q => q.Count()).Take(take).Select(q => q.Key).ToListAsync();
        }

        public async Task<Subscriber> GetExisting(long subscriberId, long clientId)
        {
            return await _dbContext.Subscribers.FirstOrDefaultAsync(q => q.ClientId == clientId && q.SubscriberId == subscriberId);
        }
    }
}
