using GestaoClientes.Domain.Entities;
using GestaoClientes.Infrastructure.Persistence;
using GestaoClientes.Infrastructure.Persistence.Repositories;
using GestaoClientes.UnitTests.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GestaoClientes.UnitTests.Infrastructure.Repositories;

public class ClienteRepositoryTests : IDisposable
{
    private readonly DataContext _context;
    private readonly ClienteRepository _repository;

    public ClienteRepositoryTests()
    {
        _context = InMemoryDbContextFactory.Create();
        _repository = new ClienteRepository(_context);
    }

    [Fact]
    public async Task GivenExistingCliente_WhenGetByEmailAsync_ThenShouldReturnCliente()
    {
        // Arrange
        var telefones = new List<Telefone>
        {
            new Telefone("21", "912345678", Domain.Enums.TipoTelefoneEnum.Celular), 
            new Telefone("11", "41234567", Domain.Enums.TipoTelefoneEnum.Fixo)
        };

        var cliente = new Cliente("Fulano Teste", "cliente.teste@mail.com", telefones);

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        // Act
        var resultado = await _repository.GetByEmailAsync("cliente.teste@mail.com");

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(cliente.Id, resultado.Id);
        Assert.Equal(cliente.Email, resultado.Email);
    }

    [Fact]
    public async Task GivenNonExistingCliente_WhenGetByEmailAsync_ThenShouldReturnNull()
    {
        // Arrange
        string email = "naocadastrado.teste@mail.com";

        // Act
        var resultado = await _repository.GetByEmailAsync(email);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public async Task GivenNewCliente_WhenAddAsync_ThenShouldAddCliente()
    {
        // Arrange
        var telefones = new List<Telefone>
        {
            new Telefone("21", "912345678", Domain.Enums.TipoTelefoneEnum.Celular),
            new Telefone("11", "41234567", Domain.Enums.TipoTelefoneEnum.Fixo)
        };

        var cliente = new Cliente("Fulano Teste", "cliente.teste@mail.com", telefones);

        // Act
        await _repository.AddAsync(cliente);
        var result = await _repository.GetByEmailAsync("cliente.teste@mail.com");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cliente.Id, result.Id);
        Assert.Equal(cliente.Email, result.Email);
    }

    [Fact]
    public async Task GivenExistingCliente_WhenUpdateAsync_ThenShouldUpdateCliente()
    {
        // Arrange
        var telefones = new List<Telefone>
        {
            new Telefone("21", "912345678", Domain.Enums.TipoTelefoneEnum.Celular),
            new Telefone("11", "41234567", Domain.Enums.TipoTelefoneEnum.Fixo)
        };

        var cliente = new Cliente("Fulano Teste", "cliente.teste@mail.com", telefones);

        await _repository.AddAsync(cliente);
        _context.Entry(cliente).State = EntityState.Detached;

        // Act
        var emailAtualizado = "atualizado.teste@mail.com";

        var telefonesAtualizados = new List<Telefone>
        {
            new Telefone("19", "987654321", Domain.Enums.TipoTelefoneEnum.Celular),
            new Telefone("11", "47654321", Domain.Enums.TipoTelefoneEnum.Fixo)
        };

        cliente.Atualizar(emailAtualizado, telefonesAtualizados);

        await _repository.UpdateAsync(cliente);
        var clienteAtualizado = await _context.Clientes.FindAsync(1);

        // Assert
        Assert.NotNull(clienteAtualizado);
        Assert.Equal(emailAtualizado, clienteAtualizado.Email);
        Assert.Equal(telefonesAtualizados, clienteAtualizado.Telefones);
    }

    [Fact]
    public async Task GivenExistingCliente_WhenDeleteAsync_ThenShouldRemoveCliente()
    {
        // Arrange
        var telefones = new List<Telefone>
        {
            new Telefone("21", "912345678", Domain.Enums.TipoTelefoneEnum.Celular),
            new Telefone("11", "41234567", Domain.Enums.TipoTelefoneEnum.Fixo)
        };

        var cliente = new Cliente("Fulano Teste", "cliente.teste@mail.com", telefones);

        await _repository.AddAsync(cliente);
        _context.Entry(cliente).State = EntityState.Detached;

        // Act
        await _repository.DeleteAsync(cliente);
        var clienteDeletado = await _context.Clientes.FindAsync(1);

        // Assert
        Assert.Null(clienteDeletado);
    }

    public void Dispose()
    {
        InMemoryDbContextFactory.Destroy(_context);
    }
}
