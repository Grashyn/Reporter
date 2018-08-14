﻿using System.Linq;
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
                //TODO: Return token!
                return Json("");
            }

            return BadRequest("Already exists");
        }
    }
}