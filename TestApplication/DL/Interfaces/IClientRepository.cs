
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.Models;

namespace DL.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> GetAsync(long id);

        Task<IEnumerable<Client>> GetAsync(IEnumerable<long> ids);

        Task<long> AddAsync(Client client);
    }
}
