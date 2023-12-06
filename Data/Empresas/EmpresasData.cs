public class EmpresasData : IEmpresasData
{
    private List<Empresas> lista = new List<Empresas>();

    public List<Empresas> Read()
    {
        return lista;
    }

    public void EmpresasCreate (Empresas empresas) 
    {
        lista.Add(empresas);
    }

    public void EmpresasDelete (int id)
    {
        foreach(var empresas in lista) 
        {
            if(empresas.IdEmpresa == id)
            {
                lista.Remove(empresas);
                break;
            }
        }
    }

    public Empresas Read (int id)
    {
        return lista.FirstOrDefault(empresas => empresas.IdEmpresa == id);
    }

    public void EmpresasUpdate(int id, Empresas empresas)
    {
        Empresas EmpresasToUpdate = lista.First(empresas => empresas.IdEmpresa == id);
        EmpresasToUpdate.IdEmpresa = empresas.IdEmpresa;
    }

    public Pessoas? LoginEmpresa (string email, string senha)
    {
        return new Pessoas();
    }
}
