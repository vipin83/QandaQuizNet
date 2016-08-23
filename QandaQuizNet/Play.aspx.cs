using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using QandaQuizNet.Models;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;

using QandaQuizNet.Utilities;
using QandaQuizNet.Settings;


namespace QandaQuizNet
{
    public partial class Play : System.Web.UI.Page
    {
        ApplicationUserManager manager;
        ApplicationUser user;
        ApplicationDbContext dbContext;
        string counrtyCode;

        QuizDetail currentActiveQuizDetails;

        public DateTime NextQuizActiveDateTime
        {
            get
            {
                return (DateTime)ViewState["NextQuizActiveDateTime"];
            }
            set
            {
                ViewState["NextQuizActiveDateTime"] = value;
            }

        }

        protected int currentActiveQuizDetailsID
        {
            get
            {
                return (int)ViewState["currentActiveQuizDetailsID"];
            }
            set
            {
                ViewState["currentActiveQuizDetailsID"] = value;
            }
        }

        public Int64 NextQuizTimeInSecs
        {
            get
            {
                return (Int64)ViewState["NextQuizTimeInSecs"];
            }
            set
            {
                ViewState["NextQuizTimeInSecs"] = value;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dbContext = Context.GetOwinContext().Get<ApplicationDbContext>();
            manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            user = manager.FindById(User.Identity.GetUserId());

            var segments = Request.GetFriendlyUrlSegments();

            if (segments.Count() > 0)
            {
                FreeCorrectAnswer.Visible = segments[0] == "FreeCorrectAnswer";
                CorrectAnswer.Visible = segments[0] == "CorrectAnswer";
                WrongAnswer.Visible = segments[0] == "WrongAnswer";
            }

            ShowPoundDollarLogo();

            if (!Page.IsPostBack)
            {
                //fetch quiz details 
                currentActiveQuizDetails = dbContext.quizDetails
                                                    .Where(q => q.quizCurrentlyActive) //get the quiz which has currentlyActive flag set to true
                                                    .OrderByDescending(x => x.quizAvtiveDateTime)//get the latest active, non-lapsed quiz details
                                                    .FirstOrDefault();

                if (currentActiveQuizDetails != null)
                {

                    currentActiveQuizDetailsID = currentActiveQuizDetails.Id;


                    numOfEntriedAllowedForQuiz.InnerText = currentActiveQuizDetails.quizTimesNumberOfEntriesAllowed.ToString();

                    var quizQuestionDetails = currentActiveQuizDetails.quizQuestion;

                    lblQuestionText.Text = quizQuestionDetails.quizQuestionText;
                    imagePlaceholder.Visible = quizQuestionDetails.quizQuestionHasImage || quizQuestionDetails.quizQuestionHasVideo;

                    //TODO - Video plugin

                    if (quizQuestionDetails.quizQuestionHasImage)
                        imageQuestion.HRef = "uploads/" + quizQuestionDetails.quizQuestionImagePath;

                    var quizAnswerDetails = quizQuestionDetails.quizAnswers.ToList();

                    //populate answer radio buttons
                    answerToQuiz_1.Value = quizAnswerDetails[0].Id.ToString();
                    answerText1.InnerText = quizAnswerDetails[0].quizAnswerText;

                    answerToQuiz_2.Value = quizAnswerDetails[1].Id.ToString();
                    answerText2.InnerText = quizAnswerDetails[1].quizAnswerText;

                    answerToQuiz_3.Value = quizAnswerDetails[2].Id.ToString();
                    answerText3.InnerText = quizAnswerDetails[2].quizAnswerText;

                    answerToQuiz_4.Value = quizAnswerDetails[3].Id.ToString();
                    answerText4.InnerText = quizAnswerDetails[3].quizAnswerText;

                    //get datetime for next quiz so countdown timer counts to it for current question
                    var nextToBeActiveQuiz = dbContext.quizDetails
                                                      .Where(qd => !qd.quizLapsed && qd.quizAvtiveDateTime > currentActiveQuizDetails.quizAvtiveDateTime)
                                                      .OrderBy(o => o.quizAvtiveDateTime)
                                                      .FirstOrDefault();

                    if (nextToBeActiveQuiz != null)
                    {
                        NextQuizActiveDateTime = nextToBeActiveQuiz.quizAvtiveDateTime;
                    }
                    else
                    {
                        NextQuizActiveDateTime = DateTime.Now.AddHours(24); //if there is no new quiz setup then fake the expiry time to be 24 hours from now.
                    }

                    NextQuizTimeInSecs = Convert.ToInt64((NextQuizActiveDateTime - DateTime.Now).TotalSeconds);

                    currentQuizPrizeMoney.InnerHtml = ((!User.Identity.IsAuthenticated) ? "Register Now - ":"") + PoundOrDollarSign() + "1 Play, <br/> to win " + PoundOrDollarSign() + currentActiveQuizDetails.quizPrizeMoney.ToString("##,#.##") + " or more";
                
                }

                //Get past winners list
                GetWinnersListForScroll();
                GetUpcomingQuizListForScroll();
            }

        }

        private string PoundOrDollarSign()
        {
            return (counrtyCode == "CA" ? "$" : "£");
        }

        private void ShowPoundDollarLogo()
        {
            counrtyCode = "GB";
            if (!User.Identity.IsAuthenticated)
            {
                counrtyCode = GeoLocationDetails.GetCountryCodeFromClientIP();
                ShowBannerBasedOnCountryCode(counrtyCode);
            }
            else
            {
                //show logo based on user choosen country flag
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = manager.FindById(User.Identity.GetUserId());
                ShowBannerBasedOnCountryCode(user.country.countryCode);

            }
        }

        private void ShowBannerBasedOnCountryCode(string Code)
        {
            imgLogo.Src = (Code == "CA") ? "~/images/Qanda Logo-Dollar.jpg" : "~/images/Qanda Logo-Pound.jpg";
        }

        private void GetUpcomingQuizListForScroll()
        {
            var tenUpComingQuizList = dbContext.quizDetails
                                       .Where(q => !q.quizLapsed && q.quizAvtiveDateTime > DateTime.Now)
                                       .OrderBy(o => o.quizAvtiveDateTime)
                                       .Take(10)
                                       .ToList();

            var scollList = new StringBuilder();
            foreach (var quiz in tenUpComingQuizList)
            {
                scollList.Append(quiz.quizTitle + " ( Win " + PoundOrDollarSign() + quiz.quizPrizeMoney.ToString("#,##.##") + ")" + "<br/><br/>");
                //scollList.Append(quiz.quizTitle + " ( Win " + PoundOrDollarSign() + quiz.quizPrizeMoney.ToString("#,##.##") + ")" + "<br/><br/>");
            }
            var finalQuizList = scollList.ToString();
            TenQuizScrollList.InnerHtml = finalQuizList;
        }

        private void GetWinnersListForScroll()
        {
            var tenQuizList = dbContext.quizDetails
                                        .Where(q => q.quizLapsed && q.quizWinnerId != null)
                                        .OrderByDescending(o => o.quizAvtiveDateTime)
                                        .Take(10)
                                        .ToList();

            if (tenQuizList.Count == 10) //after we have 10 genuine winners in the system, show them instead of fake names
            {
                var winnerList = new StringBuilder();
                foreach (var quiz in tenQuizList)
                {
                    if (quiz.quizWinner != null)
                    {
                        winnerList.Append(quiz.quizWinner.FirstName + ", " + quiz.quizWinner.TownCity + "<br/><br/>");
                        //winnerList.Append(quiz.quizWinner.FirstName + " " + quiz.quizWinner.LastName + "," + quiz.quizWinner.TownCity + "<br/><br/>");
                    }
                }

                var finalWinnerList = winnerList.ToString();
                TenWinnerScrollList.InnerHtml = finalWinnerList;
            }

        }

        private bool EnoughBalanceToPlay()
        {
            //Each quiz is a £1 or CAD1, so check user has sufficient balance to play
            return (user.accountBalance >= 1.0M);
        }

        protected void Submit_Click(object sender, CommandEventArgs e)
        {
            bool IsFreePlaySelectedByUser = e.CommandArgument.ToString().Equals("PlayForFree", StringComparison.InvariantCultureIgnoreCase);

            bool IsPromotionalFreePlay = true;

            // as this page is shown by default to anyone, check if user is logged in before accepting answer

            //if not logged in - redirect to login page
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/Account/Login");

            //only check for balance if user has played all allowed free games
            if (!IsFreePlaySelectedByUser && (user.NumberOfFreeGamesPlayed >= PlayConfiguration.NumberOfAllowedFreeGamesPerUser || user.UsedAllFreeGames))
            {
                IsPromotionalFreePlay = false;
                //if logged in then check balance, if not enough balance to play - redirect to top-up page
                if (!EnoughBalanceToPlay())
                    Response.Redirect("~/Account/TopupAccount");
            }           

            //all ok - proceed and accept user's answer

            int intAnswerID = 0;
            if (!String.IsNullOrEmpty(Request["ctl00$MainContent$answerToQuiz"]))
                intAnswerID = Convert.ToInt32(Request["ctl00$MainContent$answerToQuiz"]);
            else
            {
                //no answer selected ..
                noAnswerSelected.Attributes.Add("class", "visible");
                return;
            }


            //store quiz answer details         
            var quizAnswer = dbContext.quizAnswers.Where(ans => ans.Id == intAnswerID).FirstOrDefault();
            var quizDetail = dbContext.quizDetails.Where(det => det.Id == currentActiveQuizDetailsID).FirstOrDefault();

            var playQuiz = new QuizPlayDetail
            {
                quiz = quizDetail,
                quizAnswer = quizAnswer,
                user = user,
                quizPlayAmount = IsPromotionalFreePlay || IsFreePlaySelectedByUser ? 0.0M : 1.0M, //if free play then put 0 in play amount, so it does not get deduct from user balance
                quizSubmittedDateTime = DateTime.Now,
                IsThisFreePlay = IsFreePlaySelectedByUser
            };

            dbContext.quizPlayDetails.Add(playQuiz);


            if (IsPromotionalFreePlay && !IsFreePlaySelectedByUser)
            {
                user.NumberOfFreeGamesPlayed += 1; //increment free play count
                user.UsedAllFreeGames = user.NumberOfFreeGamesPlayed == PlayConfiguration.NumberOfAllowedFreeGamesPerUser;
            }

            user.accountBalance = !IsFreePlaySelectedByUser ? (user.accountBalance - 1) : user.accountBalance; //reduce the account balance of player by 1

            manager.Update(user);

            dbContext.SaveChanges();

            //check if quiz has reached X number of response, if it has then set quiz to expire
            var totalResponsesToQuizThusFar = dbContext.quizPlayDetails.Where(x => x.quiz.Id == currentActiveQuizDetailsID).Count();

            if (totalResponsesToQuizThusFar == quizDetail.quizTimesNumberOfEntriesAllowed)
            {
                var lapsedQuizActiveDateTime = quizDetail.quizAvtiveDateTime;

                //find the quiz which has its active date time set to be later than just lapsed quiz 
                var newActiveQuizDetails = dbContext.quizDetails
                                                     .Where(qd => !qd.quizLapsed && qd.quizAvtiveDateTime > lapsedQuizActiveDateTime)
                                                     .OrderBy(o => o.quizAvtiveDateTime)
                                                     .FirstOrDefault();

                if (newActiveQuizDetails != null)
                {
                    newActiveQuizDetails.quizCurrentlyActive = true;

                    //expire current quiz only if there is any other quiz which can be made active
                    quizDetail.quizLapsed = true;
                    quizDetail.quizCurrentlyActive = false; //this quiz has received 'X' number of responses. set the next quiz active.
                }

                dbContext.SaveChanges();
            }

            string url = FriendlyUrl.Href("~/", "Play", (IsFreePlaySelectedByUser ? "FreeCorrectAnswer" : quizAnswer.quizAnswerCorrect ? "CorrectAnswer" : "WrongAnswer"));
            Response.Redirect(url);

        }
    }
}