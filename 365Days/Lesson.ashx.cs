using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _365Days.App_Start;
using System.Web.SessionState;

namespace _365Days
{
    /// <summary>
    /// Summary description for Lesson
    /// </summary>
    public class Lesson : IHttpHandler, IReadOnlySessionState
    {
        DataEntities db = new DataEntities();
        public void ProcessRequest(HttpContext context)
        {
            long customerId = Convert.ToInt64(context.Session["CustomerId"].ToString());
            if (customerId == 0)
            {

            }
            else
            {
                int lessonId = Convert.ToInt32(context.Request.QueryString["LessonId"].ToString());
                var lastLessionRead = db.tTrackings.Where(t => t.CustomerId == customerId && t.LessionStatus == 3)
                    .OrderByDescending(t => t.TrackId).FirstOrDefault();

                if (lastLessionRead != null)
                {
                    if (lessonId - lastLessionRead.LessionId > 1)
                    {

                    }
                    else
                    {
                        var lastLessionActive = db.tTrackings.Where(t => t.LessionId == lessonId && t.CustomerId == customerId)
                        .OrderByDescending(t => t.TrackId).FirstOrDefault();
                        if (lastLessionActive != null)
                        {
                            if (lastLessionActive.LessionStatus == 2 || lastLessionActive.LessionStatus == 3)
                            {
                                string url = "Lesson_content/DAY_" + lessonId + "/index.html";
                                context.Response.Redirect(url);
                                return;
                            }
                        }
                    }
                }
            }
            context.Response.Redirect("http://365-days.andhakaara.com/Lesson_content/DAY_0/index.html");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}