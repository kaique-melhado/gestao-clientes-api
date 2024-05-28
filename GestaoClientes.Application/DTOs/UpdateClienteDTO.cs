namespace GestaoClientes.Application.DTOs;

public class UpdateClienteDTO
{
    public UpdateClienteDTO()
    {

    }

    public UpdateClienteDTO(string email, List<TelefoneDTO> telefonesDTO)
    {
        Email = email;
        Telefones = telefonesDTO;
    }

    public string Email { get; set; }
    public List<TelefoneDTO> Telefones { get; set; }
}
