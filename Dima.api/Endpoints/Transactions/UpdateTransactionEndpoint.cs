using Dima.api.Common.Api;
using Dima.api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
       => app.MapPut("/{id}", HandlerAsync)
           .WithName("Transaction: Update")
           .WithSummary("Atualiza uma transação")
           .WithDescription("Atualiza uma transação")
           .WithOrder(2)
           .Produces<Response<Transaction>>();

        private static async Task<IResult> HandlerAsync(UpdateTransactionRequest request,ClaimsPrincipal user, ITransactionHandler handler, long id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;
            var result = await handler.UpdateAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);

        }
    }
}
