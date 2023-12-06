public class Produtos : Empresas
{
    // Atributos
    public int IdProduto { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int Qtd { get; set; }
    public decimal Valor { get; set; }
    public int IdEmpresa { get; set; }
    public Empresas Empresas { get; set; }
}