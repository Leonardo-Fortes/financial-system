﻿using Dima.api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace Dima.api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Categories : Update")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<Category>>();

        private static async Task<IResult> HandlerAsync(UpdateCategoryRequest request, ClaimsPrincipal user, ICategoryHandler handler, long id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            request.Id = id;
            var result = await handler.UpdateAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
            
        }
    }
}
