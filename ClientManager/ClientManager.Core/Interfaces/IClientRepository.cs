using ClientManager.Core.Models;

namespace ClientManager.Core.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(Guid id);
        Task AddAsync(Client client);
        Task UpdateAsync(Client dto);
        Task DeleteAsync(Guid id);
    }
}
