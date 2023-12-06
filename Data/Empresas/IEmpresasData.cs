public interface IEmpresasData
{
    public List<Empresas> Read();
    public Empresas Read (int id);
    public void EmpresasCreate (Empresas empresas);
    public void EmpresasUpdate (int id, Empresas empresas);
    public void EmpresasDelete (int id);
    public Pessoas? LoginEmpresa (string email, string senha);
}