
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.Models;

namespace BL.Interfaces
{
    public interface IClientService
    {
        Task<Client> TryGetClientAsync(long id);

        Task<IEnumerable<Client>> GetTopClientsAsync(int take);

        Task<long> AddNewClientAsync(string name);
    }
}
