using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using QandaQuizNet.Models;

namespace QandaQuizNet.Account
{
    public partial class ResendEmailConfirmation : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ResendEmailConfirmationMethod(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user's email address
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser user = manager.FindByName(Email.Text);
                //if (user == null || !manager.IsEmailConfirmed(user.Id))
                if (user == null)
                {
                    FailureText.Text = "Account with this email address does not exist.";
                    ErrorMessage.Visible = true;
                    return;
                }

                string code = manager.GenerateEmailConfirmationToken(user.Id);
                string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);

                try
                {
                    manager.SendEmail(user.Id, "QandA Quiz - Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");
                }
                catch { }
                 
                loginForm.Visible = false;
                DisplayEmail.Visible = true;
            }
        }
    }
}