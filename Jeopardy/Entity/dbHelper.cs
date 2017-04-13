using Jeopardy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Jeopardy.Entity
{
    public class dbHelper
    {
        public static List<Game> getGame(int Id)
        {
            using (var db = new DbJeopardyContext())
            {
                return db.Database.SqlQuery<Game>($"CALL Jeopardy.jpGetGame({Id})").ToList();
                /*
                CREATE DEFINER = 'max'@'%'
                PROCEDURE Jeopardy.jpGetGame(IN id INT)
                BEGIN
                SELECT 
                  c.Name CategoryName,
                  q.Name Question,
                  q.QuestionID QuestionID,
                  q.Value,
                  q.Played,
                  q.Answer 
                  FROM Jeopardy.jeopardy_game g
                  left join Jeopardy.jeopardy_category c on c.Category=g.Category
                  left join Jeopardy.jeopardy_questions q on c.Category=q.Category
                where g.Game=id
                order by c.Category,q.Value asc;
                END
                */
            }
        }

        public static List<Players> getGetPlayers(int Id)
        {
            using (var db = new DbJeopardyContext())
            {
                return db.Database.SqlQuery<Players>($"CALL Jeopardy.jpGetPlayers({Id})").ToList();
                /*
                 CREATE DEFINER = 'max'@'%'
                 PROCEDURE Jeopardy.jpGetPlayers(IN id INT)
                 BEGIN
                   select PlayerName Name, p.PlayerID,
                   sum(ifnull(pl.value,0)) Points
                   from Jeopardy.jeopardy_game_by_player p
                   left join Jeopardy.jeopardy_pointsLog pl on p.game=pl.game and p.playerID=pl.PlayerID
                 where p.game=id
                 group by PlayerName,p.PlayerID;
                 END
                 */
            }
        }        
    }
}