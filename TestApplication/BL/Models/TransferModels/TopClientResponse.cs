
using System.Collections.Generic;
using System.Linq;

namespace BL.Models.TransferModels
{
    public class TopClientResponse : ClientResponse
    {
        public TopClientResponse(long id, string name, IEnumerable<ClientShortInfo> subscribers) : base(id, name, subscribers)
        {
        }

        public int SubscribersCount => Subscribers.Count();
    }
}
