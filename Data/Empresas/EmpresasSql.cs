using System.Data;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Data.SqlClient;

public class EmpresasSql : Database, IEmpresasData
{
    public void EmpresasCreate (Empresas empresas)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC sp_cadEmpresa @nomeEmpre, @emailEmpre, @senhaEmpre, @CNPJ, @datnasc";

        cmd.Parameters.AddWithValue("@nomeEmpre", empresas.Pessoas.Nome);
        cmd.Parameters.AddWithValue("@emailEmpre", empresas.Pessoas.Email);
        cmd.Parameters.AddWithValue("@senhaEmpre", empresas.Pessoas.Senha);
        cmd.Parameters.AddWithValue("@CNPJ", empresas.CNPJ);
        cmd.Parameters.AddWithValue("@datnasc", empresas.Pessoas.DataNasc);

        cmd.ExecuteNonQuery();
    }

    public void EmpresasDelete (int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC sp_DelEmp @idEmp";

        cmd.Parameters.AddWithValue("@idEmp", id);

        cmd.ExecuteNonQuery();
    }

    public List<Empresas> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM v_Empresas";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Empresas> lista = new();

        while(reader.Read())
        {
            Empresas pessoas = new Empresas();
            pessoas.IdEmpresa = reader.GetInt32(0);
            pessoas.Nome = reader.GetString(1);
            pessoas.Email = reader.GetString(2);
            pessoas.Senha = reader.GetString(3);
            pessoas.DataNasc = reader.GetDateTime(4);
            pessoas.CNPJ = reader.GetString(5);
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

    
    public Empresas Read (int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM v_Empresas WHERE IdEmpresa = @id";
        
        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if(reader.Read())
        {
            Empresas empresas = new Empresas();
            empresas.IdEmpresa = reader.GetInt32(0);
            empresas.Nome = reader.GetString(1);
            empresas.Email = reader.GetString(2);
            empresas.Senha = reader.GetString(3);
            empresas.DataNasc = reader.GetDateTime(4);
            empresas.CNPJ = reader.GetString(5);
            empresas.Pessoas = new Pessoas 
            {
                IdPessoa = reader.GetInt32(0),
                Nome = reader.GetString(1),
                Email = reader.GetString(2),
                Senha = reader.GetString(3),
                DataNasc = reader.GetDateTime(4)
            };

            return empresas;
        }

        return null;
    }

    public void EmpresasUpdate (int id, Empresas empresas)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = @"EXEC sp_ediEmp @idEmpresa, @nomeEmpre, @emailEmpre, @senhaEmpre";

        cmd.Parameters.AddWithValue("@idEmpresa", id);
        cmd.Parameters.AddWithValue("@nomeEmpre", empresas.Pessoas.Nome);
        cmd.Parameters.AddWithValue("@emailEmpre", empresas.Pessoas.Email);
        cmd.Parameters.AddWithValue("@senhaEmpre", empresas.Pessoas.Senha);

        cmd.ExecuteNonQuery();
    }

    public Pessoas LoginEmpresa (string email, string senha)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT IdPessoa, Nome FROM Pessoas WHERE email = @emailEmpre and senha = @senhaEmpre";
        
        cmd.Parameters.AddWithValue("@emailEmpre", email);
        cmd.Parameters.AddWithValue("@senhaEmpre", senha);

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
