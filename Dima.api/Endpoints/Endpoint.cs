using Dima.api.Common.Api;
using Dima.api.Endpoints.Categories;
using Dima.api.Endpoints.Transactions;
using System.Drawing.Text;

namespace Dima.api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoint(this WebApplication app)
        {
            var endpoints = app.MapGroup("v1/categories");

            endpoints.MapGroup("v1/categories").WithTags("Categories")
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<GetByIdCategoryEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>();

            var endpointstransaction = app.MapGroup("v1/transactions");

            endpointstransaction.MapGroup("v1/transactions").WithTags("Transactions")
            .MapEndpoint<CreateTransactionEndpoint>()
            .MapEndpoint<UpdateTransactionEndpoint>()
            .MapEndpoint<DeleteTransactionEndpoint>()
            .MapEndpoint<GetTransactionByIdEndpoint>()
            .MapEndpoint<GetAllTransactionEndpoint>();

        }
        
        private static IEndpointRouteBuilder MapEndpoint<TEndPoint>(this IEndpointRouteBuilder app) where TEndPoint : IEndpoint
        {
            TEndPoint.Map(app);
            return app;
        }
    }
}
