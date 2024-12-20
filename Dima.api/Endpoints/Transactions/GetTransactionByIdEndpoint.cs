﻿using Dima.api.Common.Api;
using Dima.api.Models;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
            public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandlerAsync)
                .WithName("Transaction : GetById")
                .WithSummary("Busca uma transação")
                .WithDescription("Busca uma transação")
                .WithOrder(4)
                .Produces<Response<Transaction>>();

            private static async Task<IResult> HandlerAsync(ITransactionHandler handler, ClaimsPrincipal user, long id)
            {
                var request = new GetTransactionByIdRequest
                {
                    UserId = user.Identity?.Name ?? string.Empty,
                    Id = id
                };

                var result = await handler.GetByIdAsync(request);
                return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
            }
        
    }
}
