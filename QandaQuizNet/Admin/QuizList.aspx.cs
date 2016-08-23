using QandaQuizNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data;
using Microsoft.AspNet.FriendlyUrls;

namespace QandaQuizNet
{
    public partial class QuizList : Page
    {
        ApplicationUserManager manager;
        ApplicationUser user;
        ApplicationDbContext dbContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            dbContext = Context.GetOwinContext().Get<ApplicationDbContext>();
            manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            user = manager.FindById(User.Identity.GetUserId());

            if (!Page.IsPostBack)
            {

                gvQuizList.DataSource = dbContext.quizDetails.Select(x => new
                                                                            {
                                                                                Id = x.Id,
                                                                                quizTitle = x.quizTitle,
                                                                                quizAvtiveDateTime = x.quizAvtiveDateTime,
                                                                                prize = x.quizPrizeMoney,
                                                                                responses = dbContext.quizPlayDetails.Where(p => p.quiz.Id == x.Id).Count(),
                                                                                winner = !String.IsNullOrEmpty(x.quizWinnerId) ? x.quizWinner.FirstName + " " + x.quizWinner.LastName : ""
                                                                            }).ToList();

                gvQuizList.DataBind();
            }
        }

        protected void gvQuizList_Command(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string url = FriendlyUrl.Href("~/Admin/AddQuiz", "Details", e.CommandArgument.ToString());
                Response.Redirect(url);
            }
        }

        protected void gvQuizList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dataRow = e.Row.DataItem as DataRowView;

                //check if the quiz is active or inactive
                HiddenField hdnQuizID = (HiddenField)e.Row.FindControl("hdnQuizID");
                int quizID = Convert.ToInt32(hdnQuizID.Value);

                var quizDetails = dbContext.quizDetails.Where(q => q.Id == quizID).First();

                if (quizDetails.quizLapsed)
                {
                    Button btnEdit = (Button)e.Row.FindControl("rsbEdit");
                    //btnEdit.Attributes.Add("disabled","disabled");
                    e.Row.CssClass = "bg-warning";
                    //btnEdit.CssClass = "disabled";
                    btnEdit.Visible = false;
                    //btnEdit.Enabled = false;
                }
            }
        }


    }
}