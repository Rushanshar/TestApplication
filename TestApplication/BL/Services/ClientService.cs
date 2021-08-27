
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bl.Interfaces;
using BL.Interfaces;
using BL.Models;
using DL.Interfaces;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly ILogger<ClientService> _logger;
        private readonly IClientValidationService _validationService;

        public ClientService(ISubscriberRepository subscriberRepository, IClientRepository clientRepository, ILogger<ClientService> logger, IClientValidationService validationService)
        {
            _clientRepository = clientRepository;
            _logger = logger;
            _validationService = validationService;
            _subscriberRepository = subscriberRepository;
        }

        public async Task<Client> TryGetClientAsync(long id)
        {
            try
            {
                return await _clientRepository.GetAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in getting client with id = {id}");
                return null;
            }
        }

        public async Task<IEnumerable<Client>> GetTopClientsAsync(int take)
        {
            var topClientIds = await _subscriberRepository.GetTopClientsIdsAsync(take);

            var topClients = (await _clientRepository.GetAsync(topClientIds)).ToArray();

            return topClients;
        }

        public async Task<long> AddNewClientAsync(string name)
        {
            if (!_validationService.ValidateName(name))
                throw new ArgumentException("Client name is not valid.");
            
            _logger.LogInformation($"Create client with name = {name}");
            return await _clientRepository.AddAsync(new Client(name.Trim()));
        }
    }
}
