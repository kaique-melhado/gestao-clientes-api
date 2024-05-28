using AutoMapper;
using GestaoClientes.Application.DTOs;
using GestaoClientes.Domain.Entities;

namespace GestaoClientes.Application.AutoMapper;

public class TelefoneProfile : Profile
{
    public TelefoneProfile()
    {
        CreateMap<Telefone, TelefoneDTO>().ReverseMap();
    }
}
