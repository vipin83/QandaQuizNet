using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QandaQuizNet.Models
{
    public class QuizAnswer
    {
        [Key]
        public int Id { get; set; }
        public string quizAnswerText { get; set; }
        public bool quizAnswerCorrect { get; set; }

        [ForeignKey("quizQuestion")]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int quizQuestionId { get; set; }
        
        public virtual QuizQuestion quizQuestion { get; set; }

    }
}