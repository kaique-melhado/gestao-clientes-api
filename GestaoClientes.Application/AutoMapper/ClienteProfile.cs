using AutoMapper;
using GestaoClientes.Application.DTOs;
using GestaoClientes.Application.ViewModels;
using GestaoClientes.Domain.Entities;

namespace GestaoClientes.Application.AutoMapper
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, CreateClienteDTO>()
                .ForMember(dto => dto.Telefones, dest => dest.MapFrom(cliente => cliente.Telefones)).ReverseMap();

            CreateMap<Cliente, UpdateClienteDTO>()
                .ForMember(dto => dto.Telefones, dest => dest.MapFrom(cliente => cliente.Telefones)).ReverseMap();

            CreateMap<Cliente, ClienteViewModel>()
                .ForMember(vm => vm.Telefones, dest => dest.MapFrom(cliente => cliente.Telefones)).ReverseMap();
        }
    }
}
