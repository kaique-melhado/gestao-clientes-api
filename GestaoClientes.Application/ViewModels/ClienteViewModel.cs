using GestaoClientes.Application.DTOs;

namespace GestaoClientes.Application.ViewModels;

public class ClienteViewModel
{
    public ClienteViewModel()
    {

    }

    public ClienteViewModel(string nomeCompleto, string email, List<TelefoneDTO> telefonesDTO)
    {
        NomeCompleto = nomeCompleto;
        Email = email;
        Telefones = telefonesDTO;
    }

    public int Id { get; set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public List<TelefoneDTO> Telefones { get; set; }
}
