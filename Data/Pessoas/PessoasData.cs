public class PessoasData : IPessoasData
{
    private List<Pessoas> lista = new List<Pessoas>();

    public List<Pessoas> Read()
    {
        return lista;
    }

    public List<Pessoas> Read (string search)
    {
        var result = from l in lista
            where l.Nome.ToLower().Contains(search.ToLower())
            select l;

        return result.ToList();
    }

    public void Create (Pessoas pessoas) 
    {
        lista.Add(pessoas);
    }

    public void Delete (int id)
    {
        foreach(var pessoas in lista) 
        {
            if(pessoas.IdPessoa == id)
            {
                lista.Remove(pessoas);
                break;
            }
        }
    }

    public Pessoas Read (int id)
    {
        return lista.FirstOrDefault(pessoas => pessoas.IdPessoa == id);
    }

    public void Update (int id, Pessoas pessoas)
    {
        Pessoas pessoasToUpdate = lista.First(pessoas => pessoas.IdPessoa == id);
        pessoasToUpdate.Nome = pessoas.Nome;
    }

}