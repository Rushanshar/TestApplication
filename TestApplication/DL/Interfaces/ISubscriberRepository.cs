
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DL.Interfaces
{
    public interface ISubscriberRepository
    {
        Task<IEnumerable<long>> GetByClientAsync(long clientId);

        Task AddAsync(long subscriberId, long clientForSubscribe);

        Task RemoveAsync(long clientId, long clientForUnsubscribe);

        Task<IEnumerable<long>> GetTopClientsIdsAsync(int take);
    }
}
