
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ISubscriberService
    {
        Task<bool> SubscribeClientAsync(long subscriberId, long clientForSubscribeId);

        Task<bool> UnsubscribeClientAsync(long subscriberId, long clientForUnsubscribeId);
    }
}
