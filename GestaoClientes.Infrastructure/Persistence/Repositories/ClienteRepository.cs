using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestaoClientes.Infrastructure.Persistence.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly DataContext _context;

    public ClienteRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Cliente> GetByIdAsync(int id)
    {
        return await _context.Clientes.Include(c => c.Telefones).SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Cliente> GetByEmailAsync(string email)
    {
        return await _context.Clientes.Include(c => c.Telefones).SingleOrDefaultAsync(c => c.Email == email);
    }

    public async Task<Cliente> GetByNumberAsync(string ddd, string numero)
    {
        return await _context.Clientes
            .Include(c => c.Telefones)
            .FirstOrDefaultAsync(c => c.Telefones
            .Any(t => t.DDD == ddd && t.Numero == numero));
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes.Include(c => c.Telefones).ToListAsync();
    }

    public async Task AddAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
    }
}
