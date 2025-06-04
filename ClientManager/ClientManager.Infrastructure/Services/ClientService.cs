using AutoMapper;
using ClientManager.Core.DTOs;
using ClientManager.Core.Interfaces;
using ClientManager.Core.Models;

namespace ClientManager.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }

        public async Task<ClientDto?> GetClientByIdAsync(Guid id)
        {
            var client = await _repository.GetByIdAsync(id);
            return client == null ? null : _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> CreateClientAsync(ClientDto dto)
        {
            var client = _mapper.Map<Client>(dto);
            await _repository.AddAsync(client);
            return _mapper.Map<ClientDto>(client);
        }

        public async Task UpdateClientAsync(UpdateClientDto dto)
        {
            var existingClient = await _repository.GetByIdAsync(dto.Id)
                                 ?? throw new KeyNotFoundException($"Client with ID {dto.Id} not found.");

            _mapper.Map(dto, existingClient);
            await _repository.UpdateAsync(existingClient);
        }

        public async Task DeleteClientAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        #region Report
        public async Task<IEnumerable<Client>> GetAllClientsForReportAsync()
        {
            return await _repository.GetAllAsync();
        }
        #endregion
    }
}
