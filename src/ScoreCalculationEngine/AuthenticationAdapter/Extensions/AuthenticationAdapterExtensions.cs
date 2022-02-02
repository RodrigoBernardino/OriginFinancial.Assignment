using Microsoft.AspNetCore.Builder;

namespace AuthenticationAdapter.Extensions
{
    public static class AuthenticationAdapterExtensions
    {
        public static void AddAuthenticationAdapter(this WebApplication app)
        {
            app.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
