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

            CreateMap<ClientAdditionalField, ClientAdditionalFieldDto>().ReverseMap();

            CreateMap<ClientDto, Client>()
                .ForMember(dest => dest.AdditionalFields, opt => opt.Ignore());
        }
    }
}
