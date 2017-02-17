using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PLA.Data;
using PLA.API;
using PLA.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using PLA.App_Start;

namespace PLA.Controllers
{
    public class HomeController : Controller
    {
        PLAApiController api = new PLAApiController();
        [SessionExpireUser]
        public ActionResult Index()
        {
            //string ip = Request.UserHostAddress;
            //// string ip = Request.ServerVariables["REMOTE_ADDR"];
            //IPAddress myIP = IPAddress.Parse(ip);
            //IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
            return View();
        }
        [HttpPost]
        public ActionResult Index(PlagiarismData model,string num)
        {
            PLAPost obj = new PLAPost();
            model = obj.GetResult(model);
            return View(model);
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegistrationData model)
        {
            model.Status = true;
            model.Date = DateTime.Now;
            model.Password = api.encrypt(model.Password);
            api.AddRegistaration(model);
            return View();
        }
        public ActionResult CompareText(Actual_Results model)
        {
            if (model.st != null)
            {
                string str = "";
                string s = "";
                using (WebClient client = new WebClient())
                {
                    s = client.DownloadString(model.url);

                    string rx = "<head[^>]*>((.|\n)*?)head>";
                    Regex r = new Regex(rx);
                    MatchCollection matches = r.Matches(s);
                    string s1, s2;
                    Match m = matches[0];
                    s1 = m.Value;
                    string[] Head = s1.Split('>');
                    string demoHeader = Head[0] + ">";
                    s2 = Head[0] + ">" + "<base href=\"" + model.url + "\">";
                    s = s.Replace(demoHeader, s2);
                    //string Script = client.DownloadString("http://localhost:54854/Home/Scripting");
                    string Script = client.DownloadString("http://182.74.244.188/PLA/Home/Scripting");
                    s = WebUtility.HtmlDecode(s);
                    model.st = WebUtility.HtmlDecode(model.st);
                    s = s.Replace("&#39;", "'");
                    s = s.Replace("  ", " ");
                    string whole_str = "var whole_str = \"" + model.st + "\";";
                    string str_parts = "";
                    string[] StrArray = model.st.Split('.');
                    foreach (var item in StrArray)
                    {
                        if (item != "")
                        {
                            if (str_parts == "")
                            {
                                str_parts = "\"" + item + "\"";
                            }
                            else
                            {
                                str_parts = str_parts + "," + "\"" + item + "\"";
                            }
                            string temp = item + ".";
                            if (s.Contains(temp))
                            {
                                s = s.Replace(temp, "<b class='this-class-is-added-by-automated-search'>" + temp + "</b>");
                            }
                        }
                    }
                    //string[] StrArray2 = model.st.Split();
                    //foreach (var item in StrArray2)
                    //{
                    //    if (item != "")
                    //    {
                    //        string temp = " " + item + " ";
                    //        if (s.Contains(temp))
                    //        {
                    //            s = s.Replace(temp, "<b class='this-class-is-added-by-automated-search'>" + temp + "</b>");
                    //        }
                    //    }
                    //}
                    str_parts = "str_parts = [" + str_parts + "];";

                    //  Script = Script.Replace("var whole_str = '';", whole_str);
                    //  Script = Script.Replace("str_parts = [];", str_parts);
                    Script = Script + "</head>";
                    s = s.Replace("</head>", Script);
                    //var index1 = s.IndexOf("</head>");
                    //if (index1 >= 0)
                    //{
                    //    s = s.Insert(index1 + "</head>".Length, Script);
                    //}

                    ViewBag.Src = s;
                }
                return Content(s, "text/html");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public PartialViewResult Scripting()
        {
            return PartialView();
        }

        public ActionResult Login()
        {
            ViewBag.name = "sunny patel";
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            var result = api.GetDetailRegistartionByUsernameorPasword(UserName.Trim(), api.encrypt(Password));
            if (result != null)
            {

                Session["Name"] = result.FirstName + " " + result.LastName;
                Session["Id"] = result.Id;
                HttpCookie cookie = new HttpCookie("User");

                cookie.Values["Name"] = Session["Name"].ToString();
                cookie.Values["Id"] = Session["Id"].ToString();
              
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index");
            }
            else
            {
                Response.Write("<script>alert('Please enter valid credentials.')</script>");
            }
            return View();
        }

        public ActionResult Signout()
        {
            var c = new HttpCookie("User");
            c.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(c);

            Session["Name"] = null;
            Session["Id"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult Testpage()
        {
            return View();
        }
        public ActionResult Angularpage()
        {
            return View();
        }
    }
}
