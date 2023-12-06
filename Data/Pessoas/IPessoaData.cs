public interface IPessoasData
{
    public List<Pessoas> Read();
    public List<Pessoas> Read (string search);
    public Pessoas Read (int id);
    public void Create (Pessoas pessoas);
    public void Update (int id, Pessoas pessoas);
    public void Delete (int id);
}