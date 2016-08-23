using Microsoft.AspNet.Identity.Owin;
using QandaQuizNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.FriendlyUrls;

namespace QandaQuizNet
{
    public partial class AddQuiz : Page
    {
        int quizID = 0;
        ApplicationUserManager manager;
        ApplicationDbContext dbContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            dbContext = Context.GetOwinContext().Get<ApplicationDbContext>();
            manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            //check if user is authenticated,if not then redirect to Login page
            //TODO - admin login check,to make sure it's admin who's adding the quiz
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/Account/Login");

            //check if there is any id attached to url - if it is then open this page in edit mode
            var segments = Request.GetFriendlyUrlSegments();

            if (segments.Count() > 0)
            {
                quizID = Convert.ToInt32(segments[1]);               
            }

            if (!Page.IsPostBack)
            {

                setupForm();

                txtquizQuestionNumberOfAnswers.DataSource = Enumerable.Range(1, 4).ToList();
                txtquizQuestionNumberOfAnswers.DataBind();
            }

            txtquizQuestionNumberOfAnswers.SelectedValue = "4";
            txtquizQuestionNumberOfAnswers.Enabled = false;
        }

        private void setupForm()
        {

            lblQuizHeading.Text = quizID > 0 ? "Edit Quiz" : "Create New Quiz";
            //get the details about the quiz 
            var quizDetails = dbContext.quizDetails.Where(q => q.Id == quizID).FirstOrDefault();

            if (quizDetails != null)
            {
                //Quiz Details
                txtquizTitle.Text = quizDetails.quizTitle;
                txtquizDescription.Text = quizDetails.quizDescription;
                txtquizAvtiveDateTime.Value = quizDetails.quizAvtiveDateTime.ToString("dd/MM/yyyy HH:mm");

                txtquizPrizeMoney.Text = quizDetails.quizPrizeMoney.ToString("#0.#0");
                txtquizTimesNumberOfEntriesAllowed.Text = quizDetails.quizTimesNumberOfEntriesAllowed.ToString();
                txtquizWinnerNumber.Text = quizDetails.quizWinnerNumber.ToString();

                //Quiz question
                txtquizQuestionText.Text = quizDetails.quizQuestion.quizQuestionText;
                cbImageUpload.Checked = quizDetails.quizQuestion.quizQuestionHasImage;
                cbVideoUpload.Checked = quizDetails.quizQuestion.quizQuestionHasVideo;

                var answerList = quizDetails.quizQuestion.quizAnswers.ToList();

                txtquizAnswerText1.Text = answerList[0].quizAnswerText;
                chkCorrectAnswer1.Checked = answerList[0].quizAnswerCorrect;

                txtquizAnswerText2.Text = answerList[1].quizAnswerText;
                chkCorrectAnswer2.Checked = answerList[1].quizAnswerCorrect;

                txtquizAnswerText3.Text = answerList[2].quizAnswerText;
                chkCorrectAnswer3.Checked = answerList[2].quizAnswerCorrect;

                txtquizAnswerText4.Text = answerList[3].quizAnswerText;
                chkCorrectAnswer4.Checked = answerList[3].quizAnswerCorrect;

            }
        }
        
        protected void AddQuiz_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                //check atleast one checkbox for correct answer is ticked
                if (!chkCorrectAnswer1.Checked && !chkCorrectAnswer2.Checked && !chkCorrectAnswer3.Checked && !chkCorrectAnswer4.Checked)
                {
                    ErrorMessage.Text = "Please select correct answer for the question.";
                    return;
                }

