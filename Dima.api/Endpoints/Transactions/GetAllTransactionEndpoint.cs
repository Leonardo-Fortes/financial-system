using Dima.api.Common.Api;
using Dima.Core.Configurations;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.api.Endpoints.Transactions
{
    public class GetAllTransactionEndpoint : IEndpoint
    {
            public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandlerAsync)
                .WithName("Transaction : GetAll")
                .WithSummary("Busca todas as transações")
                .WithDescription("Busca todas as transações")
                .WithOrder(5)
                .Produces<PagedResponse<Transaction>>();

            private static async Task<IResult> HandlerAsync(ITransactionHandler handler, [FromQuery] int pageSize = Configuration.DefaultPageSize, [FromQuery] int pageNumber = Configuration.DefaultPageNumber)
            {
                var request = new GetAllTransactionsRequest
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
