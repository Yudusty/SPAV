using Microsoft.Data.SqlClient;

public class PedidosSql : Database, IPedidosData
{
    public void PedidosCreate (Pedidos pedidos)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC Produtos_Pedidos_1 @ProdutoId, @mesa, @QTD, @clienteid";

        cmd.Parameters.AddWithValue("@ProdutoId", pedidos.produtos.IdProduto);
        cmd.Parameters.AddWithValue("@mesa", pedidos.Mesa);
        cmd.Parameters.AddWithValue("@QTD", pedidos.QTD);
        cmd.Parameters.AddWithValue("@clienteid", pedidos.clientes.IdCliente);

        cmd.ExecuteNonQuery();
    }

    public void PedidosDelete (int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC delPed @idPed";

        cmd.Parameters.AddWithValue("@idPed", id);

        cmd.ExecuteNonQuery();
    }

    public List<Pedidos> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM pedCli";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Pedidos> lista = new List<Pedidos>();

        while(reader.Read())
        {
            Pedidos pedidos = new Pedidos();
            pedidos.IdCliente = reader.GetInt32(0);
            pedidos.IdPedido = reader.GetInt32(1);
            pedidos.Horario = reader.GetDateTime(2);
            pedidos.Mesa = reader.GetInt32(3);
            pedidos.ValorPedido = reader.GetDecimal(4);
            pedidos.Status = reader.GetString(5);
            
            pedidos.clientes = new Clientes 
            { 
                IdCliente = reader.GetInt32(0)
            };
            lista.Add(pedidos);
        }
        return lista;
    }

    public Pedidos ReadSpecific (int id)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM pedCli WHERE pedCli.IdPedido = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if(reader.Read())
        {
            Pedidos pedidos = new Pedidos();
            pedidos.IdCliente = reader.GetInt32(0);
            pedidos.IdPedido = reader.GetInt32(1);
            pedidos.Horario = reader.GetDateTime(2);
            pedidos.Mesa = reader.GetInt32(3);
            pedidos.ValorPedido = reader.GetDecimal(4);
            pedidos.Status = reader.GetString(5);
            
            pedidos.clientes = new Clientes 
            { 
                IdCliente = reader.GetInt32(0)
            };

            return pedidos;
        }

        return null;
    }


    public List<Pedidos> Read (int? id)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM pedCli WHERE pedCli.IdCliente = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        List<Pedidos> lista = new List<Pedidos>();

        while(reader.Read())
        {
            Pedidos pedidos = new Pedidos();
            pedidos.IdCliente = reader.GetInt32(0);
            pedidos.IdPedido = reader.GetInt32(1);
            pedidos.Horario = reader.GetDateTime(2);
            pedidos.Mesa = reader.GetInt32(3);
            pedidos.ValorPedido = reader.GetDecimal(4);
            pedidos.Status = reader.GetString(5);
            
            pedidos.clientes = new Clientes 
            { 
                IdCliente = reader.GetInt32(0)
            };
            
            lista.Add(pedidos);

        }
        return lista;
    }

    public void PedidosUpdate (int id, Pedidos pedidos)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = @"UPDATE Pedidos SET mesa = @mesa WHERE idPedido = @id";

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@mesa", pedidos.Mesa);

        cmd.ExecuteNonQuery();
    }

    public void AtualizarColaborador (int id, Pedidos pedidos)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = @"UPDATE Pedidos SET colaboradorid = @IdColaborador, status = @status WHERE idPedido = @id";

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@IdColaborador", pedidos.IdColaborador);
        cmd.Parameters.AddWithValue("@status", pedidos.Status);

        cmd.ExecuteNonQuery();
    }
}