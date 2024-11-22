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
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();// 
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(CnnString);
});



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/v1/categories/", async (
    ICategoryHandler handler) //usar instancia do ICategoryHandler para manipular o request
    =>
{
    var request = new GetAllCategoryRequest
    {
        UserId = "leonardo@teste"
    };

    return await handler.GetAllAsync(request);
}).WithName("Categories: Get All ")
.WithSummary("Categoria encontrada")
.Produces<PagedResponse<List<Category>>>();


app.MapGet("/v1/categories/{id}", async (long id, //vem do corpo
    ICategoryHandler handler) //usar instancia do ICategoryHandler para manipular o request
    =>
{
    var request = new GetCategoryByIdRequest
    {
        Id = id,
        UserId = "leonardo@teste"
    };

    return await handler.GetByIdAsync(request);
}).WithName("Categories: Get by Id")
.WithSummary("Categoria encontrada")
.Produces<Response<Category>>();


app.MapPost("/v1/categories", async (CreateCategoryRequest request, //vem do corpo
    ICategoryHandler handler) //usar instancia do ICategoryHandler para manipular o request
    => await handler.CreateAsync(request)).WithName("Categories: Create")
    .WithSummary("Cria uma nova Categoria")
    .Produces<Response<Category>>();


app.MapPut("/v1/categories/{id}", async (long id, UpdateCategoryRequest request, //vem do corpo
    ICategoryHandler handler) //usar instancia do ICategoryHandler para manipular o request
    =>
    {
        request.Id = id;
        return await handler.UpdateAsync(request);
    }).WithName("Categories: Update")
    .WithSummary("Atualizar uma Categoria")
    .Produces<Response<Category>>();




app.MapDelete("/v1/categories/{id}", async (long id, //vem do corpo
    ICategoryHandler handler) //usar instancia do ICategoryHandler para manipular o request
    =>
    {
        var request = new DeleteCategoryRequest { Id = id };
        return await handler.DeleteAsync(request);
    }).WithName("Categories: Delete")
     .WithSummary("Remover uma nova Categoria")
     .Produces<Response<Category>>();






app.Run();
