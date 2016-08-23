using System;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using QandaQuizNet.Models;
using System.Linq;

namespace QandaQuizNet.Account
{
    public partial class RegisterExternalLogin : System.Web.UI.Page
    {
        protected string ProviderName
        {
            get { return (string)ViewState["ProviderName"] ?? String.Empty; }
            private set { ViewState["ProviderName"] = value; }
        }

        protected string ProviderAccountKey
        {
            get { return (string)ViewState["ProviderAccountKey"] ?? String.Empty; }
            private set { ViewState["ProviderAccountKey"] = value; }
        }

        private void RedirectOnFail()
        {
            Response.Redirect((User.Identity.IsAuthenticated) ? "~/Account/Manage" : "~/Account/Login");
        }

        protected void Page_Load()
        {
            // Process the result from an auth provider in the request
            ProviderName = IdentityHelper.GetProviderNameFromRequest(Request);
            if (String.IsNullOrEmpty(ProviderName))
            {
                RedirectOnFail();
                return;
            }
            if (!IsPostBack)
            {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                var loginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo();
                if (loginInfo == null)
                {
                    RedirectOnFail();
                    return;
                }
                var user = manager.Find(loginInfo.Login);
                if (user != null)
                {
                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else if (User.Identity.IsAuthenticated)
                {
                    // Apply Xsrf check when linking
                    var verifiedloginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo(IdentityHelper.XsrfKey, User.Identity.GetUserId());
                    if (verifiedloginInfo == null)
                    {
                        RedirectOnFail();
                        return;
                    }

                    var result = manager.AddLogin(User.Identity.GetUserId(), verifiedloginInfo.Login);
                    if (result.Succeeded)
                    {
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    }
                    else
                    {
                        AddErrors(result);
                        return;
                    }
                }
                else
                {
                    email.Text = loginInfo.Email;

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
        }        
        
        protected void LogIn_Click(object sender, EventArgs e)
        {
            CreateAndLoginUser();
        }

        private void CreateAndLoginUser()
        {
            if (!IsValid)
            {
                return;
            }
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();



            //get the country dependent on selected option
            var dbContext = Context.GetOwinContext().Get<ApplicationDbContext>();
            int selectedIndex = Convert.ToInt32(ddlCountryList.SelectedValue);
            var selectedCountry = dbContext.userCountries.Where(c => c.Id == selectedIndex).FirstOrDefault<userCountry>();
            
            var referralUserEmail = string.Empty;

            var initialBalance = 1 * QandaQuizNet.Settings.PlayConfiguration.NumberOfAllowedFreeGamesPerUser;

            var user = new ApplicationUser()
            {
                //FirstName = firstName.Text,
                //LastName = lastName.Text,
                Over16AgeConfirm = Over16AgeConfirm.Checked,
                TownCity = townCity.Text,
                country = selectedCountry,
                accountBalance = initialBalance,
                UserName = email.Text,
                Email = email.Text,
                RegisteredDateTime = DateTime.Now,
                QandaReferralUserEmailAddress = referralUserEmail
            };
            
                        
            IdentityResult result = manager.Create(user);
            if (result.Succeeded)
            {
                var loginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo();
                if (loginInfo == null)
                {
                    RedirectOnFail();
                    return;
                }
                result = manager.AddLogin(user.Id, loginInfo.Login);
                if (result.Succeeded)
                {
                    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // var code = manager.GenerateEmailConfirmationToken(user.Id);
                    // Send this link via email: IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id)

                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    return;
                }
            }
            AddErrors(result);
        }

        private void AddErrors(IdentityResult result) 
        {

            ValSummary.Visible = true;
            foreach (var error in result.Errors) 
            {
                ModelState.AddModelError("", error);
            }
        }



        protected void CreateUser_Click(object sender, EventArgs e)
        {
            //var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            ////get the country dependent on selected option
            //var dbContext = Context.GetOwinContext().Get<ApplicationDbContext>();
            //int selectedIndex = Convert.ToInt32(ddlCountryList.SelectedValue);
            //var selectedCountry = dbContext.userCountries.Where(c => c.Id == selectedIndex).FirstOrDefault<userCountry>();


            //var referralUserEmail = string.Empty;

            //var initialBalance = 1 * QandaQuizNet.Settings.PlayConfiguration.NumberOfAllowedFreeGamesPerUser;

            //var user = new ApplicationUser()
            //{
            //    //FirstName = firstName.Text,
            //    //LastName = lastName.Text,
            //    Over16AgeConfirm = Over16AgeConfirm.Checked,
            //    TownCity = townCity.Text,
            //    country = selectedCountry,
            //    accountBalance = initialBalance,
            //    UserName = email.Text,
            //    Email = email.Text,
            //    RegisteredDateTime = DateTime.Now,
            //    QandaReferralUserEmailAddress = referralUserEmail
            //};
            //IdentityResult result = manager.Create(user, Password.Text);
            //if (result.Succeeded)
            //{
            //    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            //    string code = manager.GenerateEmailConfirmationToken(user.Id);
            //    string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);

            //    try
            //    {
            //        manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");
            //    }
            //    catch { }

            //    signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
            //    //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            //    Response.Redirect("~/Account/TopupAccount/RegisterConfirmPending");
            //}
            //else
            //{
            //    ErrorMessage.Text = result.Errors.FirstOrDefault();
            //}
        }



    }
}