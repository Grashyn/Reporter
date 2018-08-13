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

        // GET api/values
        [Authorize]
        public IEnumerable<Report> Get()
        {
            return db.Reports;
        }

        // GET api/values/5
        [Authorize]
        public Report Get(Guid id)
        {
            return db.Reports.FirstOrDefault(item => item.Id == id);
        }

        // POST api/values
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

        // PUT api/values/5
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

        // DELETE api/values/5
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
