using AutoMapper;
using GestaoClientes.Application.AutoMapper;
using GestaoClientes.Application.DTOs;
using GestaoClientes.Application.Services.Implementations;
using GestaoClientes.Application.ViewModels;
using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Interfaces;
using NSubstitute;

namespace GestaoClientes.UnitTests.Application.Services;

public class ClienteServiceTests
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;
    private readonly ClienteService _clienteService;

    public ClienteServiceTests()
    {
        _clienteRepository = Substitute.For<IClienteRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ClienteProfile>();
            cfg.AddProfile<TelefoneProfile>();
        });
        _mapper = config.CreateMapper();

        _clienteService = new ClienteService(_clienteRepository, _mapper);
    }

    [Fact]
    public async Task GivenExistingClienteId_WhenGetClienteByIdAsync_ThenShouldReturnClienteViewModel()
    {
        // Arrange
        var clienteId = 1;

        var telefones = new List<Telefone>
        {
            new Telefone("21", "912345678", Domain.Enums.TipoTelefoneEnum.Celular),
            new Telefone("11", "41234567", Domain.Enums.TipoTelefoneEnum.Fixo)
        };

        var cliente = new Cliente("Fulano Teste", "cliente.teste@mail.com", telefones);
        await _clienteRepository.AddAsync(cliente);

        _clienteRepository.GetByIdAsync(1).Returns(cliente);

        // Act
        var resultado = await _clienteService.GetClienteByIdAsync(clienteId);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(cliente.Email, resultado.Email);
        Assert.Equal(cliente.NomeCompleto, resultado.NomeCompleto);
    }

    [Fact]
    public async Task GivenExistingClienteNumber_WhenGetClienteByNumberAsync_ThenShouldReturnClienteViewModel()
    {
        // Arrange
        var ddd = "11";
        var numero = "912345678";

        var telefones = new List<TelefoneDTO>
        {
            new TelefoneDTO("21", "41234567", Domain.Enums.TipoTelefoneEnum.Fixo),
            new TelefoneDTO("11", "912345678", Domain.Enums.TipoTelefoneEnum.Celular)
        };

        var clienteViewModel = new ClienteViewModel("Fulano Teste", "cliente.teste@mail.com", telefones);
        var cliente = _mapper.Map<Cliente>(clienteViewModel);

        _clienteRepository.GetByNumberAsync(ddd, numero).Returns(Task.FromResult(cliente));

        // Act
        await _clienteService.GetClienteByNumberAsync(ddd, numero);

        // Assert
        await _clienteRepository.Received(1).GetByNumberAsync(ddd, numero);
    }

    [Fact]
    public async Task GivenClientesExist_WhenGetAllClientesAsync_ThenShouldReturnAllClienteViewModels()
    {
        // Arrange
        var telefones = new List<TelefoneDTO>
        {
            new TelefoneDTO("21", "41234567", Domain.Enums.TipoTelefoneEnum.Fixo),
            new TelefoneDTO("11", "912345678", Domain.Enums.TipoTelefoneEnum.Celular)
        };

        var clientesViewModel = new List<ClienteViewModel>
        {
            new ClienteViewModel("Fulano Teste", "cliente.teste@mail.com", telefones)
        };

        var clientes = _mapper.Map<List<Cliente>>(clientesViewModel);
        _clienteRepository.GetAllAsync().Returns(Task.FromResult((IEnumerable<Cliente>)clientes));

        // Act
        await _clienteService.GetAllClientesAsync();

        // Assert
        await _clienteRepository.Received(1).GetAllAsync();
    }

    [Fact]
    public async Task GivenValidCliente_WhenCreateClienteAsync_ThenShouldCallRepositoryCreateOnce()
    {
        // Arrange
        var telefones = new List<TelefoneDTO>
        {
            new TelefoneDTO("21", "912345678", Domain.Enums.TipoTelefoneEnum.Celular),
            new TelefoneDTO("11", "41234567", Domain.Enums.TipoTelefoneEnum.Fixo)
        };

        var inputModel = new CreateClienteDTO("Fulano Teste", "cliente.teste@mail.com", telefones);

        // Act
        await _clienteService.AddClienteAsync(inputModel);

        // Assert
        await _clienteRepository.Received(1).AddAsync(Arg.Any<Cliente>());
    }   

    [Fact]
    public async Task GivenExistingCliente_WhenUpdateClienteAsync_ThenShouldCallRepositoryUpdateOnce()
    {
        // Arrange
        var telefones = new List<TelefoneDTO>
        {
            new TelefoneDTO("21", "912345678", Domain.Enums.TipoTelefoneEnum.Celular),
            new TelefoneDTO("11", "41234567", Domain.Enums.TipoTelefoneEnum.Fixo)
        };

        var clienteDTO = new CreateClienteDTO("Fulano Teste", "cliente.teste@mail.com", telefones);
        var cliente = _mapper.Map<Cliente>(clienteDTO);

        var clienteId = 1;
        var inputModel = new UpdateClienteDTO("atualizado.teste@mail.com", telefones);

        _clienteRepository.GetByIdAsync(clienteId).Returns(cliente);

        // Act
        await _clienteService.UpdateClienteAsync(clienteId, inputModel);

        // Assert
        await _clienteRepository.Received(1).UpdateAsync(Arg.Any<Cliente>());
    }

    [Fact]
    public async Task GivenExistingClienteEmail_WhenDeleteClienteAsync_ThenShouldCallRepositoryDeleteOnce()
    {
        // Arrange
        var emailDeletado = "cliente.teste@mail.com";

        var telefones = new List<Telefone>
        {
            new Telefone("21", "912345678", Domain.Enums.TipoTelefoneEnum.Celular),
            new Telefone("11", "41234567", Domain.Enums.TipoTelefoneEnum.Fixo)
        };

        var cliente = new Cliente("Fulano Teste", "cliente.teste@mail.com", telefones);

        _clienteRepository.GetByEmailAsync(emailDeletado).Returns(cliente);

        // Act
        await _clienteService.DeleteClienteAsync(emailDeletado);

        // Assert
        await _clienteRepository.Received(1).DeleteAsync(cliente);
    }    
}
