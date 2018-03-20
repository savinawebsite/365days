using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using System.Web.Services;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using _365Days.App_Start;
using Newtonsoft.Json;

using Newtonsoft.Json.Linq;
using System.Data;
using System.Globalization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Globalization;
using System.Web.Services;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using _365Days.App_Start;
using Newtonsoft.Json;

namespace _365Days
{
    public class Ultils
    {
        // recursively yield all children of json
        public static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }

        public static long getCustomerId(string emailInput)
        {
            long result = 0;
            string custJson = ShopifyServices.getCustomers();
            var jObjects = Ultils.AllChildren(JObject.Parse(custJson))
            .First(c => c.Type == JTokenType.Array && c.Path.Contains("customers"))
            .Children<JObject>();

            foreach (JObject content in jObjects)
            {
                string email = content["email"].ToString();
                long custId = Convert.ToInt64(content["id"].ToString());
                if (email != null)
                {
                    if (email.ToLower().Equals(emailInput.ToLower()))
                    {
                        result = custId;
                        break;
                    }
                }
            }

            /*
            string json = custJson.Substring(13);
            string f = json.Remove(json.Length - 1);
            JArray jarr = JArray.Parse(f);
            foreach (JObject content in jarr.Children<JObject>())
            {
                string email = content["email"].ToString();
                string custId = content["id"].ToString();
                if(email != null)
                {
                    if (email.ToLower().Equals(emailInput.ToLower()))
                    {
                        result = custId;
                        break;
                    }
                }
            }

            */
            return result;
        }

        public static string getFullName(long custId)
        {
            string custJson = ShopifyServices.getCustomer(custId.ToString());
            JObject jsonObj = JObject.Parse(custJson);
            JObject custObj = JObject.Parse(jsonObj["customer"].ToString());
            string firstName = custObj["first_name"].ToString();
            string lastName = custObj["last_name"].ToString();
            return firstName + " " + lastName;
        }



    }
}