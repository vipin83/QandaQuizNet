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

namespace QandaQuizNet.Account
{
    public partial class ConfirmTopup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NVPAPICaller PPAPICaller = new NVPAPICaller();
            NVPCodec decoder = new NVPCodec();
            string token = string.Empty;
            string payerID = string.Empty;
            string finalPaymentAmount = string.Empty;
            string retMsg = string.Empty;
            string currency = string.Empty;
            string topUpAccountID = string.Empty;
            decimal finalToppedUpAmount = 0.0M;

            token = Session["token"].ToString();

            //use the PayPal token to get the details of payment - this could include shipping details
            bool ret = PPAPICaller.GetDetails(token, ref decoder, ref retMsg);
            if (ret)
            {
                payerID = decoder["PayerID"];
                token = decoder["token"];
                finalPaymentAmount = decoder["PAYMENTREQUEST_0_AMT"];  
                finalToppedUpAmount = Convert.ToDecimal(finalPaymentAmount);
                currency = decoder["CURRENCYCODE"];
                //get the topupAccount Id we sent to paypal as custom field
                topUpAccountID = decoder["PAYMENTREQUEST_0_CUSTOM"];               
            }
            else
            {
                //error.LogError();
            }

            NVPCodec confirmdecoder = new NVPCodec();

            //confirm that payment was taken
            bool ret2 = PPAPICaller.ConfirmPayment(finalPaymentAmount, token, payerID, currency, ref confirmdecoder, ref retMsg);
            if (ret2)
            {
                //if payment was taken do some back end processing to mark order as paid
                //use token to work out which order to mark as paid
                token = confirmdecoder["token"];
                
                //update various tables with successfull paypal payment
                //1. topup account
                var dbContext = Context.GetOwinContext().Get<ApplicationDbContext>();
                var topupAccountRow = dbContext.topupAccounts.Where(ta => ta.Id == topUpAccountID).FirstOrDefault<TopupAccount>();

                if (topupAccountRow != null)
                {
                    topupAccountRow.payerID = payerID;
                    topupAccountRow.finalPaymentAmount = finalToppedUpAmount;
                    topupAccountRow.paymentSuccessfulDateTime = DateTime.Now;                   

                }

                //2. Update Account balance of user
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var user = manager.FindById(User.Identity.GetUserId());


                user.accountBalance = user.accountBalance + finalToppedUpAmount;

                dbContext.SaveChanges();

                //lblSuccess.Text = "Payment successful for - PayerID: " + payerID + "; Amount: " + finalPaymentAmount;

                Response.Redirect("~/Play");


            }
            else
            {
                //payment has not been successful - don't send goods!
            }
        }
    }
}