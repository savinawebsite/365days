using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using _365Days.App_Start;
using System.Net.Mail;

namespace _365Days
{
    public partial class Ajax : System.Web.UI.Page
    {
        DataEntities db = new DataEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            long customerId = Convert.ToInt64(Session["CustomerId"].ToString());
            if (!Page.IsPostBack)
            {
                string action = Request.QueryString["action"].ToString();
                switch (action)
                {
                    case "Read":
                        {
                            int lessionId = Convert.ToInt32(Request.QueryString["lessionId"].ToString());
                            var updateTrackRecord = db.tTrackings.Where(t => t.CustomerId == customerId && t.LessionId == lessionId).FirstOrDefault();
                            updateTrackRecord.LessionStatus = 3;
                            updateTrackRecord.ActiveDate = DateTime.Now;
                            db.SaveChanges();
                            Response.Write("success");
                        }
                        break;

                    case "Active":
                        {
                            int lessionId = Convert.ToInt32(Request.QueryString["lessionId"].ToString());
                            var lastLessionRead = db.tTrackings.Where(t => t.CustomerId == customerId && t.LessionStatus == 3)
                                .OrderByDescending(t => t.TrackId).FirstOrDefault();
                            if(lastLessionRead != null)
                            {
                                if(lessionId - lastLessionRead.LessionId > 1)
                                {
                                    Response.Write("Sorry, you should click on DAY-" + (lastLessionRead.LessionId + 1) + " to practice with it before DAY-" + lessionId + " can be accessed");
                                } 
                            }
                        }
                        break;

                    case "Enable":
                        {
                            int lessionId = Convert.ToInt32(Request.QueryString["lessionId"].ToString());
                            var lastLessionRead = db.tTrackings.Where(t => t.CustomerId == customerId && t.LessionStatus == 3)
                                .OrderByDescending(t => t.TrackId).FirstOrDefault();
                            if (lastLessionRead != null)
                            {
                                if(lessionId - lastLessionRead.LessionId == 1)
                                {
                                    DateTime lastDateReaded = lastLessionRead.ActiveDate ?? DateTime.Now;
                                    DateTime nextDateCanRead = lastDateReaded.AddDays(1);
                                    string d = nextDateCanRead.ToString("MM/dd/yyyy HH:mm");
                                    Response.Write(" Sorry, you need to wait 24 hours that DAY-" + lessionId.ToString() + " can be accessed.Please kindly come back at "+ d +"");                          
                                }
                                else
                                {
                                    Response.Write("Sorry, you should finish your practice with DAY-" + (lessionId - 1).ToString() + " before DAY-" + lessionId.ToString() + " can be accessed");
                                }
                            }
                        }
                        break;
                    case "Logout":
                        {
                            long custId = Convert.ToInt64(Session["CustomerId"].ToString());
                            var loginLog = db.tLoginLogs.Where(t => t.CustomerCode == custId).OrderByDescending(t => t.LogId).FirstOrDefault();
                            loginLog.LogOutDate = DateTime.Now;
                            db.SaveChanges();


                            Session["CustomerId"] = 0;
                            Session["FullName"] = "";
                            Response.Write("sign out success");
                          
                        }
                        break;
                    case "Login":
                        {

                            string email = Request.QueryString["email"].ToString();
                            string pass = Request.QueryString["pass"].ToString();
                            var customerInfo = db.tCustomers.Where(t => t.Email == email).FirstOrDefault();
                            if(customerInfo != null)
                            {
                                if (pass.Equals(customerInfo.Password))
                                {
                                    long custId = customerInfo.CustomerCode;
                                    Session["CustomerId"] = custId;
                                    //login success
                                    Session["FullName"] = customerInfo.FirstName + " " + customerInfo.LastName;
                                    Response.Write("1");

                                    //insert log
                                    tLoginLog tLoginLog = new tLoginLog();
                                    tLoginLog.CustomerCode = custId;
                                    tLoginLog.LogInDate = DateTime.Now;
                                    db.tLoginLogs.Add(tLoginLog);
                                    db.SaveChanges();

                                }
                                else
                                {
                                    //password is wrong
                                    Response.Write("2");
                                }
                            }else
                            {
                                //check info on Shopify
                                long custId = Ultils.getCustomerId(email);
                                if (custId == 0)
                                {
                                    //account is not exits
                                    Response.Write("0");
                                }
                                else
                                {
                                    //insert customer info
                                    Session["CustomerId"] = custId;
                                    Response.Write("3");
                                }
                            }
                                           
                        }
                        break;
                    case "Update":
                        {
                            if (!Session["CustomerId"].ToString().Equals('0'))
                            {
                                string email = Request.QueryString["email"].ToString();
                                string pass = Request.QueryString["pass"].ToString();
                                string firstname = Request.QueryString["firstname"].ToString();
                                string lastname = Request.QueryString["lastname"].ToString();

                                tCustomer tCustomer = new tCustomer();
                                tCustomer.CustomerCode = Convert.ToInt64(Session["CustomerId"]);
                                tCustomer.Email = email;
                                tCustomer.Password = pass;
                                tCustomer.FirstName = firstname;
                                tCustomer.LastName = lastname;
                                tCustomer.InsertDate = DateTime.Now;
                                tCustomer.ModifyDate = DateTime.Now;
                                db.tCustomers.Add(tCustomer);
                                db.SaveChanges();
                                //login success
                                Session["FullName"] = firstname + " " + lastname;
                                Response.Write("1");
                            }

                        }
                        break;
                    case "RequestPassword":
                        {
                            string email = Request.QueryString["email"].ToString();
                            var customerInfo = db.tCustomers.Where(t => t.Email == email).FirstOrDefault();
                            if (customerInfo != null)
                            {
                                string html = "";
                                html += "<p>Dear "+  customerInfo.FirstName + " " + customerInfo.LastName    + ",</p>";
                                html += "<p> We have sent you this email in response to your request to get back your password on http://365-days.andhakaara.com . Please kindly take note this password for your next log.<br /></p>";
                                html += "<p>Your password: <b>"+ customerInfo.Password +"</b> <br /></p>";
                                html += "<p> We recommend that you keep your password secure and not share it with anyone<br /></p>";
                                html += "<p> If you need help, or you have any other questions, feel free to email office@andhakaara.com <br /></p>";

                                html += "<p>Best regards,</p>";
                                html += "<p>Andhakaara Support Team.</p>";

                                MailMessage mailMessage = new MailMessage();
                                mailMessage.From = new MailAddress("No-reply@andhakaara.com", "Andhakaara Supporting Team");
                                mailMessage.To.Add(email);
                                mailMessage.Bcc.Add("office@andhakaara.com");
                                mailMessage.Bcc.Add("tuan.lequoc@siyosa-vn.com");
                                mailMessage.Body = html;
                                mailMessage.Subject = "From http://365-days.andhakaara.com – resend your account password";
                                mailMessage.IsBodyHtml = true;
                                mailMessage.Priority = MailPriority.High;
                                SmtpClient client = new SmtpClient();

                                //Add the Creddentials- use your own email id and password
                                client.UseDefaultCredentials = false;
                                client.Host = "mail.andhakaara.com"; //Or Your SMTP Server Address  
                                client.Port = 25;
                                client.EnableSsl = false;
                                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                                client.Credentials = new System.Net.NetworkCredential("No-reply@andhakaara.com", "andhakaaraMailsystem#123");
                                try
                                {
                                    client.Send(mailMessage);
                                    Response.Write("1");
                                }
                                catch (Exception ex)
                                {
                                    Response.Write("-1");
                                }
                               
                            }
                            else
                            {
                                Response.Write("0");
                            }
                        }
                        break;
                }
            }
        }
    }
}