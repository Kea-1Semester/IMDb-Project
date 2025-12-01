
using Microsoft.AspNetCore.Authorization;

namespace GraphQL.Auth0
{
    public class HasPermissionHandler : AuthorizationHandler<HasPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasPermissionRequirement requirement
        )
        {
            // If user does not have the permission claim, return no result
            if (!context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;



            // Get the permissions from the user claims
            var permissions = context.User
                .FindAll(c => c.Type == "permissions" && c.Issuer == requirement.Issuer).ToList();

            // Succeed if the scope array contains the required scope
            if (permissions.Any(s => s.Value == requirement.Permission))
                context.Succeed(requirement);


            return Task.CompletedTask;
        }
    }
}
