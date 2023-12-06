using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;

public class ClientesController : Controller
{
    private IClientesData data;
    private IPessoasData pessoasData;

    public ClientesController (IClientesData data, IPessoasData pessoasData)
    {
        this.data = data;
        this.pessoasData = pessoasData;
    }

    public ActionResult ClientesIndex()
    {
        List<Clientes> lista = data.Read();
        return View(lista);
    }
    
    [HttpGet]
    public ActionResult ClientesCreate()
    {
        ViewBag.Pessoas = pessoasData.Read();
        return View();
    }

    [HttpPost]
    public ActionResult ClientesCreate (Clientes clientes)
    {
        data.ClientesCreate(clientes);
        return RedirectToAction("LoginCliente");
    }

    public ActionResult ClientesDelete (int id)
    {
        data.ClientesDelete(id);
        return RedirectToAction("LoginCliente");
    }

    [HttpGet]
    public ActionResult ClientesUpdate (int id)
    {
        Clientes clientes = data.Read(id);

        if(clientes == null)
            return RedirectToAction("ClientesIndex");

        return View(clientes);
    }

    [HttpPost]
    public ActionResult ClientesUpdate (int id, Clientes clientes)
    {
        data.ClientesUpdate(id, clientes);
        return RedirectToAction("IndexCliente", "Menu");
    }

    [HttpGet]
    public ActionResult LoginCliente()
    {
        return View();
    }

    [HttpPost]
    public ActionResult LoginCliente (IFormCollection form)
    {
        string? email = form["pessoas.email"];
        string? senha = form["pessoas.senha"];

        var clientes = data.LoginCliente(email!, senha!);

        if(clientes == null)
        {
            ViewBag.Error = "Usuário/Senha inválidos";
            return View();
        }

        HttpContext.Session.SetString("cliente", JsonSerializer.Serialize(clientes));

        return RedirectToAction("IndexCliente", "Menu");
    }

    [HttpGet]
    public ActionResult LogoutCliente()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("LoginCliente");
    }
}