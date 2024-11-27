using Dima.api.Common.Api;
using Dima.api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("", HandlerAsync)
            .WithName("Transaction : Create")
            .WithDescription("Criar transação")
            .WithSummary("Criar transação")
            .WithOrder(1)
            .Produces<Response<Transaction>>();


        private static async Task<IResult> HandlerAsync(CreateTransactionRequest request, ClaimsPrincipal user, ITransactionHandler handler)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateAsync(request);

            return result.IsSuccess
                ? TypedResults.Created($"{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
