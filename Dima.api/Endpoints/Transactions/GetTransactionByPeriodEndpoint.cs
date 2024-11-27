using Dima.api.Common.Api;
using Dima.api.Models;
using Dima.Core.Configurations;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dima.api.Endpoints.Transactions
{
    public class GetTransactionByPeriodEndpoint : IEndpoint
    {
            public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandlerAsync)
                .WithName("Transaction : GetAll")
                .WithSummary("Recupera todas as transações")
                .WithDescription("Recupera todas as transações")
                .WithOrder(5)
                .Produces<PagedResponse<Transaction>>();

        private static async Task<IResult> HandlerAsync(ITransactionHandler handler, ClaimsPrincipal user,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageSize = Configuration.DefaultPageSize,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber)
            {
                var request = new GetTransactionsByPeriodRequest
                {
                    UserId = user.Identity?.Name ?? string.Empty,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    StartDate = startDate,
                    EndDate = endDate
                };

                var result = await handler.GetByPeriodAsync(request);
                return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
            }
        
    }
}
