using ClientManager.Core.DTOs;
using ClientManager.Core.Interfaces;
using ClientManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Client?> GetClientByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Client> CreateClientAsync(ClientDto dto)
        {
            var client = new Client
            {
                Name = dto.Name,
                Address = dto.Address,
                NIP = dto.NIP,
                AdditionalFields = dto.AdditionalFields
            };

            await _repository.AddAsync(client);
            return client;
        }

        public async Task UpdateClientAsync(UpdateClientDto dto)
        {
            var updated = new Client
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                NIP = dto.NIP,
                AdditionalFields = dto.AdditionalFields
            };

            await _repository.UpdateAsync(updated);
        }

        public async Task DeleteClientAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
