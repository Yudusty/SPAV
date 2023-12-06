using System.Text.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;

public class ProdutosController : Controller
{
    private IProdutosData data;
    private IEmpresasData empresasData;

    public ProdutosController (IProdutosData data, IEmpresasData empresasdata)
    {
        this.data = data;
        this.empresasData = empresasdata;
    }

    public ActionResult ProdutosIndex()
    {
        List<Produtos> lista = data.Read();
        return View(lista); 
    }

    public ActionResult Search (IFormCollection form)
    {
        string search = form["search"];
        
        List<Produtos> lista = data.Read(search);
        return View("IndexEmpresaProdutos", lista);
    }

    [HttpGet]
    public ActionResult ProdutosCreate() 
    {
        return View();
    }

    [HttpPost]
    public ActionResult ProdutosCreate (Produtos produtos)
    {
        data.ProdutosCreate(produtos);
        return RedirectToAction("IndexEmpresaProdutos", "Produtos");
    }

    public ActionResult ProdutosDelete (int id)
    {
        data.ProdutosDelete(id);
        return RedirectToAction("IndexEmpresaProdutos", "Produtos");
    }

    [HttpGet]
    public ActionResult ProdutosUpdate (int id)
    {
        Produtos produtos = data.Read(id);

        if(produtos == null)
            return RedirectToAction("IndexEmpresaProdutos", "Produtos");

        return View(produtos);
    }

    [HttpPost]
    public ActionResult ProdutosUpdate (int id, Produtos produtos)
    {
        data.ProdutosUpdate(id, produtos);
        return RedirectToAction("IndexEmpresaProdutos", "Produtos");
    }

        public ActionResult IndexEmpresaProdutos()
    {
        Pessoas? pessoas = new();
        string? session = HttpContext.Session.GetString("empresa");

        if(session != null)
        {
            pessoas = JsonSerializer.Deserialize<Pessoas>(session);
        }

        int? IdEmpresa = pessoas.IdPessoa;

        List<Produtos> lista = data.Read(IdEmpresa);
        return View(lista);
    }
}