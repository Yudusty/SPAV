public class ColaboradoresData : IColaboradoresData
{
    private List<Colaboradores> lista = new List<Colaboradores>();

    public List<Colaboradores> Read()
    {
        return lista;
    }

    public void ColaboradoresCreate (Colaboradores colaboradores) 
    {
        lista.Add(colaboradores);
    }

    public void ColaboradoresDelete (int id)
    {
        foreach(var colaboradores in lista) 
        {
            if(colaboradores.IdColaborador == id)
            {
                lista.Remove(colaboradores);
                break;
            }
        }
    }

    public Colaboradores Read (int id)
    {
        return lista.FirstOrDefault(colaboradores => colaboradores.IdColaborador == id);
    }

    public List<Colaboradores> Read (int? id)
        {
            return lista;
        }

    public void ColaboradoresUpdate (int id, Colaboradores colaboradores)
    {
        Colaboradores ColaboradoresToUpdate = lista.First(colaboradores => colaboradores.IdColaborador == id);
        ColaboradoresToUpdate.IdColaborador = colaboradores.IdColaborador;
    }

    public Pessoas? LoginColaborador (string email, string senha)
    {
        return new Pessoas();
    }

}
