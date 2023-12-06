using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class PedidosController : Controller
{
    private IPedidosData pedidosData;
    private IProdutosData produtosData;
    private IColaboradoresData colaboradoresData;

    public PedidosController (IPedidosData pedidosData, IProdutosData produtosData, IColaboradoresData colaboradoresData)
    {
        this.pedidosData = pedidosData;
        this.produtosData = produtosData;
        this.colaboradoresData = colaboradoresData;
    }

    public ActionResult PedidosIndex()
    {
        List<Pedidos> lista = pedidosData.Read();
        return View(lista); 
    }
    
    [HttpGet]
    public ActionResult PedidosCreate() 
    {

        ViewBag.Produtos = produtosData.Read().Select(c => new SelectListItem()
        {
            Text = c.Nome,
            Value = c.IdProduto.ToString()
        }).ToList();

        return View();
    }   

    [HttpPost]
    public ActionResult PedidosCreate (Pedidos pedidos)
    {
        pedidosData.PedidosCreate(pedidos);
        return RedirectToAction("IndexCliente", "Menu");
    }

    public ActionResult PedidosDelete (int id) 
    {
        pedidosData.PedidosDelete(id);
        return RedirectToAction("IndexCliente", "Menu");
    }

    [HttpGet]
    public ActionResult PedidosUpdate (int id)
    {
        Pedidos pedidos = pedidosData.ReadSpecific(id);

        if(pedidos == null)
        {
            return RedirectToAction("IndexCliente", "Menu");
        }

        return View(pedidos);
    }

    [HttpPost]
    public ActionResult PedidosUpdate (int id, Pedidos pedidos)
    {
        pedidosData.PedidosUpdate(id, pedidos);
        return RedirectToAction("IndexCliente", "Menu");
    }

    [HttpGet]
    public ActionResult AtualizarColaborador (int id)
    {
        Pedidos pedidos = pedidosData.ReadSpecific(id);

        if(pedidos == null)
        {
            return RedirectToAction("IndexPedidos", "Menu");
        }

        return View(pedidos);
    }

    [HttpPost]
    public ActionResult AtualizarColaborador (int id, Pedidos pedidos)
    {
        pedidosData.AtualizarColaborador(id, pedidos);
        return RedirectToAction("IndexPedidos", "Menu");
    }
}