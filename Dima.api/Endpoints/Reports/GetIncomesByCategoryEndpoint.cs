using Dima.api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.api.Endpoints.Reports
{
    public class GetIncomesByCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapGet("/incomes", HandlerAsync).Produces<Response<List<IncomesByCategory>?>>();

        private static async Task<IResult> HandlerAsync(ClaimsPrincipal user, IReportHandler handler)
        {
            var request = new GetIncomesByCategoryRequest
            {
                UserId = user.Identity?.Name ?? string.Empty
            };
            var result = await handler.GetIncomesByCategoryReportAsync(request);
            return result.IsSuccess ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
