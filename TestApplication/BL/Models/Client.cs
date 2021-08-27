
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Models
{
    public class Client
    {
        public Client(string name)
        {
            Name = name;
            _subscribers = new List<ClientShortInfo>();
        }

        public long Id { get; set; }

        public string Name { get; }

        private List<ClientShortInfo> _subscribers;
        private HashSet<long> SubscribersIds => _subscribers.Select(q => q.Id).ToHashSet();
        public IEnumerable<ClientShortInfo> GetSubscribers()
        {
            return _subscribers ??= new List<ClientShortInfo>();
        }

        public bool AddSubscribers(IEnumerable<ClientShortInfo> subscribers)
        {
            foreach (var subscriber in subscribers)
            {
                if (!_subscribers.Contains(subscriber))
                    _subscribers.Add(subscriber);
            }

            return true;
        }

        public bool AddSubscriber(ClientShortInfo subscriber)
        {
            if (SubscribersIds.Contains(subscriber.Id))
                throw new ArgumentException($"Client {subscriber.Name} already subscribe {Name}");

            _subscribers.Add(subscriber);
            return true;
        }

        public bool RemoveSubscriber(ClientShortInfo subscriber)
        {
            if (!SubscribersIds.Contains(subscriber.Id))
                throw new ArgumentException($"Client {subscriber.Name} does not subscribe {Name}");

            _subscribers.Remove(subscriber);
            return true;
        }
    }
}
