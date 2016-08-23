using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QandaQuizNet.Utilities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace QandaQuizNet
{
    public partial class Info : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string counrtyCode = "GB";
            //if (!User.Identity.IsAuthenticated)
            //{
            //    counrtyCode = GeoLocationDetails.GetCountryCodeFromClientIP();
            //    ShowBannerBasedOnCountryCode(counrtyCode);
            //}
            //else
            //{
            //    //show logo based on user choosen country flag
            //    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //    var user = manager.FindById(User.Identity.GetUserId());
            //    ShowBannerBasedOnCountryCode(user.country.countryCode);

            //}
          
        }


        private void ShowBannerBasedOnCountryCode(string Code)
        {            
            //imgLogo.Src = (Code == "CA") ? "~/images/Qanda Logo-Dollar.jpg" : "~/images/Qanda Logo-Pound.jpg";
        }
    }
}