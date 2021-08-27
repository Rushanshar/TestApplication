
using System;
using System.Threading.Tasks;
using BL.Interfaces;
using BL.Models;
using DL.Interfaces;
using Microsoft.Extensions.Logging;

namespace BL.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly ILogger<SubscriberService> _logger;
        private readonly IClientService _clientService;

        public SubscriberService(ISubscriberRepository subscriberRepository, ILogger<SubscriberService> logger, IClientService clientService)
        {
            _subscriberRepository = subscriberRepository;
            _logger = logger;
            _clientService = clientService;
        }

        public async Task<bool> SubscribeClientAsync(long subscriberId, long clientForSubscribeId)
        {
            var clientForSubscribe = await GetClientAsync(clientForSubscribeId);

            var subscriber = await GetClientAsync(subscriberId);

            return await AddToSubscribersAsync(new ClientShortInfo(subscriberId, subscriber.Name), clientForSubscribe);
        }

        public async Task<bool> UnsubscribeClientAsync(long subscriberId, long clientForUnsubscribeId)
        {
            var clientForUnsubscribe = await GetClientAsync(clientForUnsubscribeId);

            var subscriber = await GetClientAsync(subscriberId);

            return await RemoveFromSubscribersAsync(new ClientShortInfo(subscriberId, subscriber.Name), clientForUnsubscribe);
        }

        private async Task<Client> GetClientAsync(long id)
        {
            var client = await _clientService.TryGetClientAsync(id);
            if (client == null)
                throw new ArgumentException($"Cannot find client with id {id}");

            return client;
        }

        private async Task<bool> AddToSubscribersAsync(ClientShortInfo subscriber, Client clientForSubscribe)
        {
            if (!clientForSubscribe.AddSubscriber(subscriber))
                return false;

            if (await TryInsertSubscriberAsync(subscriber.Id, clientForSubscribe.Id))
            {
                _logger.LogInformation($"Client {subscriber.Name} now subscribe client {clientForSubscribe.Name}");
                return true;
            }

            clientForSubscribe.RemoveSubscriber(subscriber);
            return false;
        }

        private async Task<bool> RemoveFromSubscribersAsync(ClientShortInfo subscriber, Client clientForUnsubscribe)
        {
            if (!clientForUnsubscribe.RemoveSubscriber(subscriber))
                return false;

            if (await TryRemoveSubscriberAsync(subscriber.Id, clientForUnsubscribe.Id))
            {
                _logger.LogInformation($"Client {subscriber.Name} now unsubscribe client {clientForUnsubscribe.Name}");
                return true;
            }

            clientForUnsubscribe.AddSubscriber(subscriber);
            return false;
        }

        private async Task<bool> TryInsertSubscriberAsync(long subscriberId, long clientForSubscribeId)
        {
            try
            {
                await _subscriberRepository.AddAsync(subscriberId, clientForSubscribeId);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Cannot insert to database subscriber {subscriberId} for client {clientForSubscribeId}");
                return false;
            }
        }

        private async Task<bool> TryRemoveSubscriberAsync(long subscriberId, long clientForUnsubscribeId)
        {
            try
            {
                await _subscriberRepository.RemoveAsync(subscriberId, clientForUnsubscribeId);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Cannot delete from database subscriber {subscriberId} from client {clientForUnsubscribeId}");
                return false;
            }
        }
    }
}
