using Dima.api.Endpoints;
using Dima.api.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Dima.api.Common.Api
{
    public static class AppExtension
    {
        public static void ConfigureDevEnvironment(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapSwagger().RequireAuthorization();
        }
        public static void UseSecuriy(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
