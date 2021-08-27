
using System.Collections.Generic;

namespace BL.Models.TransferModels
{
    public class ClientResponse
    {
        public ClientResponse(long id, string name, IEnumerable<ClientShortInfo> subscribers)
        {
            Id = id;
            Name = name;
            Subscribers = subscribers;
        }

        public long Id { get; }

        public string Name { get; }

        public IEnumerable<ClientShortInfo> Subscribers { get; }
    }
}
