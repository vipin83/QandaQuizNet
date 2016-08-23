using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ef = QandaQuizEntityFramework;

namespace QandaQuizNet.Utilities
{
    public static class Housekeeping
    {

        public static ef.QandaQuizContext dbContext = new ef.QandaQuizContext();



        public static void runMundaneTasks()
        {
            //run this peice of code in a timely fashion

            dbContext.LogEntries.Add(new ef.LogEntry
            {
                LogEntryDateTime = DateTime.Now,
                LogEntryDescription = "Housekeeping - Start"
            });

            dbContext.SaveChanges();

            var currentActiveQuizDetails = dbContext.quizDetails
                                                    .Where(qd => !qd.quizLapsed && qd.quizCurrentlyActive)
                                                    .FirstOrDefault();


            //1. check if active date time of current quiz is past the next quiz active date time. if so then lapse first quiz and make next quiz state as active
            if (currentActiveQuizDetails != null)
            {

                dbContext.LogEntries.Add(new ef.LogEntry
               {
                   LogEntryDateTime = DateTime.Now,
                   LogEntryDescription = "Current active quiz details - " + currentActiveQuizDetails.Id
               });

                dbContext.SaveChanges();

                ef.QuizDetail nextToBeActiveQuizDetails = dbContext.quizDetails
                                                           .Where(qd => !qd.quizLapsed && qd.quizAvtiveDateTime > currentActiveQuizDetails.quizAvtiveDateTime)
                                                           .OrderBy(o => o.quizAvtiveDateTime)
                                                           .FirstOrDefault();


                if (nextToBeActiveQuizDetails != null) //expire current quiz only if there is any other quiz in the system which can be made active
                {

                    dbContext.LogEntries.Add(new ef.LogEntry
                    {
                        LogEntryDateTime = DateTime.Now,
                        LogEntryDescription = "Next to-be active quiz details - " + nextToBeActiveQuizDetails.Id + " - " + nextToBeActiveQuizDetails.quizAvtiveDateTime
                    });

                    dbContext.SaveChanges();

                    //check if current time is past the next ToBe active quiz datetime
                    if (DateTime.Now >= nextToBeActiveQuizDetails.quizAvtiveDateTime)
                    {

                        dbContext.LogEntries.Add(new ef.LogEntry
                        {
                            LogEntryDateTime = DateTime.Now,
                            LogEntryDescription = "Expiring current quiz"
                        });

                        dbContext.SaveChanges();

                        nextToBeActiveQuizDetails.quizCurrentlyActive = true;

                        currentActiveQuizDetails.quizLapsed = true;
                        currentActiveQuizDetails.quizCurrentlyActive = false;

                        dbContext.SaveChanges();
                    }
                }
            }

            //2. check if there is any lapsed quiz which does not have winner
            var lapsedQuizNoWinner = dbContext.quizDetails
                                              .Where(qd => qd.quizLapsed && String.IsNullOrEmpty(qd.quizWinnerId))
                                              .OrderBy(o => o.quizAvtiveDateTime)
                                              .ToList();

            foreach (var lapsedQuiz in lapsedQuizNoWinner)
            {

                var correctEntriesForQuiz = dbContext.quizPlayDetails
                                                     .Where(p => p.QuizDetail.Id == lapsedQuiz.Id &&
                                                                 p.QuizAnswer.Id == p.QuizDetail.QuizQuestion.QuizAnswers.Where(q => q.quizAnswerCorrect).FirstOrDefault().Id
                                                           );

                var correctEntriesCountForQuiz = correctEntriesForQuiz.Count();

                //(announce winner only if it has receievd sufficient 'correct' responses? )
                if (correctEntriesCountForQuiz >= lapsedQuiz.quizTimesNumberOfEntriesAllowed)
                {
                    //3. announce winner
                    var quizWinnerNumber = lapsedQuiz.quizWinnerNumber;

                    //sort quiz answer entries in the order they were answered
                    var quizWinnerEntry = correctEntriesForQuiz.OrderBy(o => o.quizSubmittedDateTime)
                                                               .Skip(quizWinnerNumber - 1)
                                                               .FirstOrDefault();

                    //update lapsed quiz with the winner number
                    lapsedQuiz.quizWinnerId = quizWinnerEntry.user_Id;
                    dbContext.SaveChanges();

                    //4. Send email to winner + site owner
                    var emailMgr = new clsEmail();

                    try
                    {
                        emailMgr.SendEmailUsingSMTP(quizWinnerEntry.AspNetUser.Email,
                            "admin@QandAQuiz.com",
                            "Congratulations! you are the winner of QandA Quiz!!",
                            "You have won the quiz with prize money of " + lapsedQuiz.quizPrizeMoney.ToString("C"));

                        emailMgr.SendEmailUsingSMTP("admin@QandAQuiz.com",
                            "admin@QandAQuiz.com",
                            "Quiz # " + lapsedQuiz.Id + "(" + lapsedQuiz.quizTitle + ") winner announced",
                            quizWinnerEntry.AspNetUser.Email + " has been declared winner of quiz# " + lapsedQuiz.Id + "(" + lapsedQuiz.quizTitle + ")");
                    }
                    catch
                    { }
                }

            }

            dbContext.LogEntries.Add(new ef.LogEntry
            {
                LogEntryDateTime = DateTime.Now,
                LogEntryDescription = "Housekeeping - End"
            });

            dbContext.SaveChanges();
        }


    }
}