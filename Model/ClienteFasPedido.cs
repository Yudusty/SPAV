public class ClienteFazPedido
{
    public int IdPedido { get; set; }
    public DateTime Horario  { get; set; }
    public int Mesa { get; set; }
    public decimal ValorPedido { get; set; }
    public int IdCliente { get; set; }
    public Clientes Clientes { get; set; }
}