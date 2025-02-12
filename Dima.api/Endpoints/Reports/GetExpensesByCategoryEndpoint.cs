using Dima.api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.api.Endpoints.Reports
{
    public class GetExpensesByCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/expenses", HandlerAsync).Produces<Response<List<ExpensesByCategory>?>>();
        private static async Task<IResult>HandlerAsync(ClaimsPrincipal user, IReportHandler handler)
        {
            var request = new GetExpensesByCategoryRequest
            {
                UserId = user.Identity?.Name ?? string.Empty
            };
            var result = await handler.GetExpensesByCategoryReportAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result) 
                : TypedResults.BadRequest(result);

        }
    }
}
