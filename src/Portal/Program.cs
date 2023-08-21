using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// sqlite connection string and injection dependency
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AplicacaoContext>(options =>  options.UseLazyLoadingProxies().UseSqlite(connectionString));

// injection dependency for repositories
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProducaoRepository, ProducaoRepository>();
builder.Services.AddScoped<IOrdemProducaoRepository, OrdemProducaoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();


// Cookie Authentication and Authorization
builder.Services.Configure<CookiePolicyOptions>(options =>  
{  
    options.CheckConsentNeeded = context => true;  
    options.MinimumSameSitePolicy = SameSiteMode.None;
});  
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
    {
        config.Cookie.Name = "UserLoginCookie";
        config.LoginPath = "/Login/Index";
    }); 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();  
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();