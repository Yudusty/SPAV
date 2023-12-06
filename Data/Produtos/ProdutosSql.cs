using Microsoft.Data.SqlClient;

public class ProdutosSql : Database, IProdutosData
{
    public void ProdutosCreate (Produtos produtos)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "EXEC  sp_CadProduto @nome, @descricao, @qtd, @valor, @idProduto";

        cmd.Parameters.AddWithValue("@nome", produtos.Nome);
        cmd.Parameters.AddWithValue("@descricao", produtos.Descricao);
        cmd.Parameters.AddWithValue("@qtd", produtos.Qtd);
        cmd.Parameters.AddWithValue("@valor", produtos.Valor);
        cmd.Parameters.AddWithValue("@idProduto", produtos.Empresas.IdEmpresa);

        cmd.ExecuteNonQuery();
    }

    public void ProdutosDelete (int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "DELETE FROM Produtos WHERE IdProduto = @id";

        cmd.Parameters.AddWithValue("@id", id);

        cmd.ExecuteNonQuery();
    }

    public List<Produtos> Read()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Produtos";

        SqlDataReader reader = cmd.ExecuteReader();

        List<Produtos> lista = new List<Produtos>();

        while(reader.Read())
        {
            Produtos empresas = new Produtos();
            empresas.IdProduto = reader.GetInt32(0);
            empresas.IdEmpresa = reader.GetInt32(1);
            empresas.Nome = reader.GetString(2);
            empresas.Descricao = reader.GetString(3);
            empresas.Qtd = reader.GetInt32(4);
            empresas.Valor = reader.GetDecimal(5);
            empresas.Empresas = new Empresas
            {
                IdEmpresa = reader.GetInt32(1)
            };

            lista.Add(empresas);
        }
        return lista;
    }

    public List<Produtos> Read (string search)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Produtos WHERE Nome LIKE @nome";

        cmd.Parameters.AddWithValue("@nome", "%" + search + "%");

        SqlDataReader reader = cmd.ExecuteReader();

        List<Produtos> lista = new List<Produtos>();

        while(reader.Read())
        {
            Produtos produtos = new Produtos();
            produtos.IdProduto = reader.GetInt32(0);
            produtos.IdEmpresa = reader.GetInt32(1);
            produtos.Nome = reader.GetString(2);
            produtos.Descricao = reader.GetString(3);
            produtos.Qtd = reader.GetInt32(4);
            produtos.Valor = reader.GetDecimal(5);
            produtos.Empresas = new Empresas
            {
                IdEmpresa = reader.GetInt32(1)
            };

            lista.Add(produtos);
        }
        return lista;
    }


    public Produtos Read (int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Produtos WHERE IdProduto = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        if(reader.Read())
        {
            Produtos produtos = new Produtos();
            produtos.IdProduto = reader.GetInt32(0);
            produtos.IdEmpresa = reader.GetInt32(1);
            produtos.Nome = reader.GetString(2);
            produtos.Descricao = reader.GetString(3);
            produtos.Qtd = reader.GetInt32(4);
            produtos.Valor = reader.GetDecimal(5);
            produtos.Empresas = new Empresas
            {
                IdEmpresa = reader.GetInt32(1)
            };

            return produtos;
        }

        return null;
    }

    public List<Produtos> Read (int? id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = "SELECT * FROM Produtos WHERE empresaid = @id";

        cmd.Parameters.AddWithValue("@id", id);

        SqlDataReader reader = cmd.ExecuteReader();

        List<Produtos> lista = new List<Produtos>();

        while(reader.Read())
        {
            Produtos produtos = new Produtos();
            produtos.IdProduto = reader.GetInt32(0);
            produtos.IdEmpresa = reader.GetInt32(1);
            produtos.Nome = reader.GetString(2);
            produtos.Descricao = reader.GetString(3);
            produtos.Qtd = reader.GetInt32(4);
            produtos.Valor = reader.GetDecimal(5);
            produtos.Empresas = new Empresas
            {
                IdEmpresa = reader.GetInt32(1)
            };

            lista.Add(produtos);
        }
        return lista;
    }

    public void ProdutosUpdate (int id, Produtos produtos)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connection;
        cmd.CommandText = @"EXEC sp_ediProd @idprod, @descricao, @qtd, @valor";

        cmd.Parameters.AddWithValue("@idprod", id);
        cmd.Parameters.AddWithValue("@descricao", produtos.Descricao);
        cmd.Parameters.AddWithValue("@qtd", produtos.Qtd);
        cmd.Parameters.AddWithValue("@valor", produtos.Valor);

        cmd.ExecuteNonQuery();
    }
}