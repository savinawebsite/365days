using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Configuration;

namespace _365Days
{
    public class ShopifyServices
    {
        public static string getCustomers()
        {
            string url = "https://andhakaara.myshopify.com/admin/customers.json";
            return getHTTPS(url);
        }

        public static string getCustomer(string custId)
        {
            string url = "https://andhakaara.myshopify.com/admin/customers/"+ custId +".json";
            return getHTTPS(url);
        }

        public static string getOrdersByCustomer(long custId)
        {
            string url = "https://andhakaara.myshopify.com/admin/customers/" + custId + "/orders.json";
            return getHTTPS(url);
        }

        private static string getHTTPS(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.ContentType = "application/json";
            req.Credentials = getCredential(url);
            req.PreAuthenticate = true;

            using (var resp = (HttpWebResponse)req.GetResponse())
            {
                if (resp.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("Call failed. Received HTTP {0}", resp.StatusCode);
                    throw new ApplicationException(message);
                }

                var sr = new StreamReader(resp.GetResponseStream());
                return sr.ReadToEnd();
            }
        }
    

        private static CredentialCache getCredential(string url)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
              | SecurityProtocolType.Tls11
              | SecurityProtocolType.Tls12
              | SecurityProtocolType.Ssl3;
            // Skip validation of SSL/TLS certificate
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var credentialCache = new CredentialCache();
            credentialCache.Add(new Uri(url), "Basic", new NetworkCredential(ConfigurationManager.AppSettings["API_KEY"], ConfigurationManager.AppSettings["API_PWD"]));
            return credentialCache;
        }
    }
}