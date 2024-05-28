using GestaoClientes.Domain.Enums;

namespace GestaoClientes.Application.DTOs;

public class TelefoneDTO
{
    public TelefoneDTO()
    {
        
    }

    public TelefoneDTO(string ddd, string numero, TipoTelefoneEnum tipo)
    {
        DDD = ddd;
        Numero = numero;
        Tipo = tipo;
    }

    public string DDD { get; set; }
    public string Numero { get; set; }
    public TipoTelefoneEnum Tipo { get; set; }
}
