using Dima.api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.api.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Transaction : Delete")
            .WithSummary("Deleta uma transação")
            .WithDescription("Deleta uma transação")
            .WithOrder(3)
            .Produces<Response<Transaction>>();

        private static async Task<IResult> HandlerAsync(ITransactionHandler handler, long id)
        {
            var request = new DeleteTransactionRequest
            {
                UserId = "leonardo@teste",
                Id = id
            };

            var result = await handler.DeleteAsync(request);

            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
        }
    }
}
