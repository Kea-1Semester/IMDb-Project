using Microsoft.AspNetCore.Authorization;

namespace GraphQL.Auth0
{
    public class DevAuthorizationHandler : IAuthorizationHandler
    {
        private readonly IHostEnvironment _env;

        public DevAuthorizationHandler(IHostEnvironment env)
        {
            _env = env;
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            if (_env.IsDevelopment())
            {
                foreach (var req in context.PendingRequirements.ToList())
                {
                    context.Succeed(req);
                }
            }

            return Task.CompletedTask;
        }
    }
}