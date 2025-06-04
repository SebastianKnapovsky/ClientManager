using ClientManager.Core.Interfaces;
using ClientManager.Core.Models;
using ClientManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClientManager.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients
                .Include(c => c.AdditionalFields)
                .ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(Guid id)
        {
            return await _context.Clients
                .Include(c => c.AdditionalFields)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            var existingClient = await _context.Clients
                .Include(c => c.AdditionalFields)
                .FirstOrDefaultAsync(c => c.Id == client.Id);

            if (existingClient is null)
                return;

            existingClient.Name = client.Name;
            existingClient.Address = client.Address;
            existingClient.NIP = client.NIP;

            if (existingClient.AdditionalFields != null && existingClient.AdditionalFields.Any())
            {
                _context.ClientAdditionalFields.RemoveRange(existingClient.AdditionalFields);
            }

            existingClient.AdditionalFields = client.AdditionalFields?.Select(f => new ClientAdditionalField
            {
                NameField = f.NameField,
                Value = f.Value,
                ClientId = existingClient.Id
            }).ToList();

            _context.Clients.Update(existingClient);

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Guid id)
        {
            var client = await _context.Clients
                .Include(c => c.AdditionalFields)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}
