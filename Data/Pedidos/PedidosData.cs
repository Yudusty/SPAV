public class PedidosData : IPedidosData
{
    private List<Pedidos> lista = new List<Pedidos>();

    public List<Pedidos> Read()
    {
        return lista;
    }

    public void PedidosCreate (Pedidos pedidos)
    {
        lista.Add(pedidos);
    }

    public void PedidosDelete (int id)
    {
        foreach(var pedidos in lista)
        {
            if(pedidos.IdPedido == id)
            {
                lista.Remove(pedidos);
                break;
            }
        }
    }

    public Pedidos ReadSpecific (int id)
    {
        return lista.FirstOrDefault(pedidos => pedidos.IdPedido == id);
    }
    
    public List<Pedidos> Read (int? id)
    {
        return lista;
    }

    public void PedidosUpdate (int id, Pedidos pedidos)
    {
        Pedidos PedidosToUpdate = lista.First(pedidos => pedidos.IdPedido == id);
        PedidosToUpdate.IdPedido = pedidos.IdPedido;
    }

    public void AtualizarColaborador (int id, Pedidos idColaborador)
    {
        Pedidos PedidosToUpdate = lista.First(pedidos => pedidos.IdPedido == id);
        PedidosToUpdate.IdPedido = idColaborador.IdPedido;
    }
}