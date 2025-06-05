using AutoMapper;
using ClientManager.Core.DTOs;
using ClientManager.Core.Models;

namespace ClientManager.API.Mappings
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<CreateClientDto, Client>();
            CreateMap<ClientAdditionalField, ClientAdditionalFieldDto>().ReverseMap();
        }
    }
}
