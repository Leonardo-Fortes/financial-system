﻿using Dima.api.Common.Api;
using Dima.api.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Dima.api.Endpoints.Identity
{
    public class GetRolesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)

        => app.MapGet("/roles", Handle).RequireAuthorization();



        private static  Task<IResult> Handle(ClaimsPrincipal user)
        {
            if (user.Identity is null || !user.Identity.IsAuthenticated)
                return Task.FromResult(Results.Unauthorized());

            var identity = (ClaimsIdentity)user.Identity;
            var roles = identity.FindAll(identity.RoleClaimType).Select(c => new
            {
                c.Issuer,
                c.OriginalIssuer,
                c.Type,
                c.Value,
                c.ValueType
            });
            return  Task.FromResult(Results.Json(roles));
           
        }
    }
    
}
