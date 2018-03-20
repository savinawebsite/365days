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
using System.Configuration;




namespace _365Days
{
    public partial class _365Days : System.Web.UI.Page
    {
        DataEntities db = new DataEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            loadLession();
        }

        private void loadLession()
        {
            string html1 = "";
            string html2 = "";
            string html3 = "";
            string html4 = "";
            string html5 = "";
            string html6 = "";
            string html7 = "";
            string html8 = "";
            string html9 = "";
            string html10 = "";

            if (Session["CustomerId"].ToString().Equals("0"))
            {
                this.loggedInUser.Style.Add("display", "none");
                for (int i = 1; i <= 365; i++)
                {
                    #region Part 00: Have not buy yet

                    int lessionStatus = 0;
                    if (i >= 1 && i <= 51)
                    {
                        html1 += "<button type=\"button\" disabled class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                    }
                    if (i >= 52 && i <= 107)
                    {
                        html2 += "<button type=\"button\" disabled class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                    }
                    if (i >= 108 && i <= 137)
                    {
                        html3 += "<button type=\"button\" disabled class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                    }
                    if (i >= 138 && i <= 168)
                    {
                        html4 += "<button type=\"button\" disabled class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                    }
                    if (i >= 169 && i <= 201)
                    {
                        html5 += "<button type=\"button\" disabled class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                    }
                    if (i >= 202 && i <= 230)
                    {
                        html6 += "<button type=\"button\" disabled class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                    }
                    if (i >= 231 && i <= 264)
                    {
                        html7 += "<button type=\"button\" disabled class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                    }
                    if (i >= 265 && i <= 292)
                    {
                        html8 += "<button type=\"button\" disabled class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                    }
                    if (i >= 293 && i <= 317)
                    {
                        html9 += "<button type=\"button\" disabled class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                    }
                    if (i >= 318 && i <= 365)
                    {
                        html10 += "<button type=\"button\" disabled class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                    }

                    #endregion
                }
                string html = html1 + html2 + html3 + html4 + html5 + html6 + html7 + html8 + html9 + html10;
                dvLessions.InnerHtml = html;
            }
            else
            {
                long custId = Convert.ToInt64(Session["CustomerId"].ToString());
                int levelPurchased = getLevelPurchased(custId);
                Session["Level"] = levelPurchased;
                this.loggedInUser.Style.Add("display", "block");

                //show full name
                string fullName = Session["FullName"].ToString();
                userWelcomUsername.InnerHtml = "Welcome <strong>" + fullName + "</strong>";

                //you are currently on day
        
                var lastLessionRead = db.tTrackings.Where(t => t.CustomerId == custId && t.LessionStatus == 3)
                    .OrderByDescending(t => t.TrackId).FirstOrDefault();
                string level = "";

                if (lastLessionRead == null)
                {
                    appent_count_number.InnerText =  "1";
                    appent_count_number_progress.InnerText = "2";
                    if (levelPurchased >= 1)
                    {
                        level = "Darkness";
                        userWelcomLevel.InnerHtml = "Level: <strong>" + level + "</strong>";
                    }
                }
                else
                {
                    appent_count_number.InnerText = lastLessionRead.LessionId.ToString();
                    appent_count_number_progress.InnerText = (lastLessionRead.LessionId + 1).ToString();

                    int currentLastLessionRead = lastLessionRead.LessionId;
                    if (currentLastLessionRead >= 1 && currentLastLessionRead <= 51)
                    {
                        level = "Darkness";
                    }
                    if (currentLastLessionRead >= 52 && currentLastLessionRead <= 107)
                    {
                        level = "Picasso";
                    }
                    if (currentLastLessionRead >= 108 && currentLastLessionRead <= 137)
                    {
                        level = "Beheaded";
                    }
                    if (currentLastLessionRead >= 138 && currentLastLessionRead <= 168)
                    {
                        level = "Warrior";
                    }
                    if (currentLastLessionRead >= 169 && currentLastLessionRead <= 201)
                    {
                        level = "Mother";
                    }
                    if (currentLastLessionRead >= 202 && currentLastLessionRead <= 230)
                    {
                        level = "Fortune";
                    }
                    if (currentLastLessionRead >= 231 && currentLastLessionRead <= 264)
                    {
                        level = "Death";
                    }
                    if (currentLastLessionRead >= 265 && currentLastLessionRead <= 292)
                    {
                        level = "Earth";
                    }
                    if (currentLastLessionRead >= 293 && currentLastLessionRead <= 317)
                    {
                        level = "Youth";
                    }
                    if (currentLastLessionRead >= 318 && currentLastLessionRead <= 365)
                    {
                        level = "Hypnotizer";
                    }
                    userWelcomLevel.InnerHtml = "Level: <strong>" + level + "</strong>";
                }


				//your lesson progress
                var lastLessionActive = db.tTrackings.Where(t => t.CustomerId == custId && t.LessionStatus == 2)
                    .OrderByDescending(t => t.TrackId).FirstOrDefault();

                var trackList = (from tr in db.tTrackings
                                 where tr.CustomerId == custId
                                 select new
                                 {
                                     tr.CustomerId,
                                     tr.LessionId,
                                     tr.LessionStatus,
                                     tr.InsertDate,
                                     tr.ActiveDate
                                 });
                if (trackList.Count() == 0)
                {
                    // insert customer to tTracking
                    #region insert customer to tTracking
                    for (int i = 1; i <= 365; i++)
                    {
                        //Part 00: Have not buy yet
                        #region Part 00: Have not buy yet
                        if (levelPurchased == 0)
                        {
                            int lessionStatus = 0;
                            //insertTracking(lessionStatus, i, custId);

                            if (i >= 1 && i <= 51)
                            {
                                html1 += "<button type=\"button\" disabled class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 52 && i <= 107)
                            {
                                html2 += "<button type=\"button\" disabled class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 108 && i <= 137)
                            {
                                html3 += "<button type=\"button\" disabled class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 138 && i <= 168)
                            {
                                html4 += "<button type=\"button\" disabled class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 169 && i <= 201)
                            {
                                html5 += "<button type=\"button\" disabled class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 202 && i <= 230)
                            {
                                html6 += "<button type=\"button\" disabled class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 231 && i <= 264)
                            {
                                html7 += "<button type=\"button\" disabled class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 265 && i <= 292)
                            {
                                html8 += "<button type=\"button\" disabled class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 293 && i <= 317)
                            {
                                html9 += "<button type=\"button\" disabled class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 318 && i <= 365)
                            {
                                html10 += "<button type=\"button\" disabled class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                        }
                        #endregion

                        //Level TRY OUT - (Day 1 to 10)
                        #region Level TRY OUT - (Day 1 to 10)
                        if(levelPurchased == 11)
                        {
                            if (i >= 1 && i <= 7)
                            {
                                int lessionStatus = 1;
                                if (lessionStatus == 1 && i == 1)
                                {
                                    lessionStatus = 3;
                                }
                                insertTracking(lessionStatus, i, custId);
                                html1 += "<button type=\"button\"  class=\"lession1 lvtrial\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }

                            if (i >= 8 && i <= 51)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html2 += "<button type=\"button\" disabled class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }

                            if (i >= 52 && i <= 107)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html2 += "<button type=\"button\" disabled class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 108 && i <= 137)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html3 += "<button type=\"button\" disabled class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 138 && i <= 168)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html4 += "<button type=\"button\" disabled class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 169 && i <= 201)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html5 += "<button type=\"button\" disabled class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 202 && i <= 230)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html6 += "<button type=\"button\" disabled class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 231 && i <= 264)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html7 += "<button type=\"button\" disabled class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 265 && i <= 292)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html8 += "<button type=\"button\" disabled class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 293 && i <= 317)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html9 += "<button type=\"button\" disabled class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 318 && i <= 365)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html10 += "<button type=\"button\" disabled class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                        }
                        #endregion

                        // Part 01: Level - Darkness(Day 1 to 51)
                        #region Part 01: Level - Darkness(Day 1 to 51)
                        if (levelPurchased == 1)
                        {

                            if (i >= 1 && i <= 51)
                            {
                                int lessionStatus = 1;
                                if(lessionStatus == 1 && i == 1)
                                {
                                    lessionStatus = 3;
                                }
                                insertTracking(lessionStatus, i, custId);
                                html1 += "<button type=\"button\"  class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 52 && i <= 107)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html2 += "<button type=\"button\" disabled class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 108 && i <= 137)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html3 += "<button type=\"button\" disabled class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 138 && i <= 168)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html4 += "<button type=\"button\" disabled class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 169 && i <= 201)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html5 += "<button type=\"button\" disabled class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 202 && i <= 230)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html6 += "<button type=\"button\" disabled class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 231 && i <= 264)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html7 += "<button type=\"button\" disabled class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 265 && i <= 292)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html8 += "<button type=\"button\" disabled class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 293 && i <= 317)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html9 += "<button type=\"button\" disabled class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 318 && i <= 365)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html10 += "<button type=\"button\" disabled class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }

                        }
                        #endregion



                        //Part 02: Level- Picasso (Day 52 to 107)
                        #region Part 02: Level- Picasso (Day 52 to 107)
                        if (levelPurchased == 2)
                        {

                            if (i >= 1 && i <= 51)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html1 += "<button type=\"button\"  class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 52 && i <= 107)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html2 += "<button type=\"button\"  class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 108 && i <= 137)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html3 += "<button type=\"button\" disabled class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 138 && i <= 168)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html4 += "<button type=\"button\" disabled class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 169 && i <= 201)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html5 += "<button type=\"button\" disabled class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 202 && i <= 230)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html6 += "<button type=\"button\" disabled class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 231 && i <= 264)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html7 += "<button type=\"button\" disabled class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 265 && i <= 292)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html8 += "<button type=\"button\" disabled class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 293 && i <= 317)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html9 += "<button type=\"button\" disabled class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 318 && i <= 365)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html10 += "<button type=\"button\" disabled class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                        }
                        #endregion

                        //Part 03: Level- Picasso (Day 108 to 137)
                        #region
                        if (levelPurchased == 3)
                        {

                            if (i >= 1 && i <= 51)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html1 += "<button type=\"button\"  class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 52 && i <= 107)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html2 += "<button type=\"button\"  class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 108 && i <= 137)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html3 += "<button type=\"button\"  class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 138 && i <= 168)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html4 += "<button type=\"button\" disabled class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 169 && i <= 201)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html5 += "<button type=\"button\" disabled class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 202 && i <= 230)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html6 += "<button type=\"button\" disabled class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 231 && i <= 264)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html7 += "<button type=\"button\" disabled class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 265 && i <= 292)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html8 += "<button type=\"button\" disabled class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 293 && i <= 317)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html9 += "<button type=\"button\" disabled class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 318 && i <= 365)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html10 += "<button type=\"button\" disabled class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                        }
                        #endregion

                        //Part 04: Level- Picasso (Day 138 to 168)
                        if (levelPurchased == 4)
                        {
                            if (i >= 1 && i <= 51)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html1 += "<button type=\"button\"  class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 52 && i <= 107)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html2 += "<button type=\"button\"  class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 108 && i <= 137)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html3 += "<button type=\"button\"  class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 138 && i <= 168)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html4 += "<button type=\"button\"  class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 169 && i <= 201)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html5 += "<button type=\"button\" disabled class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 202 && i <= 230)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html6 += "<button type=\"button\" disabled class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 231 && i <= 264)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html7 += "<button type=\"button\" disabled class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 265 && i <= 292)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html8 += "<button type=\"button\" disabled class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 293 && i <= 317)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html9 += "<button type=\"button\" disabled class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 318 && i <= 365)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html10 += "<button type=\"button\" disabled class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                        }

                        //Part 05: Level- Picasso (Day 169 to 201)
                        if (levelPurchased == 5)
                        {
                            if (i >= 1 && i <= 51)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html1 += "<button type=\"button\"  class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 52 && i <= 107)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html2 += "<button type=\"button\"  class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 108 && i <= 137)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html3 += "<button type=\"button\"  class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 138 && i <= 168)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html4 += "<button type=\"button\"  class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 169 && i <= 201)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html5 += "<button type=\"button\"  class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 202 && i <= 230)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html6 += "<button type=\"button\" disabled class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 231 && i <= 264)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html7 += "<button type=\"button\" disabled class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 265 && i <= 292)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html8 += "<button type=\"button\" disabled class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 293 && i <= 317)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html9 += "<button type=\"button\" disabled class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 318 && i <= 365)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html10 += "<button type=\"button\" disabled class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                        }

                        //Part 06: Level- Picasso (Day 202 to 230)
                        if (levelPurchased == 6)
                        {
                            if (i >= 1 && i <= 51)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html1 += "<button type=\"button\"  class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 52 && i <= 107)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html2 += "<button type=\"button\"  class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 108 && i <= 137)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html3 += "<button type=\"button\"  class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 138 && i <= 168)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html4 += "<button type=\"button\"  class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 169 && i <= 201)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html5 += "<button type=\"button\"  class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 202 && i <= 230)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html6 += "<button type=\"button\"  class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 231 && i <= 264)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html7 += "<button type=\"button\" disabled class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 265 && i <= 292)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html8 += "<button type=\"button\" disabled class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 293 && i <= 317)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html9 += "<button type=\"button\" disabled class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 318 && i <= 365)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html10 += "<button type=\"button\" disabled class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                        }

                        //Part 07: Level- Picasso (Day 231 to 264)
                        if (levelPurchased == 7)
                        {
                            if (i >= 1 && i <= 51)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html1 += "<button type=\"button\"  class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 52 && i <= 107)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html2 += "<button type=\"button\"  class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 108 && i <= 137)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html3 += "<button type=\"button\"  class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 138 && i <= 168)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html4 += "<button type=\"button\"  class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 169 && i <= 201)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html5 += "<button type=\"button\"  class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 202 && i <= 230)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html6 += "<button type=\"button\"  class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 231 && i <= 264)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html7 += "<button type=\"button\"  class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 265 && i <= 292)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html8 += "<button type=\"button\" disabled class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 293 && i <= 317)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html9 += "<button type=\"button\" disabled class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 318 && i <= 365)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html10 += "<button type=\"button\" disabled class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                        }

                        //Part 08: Level- Picasso (Day 265 to 292)
                        if (levelPurchased == 8)
                        {
                            if (i >= 1 && i <= 51)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html1 += "<button type=\"button\"  class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 52 && i <= 107)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html2 += "<button type=\"button\"  class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 108 && i <= 137)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html3 += "<button type=\"button\"  class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 138 && i <= 168)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html4 += "<button type=\"button\"  class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 169 && i <= 201)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html5 += "<button type=\"button\"  class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 202 && i <= 230)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html6 += "<button type=\"button\"  class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 231 && i <= 264)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html7 += "<button type=\"button\"  class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 265 && i <= 292)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html8 += "<button type=\"button\"  class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 293 && i <= 317)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html9 += "<button type=\"button\" disabled class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 318 && i <= 365)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html10 += "<button type=\"button\" disabled class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                        }

                        //Part 09: Level- Picasso (Day 293 to 317)
                        if (levelPurchased == 9)
                        {
                            if (i >= 1 && i <= 51)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html1 += "<button type=\"button\"  class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 52 && i <= 107)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html2 += "<button type=\"button\"  class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 108 && i <= 137)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html3 += "<button type=\"button\"  class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 138 && i <= 168)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html4 += "<button type=\"button\"  class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 169 && i <= 201)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html5 += "<button type=\"button\"  class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 202 && i <= 230)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html6 += "<button type=\"button\"  class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 231 && i <= 264)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html7 += "<button type=\"button\"  class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 265 && i <= 292)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html8 += "<button type=\"button\"  class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 293 && i <= 317)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html9 += "<button type=\"button\"  class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 318 && i <= 365)
                            {
                                int lessionStatus = 0;
                                insertTracking(lessionStatus, i, custId);
                                html10 += "<button type=\"button\" disabled class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                        }

                        //Part 10: Level- Picasso (Day 318 to 365)
                        if (levelPurchased == 10)
                        {
                            if (i >= 1 && i <= 51)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html1 += "<button type=\"button\"  class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 52 && i <= 107)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html2 += "<button type=\"button\"  class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 108 && i <= 137)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html3 += "<button type=\"button\"  class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 138 && i <= 168)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html4 += "<button type=\"button\"  class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 169 && i <= 201)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html5 += "<button type=\"button\"  class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 202 && i <= 230)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html6 += "<button type=\"button\"  class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 231 && i <= 264)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html7 += "<button type=\"button\"  class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 265 && i <= 292)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html8 += "<button type=\"button\"  class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 293 && i <= 317)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html9 += "<button type=\"button\"  class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                            if (i >= 318 && i <= 365)
                            {
                                int lessionStatus = 1;
                                insertTracking(lessionStatus, i, custId);
                                html10 += "<button type=\"button\"  class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                            }
                        }
                    }
                    #endregion

                }
                else
                {
                    if (trackList.Count() == 365)
                    {
                        //update lession status by lestion have been purchase

                        for (int i = 1; i <= 365; i++)
                        {
                            //Level TRY OUT - (Day 1 to 10)
                            #region Level TRY OUT - (Day 1 to 10)
                            if (levelPurchased == 11)
                            {
                                if (i >= 1 && i <= 7)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);
                                }
                            }
                            #endregion

                            // Part 01: Level - Darkness(Day 1 to 51)
                            #region Part 01: Level - Darkness(Day 1 to 51)
                            if (levelPurchased == 1)
                            {
                                if (i >= 1 && i <= 51)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);
                                }
                            }
                            #endregion

                            //Part 02: Level- Picasso (Day 52 to 107)
                            #region Part 02: Level- Picasso (Day 52 to 107)
                            if (levelPurchased == 2)
                            {

                                if (i >= 1 && i <= 51)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);
                                }
                                if (i >= 52 && i <= 107)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);
                                }
                            }
                            #endregion

                            //Part 03: Level- Picasso (Day 108 to 137)
                            #region
                            if (levelPurchased == 3)
                            {

                                if (i >= 1 && i <= 51)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 52 && i <= 107)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 108 && i <= 137)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                            }
                            #endregion

                            //Part 04: Level- Picasso (Day 138 to 168)
                            #region
                            if (levelPurchased == 4)
                            {
                                if (i >= 1 && i <= 51)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 52 && i <= 107)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 108 && i <= 137)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 138 && i <= 168)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }

                            }
                            #endregion

                            //Part 05: Level- Picasso (Day 169 to 201)
                            #region
                            if (levelPurchased == 5)
                            {
                                if (i >= 1 && i <= 51)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);
                                }
                                if (i >= 52 && i <= 107)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);
                                }
                                if (i >= 108 && i <= 137)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);
                                }
                                if (i >= 138 && i <= 168)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);
                                }
                                if (i >= 169 && i <= 201)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);
                                }

                            }
                            #endregion

                            //Part 06: Level- Picasso (Day 202 to 230)
                            #region
                            if (levelPurchased == 6)
                            {
                                if (i >= 1 && i <= 51)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 52 && i <= 107)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 108 && i <= 137)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 138 && i <= 168)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 169 && i <= 201)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 202 && i <= 230)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }

                            }
                            #endregion

                            //Part 07: Level- Picasso (Day 231 to 264)
                            #region
                            if (levelPurchased == 7)
                            {
                                if (i >= 1 && i <= 51)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 52 && i <= 107)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 108 && i <= 137)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 138 && i <= 168)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 169 && i <= 201)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 202 && i <= 230)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 231 && i <= 264)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }

                            }
                            #endregion

                            //Part 08: Level- Picasso (Day 265 to 292)
                            #region
                            if (levelPurchased == 8)
                            {
                                if (i >= 1 && i <= 51)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 52 && i <= 107)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 108 && i <= 137)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 138 && i <= 168)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 169 && i <= 201)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 202 && i <= 230)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 231 && i <= 264)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 265 && i <= 292)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }

                            }
                            #endregion

                            //Part 09: Level- Picasso (Day 293 to 317)
                            #region
                            if (levelPurchased == 9)
                            {
                                if (i >= 1 && i <= 51)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 52 && i <= 107)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 108 && i <= 137)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 138 && i <= 168)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 169 && i <= 201)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 202 && i <= 230)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 231 && i <= 264)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 265 && i <= 292)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 293 && i <= 317)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }

                            }
                            #endregion

                            //Part 10: Level- Picasso (Day 318 to 365)
                            #region
                            if (levelPurchased == 10)
                            {
                                if (i >= 1 && i <= 51)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 52 && i <= 107)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 108 && i <= 137)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 138 && i <= 168)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 169 && i <= 201)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 202 && i <= 230)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 231 && i <= 264)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 265 && i <= 292)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 293 && i <= 317)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                                if (i >= 318 && i <= 365)
                                {
                                    int lessionStatus = 1;
                                    updateTracking(lessionStatus, i, custId);

                                }
                            }
                            #endregion
                        }



                        //get condition from tracking and show
                        var tracks = (from tr in db.tTrackings
                                      where tr.CustomerId == custId
                                      select new
                                      {
                                          tr.CustomerId,
                                          tr.LessionId,
                                          tr.LessionStatus,
                                          tr.InsertDate,
                                          tr.ActiveDate
                                      });
                        if (tracks.Count() == 365)
                        {

                            for (int i = 1; i <= 365; i++)
                            {
                                var item = tracks.ToList()[i - 1];
                                int lessionStatus = item.LessionStatus;
                                if (i >= 1 && i <= 51)
                                {
                                    if (lessionStatus == 0)
                                    {
                                        html1 += "<button type=\"button\" disabled class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                    else
                                    {
                                        html1 += "<button type=\"button\"  class=\"lession1\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                }
                                if (i >= 52 && i <= 107)
                                {

                                    if (lessionStatus == 0)
                                    {
                                        html2 += "<button type=\"button\" disabled class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                    else
                                    {
                                        html2 += "<button type=\"button\"  class=\"lession2\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                }
                                if (i >= 108 && i <= 137)
                                {

                                    if (lessionStatus == 0)
                                    {
                                        html3 += "<button type=\"button\" disabled class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                    else
                                    {
                                        html3 += "<button type=\"button\"  class=\"lession3\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                }
                                if (i >= 138 && i <= 168)
                                {

                                    if (lessionStatus == 0)
                                    {
                                        html4 += "<button type=\"button\" disabled class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                    else
                                    {
                                        html4 += "<button type=\"button\"  class=\"lession4\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                }
                                if (i >= 169 && i <= 201)
                                {

                                    if (lessionStatus == 0)
                                    {
                                        html5 += "<button type=\"button\" disabled class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                    else
                                    {
                                        html5 += "<button type=\"button\"  class=\"lession5\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                }
                                if (i >= 202 && i <= 230)
                                {

                                    if (lessionStatus == 0)
                                    {
                                        html6 += "<button type=\"button\" disabled class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                    else
                                    {
                                        html6 += "<button type=\"button\"  class=\"lession6\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                }
                                if (i >= 231 && i <= 264)
                                {

                                    if (lessionStatus == 0)
                                    {
                                        html7 += "<button type=\"button\" disabled class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                    else
                                    {
                                        html7 += "<button type=\"button\"  class=\"lession7\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                }
                                if (i >= 265 && i <= 292)
                                {

                                    if (lessionStatus == 0)
                                    {
                                        html8 += "<button type=\"button\" disabled class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                    else
                                    {
                                        html8 += "<button type=\"button\"  class=\"lession8\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                }
                                if (i >= 293 && i <= 317)
                                {

                                    if (lessionStatus == 0)
                                    {
                                        html9 += "<button type=\"button\" disabled class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                    else
                                    {
                                        html9 += "<button type=\"button\"  class=\"lession9\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                }
                                if (i >= 318 && i <= 365)
                                {

                                    if (lessionStatus == 0)
                                    {
                                        html10 += "<button type=\"button\" disabled class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                    else
                                    {
                                        html10 += "<button type=\"button\"  class=\"lession10\" id=\"" + i + "\" value=\"" + lessionStatus + "\" onclick=\"actionClick('" + i + "')\">" + i + "</button>";
                                    }
                                }
                            }
                        }
                    }
                }

                string html = html1 + html2 + html3 + html4 + html5 + html6 + html7 + html8 + html9 + html10;
                dvLessions.InnerHtml = html;
            }
        }

        private void insertTracking(int lessionStatus, int lessionId, long customerId)
        {
            tTracking tTracking = new tTracking();
            tTracking.CustomerId = customerId;
            tTracking.LessionId = lessionId;
            tTracking.InsertDate = DateTime.Now;
            tTracking.LessionStatus = lessionStatus;
            if(lessionStatus == 3 && lessionId == 1)
            {
                tTracking.ActiveDate = DateTime.Now;
            }
            db.tTrackings.Add(tTracking);
            db.SaveChanges();
        }

        private void updateTracking(int lessionStatus, int lessionId, long customerId)
        {
            var trackingItem = db.tTrackings.Where(t => t.CustomerId == customerId && t.LessionId == lessionId).FirstOrDefault();
            if (trackingItem.LessionStatus == 0)
            {
                var updateTrackRecord = db.tTrackings.Where(t => t.CustomerId == customerId && t.LessionId == lessionId).FirstOrDefault();
                updateTrackRecord.LessionStatus = lessionStatus;
                db.SaveChanges();
            }
            else
            {
                if (trackingItem.LessionStatus == 1)
                {
                    if (lessionId > 1)
                    {
                        var trackingItemPrevious = db.tTrackings.Where(t => t.CustomerId == customerId && t.LessionId == lessionId - 1).FirstOrDefault();
                        if (trackingItemPrevious.ActiveDate != null)
                        {
                            DateTime readDatePreviousLession = trackingItemPrevious.ActiveDate ?? DateTime.Now;
                            if (trackingItemPrevious.LessionStatus == 3 && subtract2Datetime(readDatePreviousLession, DateTime.Now) >= 24)
                            {
                                var updateTrackRecord = db.tTrackings.Where(t => t.CustomerId == customerId && t.LessionId == lessionId).FirstOrDefault();
                                updateTrackRecord.LessionStatus = 2;
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
        }






        private int getLevelPurchased(long custId)
        {
            int result = 0;

            string ordersJson = ShopifyServices.getOrdersByCustomer(custId);
            var ordersObj = Ultils.AllChildren(JObject.Parse(ordersJson))
            .First(c => c.Type == JTokenType.Array && c.Path.Contains("orders"))
            .Children<JObject>();

            var proCodeList = (from pr in db.tProducts select new { pr.ProductCode, pr.ProductLevel, pr.LevelType });
            for (int i = 0; i < proCodeList.Count(); i++)
            {
                foreach (JObject order in ordersObj)
                {
                    string fulfillments = order["fulfillments"].ToString();
                    JArray fulfillmentsJArr = JArray.Parse(fulfillments);
                    foreach (JObject fulfillment in fulfillmentsJArr.Children<JObject>())
                    {
                        string status = fulfillment["status"].ToString();
                        if ("success".Equals(status))
                        {
                            string line_items = fulfillment["line_items"].ToString();
                            JArray line_itemsJArr = JArray.Parse(line_items);
                            foreach (JObject line_item in line_itemsJArr.Children<JObject>())
                            {
                                long product_id = Convert.ToInt64(line_item["product_id"].ToString());
                                var proItem = proCodeList.ToList()[i];

                                if (product_id == proItem.ProductCode)
                                {
                                    if(proItem.ProductLevel > result)
                                    {
                                        if (proItem.LevelType == 1)
                                        {
                                            result = proItem.ProductLevel;
                                        }else
                                        {
                                            if(result == 0)
                                            {
                                                result = proItem.ProductLevel;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        private double subtract2Datetime(DateTime start, DateTime end)
        {
            TimeSpan? difference = end - start;
            return difference.Value.TotalHours;
        }



    }
}