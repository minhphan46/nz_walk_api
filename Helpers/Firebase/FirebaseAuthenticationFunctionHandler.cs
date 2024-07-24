using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace NZWalks.Helpers.Firebase
{
    public class FirebaseAuthenticationFunctionHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly FirebaseApp _firebaseApp;
        public FirebaseAuthenticationFunctionHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            FirebaseApp firebaseApp
            )
            : base(options, logger, encoder, clock)
        {
            _firebaseApp = firebaseApp;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Context.Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.NoResult();
            }

            string bearerToken = Context.Request.Headers["Authorization"];
            if (bearerToken == null || !bearerToken.StartsWith("Bearer "))
            {
                return AuthenticateResult.Fail("Invalid Scheme");
            }

            string token = bearerToken.Substring("Bearer ".Length);

            try
            {
                FirebaseToken firebaseToken = await FirebaseAuth.GetAuth(_firebaseApp).VerifyIdTokenAsync(token);

                return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new List<ClaimsIdentity>() {
                new ClaimsIdentity(ToClaims(firebaseToken.Claims), nameof(FirebaseAuthenticationFunctionHandler))
                }), JwtBearerDefaults.AuthenticationScheme));
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex);
            }

        }

        private IEnumerable<Claim> ToClaims(IReadOnlyDictionary<string, object> claims)
        {
            return new List<Claim>();
            //Users currentUser = new Users();
            //using (CheemsDbContext context = _dbContextFactory.CreateDbContext())
            //{
            //    var users = (from user in context.users
            //                 join role in context.roles on user.RolesId equals role.Id
            //                 select new Users
            //                 {
            //                     Id = user.Id,
            //                     Email = user.Email,
            //                     Phone = user.Phone,
            //                     Roles = role
            //                 }).ToList().AsEnumerable();

            //    users = users.Where(_ => claims["email"].ToString().ToLower().Equals(_.Email));
            //    currentUser = users.FirstOrDefault();
            //}

            //return new List<Claim>
            //{
            //    new Claim("id", currentUser.Id.ToString()),
            //    new Claim("email", currentUser.Email.ToString()),
            //    new Claim("role", currentUser.Roles.Name.ToString()),
            //    /*new Claim("name", currentUser.Fullname.ToString()),
            //    new Claim("phone", currentUser.Phone.ToString()),*/
            //};
        }
    }
}