public interface IProdutosData
{
    public List<Produtos> Read();
    public List<Produtos> Read (string search);
    public Produtos Read (int id);
    public List<Produtos> Read (int? id);
    public void ProdutosCreate (Produtos produtos);
    public void ProdutosUpdate (int id, Produtos produtos);
    public void ProdutosDelete (int id);
}