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

            if (existingClient == null)
                throw new Exception("Client not found");

            existingClient.Name = client.Name;
            existingClient.Address = client.Address;
            existingClient.NIP = client.NIP;

            var incoming = client.AdditionalFields ?? new();

            var incomingIds = incoming.Where(f => f.Id != 0).Select(f => f.Id).ToList();
            var toRemove = existingClient.AdditionalFields
                .Where(f => !incomingIds.Contains(f.Id))
                .ToList();

            _context.ClientAdditionalFields.RemoveRange(toRemove);

            foreach (var field in incoming.Where(f => f.Id != 0))
            {
                var existingField = existingClient.AdditionalFields
                    .FirstOrDefault(f => f.Id == field.Id);

                if (existingField != null)
                {
                    existingField.NameField = field.NameField;
                    existingField.Value = field.Value;
                }
            }

            var newFields = incoming
                .Where(f => f.Id == 0)
                .Select(f => new ClientAdditionalField
                {
                    NameField = f.NameField,
                    Value = f.Value,
                    ClientId = existingClient.Id
                })
                .ToList();

            existingClient.AdditionalFields.AddRange(newFields);

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
