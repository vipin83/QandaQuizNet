using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Text;

namespace QandaQuizNet.Paypal
{
    public class NVPAPICaller
    {
        //Flag that determines the PayPal environment (live or sandbox)
        private const bool bSandbox = false;

        private string pendpointurl = "https://api-3t.paypal.com/nvp";

        public string APIUsername = "info_api1.qandaquiz.com";//"info-facilitator_api1.qandaquiz.com";//
        private string APIPassword = "L6FWVTFHH6L9DTN8";//"N42DKP9T9H4VVAQW";//
        private string APISignature = "Ak2oIEq17RVQSmQ1CVF2b9KJSQoRAaktiqgBXoAOOz4vziCggNUmJHmS";//"AeteNEj9RZq.KtO79-hBWL5UTGKWAgis9Ax8zSkGdcwTb4zWsx96bhbD";//
                                      //"Ak2oIEq17RVQSmQ1CVF2b9KJSQoRAaktiqgBXoAOOz4vziCggNUmJHmS"
        //private string returnURL = "http://"+ HttpContext.Current.Request.Url.Authority+"/Account/ConfirmTopup";

        private string returnURL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/Account/ConfirmTopup";
        private string cancelURL = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "/";

        //HttpWebRequest Timeout specified in milliseconds 
        private const int Timeout = 10000;

        private string Subject = "";
        private string BNCode = "PP-ECWizard";
        private const string CVV2 = "CVV2";
        private const string SIGNATURE = "SIGNATURE";
        private const string PWD = "PWD";
        private const string ACCT = "ACCT";
        private static readonly string[] SECURED_NVPS = new string[] { ACCT, CVV2, SIGNATURE, PWD };

        // Sets Paypal API Credentials        
        public void SetCredentials(string Userid, string Pwd, string Signature)
        {
            APIUsername = Userid;
            APIPassword = Pwd;
            APISignature = Signature;
        }

        //Step 1 . Calls SetExpressCheckout 
        public bool ExpressCheckout(string name, string description, string price, string quantity, string currency, string topupID, ref string token, ref string retMsg)
        {

            string host = "www.paypal.com";
            if (bSandbox)
            {
                pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
                host = "www.sandbox.paypal.com";
            }

            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "SetExpressCheckout";
            encoder["RETURNURL"] = returnURL;
            encoder["CANCELURL"] = cancelURL;

            double dblQuantity = Convert.ToDouble(quantity);
            double dblPrice = Convert.ToDouble(price);
            double totalPrice = dblQuantity * dblPrice;

            encoder["L_PAYMENTREQUEST_0_NAME0"] = name;
            encoder["L_PAYMENTREQUEST_0_DESC0"] = description;
            encoder["L_PAYMENTREQUEST_0_AMT0"] = price;
            encoder["L_PAYMENTREQUEST_0_QTY0"] = quantity;
            encoder["PAYMENTREQUEST_0_CUSTOM"] = topupID;
            encoder["PAYMENTREQUEST_0_INVNUM"] = topupID;
            encoder["NOSHIPPING"] = "1";

            encoder["PAYMENTREQUEST_0_AMT"] = totalPrice.ToString();
            encoder["PAYMENTREQUEST_0_ITEMAMT"] = totalPrice.ToString();
            encoder["PAYMENTREQUEST_0_PAYMENTACTION"] = "SALE";
            encoder["PAYMENTREQUEST_0_CURRENCYCODE"] = currency;

            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = HttpCall(pStrrequestforNvp);

            NVPCodec decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                token = decoder["TOKEN"];

                string ECURL = "https://" + host + "/cgi-bin/webscr?cmd=_express-checkout" + "&token=" + token + "&useraction=COMMIT";

                retMsg = ECURL;
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                return false;
            }
        }

