using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PsikoWeb.Modules;
using PsikoWeb.WebApp;
using PsikoWeb.WebApp.Services;
using Repository;
using Service.Mapping;
using Service.Validations;
using System.Reflection;
using System.Security.Policy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>()); ;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Host.UseServiceProviderFactory
    (
        new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(conteinerBuilder => conteinerBuilder.RegisterModule(new RepoServiceModule()));
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddHttpClient<ProductApiService>(options => 
    {
    options.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
    });
builder.Services.AddHttpClient<CategoryApiService>(options => 
    {
        options.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
    });
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