                if (quizID == 0)
                {
                    //create a quiz details row
                    var newQuizDetails = new QuizDetail();
                    newQuizDetails.quizTitle = txtquizTitle.Text;
                    newQuizDetails.quizDescription = txtquizDescription.Text;
                    newQuizDetails.quizAvtiveDateTime = DateTime.ParseExact(txtquizAvtiveDateTime.Value, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                    decimal prizeMoney;
                    if (Decimal.TryParse(txtquizPrizeMoney.Text, out prizeMoney))
                        newQuizDetails.quizPrizeMoney = prizeMoney;
                    else
                        newQuizDetails.quizPrizeMoney = 0.0M;

                    newQuizDetails.quizTimesNumberOfEntriesAllowed = Convert.ToInt32(txtquizTimesNumberOfEntriesAllowed.Text);
                    newQuizDetails.quizWinnerNumber = Convert.ToInt32(txtquizWinnerNumber.Text);

                    var newQuizQuestion = new QuizQuestion();
                    newQuizQuestion.quizQuestionText = txtquizQuestionText.Text;
                    newQuizQuestion.quizQuestionHasImage = cbImageUpload.Checked;
                    newQuizQuestion.quizQuestionHasVideo = cbVideoUpload.Checked;

                    //TODO save image or video to local path and then save path into object
                    HttpPostedFile uploadededFile = Request.Files["uploadFileName"];
                    if (uploadededFile != null && uploadededFile.ContentLength > 0)
                    {
                        string fname = Path.GetFileNameWithoutExtension(uploadededFile.FileName) + DateTime.Now.ToString("_yyyMMdd_HHmmss") + Path.GetExtension(uploadededFile.FileName);
                        uploadededFile.SaveAs(Server.MapPath(Path.Combine("~/Uploads/", fname)));

                        newQuizQuestion.quizQuestionImagePath = cbImageUpload.Checked ? fname : null;
                        newQuizQuestion.quizQuestionVideoPath = cbVideoUpload.Checked ? fname : null;
                    }

                    newQuizQuestion.quizQuestionNumberOfAnswers = Convert.ToInt16(txtquizQuestionNumberOfAnswers.SelectedValue);
                    newQuizQuestion.quizAnswers = new List<QuizAnswer>();

                    var newQuizAnswer1 = new QuizAnswer();
                    newQuizAnswer1.quizAnswerText = txtquizAnswerText1.Text;
                    newQuizAnswer1.quizAnswerCorrect = chkCorrectAnswer1.Checked;

                    newQuizQuestion.quizAnswers.Add(newQuizAnswer1);

                    var newQuizAnswer2 = new QuizAnswer();
                    newQuizAnswer2.quizAnswerText = txtquizAnswerText2.Text;
                    newQuizAnswer2.quizAnswerCorrect = chkCorrectAnswer2.Checked;

                    newQuizQuestion.quizAnswers.Add(newQuizAnswer2);

                    var newQuizAnswer3 = new QuizAnswer();
                    newQuizAnswer3.quizAnswerText = txtquizAnswerText3.Text;
                    newQuizAnswer3.quizAnswerCorrect = chkCorrectAnswer3.Checked;

                    newQuizQuestion.quizAnswers.Add(newQuizAnswer3);

                    var newQuizAnswer4 = new QuizAnswer();
                    newQuizAnswer4.quizAnswerText = txtquizAnswerText4.Text;
                    newQuizAnswer4.quizAnswerCorrect = chkCorrectAnswer4.Checked;

                    newQuizQuestion.quizAnswers.Add(newQuizAnswer4);

                    newQuizDetails.quizQuestion = newQuizQuestion;

                    dbContext.quizDetails.Add(newQuizDetails);
                }
                else
                {
                    //save existing quiz 

                    var existingQuizDetails = dbContext.quizDetails.Where(q => q.Id == quizID).FirstOrDefault();

                    if (existingQuizDetails != null)
                    {
                        existingQuizDetails.quizTitle = txtquizTitle.Text;
                        existingQuizDetails.quizDescription = txtquizDescription.Text;
                        existingQuizDetails.quizAvtiveDateTime = DateTime.ParseExact(txtquizAvtiveDateTime.Value, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);

                        decimal prizeMoney;
                        if (Decimal.TryParse(txtquizPrizeMoney.Text, out prizeMoney))
                            existingQuizDetails.quizPrizeMoney = prizeMoney;
                        else
                            existingQuizDetails.quizPrizeMoney = 0.0M;

                        existingQuizDetails.quizTimesNumberOfEntriesAllowed = Convert.ToInt32(txtquizTimesNumberOfEntriesAllowed.Text);
                        existingQuizDetails.quizWinnerNumber = Convert.ToInt32(txtquizWinnerNumber.Text);

                        var newQuizQuestion = existingQuizDetails.quizQuestion;
                        newQuizQuestion.quizQuestionText = txtquizQuestionText.Text;
                        newQuizQuestion.quizQuestionHasImage = cbImageUpload.Checked;
                        newQuizQuestion.quizQuestionHasVideo = cbVideoUpload.Checked;

                        //TODO save image or video to local path and then save path into object
                        HttpPostedFile uploadededFile = Request.Files["uploadFileName"];
                        if (uploadededFile != null && uploadededFile.ContentLength > 0)
                        {
                            string fname = Path.GetFileNameWithoutExtension(uploadededFile.FileName) + DateTime.Now.ToString("_yyyMMdd_HHmmss") + Path.GetExtension(uploadededFile.FileName);
                            uploadededFile.SaveAs(Server.MapPath(Path.Combine("~/Uploads/", fname)));

                            newQuizQuestion.quizQuestionImagePath = cbImageUpload.Checked ? fname : null;
                            newQuizQuestion.quizQuestionVideoPath = cbVideoUpload.Checked ? fname : null;
                        }

                        newQuizQuestion.quizQuestionNumberOfAnswers = Convert.ToInt16(txtquizQuestionNumberOfAnswers.SelectedValue);
                        var quizAnswers = newQuizQuestion.quizAnswers.ToList();

                        var newQuizAnswer1 = quizAnswers[0];
                        newQuizAnswer1.quizAnswerText = txtquizAnswerText1.Text;
                        newQuizAnswer1.quizAnswerCorrect = chkCorrectAnswer1.Checked;

                        dbContext.Entry(newQuizAnswer1).State = System.Data.Entity.EntityState.Modified;



                        var newQuizAnswer2 = quizAnswers[1];
                        newQuizAnswer2.quizAnswerText = txtquizAnswerText2.Text;
                        newQuizAnswer2.quizAnswerCorrect = chkCorrectAnswer2.Checked;

                        dbContext.Entry(newQuizAnswer2).State = System.Data.Entity.EntityState.Modified;

                        var newQuizAnswer3 = quizAnswers[2];
                        newQuizAnswer3.quizAnswerText = txtquizAnswerText3.Text;
                        newQuizAnswer3.quizAnswerCorrect = chkCorrectAnswer3.Checked;

                        dbContext.Entry(newQuizAnswer3).State = System.Data.Entity.EntityState.Modified;

                        var newQuizAnswer4 = quizAnswers[3];
                        newQuizAnswer4.quizAnswerText = txtquizAnswerText4.Text;
                        newQuizAnswer4.quizAnswerCorrect = chkCorrectAnswer4.Checked;

                        dbContext.Entry(newQuizAnswer4).State = System.Data.Entity.EntityState.Modified;

                        dbContext.Entry(newQuizQuestion).State = System.Data.Entity.EntityState.Modified;
                        dbContext.Entry(existingQuizDetails).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();

                Response.Redirect("~/Admin/QuizList");
            }
            else
            {
                ErrorMessage.Text = "There were issues with adding quiz details";
            }
        }

        

    }
}