namespace GestaoClientes.Domain.Entities;

public class Cliente
{
    public Cliente()
    {
        
    }

    public Cliente(string nomeCompleto, string email, List<Telefone> telefones)
    {
        NomeCompleto = nomeCompleto;
        Email = email;
        Telefones = telefones;
    }

    public int Id { get; private set; }
    public string NomeCompleto { get; private set; }
    public string Email { get; private set; }

    public virtual ICollection<Telefone> Telefones { get; private set; }

    public void Atualizar(string email, List<Telefone> telefones)
    {
        Email = email;
        Telefones = telefones;
    }
}
