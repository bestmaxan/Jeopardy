using Jeopardy.Entity;
using Jeopardy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Jeopardy.Controllers
{
    public class HomeController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            try
            {
                var cq = dbHelper.getGame(id);
                var pp = dbHelper.getGetPlayers(id);
                var gg = new List<gg>();

                foreach (var q in cq)
                {
                    if (gg.Any(p => p.CategoryName == q.CategoryName))
                    {
                        var que = new Question()
                        {
                            Id = q.QuestionID,
                            Played = q.Played,
                            QuestionName = q.Question,
                            Value = q.Value,
                            Answer = q.Answer
                        };
                        gg.Where(p => p.CategoryName == q.CategoryName).FirstOrDefault().Questions.Add(que);
                    }
                    else
                    {
                        var categ = new Models.gg()
                        {
                            CategoryName = q.CategoryName,
                            Questions = new List<Question>()
                                {
                                    new Question()
                                    {
                                        Id = q.QuestionID,
                                        Played = q.Played,
                                        QuestionName = q.Question,
                                        Value = q.Value,
                                        Answer = q.Answer
                                    }
                                }
                        };
                        gg.Add(categ);
                    }
                }

                return Ok(new
                {
                    CQ = gg,
                    PP = pp
                });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [ResponseType(typeof(QuestionViewModel))]
        public IHttpActionResult Post(QuestionViewModel Qid)
        {
            try
            {
                using (DbJeopardyContext db = new DbJeopardyContext())
                {
                    db.Database.ExecuteSqlCommand($"UPDATE Jeopardy.jeopardy_questions set Played = true where QuestionID = {Qid.Qid}");
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
