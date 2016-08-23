using QandaQuizNet.Models;
using QandaQuizNet.Paypal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.FriendlyUrls;

namespace QandaQuizNet.Account
{
    public partial class TopupAccountPage : System.Web.UI.Page
    {
        public ApplicationDbContext dbContext;
        public ApplicationUserManager manager;
        public ApplicationUser user;

        protected void Page_Load(object sender, EventArgs e)
        {
            //check if user is authenticated,if not then redirect to Login page
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/Account/Login");


            //get db context to create a row in database 
            dbContext = Context.GetOwinContext().Get<ApplicationDbContext>();
            manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            user = manager.FindById(User.Identity.GetUserId());

            var segments = Request.GetFriendlyUrlSegments();

            if (segments.Count() > 0)
            {              
                EmailConfirmPendingDialog.Visible = true;
            }


            if (!Page.IsPostBack)
            {
                ddlTopupAmount.DataSource = Enumerable.Range(0, 9).Select(x => 5 + 5*x).ToList(); //increment in steps of 5
                ddlTopupAmount.DataBind();
            }
        }

        protected void TopupAccount_Click(object sender, EventArgs e)
        {

            string currency = user.country.currencyCode; //GBP or CAD; 
            string token = string.Empty;
            string retMsg = string.Empty;

            NVPAPICaller PPCaller = new NVPAPICaller();

            //before making a call to paypal setExpressCheckout - store the call in our DB with values.
            var topupID = Guid.NewGuid().ToString();
            
            dbContext.topupAccounts.Add(new TopupAccount
            {
                Id = topupID,
                user = user,
                Amount = Convert.ToDecimal(ddlTopupAmount.SelectedValue),
                topupDateTime = DateTime.Now
            });

            dbContext.SaveChanges();

            bool ret = PPCaller.ExpressCheckout("QandA", "QandA quiz topup", ddlTopupAmount.SelectedValue, "1", currency, topupID, ref token, ref retMsg);
            if (ret)
            {
                HttpContext.Current.Session["token"] = token;
                //update topupAccount row with paypal returned token
                var topupAccountRow = dbContext.topupAccounts.Where(ta => ta.Id == topupID).FirstOrDefault<TopupAccount>();

                if (topupAccountRow != null)
                {
                    topupAccountRow.paypalToken = token;
                    dbContext.SaveChanges();
                }
                Response.Redirect(retMsg);
            }
            else
            {
                //PayPal has not responded successfully, let user know
                ErrorMessage.Text = "There was a problem with PayPal payment. Please try again in a few moments, if problem persists please contact us.";
            }
        }
    }
}