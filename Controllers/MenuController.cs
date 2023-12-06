using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;

public class MenuController : Controller
{
    private IColaboradoresData colaboradoresData;
    private IPedidosData pedidosData;
    private IProdutosData produtosData;

    public MenuController (IColaboradoresData colaboradoresData, IPedidosData pedidosData, IProdutosData produtosData)
    {
        this.colaboradoresData = colaboradoresData;
        this.pedidosData = pedidosData;
        this.produtosData = produtosData;
    }

    public ActionResult Index()
    {
        return View();
    }

    public ActionResult IndexCliente()
    {
        Pessoas? pessoas = new();
        string? session = HttpContext.Session.GetString("cliente");

        if(session != null)
        {
            pessoas = JsonSerializer.Deserialize<Pessoas>(session);
        }

        int? IdCliente = pessoas.IdPessoa;

        ViewBag.IdCliente = IdCliente;

        List<Pedidos> lista = pedidosData.Read(IdCliente);
        return View(lista);
    }

    public ActionResult IndexEmpresa()
    {
        Pessoas? pessoas = new();
        string? session = HttpContext.Session.GetString("empresa");

        if(session != null)
        {
            pessoas = JsonSerializer.Deserialize<Pessoas>(session);
        }

        int? IdEmpresa = pessoas.IdPessoa;

        List<Colaboradores> lista = colaboradoresData.Read(IdEmpresa);
        return View(lista);
    }

    public ActionResult IndexColaborador()
    {
        Pessoas? pessoas = new();
        string? session = HttpContext.Session.GetString("cliente");

        if(session != null)
        {
            pessoas = JsonSerializer.Deserialize<Pessoas>(session);
        }

        int? IdColaborador = pessoas.IdPessoa;

        List<Pedidos> lista = pedidosData.Read(IdColaborador);
        return View(lista);
    }

    public ActionResult IndexPedidos()
    {
        List<Pedidos> lista = pedidosData.Read();
        return View(lista);
    }

    public ActionResult IndexPagar()
    {
        Pedidos pedidos = new();
        string? session = HttpContext.Session.GetString("pedido");

        if(session != null)
        {
            pedidos = JsonSerializer.Deserialize<Pedidos>(session);
        }

        int? IdCliente = pedidos.IdPedido;
        
        List<Pedidos> lista = pedidosData.Read();
        return View(lista);
    }
}

