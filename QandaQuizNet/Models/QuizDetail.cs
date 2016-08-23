using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QandaQuizNet.Models
{
   
    public class QuizDetail
    {
        [Key]
        public int Id { get; set; }
        public string quizTitle { get; set; }
        public string quizDescription { get; set; }
        public DateTime quizAvtiveDateTime { get; set; }
        public decimal quizPrizeMoney { get; set; }
        public int quizTimesNumberOfEntriesAllowed { get; set; }
        public int quizWinnerNumber { get; set; }
        public string quizWinnerId { get; set; }
        public bool quizLapsed { get; set; }
        public bool quizCurrentlyActive { get; set; }
        public virtual ApplicationUser quizWinner { get; set; }

        //[ForeignKey("quizQuestion")]
        //public int quizQuestionId { get; set; }
        public virtual QuizQuestion quizQuestion { get; set; }

        //keep history of quiz like who added it and when
        //TODO add another class to keep track of the lifetime of quiz

    }
}