using Jeopardy.Entity;
using Jeopardy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Jeopardy.Controllers
{
    public class ResultsController : Controller
    {
        public ActionResult Index(int id, string all = "")
        {
            using (DbJeopardyContext db = new DbJeopardyContext())
            {
                var cq = db.Database.SqlQuery<Players>($@"select {(all == "" ? "p.Game," : "")}PlayerName Name, p.PlayerID,
                                                          sum(ifnull(pl.value,0)) Points, p.Winner
                                                          from Jeopardy.jeopardy_game_by_player p
                                                          left join Jeopardy.jeopardy_pointsLog pl on p.game=pl.game and p.playerID=pl.PlayerID
                                                        where p.round={id}
                                                        group by {(all == "" ? "p.Game," : "")}PlayerName,p.PlayerID, p.Winner
                                                        order by {(all == "" ? "p.Game asc," : "")}Points desc").ToList();
                ViewBag.Results = cq;
                ViewBag.Round = id;
                return View();
            }
        }
    }

    public class FinalJeopardyController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            try
            {
                var cq = dbHelper.getGame(100);
                var pp = dbHelper.getGetPlayers(id);
                var gg = new List<gg>();
                return Ok(new
                {
                    CQ = cq,
                    PP = pp
                });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
