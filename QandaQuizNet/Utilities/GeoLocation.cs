using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace QandaQuizNet.Utilities
{
    public static class GeoLocationDetails
    {
        public static string GetCountryCodeFromClientIP()
        {
            string countryCode = "GB";
            using (var client = new WebClient())
            {
                //form IP service URL

                var ipAddressObj = new
                {
                    city = "",
                    country = new
                    {
                        name = "",
                        code = ""
                    },
                    location = new
                    {
                        latitude = "",
                        longitude = "",
                        time_zone = ""
                    },
                    ip = ""
                };

                try
                {
                    var uriIPWebService = new Uri(String.Format("http://geoip.nekudo.com/api/{0}", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"])); //"72.229.28.185"

                    // var uriIPWebService = new Uri(String.Format("http://freegeoip.net/xml/{0}", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"])); //72.229.28.185
                    string webResponse = client.DownloadString(uriIPWebService);

                    var returnIPDetails = JsonConvert.DeserializeAnonymousType(webResponse, ipAddressObj);

                    var xmlResponse = XDocument.Parse(webResponse);


                    countryCode = returnIPDetails.country.code; //xmlResponse.Root.Element("CountryCode").Value;
                }
                catch
                {
                    //Looks like we didn't get what we expected.                        
                }

            }

            return countryCode;
        }
    }

}