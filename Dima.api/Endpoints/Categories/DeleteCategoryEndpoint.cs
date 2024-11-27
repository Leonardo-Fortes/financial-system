using Dima.api.Common.Api;
using Dima.api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.api.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Categories : Delete")
            .WithSummary("Deleta uma categoria")
            .WithDescription("Deleta uma categoria")
            .WithOrder(3)
            .Produces<Response<Category>>();

        private static async Task<IResult> HandlerAsync(ICategoryHandler handler, ClaimsPrincipal user, long id)
        {
            var request = new DeleteCategoryRequest
            {
                UserId =  user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.DeleteAsync(request);

            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
        
    }
}
