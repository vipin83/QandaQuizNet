using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QandaQuizNet.Models
{
    public class userCountry
    {
        public int Id { get; set; }
        public string countryCode { get; set; }
        public string country { get; set; }
        public string currencyCode { get; set; }
        public string currency { get; set; }
        public string currencySymbol { get; set; }
        public string languageCode { get; set; }
        public string language { get; set; }
    }
}