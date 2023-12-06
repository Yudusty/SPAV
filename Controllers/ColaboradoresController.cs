using System.Text.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;

public class ColaboradoresController : Controller
{
    private IColaboradoresData data;
    private IEmpresasData empresasData;
    private IPessoasData pessoasData;

    public ColaboradoresController (IColaboradoresData data, IPessoasData pessoasData, IEmpresasData empresasData)
    {
        this.data = data;
        this.pessoasData = pessoasData;
        this.empresasData = empresasData;
    }

    public ActionResult ColaboradoresIndex()
    {
        List<Colaboradores> lista = data.Read();
        return View(lista); 
    }
    
    [HttpGet]
    public ActionResult ColaboradoresCreate()
    {
        ViewBag.Pessoas = pessoasData.Read();
        return View();
    }

    [HttpPost]
    public ActionResult ColaboradoresCreate (Colaboradores colaboradores)
    {
        data.ColaboradoresCreate(colaboradores);
        return RedirectToAction("IndexEmpresa","Menu");
    }

    public ActionResult ColaboradoresDelete (int id) 
    {
        data.ColaboradoresDelete(id);
        return RedirectToAction("IndexEmpresa","Menu");
    }

    [HttpGet]
    public ActionResult ColaboradoresUpdate (int id)
    {
        Colaboradores colaboradores = data.Read(id);

        if(colaboradores == null)
            return RedirectToAction("IndexEmpresa","Menu");

        return View(colaboradores);
    }

    [HttpPost]
    public ActionResult ColaboradoresUpdate (int id, Colaboradores colaboradores)
    {
        data.ColaboradoresUpdate(id, colaboradores);
        return RedirectToAction("IndexEmpresa","Menu");
    }

    [HttpGet]
    public ActionResult LoginColaborador()
    {
        return View();
    }

    [HttpPost]
    public ActionResult LoginColaborador (IFormCollection form)
    {
        string? email = form["pessoas.email"];
        string? senha = form["pessoas.senha"];

        var colaboradores = data.LoginColaborador(email!, senha!);

        if(colaboradores == null)
        {
            ViewBag.Error = "Usuário/Senha inválidos";
            return View();
        }

        HttpContext.Session.SetString("colaborador", JsonSerializer.Serialize(colaboradores));

        return RedirectToAction("IndexColaborador", "Menu");
    }

    [HttpGet]
    public ActionResult LogoutColaborador()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("LoginColaborador");
    }
}
