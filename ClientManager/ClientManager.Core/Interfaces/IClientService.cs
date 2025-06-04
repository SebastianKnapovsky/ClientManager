using ClientManager.Core.DTOs;
using ClientManager.Core.Models;

namespace ClientManager.Core.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task<ClientDto?> GetClientByIdAsync(Guid id);
        Task<ClientDto> CreateClientAsync(ClientDto dto);
        Task UpdateClientAsync(UpdateClientDto dto);
        Task DeleteClientAsync(Guid id);
        Task<IEnumerable<Client>> GetAllClientsForReportAsync();
    }
}
