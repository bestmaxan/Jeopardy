using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Jeopardy.Models
{
    public class Game
    {
        public string CategoryName { get; set; }
        public string Question { get; set; }
        public int QuestionID { get; set; }
        public long Value { get; set; }
        public bool Played { get; set; }
        public string Answer { get; set; }
    }

    public class gg
    {
        public string CategoryName { get; set; }
        public List<Question> Questions { get; set; }
    }

    public class Question
    {
        public string QuestionName { get; set; }
        public int Id { get; set; }
        public long Value { get; set; }
        public bool Played { get; set; }
        public string Answer { get; set; }
    }

    public class Players
    {
        public int Game { get; set; }
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public bool Winner { get; set; }
    }

    public class QuestionViewModel
    {
        [Required]
        public int Qid { get; set; }
    }

    public class PointViewModel
    {
        [Required]
        public int Game { get; set; }
        [Required]
        public int PlayerID { get; set; }
        [Required]
        public int QuestionID { get; set; }
        [Required]
        public int Value { get; set; }
    }

    public class DbJeopardyContext : DbContext
    {
        public DbJeopardyContext() : base("Jeopardy12") { }

    }
}