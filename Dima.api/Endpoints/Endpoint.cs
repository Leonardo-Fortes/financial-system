using Dima.api.Common.Api;
using Dima.api.Endpoints.Categories;
using Dima.api.Endpoints.Identity;
using Dima.api.Endpoints.Transactions;
using Dima.api.Models;


namespace Dima.api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoint(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("/")
                .WithTags("Health check")
                .MapGet("/", () => new { message = "OK" });


            endpoints.MapGroup("v1/categories").WithTags("Categories")
            .RequireAuthorization()
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<GetByIdCategoryEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>();


            endpoints.MapGroup("v1/transactions").WithTags("transactions")
            .RequireAuthorization()
            .MapEndpoint<CreateTransactionEndpoint>()
            .MapEndpoint<UpdateTransactionEndpoint>()
            .MapEndpoint<DeleteTransactionEndpoint>()
            .MapEndpoint<GetTransactionByIdEndpoint>()
            .MapEndpoint<GetTransactionByPeriodEndpoint>();

            endpoints.MapGroup("v1/identity").WithTags("identity")
            .MapIdentityApi<User>();

            endpoints.MapGroup("v1/identity").WithTags("identity")
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetRolesEndpoint>();

        }

        private static IEndpointRouteBuilder MapEndpoint<TEndPoint>(this IEndpointRouteBuilder app) where TEndPoint : IEndpoint
        {
            TEndPoint.Map(app);
            return app;
        }
    }
}
