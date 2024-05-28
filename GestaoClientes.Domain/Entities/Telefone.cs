using GestaoClientes.Domain.Enums;

namespace GestaoClientes.Domain.Entities;

public class Telefone
{
    public Telefone()
    {
        
    }

    public Telefone(string ddd, string numero, TipoTelefoneEnum tipo)
    {
        DDD = ddd;
        Numero = numero;
        Tipo = tipo;
    }

    public int Id { get; private set; }
    public string DDD { get; private set; }
    public string Numero { get; private set; }
    public TipoTelefoneEnum Tipo { get; private set; }

    public int ClienteId { get; private set; }
    public virtual Cliente Cliente { get; private set; }

    public void Atualizar(string ddd, string numero, TipoTelefoneEnum tipo)
    {
        DDD = ddd;
        Numero = numero;
        Tipo = tipo;
    }
}
