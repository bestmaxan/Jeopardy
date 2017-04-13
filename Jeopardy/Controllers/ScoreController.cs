using Jeopardy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Jeopardy.Controllers
{

    public class ScoreController : ApiController
    {
        [ResponseType(typeof(PointViewModel))]
        public IHttpActionResult Post(PointViewModel model)
        {
            try
            {
                using (DbJeopardyContext db = new DbJeopardyContext())
                {
                    db.Database.ExecuteSqlCommand($@"INSERT INTO  Jeopardy.jeopardy_pointsLog(Game,PlayerID,QuestionID,Value)
                                                    VALUE({model.Game}, {model.PlayerID}, {model.QuestionID}, {model.Value}); ");
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
