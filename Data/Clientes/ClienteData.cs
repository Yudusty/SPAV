public class ClientesData : IClientesData
{
    private List<Clientes> lista = new List<Clientes>();

    public List<Clientes> Read()
    {
        return lista;
    }

    public void ClientesCreate (Clientes clientes) 
    {
        lista.Add(clientes);
    }

    public void ClientesDelete (int id)
    {
        foreach(var clientes in lista) 
        {
            if(clientes.IdCliente == id)
            {
                lista.Remove(clientes);
                break;
            }
        }
    }

    public Clientes Read (int id)
    {
        return lista.FirstOrDefault(clientes => clientes.IdCliente == id);
    }

    public void ClientesUpdate (int id, Clientes clientes)
    {
        Clientes ClientesToUpdate = lista.First(clientes => clientes.IdCliente == id);
        ClientesToUpdate.IdCliente = clientes.IdCliente;
    }

    public Pessoas? LoginCliente (string email, string senha)
    {
        return new Pessoas();
    }
}
