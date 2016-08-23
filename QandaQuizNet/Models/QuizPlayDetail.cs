using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QandaQuizNet.Models
{
    public class QuizPlayDetail
    {
        [Key]
        public int Id { get; set; }
        public virtual ApplicationUser user { get; set; }
        public virtual QuizDetail quiz { get; set; }
        public virtual QuizAnswer quizAnswer { get; set; }
        public DateTime quizSubmittedDateTime { get; set; }
        public decimal quizPlayAmount { get; set; }
        public bool IsThisFreePlay { get; set; }

    }
}