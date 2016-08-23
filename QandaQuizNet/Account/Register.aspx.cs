using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using QandaQuizNet.Models;

namespace QandaQuizNet.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //fetch the country list
            //select country list to bind on screen 
            if (!Page.IsPostBack)
            { 
                var dbContext = Context.GetOwinContext().Get<ApplicationDbContext>();
                var countryList = dbContext.userCountries.Select(x => new { Name = x.country, Id = x.Id });
                ddlCountryList.DataTextField = "Name";
                ddlCountryList.DataValueField = "Id";

                var ddlList = countryList.ToList();
                ddlList.Insert(0, new { Name = "Select Country", Id = 0 });

                ddlCountryList.DataSource = ddlList;
                ddlCountryList.DataBind();
            }
        }
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            //get the country dependent on selected option
            var dbContext = Context.GetOwinContext().Get<ApplicationDbContext>();
            int selectedIndex = Convert.ToInt32(ddlCountryList.SelectedValue);
            var selectedCountry = dbContext.userCountries.Where(c => c.Id == selectedIndex).FirstOrDefault<userCountry>();


            var referralUserEmail = referralEmail.Text ?? string.Empty;

            var initialBalance = 1 * QandaQuizNet.Settings.PlayConfiguration.NumberOfAllowedFreeGamesPerUser;

            var user = new ApplicationUser() {
                                                FirstName = firstName.Text, 
                                                LastName = lastName.Text, 
                                                Over16AgeConfirm = Over16AgeConfirm.Checked,
                                                TownCity = townCity.Text,
                                                country = selectedCountry,
                                                accountBalance = initialBalance,
                                                UserName = Email.Text, 
                                                Email = Email.Text,
                                                RegisteredDateTime = DateTime.Now,
                                                QandaReferralUserEmailAddress = referralUserEmail
                                            };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);

                try
                {
                    manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");
                }
                catch { }

                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                Response.Redirect("~/Account/TopupAccount/RegisterConfirmPending");
            }
            else 
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }
}