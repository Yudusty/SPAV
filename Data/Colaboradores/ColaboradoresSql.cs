using Microsoft.Data.SqlClient;

public class ColaboradoresSql : Database, IColaboradoresData
{
    public void ColaboradoresCreate (Colaboradores colaboradores)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC sp_cadColaboradores @nomeCol, @CPFCol, @emailCol, @senhaCol, @salario, @dataNasc, @idEmpresa";

        cmd.Parameters.AddWithValue("@nomeCol", colaboradores.Pessoas.Nome);
        cmd.Parameters.AddWithValue("@CPFCol", colaboradores.CPF);
        cmd.Parameters.AddWithValue("@emailCol", colaboradores.Pessoas.Email);
        cmd.Parameters.AddWithValue("@senhaCol", colaboradores.Pessoas.Senha);
        cmd.Parameters.AddWithValue("@salario", colaboradores.Salario);
        cmd.Parameters.AddWithValue("@dataNasc", colaboradores.Pessoas.DataNasc);
        cmd.Parameters.AddWithValue("@idEmpresa", colaboradores.Empresas.IdEmpresa);

        cmd.ExecuteNonQuery();
        
    }

    public void ColaboradoresDelete (int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC delCol @idColaborador";

        cmd.Parameters.AddWithValue("@idColaborador", id);

        cmd.ExecuteNonQuery();
    }

    public List<Colaboradores> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM EmpColaborador";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Colaboradores> lista = new();

        while(reader.Read())
        {
            Colaboradores pessoas = new Colaboradores();
            pessoas.IdEmpresa = reader.GetInt32(0);
            pessoas.IdColaborador = reader.GetInt32(1);
            pessoas.Nome = reader.GetString(2);
            pessoas.Email = reader.GetString(3);
            pessoas.Senha = reader.GetString(4);
            pessoas.DataNasc = reader.GetDateTime(5);
            pessoas.Salario = reader.GetDecimal(6);
            
            pessoas.Pessoas = new Pessoas 
            { 
                IdPessoa = reader.GetInt32(1),
                Nome = reader.GetString(2),
                Email = reader.GetString(3),
                Senha = reader.GetString(4),
                DataNasc = reader.GetDateTime(5)
            };

            pessoas.Empresas = new Empresas
            {
                IdEmpresa = reader.GetInt32(0)
            };

            lista.Add(pessoas);
        }
        return lista;
    }

    public Colaboradores Read (int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM EmpColaborador WHERE IdColaborador = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if(reader.Read())
        {
            Colaboradores colaboradores = new Colaboradores();
            colaboradores.IdEmpresa = reader.GetInt32(0);
            colaboradores.IdColaborador = reader.GetInt32(1);
            colaboradores.Nome = reader.GetString(2);
            colaboradores.Email = reader.GetString(3);
            colaboradores.Senha = reader.GetString(4);
            colaboradores.DataNasc = reader.GetDateTime(5);
            colaboradores.Salario = reader.GetDecimal(6);
            
            colaboradores.Pessoas = new Pessoas 
            { 
                IdPessoa = reader.GetInt32(1),
                Nome = reader.GetString(2),
                Email = reader.GetString(3),
                Senha = reader.GetString(4),
                DataNasc = reader.GetDateTime(5)
            };

            colaboradores.Empresas = new Empresas
            {
                IdEmpresa = reader.GetInt32(0)
            };

            return colaboradores;
        }

        return null;
    }

    public List<Colaboradores> Read (int? id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM EmpColaborador WHERE IdEmpresa = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        List<Colaboradores> lista = new List<Colaboradores>();

        if(reader.Read())
        {
            Colaboradores colaboradores = new Colaboradores();
            colaboradores.IdEmpresa = reader.GetInt32(0);
            colaboradores.IdColaborador = reader.GetInt32(1);
            colaboradores.Nome = reader.GetString(2);
            colaboradores.Email = reader.GetString(3);
            colaboradores.Senha = reader.GetString(4);
            colaboradores.DataNasc = reader.GetDateTime(5);
            colaboradores.Salario = reader.GetDecimal(6);
            
            colaboradores.Pessoas = new Pessoas 
            { 
                IdPessoa = reader.GetInt32(1),
                Nome = reader.GetString(2),
                Email = reader.GetString(3),
                Senha = reader.GetString(4),
                DataNasc = reader.GetDateTime(5)
            };

            colaboradores.Empresas = new Empresas
            {
                IdEmpresa = reader.GetInt32(0)
            };

            lista.Add(colaboradores);
        }
        return lista;
    }

    public void ColaboradoresUpdate (int id, Colaboradores colaboradores)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = @"EXEC sp_ediCola @idCol, @nomeCol, @emailCol, @senhaCol, @salario";

        cmd.Parameters.AddWithValue("@idCol", id);
        cmd.Parameters.AddWithValue("@nomeCol", colaboradores.Pessoas.Nome);
        cmd.Parameters.AddWithValue("@emailCol", colaboradores.Pessoas.Email);
        cmd.Parameters.AddWithValue("@senhaCol", colaboradores.Pessoas.Senha);
        cmd.Parameters.AddWithValue("@salario", colaboradores.Salario);

        cmd.ExecuteNonQuery();
    }

    public Pessoas LoginColaborador (string email, string senha)
    {
        SqlCommand cmd = new();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT IdPessoa, Nome FROM Pessoas WHERE email = @emailCol and senha = @senhaCol";

        cmd.Parameters.AddWithValue("@emailCol", email);
        cmd.Parameters.AddWithValue("@senhaCol", senha);

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