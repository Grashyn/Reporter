﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Reporter.Controllers
{
    public class CommentsController : ApiController
    {
        private ReporterDBEntities db = new ReporterDBEntities();

        // GET api/values/5
        public IEnumerable<Comment> Get(Guid reportId)
        {
            return db.Comments.Where(item => item.ReportId == reportId);
        }

        // POST api/values/5
        public async Task<IHttpActionResult> Post([FromUri]Guid reportId, [FromBody]Comment value)
        {
            var report = db.Reports.FirstOrDefault(item => item.Id == reportId);
            if (report != null)
            {
                report.Comments.Add(value);
                await db.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        // PUT api/values/
        public async Task<IHttpActionResult> Put([FromBody]Comment value)
        {
            if (value != null)
            {
                var containsItem = db.Comments.FirstOrDefault(item => item.Id == value.Id);
                if (containsItem != null)
                {
                    db.Comments.Remove(containsItem);
                }

                db.Comments.Add(value);
                await db.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/values/5
        public void Delete(Guid id)
        {
            var item = db.Comments.FirstOrDefault(element => element.Id == id);
            if (item != null)
            {
                db.Comments.Remove(item);
                db.SaveChanges();
            }
        }
    }
}