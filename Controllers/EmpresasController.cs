using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public class EmpresasController : Controller
{
    private IEmpresasData data;
    private IPessoasData pessoasData;

    public EmpresasController (IEmpresasData data, IPessoasData pessoasData)
    {
        this.data = data;
        this.pessoasData = pessoasData;
    }

    public ActionResult EmpresasIndex()
    {
        List<Empresas> lista = data.Read();
        return View(lista); 
    }
    
    [HttpGet]
    public ActionResult EmpresasCreate() 
    {
        ViewBag.Pessoas = pessoasData.Read();
        return View();
    }

    [HttpPost]
    public ActionResult EmpresasCreate (Empresas empresas)
    {
        data.EmpresasCreate(empresas);
        return RedirectToAction("LoginEmpresa");
    }

    public ActionResult EmpresasDelete (int id) 
    {
        data.EmpresasDelete(id);
        return RedirectToAction("LoginEmpresa");
    }

    [HttpGet]
    public ActionResult EmpresasUpdate (int id)
    {
        Empresas empresas = data.Read(id);

        if(empresas == null)
            return RedirectToAction("EmpresasIndex");

        return View(empresas);
    }

    [HttpPost]
    public ActionResult EmpresasUpdate (int id, Empresas empresas)
    {
        data.EmpresasUpdate(id, empresas);
        return RedirectToAction("IndexEmpresa", "Menu");
    }

    [HttpGet]
    public ActionResult LoginEmpresa()
    {
        return View();
    }

    [HttpPost]
    public ActionResult LoginEmpresa (IFormCollection form)
    {
        string? email = form["pessoas.email"];
        string? senha = form["pessoas.senha"];

        var empresas = data.LoginEmpresa(email!, senha!);

        if (empresas == null)
        {
            ViewBag.Error = "Usuário/Senha Inválidos";
            return View();
        }

        HttpContext.Session.SetString("empresa", JsonSerializer.Serialize(empresas));

        return RedirectToAction("IndexEmpresa", "Menu");
    }

    [HttpGet]
    public ActionResult LogoutEmpresa()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("LoginEmpresa");
    }
}
