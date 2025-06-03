using ClientManager.Core.DTOs;
using ClientManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Core.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<Client?> GetClientByIdAsync(Guid id);
        Task<Client> CreateClientAsync(ClientDto dto);
        Task UpdateClientAsync(UpdateClientDto dto);
        Task DeleteClientAsync(Guid id);
    }
}
