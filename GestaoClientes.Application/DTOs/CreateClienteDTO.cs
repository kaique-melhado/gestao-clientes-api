namespace GestaoClientes.Application.DTOs;

public class CreateClienteDTO
{
    public CreateClienteDTO()
    {
        
    }

    public CreateClienteDTO(string nomeCompleto, string email, List<TelefoneDTO> telefonesDTO)
    {
        NomeCompleto = nomeCompleto;
        Email = email;
        Telefones = telefonesDTO;
    }

    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public List<TelefoneDTO> Telefones { get; set; }
}