        //Step 2. Calls GetExpressCheckoutDetails
        public bool GetDetails(string token, ref NVPCodec decoder, ref string retMsg)
        {

            if (bSandbox)
            {
                pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
            }

            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "GetExpressCheckoutDetails";
            encoder["TOKEN"] = token;

            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = HttpCall(pStrrequestforNvp);

            decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                return false;
            }
        }

        //Step 3. Calls DoExpressCheckout        
        public bool ConfirmPayment(string finalPaymentAmount, string token, string PayerId, string currency, ref NVPCodec decoder, ref string retMsg)
        {
            if (bSandbox)
            {
                pendpointurl = "https://api-3t.sandbox.paypal.com/nvp";
            }

            NVPCodec encoder = new NVPCodec();
            encoder["METHOD"] = "DoExpressCheckoutPayment";
            encoder["TOKEN"] = token;
            encoder["PAYMENTACTION"] = "Sale";
            encoder["PAYERID"] = PayerId;
            encoder["AMT"] = finalPaymentAmount;
            encoder["CURRENCYCODE"] = currency;

            string pStrrequestforNvp = encoder.Encode();
            string pStresponsenvp = HttpCall(pStrrequestforNvp);

            decoder = new NVPCodec();
            decoder.Decode(pStresponsenvp);

            string strAck = decoder["ACK"].ToLower();
            if (strAck != null && (strAck == "success" || strAck == "successwithwarning"))
            {
                return true;
            }
            else
            {
                retMsg = "ErrorCode=" + decoder["L_ERRORCODE0"] + "&" +
                    "Desc=" + decoder["L_SHORTMESSAGE0"] + "&" +
                    "Desc2=" + decoder["L_LONGMESSAGE0"];

                return false;
            }
        }

        //Helper method to make http url call for paypal epxress checkout
        public string HttpCall(string NvpRequest) //CallNvpServer
        {
            string url = pendpointurl;

            //To Add the credentials from the profile
            string strPost = NvpRequest + "&" + buildCredentialsNVPString();
            //strPost = strPost + "&BUTTONSOURCE=" + HttpUtility.UrlEncode(BNCode);

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Timeout = Timeout;
            objRequest.Method = "POST";
            objRequest.ContentLength = strPost.Length;

            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            try
            {
                using (StreamWriter myWriter = new StreamWriter(objRequest.GetRequestStream()))
                {
                    myWriter.Write(strPost);
                }
            }
            catch (Exception e)
            {
              //do some error handling
            }

            //Retrieve the Response returned from the NVP API call to PayPal
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            string result;
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }
               
        //creates user credentials NVP
        private string buildCredentialsNVPString()
        {
            NVPCodec codec = new NVPCodec();

            if (!String.IsNullOrEmpty(APIUsername))
                codec["USER"] = APIUsername;

            if (!String.IsNullOrEmpty(APIPassword))
                codec["PWD"] = APIPassword;

            if (!String.IsNullOrEmpty(APISignature))
                codec["SIGNATURE"] = APISignature;

            if (!String.IsNullOrEmpty(Subject))
                codec["SUBJECT"] = Subject;

            codec["VERSION"] = "84.0";

            return codec.Encode();
        }
      
    }

    public sealed class NVPCodec : NameValueCollection
    {
        private const string AMPERSAND = "&";
        private const string EQUALS = "=";
        private static readonly char[] AMPERSAND_CHAR_ARRAY = AMPERSAND.ToCharArray();
        private static readonly char[] EQUALS_CHAR_ARRAY = EQUALS.ToCharArray();

        /// <summary>
        /// Returns the built NVP string of all name/value pairs in the Hashtable
        /// </summary>
        /// <returns></returns>
        public string Encode()
        {
            StringBuilder sb = new StringBuilder();
            bool firstPair = true;
            foreach (string kv in AllKeys)
            {
                string name = HttpUtility.UrlEncode(kv);
                string value = HttpUtility.UrlEncode(this[kv]);

                if (!firstPair)
                {
                    sb.Append(AMPERSAND);
                }
                sb.Append(name).Append(EQUALS).Append(value);
                firstPair = false;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Decoding the string
        /// </summary>
        /// <param name="nvpstring"></param>
        public void Decode(string nvpstring)
        {
            Clear();
            foreach (string nvp in nvpstring.Split(AMPERSAND_CHAR_ARRAY))
            {
                string[] tokens = nvp.Split(EQUALS_CHAR_ARRAY);
                if (tokens.Length >= 2)
                {
                    string name = HttpUtility.UrlDecode(tokens[0]);
                    string value = HttpUtility.UrlDecode(tokens[1]);
                    Add(name, value);
                }
            }
        }


        #region Array methods
        public void Add(string name, string value, int index)
        {
            this.Add(GetArrayName(index, name), value);
        }

        public void Remove(string arrayName, int index)
        {
            this.Remove(GetArrayName(index, arrayName));
        }
              
        public string this[string name, int index]
        {
            get
            {
                return this[GetArrayName(index, name)];
            }
            set
            {
                this[GetArrayName(index, name)] = value;
            }
        }

        private static string GetArrayName(int index, string name)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", "index can not be negative : " + index);
            }
            return name + index;
        }
        #endregion
    }
}