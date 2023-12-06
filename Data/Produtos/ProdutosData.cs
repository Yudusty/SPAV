public class ProdutosData : IProdutosData
{
    private List<Produtos> lista = new List<Produtos>();

    public List<Produtos> Read()
    {
        return lista;
    }

    public List<Produtos> Read (string search)
    {
        var result = from l in lista
            where l.Nome.ToLower().Contains(search.ToLower())
            select l;

        return result.ToList();
    }

    public void ProdutosCreate (Produtos produtos)
    {
        lista.Add(produtos);
    }

    public void ProdutosDelete (int id)
    {
        foreach(var produtos in lista)
        {
            if(produtos.IdProduto == id)
            {
                lista.Remove(produtos);
                break;
            }
        }
    }

    public Produtos Read (int id)
    {
        return lista.FirstOrDefault(produtos => produtos.IdProduto == id);
    }

    public List<Produtos> Read (int? id)
    {
        return lista;
    }

    public void ProdutosUpdate (int id, Produtos produtos)
    {
        Produtos produtosToUpdate = lista.First(produtos => produtos.IdProduto == id);
        produtosToUpdate.Descricao = produtos.Descricao;
    }

}