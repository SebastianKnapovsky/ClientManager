using ClientManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Core.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(Guid id);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Guid id);
    }
}
