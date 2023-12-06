// Classe CLientes
public class Clientes : Pessoas
{
    // Atributos
    public int IdCliente { get; set; }
    public string CPF { get; set; }
    public int IdPessoa { get; set; }
    public Pessoas Pessoas { get; set; }
}