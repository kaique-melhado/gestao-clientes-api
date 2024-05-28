using GestaoClientes.Domain.Entities;

namespace GestaoClientes.Domain.Interfaces;

public interface IClienteRepository
{
    Task<Cliente> GetByIdAsync(int id);
    Task<Cliente> GetByEmailAsync(string email);
    Task<Cliente> GetByNumberAsync(string ddd, string numero);
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task AddAsync(Cliente cliente);
    Task UpdateAsync(Cliente cliente);
    Task DeleteAsync(Cliente cliente);
}
