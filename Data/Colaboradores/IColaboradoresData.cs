public interface IColaboradoresData
{
    public List<Colaboradores> Read();
    public Colaboradores Read (int id);
    public List<Colaboradores> Read (int? id);
    public void ColaboradoresCreate (Colaboradores colaboradores);
    public void ColaboradoresUpdate (int id, Colaboradores colaboradores);
    public void ColaboradoresDelete (int id);
    public Pessoas? LoginColaborador (string email, string senha);
}
