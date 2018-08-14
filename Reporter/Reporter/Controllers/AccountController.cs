using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Reporter.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Reporter.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private ReporterDBEntities db = new ReporterDBEntities();

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register([FromBody]User userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = db.Users.FirstOrDefault(item => item.Email == userModel.Email);
            if (user == null)
            {
                db.Users.Add(userModel);
                await db.SaveChangesAsync();
                return Ok();
            }

            return BadRequest("Already exists");
        }

        // POST api/Account/Login
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IHttpActionResult> Login([FromBody]User userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = db.Users.FirstOrDefault(item => item.Email == userModel.Email && item.Password == userModel.Password);
            if (user != null)
            {
                var tokenExpiration = TimeSpan.FromDays(1);
                ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
                identity.AddClaim(new Claim("role", "user"));
                var props = new AuthenticationProperties()
                {
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
                };
                var ticket = new AuthenticationTicket(identity, props);
                var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);
                return Ok(new UserResponse
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Id = user.Id,
                    Rights = user.Rights,
                    Token = accessToken
                });
            }

            return BadRequest("Already exists");
        }
    }
}