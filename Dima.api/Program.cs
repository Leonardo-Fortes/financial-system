using Dima.api;
using Dima.api.Common.Api;
using Dima.api.Endpoints;



var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();



if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();
app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecuriy();
app.MapEndpoint();
app.Run();
