using Dima.api.Data;
using Dima.api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var CnnString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>{
    x.CustomSchemaIds(n => n.FullName);
});
builder.Services.AddTransient<ICategoryHandler>();// 
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(CnnString);
});



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/v1/categories", (CreateCategoryRequest request, //vem do corpo
    ICategoryHandler handler) //usar instancia do ICategoryHandler para manipular o request
    => handler.CreateAsync(request)).WithName("Categories: Create")
.WithSummary("Cria uma nova Categoria")
.Produces<Response<Category>>();


app.Run();
