using Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Core.UnitOfWorks;
using Repository.UnitOfWorks;
using Core.Repositories;
using Repository.Repositories;
using Core.Services;
using Service.Services;
using Service.Mapping;
using FluentValidation.AspNetCore;
using Core.DTOs;
using Service.Validations;
using PsikoWebApi.Filter;
using Microsoft.AspNetCore.Mvc;
using PsikoWebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x =>x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepositories, ProductRepository>();
builder.Services.AddScoped<IProductServis, ProductService>();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCustomException();
app.UseAuthorization();

app.MapControllers();

app.Run();
