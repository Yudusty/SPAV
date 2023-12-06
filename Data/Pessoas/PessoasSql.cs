using Microsoft.Data.SqlClient;

public class PessoasSql : Database, IPessoasData
{
    public void Create (Pessoas pessoas)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "INSERT INTO Pessoas VALUES (@nome, @email, @senha, @DataNasc)";

        cmd.Parameters.AddWithValue("@nome", pessoas.Nome);
        cmd.Parameters.AddWithValue("@email", pessoas.Email);
        cmd.Parameters.AddWithValue("@senha", pessoas.Senha);
        cmd.Parameters.AddWithValue("@DataNasc", pessoas.DataNasc);

        cmd.ExecuteNonQuery();
    }

    public void Delete (int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Pessoas WHERE IdPessoa = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public List<Pessoas> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Pessoas";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Pessoas> lista = new List<Pessoas>();

        while(reader.Read())
        {
            Pessoas pessoas = new Pessoas();
            pessoas.IdPessoa = reader.GetInt32(0);
            pessoas.Nome = reader.GetString(1);
            pessoas.Email = reader.GetString(2);
            pessoas.Senha = reader.GetString(3);
            pessoas.DataNasc = reader.GetDateTime(4);

            lista.Add(pessoas);
        }
        return lista;
    }

    public List<Pessoas> Read (string search)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Pessoas WHERE Nome LIKE @Nome";

        cmd.Parameters.AddWithValue("@Nome", "%" + search + "%");

        SqlDataReader reader = cmd.ExecuteReader();

        List<Pessoas> lista = new List<Pessoas>();

        while(reader.Read())
        {
            Pessoas pessoas = new Pessoas();
            pessoas.IdPessoa = reader.GetInt32(0);
            pessoas.Nome = reader.GetString(1);
            pessoas.Email = reader.GetString(2);
            pessoas.Senha = reader.GetString(3);
            pessoas.DataNasc = reader.GetDateTime(4);

            lista.Add(pessoas);
        }
        return lista;
    }

    public Pessoas Read (int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Pessoas WHERE IdPessoa = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if(reader.Read())
        {
            Pessoas pessoas = new Pessoas();
            pessoas.IdPessoa = reader.GetInt32(0);
            pessoas.Nome = reader.GetString(1);
            pessoas.Email = reader.GetString(2);
            pessoas.Senha = reader.GetString(3);
            pessoas.DataNasc = reader.GetDateTime(4);

            return pessoas;
        }

        return null;
    }

    public void Update (int id, Pessoas pessoas)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = @"UPDATE Pessoas SET Nome = @nome, Email = @email, Senha = @senha, DataNasc = @DataNasc WHERE IdPessoa = @id";

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@nome", pessoas.Nome);
        cmd.Parameters.AddWithValue("@email", pessoas.Email);
        cmd.Parameters.AddWithValue("@senha", pessoas.Senha);
        cmd.Parameters.AddWithValue("@DataNasc", pessoas.DataNasc);

        cmd.ExecuteNonQuery();
    }
}