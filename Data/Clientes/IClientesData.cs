public interface IClientesData
{
    public List<Clientes> Read();
    public Clientes Read (int id);
    public void ClientesCreate (Clientes clientes);
    public void ClientesUpdate (int id, Clientes clientes);
    public void ClientesDelete (int id);
    public Pessoas? LoginCliente (string email, string senha);
}