using SMD.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Implementation.Services
{
    public class SmsServiceCustom : ISmsServiceCustom
    {


        public bool SendSMS(string toNumber, string MessageText)
        {

            string MyUsername = "923334168877";
            string MyPassword = "4926";
            string Masking = "Cash4ads";

            String URI = "http://Sendpk.com" +
            "/api/sms.php?" +
            "username=" + MyUsername +
            "&password=" + MyPassword +
            "&sender=" + Masking +
            "&mobile=" + toNumber +
            "&message=" + Uri.UnescapeDataString(MessageText); // Visual Studio 10-15 
            //"//&message=" + System.Net.WebUtility.UrlEncode(MessageText);// Visual Studio 12 
            try
            {
                WebRequest req = WebRequest.Create(URI);
                WebResponse resp = req.GetResponse();
                var sr = new System.IO.StreamReader(resp.GetResponseStream());
                var result = sr.ReadToEnd().Trim();

                if (result.StartsWith("OK"))
                    return true;
                else
                {
                    throw new Exception("Sending Failed, Error Code : " + result);
                }
            }
            catch (WebException ex)
            {
                var httpWebResponse = ex.Response as HttpWebResponse;
                if (httpWebResponse != null)
                {
                    switch (httpWebResponse.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            throw new Exception( "404:URL not found :" + URI);
                            break;
                        case HttpStatusCode.BadRequest:
                            throw new Exception(  "400:Bad Request");
                            break;
                        default:
                            throw new Exception(  httpWebResponse.StatusCode.ToString());
                    }
                }
            }
            return false;
        }
    }
}
