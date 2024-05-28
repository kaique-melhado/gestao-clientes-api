using AutoMapper;
using GestaoClientes.Application.DTOs;
using GestaoClientes.Application.Services.Interfaces;
using GestaoClientes.Application.ViewModels;
using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Interfaces;

namespace GestaoClientes.Application.Services.Implementations;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    public async Task<ClienteViewModel> GetClienteByIdAsync(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        var clienteViewModel = _mapper.Map<ClienteViewModel>(cliente);

        return clienteViewModel;
    }

    public async Task<ClienteViewModel> GetClienteByNumberAsync(string ddd, string numero)
    {
        var cliente = await _clienteRepository.GetByNumberAsync(ddd, numero);
        var clienteViewModel = _mapper.Map<ClienteViewModel>(cliente);

        return clienteViewModel;
    }

    public async Task<IEnumerable<ClienteViewModel>> GetAllClientesAsync()
    {
        var clientes = await _clienteRepository.GetAllAsync();
        var clientesViewModel = _mapper.Map<IEnumerable<ClienteViewModel>>(clientes);

        return clientesViewModel;
    }

    public async Task<int> AddClienteAsync(CreateClienteDTO inputModel)
    {
        var cliente = _mapper.Map<Cliente>(inputModel);
        await _clienteRepository.AddAsync(cliente);

        return cliente.Id;
    }

    public async Task UpdateClienteAsync(int id, UpdateClienteDTO inputModel)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        if (cliente != null)
        {
            cliente = _mapper.Map(inputModel, cliente);
            await _clienteRepository.UpdateAsync(cliente);
        }
    }

    public async Task DeleteClienteAsync(string email)
    {
        var cliente = await _clienteRepository.GetByEmailAsync(email);

        if (cliente != null)
            await _clienteRepository.DeleteAsync(cliente);
    }
}
