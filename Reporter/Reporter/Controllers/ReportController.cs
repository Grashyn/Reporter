using Reporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Reporter.Controllers
{
    public class ReportController : ApiController
    {
        private ReporterDBEntities db = new ReporterDBEntities();

        [Authorize]
        public IEnumerable<Report> Get()
        {
            return db.Reports;
        }

        [Authorize]
        [Route("GetUserReports")]
        public IEnumerable<Report> GetUserReports([FromUri] Guid userId)
        {
            return db.Reports.Where(item => item.CreatorId == userId ||
                                            item.AssigneeId == userId);
        }

        [Authorize]
        public IHttpActionResult Get(Guid id)
        {
            var reports = db.Reports.Where(item => item.Id == id)
                                   .Select(item => new ReportResponse
                                   {
                                       Id = item.Id,
                                       CreationDate = item.CreationDate,
                                       LastUpdateDate = item.LastUpdateDate,
                                       Text = item.Text,
                                       Title = item.Title,
                                       Assignee = new UserResponse
                                       {
                                           DisplayName = item.Assignee.DisplayName,
                                           Email = item.Assignee.Email
                                       },
                                       Creator = new UserResponse
                                       {
                                           DisplayName = item.Creator.DisplayName,
                                           Email = item.Creator.Email
                                       },
                                       Comments = item.Comments.Select(comment => new CommentResponse
                                       {
                                           Id = comment.Id,
                                           PostDate = comment.PostDate,
                                           Text = comment.Text,
                                           User = new UserResponse
                                           {
                                               DisplayName = comment.User.DisplayName,
                                               Email = comment.User.Email
                                           }
                                       }),
                                   }).ToList();
            if (reports.Any())
            {
                return Ok(Json(reports.First()));
            }

            return BadRequest("Report is missing");
        }

        [Authorize]
        public async Task<IHttpActionResult> Post([FromBody]Report value)
        {
            db.Reports.Add(value);
            var now = DateTime.UtcNow;
            value.CreationDate = now;
            value.LastUpdateDate = now;
            db.SaveChanges();
            return Ok();
        }

        [Authorize]
        public void Put(Guid id, [FromBody]Report value)
        {
            var item = db.Reports.FirstOrDefault(element => element.Id == id);
            var now = DateTime.UtcNow;
            if (item != null)
            {
                db.Reports.Remove(item);
            }

            value.CreationDate = item != null ? item.CreationDate : now;
            value.LastUpdateDate = now;
            db.Reports.Add(value);
            db.SaveChanges();
        }

        [Authorize]
        public void Delete(Guid id)
        {
            var item = db.Reports.FirstOrDefault(element => element.Id == id);
            if (item != null)
            {
                db.Reports.Remove(item);
                db.SaveChanges();
            }
        }
    }
}
