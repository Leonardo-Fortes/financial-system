using Dima.api.Common.Api;
using Dima.Core.Configurations;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Categories : GetAll")
            .WithSummary("Busca todas as categorias")
            .WithDescription("Busca todas as categorias")
            .WithOrder(5)
            .Produces<PagedResponse<Category>>();

        private static async Task<IResult>HandlerAsync(ICategoryHandler handler, [FromQuery]int pageSize = Configuration.DefaultPageSize, [FromQuery]int pageNumber = Configuration.DefaultPageNumber)
        {
            var request = new GetAllCategoryRequest
            {
                UserId = "leonardo@teste",
                PageNumber = pageNumber,
                PageSize = pageSize    
            };

            var result = await handler.GetAllAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
