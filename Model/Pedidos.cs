using System;
using System.Collections;

// Classe Pedidos
public class Pedidos 
{
    // Atributos
    public int IdPedido { get; set; }
    public DateTime Horario  { get; set; }
    public int Mesa { get; set; }
    public int QTD { get; set; }
    public decimal ValorPedido { get; set; }
    public string Status { get; set; }
    public int IdCliente { get; set; }
    public int IdColaborador { get; set; }
    public int IdProduto { get; set; }
    public Clientes clientes { get; set; }
    public Colaboradores colaboradores { get; set; }
    public Produtos produtos { get; set; }

    public Pedidos()
    {
        clientes = new Clientes();
        colaboradores = new Colaboradores();
        produtos = new Produtos();
    }

    // static void Cardapio()
    // {
    //     // Creating a collection (e.g., ArrayList)
    //     ArrayList list = new ArrayList();
    //     list.Add("Hello");
    //     list.Add("world");
    //     list.Add("!");

    //     // Getting the enumerator
    //     IEnumerator enumerator = list.GetEnumerator();

    //     // Iterating through the collection using the enumerator
    //     while (enumerator.MoveNext())
    //     {
    //         Console.WriteLine(enumerator.Current);
    //     }
    // }
}