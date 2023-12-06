// Classe Colaboradores
public class Colaboradores : Pessoas
{
    // Atributos
    public int IdColaborador { get; set; }
    public decimal Salario { get; set; }
    public string CPF { get; set; }
    public int IdPessoa { get; set; }
    public int IdEmpresa { get; set; }
    public Pessoas Pessoas { get; set; }
    public Empresas Empresas { get; set; }
}