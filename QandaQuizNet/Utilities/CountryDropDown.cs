using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using QandaQuizNet.Models;


namespace QandaQuizNet.Utilities
{
    public static class CountryDropDown
    {

        public static void BindCountriesListToDropDown(ref System.Web.UI.WebControls.DropDownList ddlCountryList)
        {
            
            //var dbContext =  Context.GetOwinContext().Get<ApplicationDbContext>();
            //var countryList = dbContext.userCountries.Select(x => new { Name = x.country, Id = x.Id });
            ddlCountryList.DataTextField = "Name";
            ddlCountryList.DataValueField = "Id";

            //var ddlList = countryList.ToList();
            //ddlList.Insert(0, new { Name = "Select Country", Id = 0 });

            //ddlCountryList.DataSource = ddlList;
            ddlCountryList.DataBind();
        }

    }
}