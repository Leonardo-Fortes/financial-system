using Dima.api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

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

        private static async Task<IResult> HandlerAsync(ICategoryHandler handler, long id)
        {
            var request = new DeleteCategoryRequest
            {
                UserId = "leonardo@teste",
                Id = id
            };

            var result = await handler.DeleteAsync(request);

            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
        
    }
}
