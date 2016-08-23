using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QandaQuizNet.Models
{
    public class QuizQuestion
    {
        [Key, ForeignKey("quizDetails")]
        public int Id { get; set; }
        public string quizQuestionText { get; set; }
        public bool quizQuestionHasImage { get; set; }
        public string quizQuestionImagePath { get; set; }
        public bool quizQuestionHasVideo { get; set; }
        public string quizQuestionVideoPath { get; set; }
        public int quizQuestionNumberOfAnswers { get; set; }
        public virtual ICollection<QuizAnswer> quizAnswers { get; set; }

        //[ForeignKey("quizDetails")]
        //public int quizDetailsID { get; set; }
        public virtual QuizDetail quizDetails { get; set; }



    }
}