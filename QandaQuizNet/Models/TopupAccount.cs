using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QandaQuizNet.Models
{
    public class TopupAccount
    {
        public string Id { get; set; }        
        public ApplicationUser user { get; set; }
        public decimal Amount { get; set; }
        public DateTime topupDateTime { get; set; }
        public string paypalToken { get; set; }

        public string payerID { get; set; }
        public DateTime? paymentSuccessfulDateTime { get; set; }

        public decimal? finalPaymentAmount { get; set; }
    }
}