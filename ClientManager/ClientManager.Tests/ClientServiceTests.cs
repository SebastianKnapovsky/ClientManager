using AutoMapper;
using ClientManager.Core.DTOs;
using ClientManager.Core.Interfaces;
using ClientManager.Core.Models;
using ClientManager.Infrastructure.Services;
using Moq;

public class ClientServiceTests
{
    private readonly Mock<IClientRepository> _repositoryMock;
    private readonly IMapper _mapper;
    private readonly ClientService _service;

    public ClientServiceTests()
    {
        _repositoryMock = new Mock<IClientRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ClientDto, Client>().ReverseMap();
            cfg.CreateMap<ClientAdditionalFieldDto, ClientAdditionalField>().ReverseMap();
        });

        _mapper = config.CreateMapper();
        _service = new ClientService(_repositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task UpdateClientAsync_ValidDto_UpdatesClient()
    {
        // Arrange
        var dto = new ClientDto
        {
            Id = Guid.NewGuid(),
            Name = "Updated Name",
            Address = "Updated Address",
            NIP = "9999999999",
            AdditionalFields = new List<ClientAdditionalFieldDto>
            {
                new ClientAdditionalFieldDto
                {
                    Id = 1,
                    NameField = "Email",
                    Value = "new@example.com"
                }
            }
        };

        var existingClient = new Client
        {
            Id = dto.Id,
            Name = "Old Name",
            Address = "Old Address",
            NIP = "1234567890",
            AdditionalFields = new List<ClientAdditionalField>
            {
                new ClientAdditionalField
                {
                    Id = 1,
                    NameField = "Email",
                    Value = "old@example.com"
                }
            }
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(dto.Id))
            .ReturnsAsync(existingClient);

        // Act
        await _service.UpdateClientAsync(dto);

        // Assert
        _repositoryMock.Verify(r => r.UpdateAsync(It.Is<Client>(c =>
            c.Name == dto.Name &&
            c.Address == dto.Address &&
            c.NIP == dto.NIP &&
            c.AdditionalFields!.Any(f =>
                f.Id == 1 &&
                f.NameField == "Email" &&
                f.Value == "new@example.com"
            )
        )), Times.Once);
    }
}
