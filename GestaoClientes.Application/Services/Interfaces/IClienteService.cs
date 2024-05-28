using GestaoClientes.Application.DTOs;
using GestaoClientes.Application.ViewModels;

namespace GestaoClientes.Application.Services.Interfaces;

public interface IClienteService
{
    Task<ClienteViewModel> GetClienteByIdAsync(int id);
    Task<ClienteViewModel> GetClienteByNumberAsync(string ddd, string numero);
    Task<IEnumerable<ClienteViewModel>> GetAllClientesAsync();
    Task<int> AddClienteAsync(CreateClienteDTO inputModel);
    Task UpdateClienteAsync(int id, UpdateClienteDTO inputModel);
    Task DeleteClienteAsync(string email);
}
