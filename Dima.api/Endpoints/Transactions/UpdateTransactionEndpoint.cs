using Dima.api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
       => app.MapPut("/{id}", HandlerAsync)
           .WithName("Categories : Update")
           .WithSummary("Atualiza uma categoria")
           .WithDescription("Atualiza uma categoria")
           .WithOrder(2)
           .Produces<Response<Transaction>>();

        private static async Task<IResult> HandlerAsync(UpdateTransactionRequest request, ITransactionHandler handler, long id)
        {
            request.UserId = "leonardo@teste";
            request.Id = id;
            var result = await handler.UpdateAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);

        }
    }
}
