
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BL.Models;
using DL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DL.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IMapper _mapper;

        public ClientRepository(ApplicationDbContext dbContext, ISubscriberRepository subscriberRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _subscriberRepository = subscriberRepository;
            _mapper = mapper;
        }

        public async Task<Client> GetAsync(long id)
        {
            var clientEntity = await _dbContext.Clients.FirstOrDefaultAsync(q => q.Id == id);
            if (clientEntity == null)
                return null;

            return await MapAndFillSubscribersAsync(clientEntity);
        }

        public async Task<IEnumerable<Client>> GetAsync(IEnumerable<long> ids)
        {
            var clientEntities = await _dbContext.Clients.Where(q => ids.Contains(q.Id)).ToListAsync();
            if (!clientEntities.Any())
                return new List<Client>();

            return await MapAndFillSubscribersAsync(clientEntities);
        }

        public async Task<long> AddAsync(Client client)
        {
            var mappedClient = _mapper.Map<Client>(client);

            var addedClinet = await _dbContext.AddAsync(mappedClient);

            await _dbContext.SaveChangesAsync();

            return addedClinet.Entity.Id;
        }

        private async Task<Client> MapAndFillSubscribersAsync(Entities.Client clientEntity)
        {
            var client = _mapper.Map<Client>(clientEntity);

            await FillSubscribersAsync(client);

            return client;
        }

        private async Task<IEnumerable<Client>> MapAndFillSubscribersAsync(IEnumerable<Entities.Client> clientEntities)
        {
            var mappedClients = _mapper.Map<List<Client>>(clientEntities);

            var clients = new List<Client>();
            foreach (var client in mappedClients)
            {
                await FillSubscribersAsync(client);
                clients.Add(client);
            }
            
            return clients;
        }

        private async Task FillSubscribersAsync(Client client)
        {
            var subscriberIds = await _subscriberRepository.GetByClientAsync(client.Id);

            var subscribers = await _dbContext.Clients.Where(q => subscriberIds.Contains(q.Id)).ToListAsync();

            client.AddSubscribers(subscribers.Select(q => new ClientShortInfo(q.Id, q.Name)));
        }
    }
}
