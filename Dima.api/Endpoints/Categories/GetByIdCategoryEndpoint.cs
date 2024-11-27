using Dima.api.Common.Api;
using Dima.api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.api.Endpoints.Categories
{
    public class GetByIdCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Categories : GetById")
            .WithSummary("Busca uma categoria")
            .WithDescription("Busca uma categoria")
            .WithOrder(4)
            .Produces<Response<Category>>();

        private static async Task<IResult> HandlerAsync(ICategoryHandler handler,ClaimsPrincipal user, long id)
        {
            var request = new GetCategoryByIdRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }


}
