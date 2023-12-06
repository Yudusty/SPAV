// Classe Empresas
public class Empresas : Pessoas
{
    // Atributos
    public int IdEmpresa { get; set; }
    public string CNPJ { get; set; }
    public int IdPessoa { get; set; }
    public Pessoas Pessoas { get; set; }
}