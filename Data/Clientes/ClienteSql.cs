using System.Data;
using Microsoft.Data.SqlClient;

public class ClientesSql : Database, IClientesData
{
    public void ClientesCreate (Clientes clientes)
    {  
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC sp_cadCliente @nomeCli, @emailCli, @senhaCli, @NascCli, @CPFCli";

        cmd.Parameters.AddWithValue("@nomeCli", clientes.Pessoas.Nome);
        cmd.Parameters.AddWithValue("@emailCli", clientes.Pessoas.Email);
        cmd.Parameters.AddWithValue("@senhaCli", clientes.Pessoas.Senha);
        cmd.Parameters.AddWithValue("@NascCli", clientes.Pessoas.DataNasc);
        cmd.Parameters.AddWithValue("@CPFCli", clientes.CPF);

        cmd.ExecuteNonQuery();
    }

    public void ClientesDelete (int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC sp_DelCLi @idCli";

        cmd.Parameters.AddWithValue("@idCli", id);

        cmd.ExecuteNonQuery();
    }

    public List<Clientes> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM v_Clientes";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Clientes> lista = new();

        while(reader.Read())
        {
            Clientes pessoas = new Clientes();
            pessoas.IdCliente = reader.GetInt32(0);
            pessoas.Nome = reader.GetString(1);
            pessoas.Email = reader.GetString(2);
            pessoas.Senha = reader.GetString(3);
            pessoas.DataNasc = reader.GetDateTime(4);
            pessoas.CPF = reader.GetString(5);
            pessoas.Pessoas = new Pessoas 
            { 
                IdPessoa = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Email = reader.GetString(2),
                Senha = reader.GetString(3),
                DataNasc = reader.GetDateTime(4)
            };

            lista.Add(pessoas);
        }
        return lista;
    }

    
    public Clientes Read (int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM v_Clientes WHERE IdCliente = @id";
        
        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if(reader.Read())
        {
            Clientes clientes = new Clientes();
            clientes.IdCliente = reader.GetInt32(0);
            clientes.Nome = reader.GetString(1);
            clientes.Email = reader.GetString(2);
            clientes.Senha = reader.GetString(3);
            clientes.DataNasc = reader.GetDateTime(4);
            clientes.CPF = reader.GetString(5);
            clientes.Pessoas = new Pessoas
            {
                IdPessoa = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Email = reader.GetString(2),
                Senha = reader.GetString(3),
                DataNasc = reader.GetDateTime(4)
            }; 

            return clientes;
        }

        return null;
    }

    public void ClientesUpdate (int id, Clientes clientes)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = @"EXEC sp_ediCli @nomeCli, @emailCli, @senhaCli, @idCliente";

        cmd.Parameters.AddWithValue("@nomeCli", clientes.Pessoas.Nome);
        cmd.Parameters.AddWithValue("@emailCli", clientes.Pessoas.Email);
        cmd.Parameters.AddWithValue("@senhaCli", clientes.Pessoas.Senha);
        cmd.Parameters.AddWithValue("@idCliente", id);

        cmd.ExecuteNonQuery();
    }

    public Pessoas LoginCliente (string email, string senha)
    {
        SqlCommand cmd = new();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT IdPessoa, Nome FROM Pessoas WHERE email = @emailCli and senha = @senhaCli";

        cmd.Parameters.AddWithValue("@emailCli", email);
        cmd.Parameters.AddWithValue("@senhaCli", senha);

        SqlDataReader reader = cmd.ExecuteReader();

        if(reader.Read())
        {
            Pessoas pessoa = new();
            pessoa.IdPessoa = reader.GetInt32(0);
            pessoa.Nome = reader.GetString(1);

            return pessoa;
        }

    return null;
    }

}