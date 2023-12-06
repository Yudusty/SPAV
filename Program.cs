var builder = WebApplication.CreateBuilder(args);

// middlewares (adiciono)
builder.Services.AddControllersWithViews();

//
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession();

builder.Services.AddTransient<IPessoasData, PessoasSql>();
builder.Services.AddTransient<IProdutosData, ProdutosSql>();
builder.Services.AddTransient<IPedidosData, PedidosSql>();
builder.Services.AddTransient<IClientesData, ClientesSql>();
builder.Services.AddTransient<IEmpresasData, EmpresasSql>();
builder.Services.AddTransient<IColaboradoresData, ColaboradoresSql>();


var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

// middlewares (configuro)
app.MapControllerRoute("default", "/{controller=Menu}/{action=Index}/{id?}");

app.Run();