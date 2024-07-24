using Microsoft.AspNetCore.Authentication;

namespace NZWalks.Helpers.Firebase
{
    public static class AddFirebaseAuthenticationExtensions
    {
        public static IServiceCollection AddFirebaseAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer").AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationFunctionHandler>("Bearer", delegate
            {
            });
            services.AddScoped<FirebaseAuthenticationFunctionHandler>();
            return services;
        }
    }
}
