using Microsoft.AspNetCore.Mvc;

public class PessoasController : Controller
{
    private IPessoasData data;

    public PessoasController (IPessoasData data)
    {
        this.data = data;
    }

    public ActionResult Index()
    {
        List<Pessoas> lista = data.Read();
        return View(lista); 
    }

    public ActionResult Search (IFormCollection form)
    {
        string search = form["search"];
        
        List<Pessoas> lista = data.Read(search);
        return View("Index", lista);
    }

    [HttpGet]
    public ActionResult Create() 
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create (Pessoas pessoas)
    {
        data.Create(pessoas);
        return RedirectToAction("Index");
    }

    public ActionResult Delete (int id) 
    {
        data.Delete(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult Update (int id)
    {
        Pessoas pessoas = data.Read(id);

        if(pessoas == null)
            return RedirectToAction("Index");

        return View(pessoas);
    }

    [HttpPost]
    public ActionResult Update (int id, Pessoas pessoas)
    {
        data.Update(id, pessoas);
        return RedirectToAction("Index");
    }
}