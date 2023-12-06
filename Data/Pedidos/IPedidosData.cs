public interface IPedidosData
{
    public List<Pedidos> Read ();
    public Pedidos ReadSpecific (int id);
    public List<Pedidos> Read (int? id);
    public void PedidosCreate (Pedidos pedidos);
    public void PedidosUpdate (int id, Pedidos pedidos);
    public void PedidosDelete (int id);
    public void AtualizarColaborador (int id, Pedidos pedidos);
}