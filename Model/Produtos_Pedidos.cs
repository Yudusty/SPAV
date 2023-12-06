public class Produtos_Pedidos
{
    public int Qtd { get; set; }
    public int IdPedido { get; set; }
    public int IdProduto { get; set; }
    public Pedidos Pedidos { get; set; }
    public Produtos Produtos { get; set; }
}