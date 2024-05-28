using GestaoClientes.Application.DTOs;
using GestaoClientes.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.API.Controllers;

/// <summary>
/// API para gerenciamento de clientes.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="ClienteController"/>.
    /// </summary>
    /// <param name="clienteService">Serviço de cliente.</param>
    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }


    ///<summary>
    ///Retorna uma lista de todos os clientes.
    ///</summary>
    /// <returns>Lista de clientes.</returns>
    /// <response code="200">Busca realizada com sucesso</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClientes()
    {
        var clientes = await _clienteService.GetAllClientesAsync();
        return Ok(clientes);
    }

    /// <summary>
    /// Retorna um cliente pelo ID.
    /// </summary>
    /// <param name="id">ID do cliente.</param>
    /// <returns>Cliente correspondente ao ID informado.</returns>
    /// <response code="200">Busca realizada com sucesso</response>
    /// <response code="404">Busca sem retorno de dados</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClienteById(int id)
    {
        var cliente = await _clienteService.GetClienteByIdAsync(id);
        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    /// <summary>
    /// Retorna um cliente pelo DDD e número de telefone.
    /// </summary>
    /// <param name="ddd">DDD do cliente.</param>
    /// <param name="numero">Número de telefone do cliente.</param>
    /// <returns>Cliente correspondente ao DDD e Número informados.</returns>
    /// <response code="200">Busca realizada com sucesso</response>
    /// <response code="404">Busca sem retorno de dados</response>
    [HttpGet("{ddd}/{numero}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClienteByNumber(string ddd, string numero)
    {
        var cliente = await _clienteService.GetClienteByNumberAsync(ddd, numero);
        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    /// <summary>
    /// Cadastra um novo cliente com nome completo, e-mail e uma lista de telefones.
    /// </summary>
    /// <param name="inputModel">Dados do novo cliente.</param>
    /// <returns>Objeto recém-criado.</returns>
    /// <response code="201">Cliente cadastrado com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostCliente([FromBody] CreateClienteDTO inputModel)
    {
        var clienteId = await _clienteService.AddClienteAsync(inputModel);
        return CreatedAtAction(nameof(GetClienteById), new { id = clienteId });
    }

    /// <summary>
    /// Atualiza um cliente existente.
    /// </summary>
    /// <param name="id">ID do cliente a ser atualizado.</param>
    /// <param name="inputModel">Dados atualizados do cliente.</param>
    /// <returns>Resultado da operação.</returns>
    /// <response code="204">Dados atualizados com sucesso</response>
    /// <response code="400">Dados inválidos fornecidos</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutCliente(int id, [FromBody] UpdateClienteDTO inputModel)
    {
        await _clienteService.UpdateClienteAsync(id, inputModel);
        return NoContent();
    }

    /// <summary>
    /// Deleta um cliente existente.
    /// </summary>
    /// <param name="email">E-mail do cliente a ser deletado.</param>
    /// <returns>Resultado da operação.</returns>
    /// <response code="204">Cliente removido com sucesso</response>
    [HttpDelete("{email}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCliente(string email)
    {
        await _clienteService.DeleteClienteAsync(email);
        return NoContent();
    }
}
