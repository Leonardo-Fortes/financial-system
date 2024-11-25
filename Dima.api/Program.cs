using Dima.api.Data;
using Dima.api.Endpoints;
using Dima.api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var CnnString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();// 
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(CnnString);
});



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.Map("", () => new { message = "OK" });
app.MapEndpoint();


app.Run();
