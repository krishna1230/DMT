using PLA.API;
using PLA.App_Start;
using PLA.Data;
using PLA.Models;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLA.Areas.HelpPage.Controllers
{
    public class LoginController : Controller
    {

        PLAApiController api = new PLAApiController();
        //
        // GET: /HelpPage/Login/
        [SessionExpireAdmin]
        public ActionResult Dashboard()
        {          
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
      
        public ActionResult AdminLogin()
        {
            Session.Clear();
            Session.Abandon();

            HttpCookie cookie = Request.Cookies["AdminLogin"];

            if ((cookie != null) && (cookie.Value != ""))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult AdminLogin(AdminLoginModel model, bool remember_me)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "";
                int flag = 0;
                var admin = api.GetAllAdminList();

                foreach (var user in admin)
                {
                    string str = api.decrypt(user.Password);
                    if (api.decrypt(user.Password) == model.Password && user.EmailId == model.EmailId)
                    {
                        if (user.Status == true)
                        {
                            flag = 1;
                            Session["EmailId"] = user.EmailId;
                            Session["Password"] = (user.Password);
                            Session["AdminId"] = user.Id;
                            Session["AdminName"] = user.Name;

                            break;
                        }
                        else
                        {
                            Response.Write(@"<script language='javascript'>alert('You Are Not an Active Admin.');</script>");
                            return View();
                        }
                    }
                }
                if (remember_me == true)
                {
                    HttpCookie cookie = new HttpCookie("AdminLogin");

                    cookie.Values["EmailId"] = Session["EmailId"].ToString();
                    cookie.Values["Password"] = Session["Password"].ToString();
                    cookie.Values["AdminId"] = Session["AdminId"].ToString();
                    cookie.Values["AdminId"] = Session["AdminName"].ToString();
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);
                }

                if (flag == 1)
                {
                    return RedirectToAction("Index", "Login");

                }
                else
                {
                    Response.Write(@"<script language='javascript'>alert('User Name or Password is Incorrect.');</script>");
                    return View();
                }

            }
            else
            {


                return View();

            }

        }
        public ActionResult AdminRegistration(int Id = 0)
        {
        
            if (Id == 0)
            {

                return View();
            }
            else
            {
                var admin = api.GetAdminDetail(Id);


                return View(admin);
            }

        }
        [HttpPost]
        public ActionResult AdminRegistration(LoginAdminData model, HttpPostedFileBase AdminPhoto)
        {
          
            if (ModelState.IsValid)
            {
                if (AdminPhoto != null && AdminPhoto.ContentLength > 0)
                {
                    string url = string.Empty;
                    string fileName = Guid.NewGuid().ToString();
                    string path = string.Empty;
                    path = Path.Combine(Constant.Path, fileName.ToString() + "." + Path.GetExtension(AdminPhoto.FileName).Substring(1));
                    AdminPhoto.SaveAs(path);
                    url = fileName + "." + Path.GetExtension(AdminPhoto.FileName).Substring(1);
                    model.AdminImage = url;
                }
                else
                {
                    model.AdminImage = "Avtar.png";
                }
                model.Status = true;
                model.CreatedDate = System.DateTime.Today;
                model.Password = api.encrypt(model.Password);
                api.AdminRegister(model);
                return View();
            }
            else
            {
                return View();
            }
        }
        public PartialViewResult AdminList()
        {
            var admin = api.GetAllAdminList();
            return PartialView(admin);
        }
        public ActionResult EditAdmin(int Id)
        {
          
            var admin = api.GetAdminDetail(Id);
            return View(admin);

        }
        [HttpPost]
        public ActionResult EditAdmin(LoginAdminData model, HttpPostedFileBase AdminPhoto)
        {
            
            if (ModelState.IsValid)
            {

                if (AdminPhoto != null && AdminPhoto.ContentLength > 0)
                {
                    string url = string.Empty;
                    string fileName = Guid.NewGuid().ToString();
                    string path = string.Empty;
                    path = Path.Combine(Constant.Path, fileName.ToString() + "." + Path.GetExtension(AdminPhoto.FileName).Substring(1));
                    AdminPhoto.SaveAs(path);
                    url = fileName + "." + Path.GetExtension(AdminPhoto.FileName).Substring(1);
                    model.AdminImage = url;
                }
                else
                {
                    var admin = api.GetAdminDetail(model.Id);
                    model.AdminImage = admin.AdminPhoto;
                }
                api.EditAdminRegister(model);
                return View();
            }
            else
            {
                return View();
            }
        }
        public ActionResult DeleteAdmin(int Id)
        {
            api.DeleteAdmin(Id);
            return RedirectToAction("AdminRegistration");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                LoginAdminData result;
                result = api.GetAdminDetailByEmail(model.EmailId);
                string Subject = "PLAGIARISM Admin Credentials";

                string Body = "Your Credentials as follows : <br/><br/>Email : " + model.EmailId.Trim() + "<br/><br/>Password : "
                    + api.decrypt(result.Password) + "<br/><br/><a href='http://localhost:60084/Admin/Login/AdminLogin'>Click Here</a> To Login.";
                string To = model.EmailId.Trim();
                api.SendEmail(Subject, Body, To);
                return RedirectToAction("AdminLogin", "Login");
            }
            else
            {
                return View();
            }
        }
        [SessionExpireAdmin]
        public ActionResult Signout()
        {
            HttpCookie cookie = Request.Cookies["AdminLogin"];

            if ((cookie != null) && (cookie.Value != ""))
            {
                var c = new HttpCookie("AdminLogin");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);

            }
            return RedirectToAction("AdminLogin", "Login");
        }
        [SessionExpireAdmin]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [SessionExpireAdmin]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                LoginAdminData result;
                result = api.GetAdminDetail(Convert.ToInt32(Session["AdminId"]));
                if (result != null)
                {
                    if (api.encrypt(model.OldPassword) == result.Password)
                    {
                        result.Password = api.encrypt(model.NewPassword);
                        api.ChangeAdminPassword(result);
                        Session.Clear();
                        Session.Abandon();
                        return RedirectToAction("AdminLogin", "Login");
                    }
                    else
                    {
                        Response.Write(@"<script language='javascript'>alert('Password is Incorrect.');</script>");
                        return View();
                    }

                }
                else
                {
                    Response.Write(@"<script language='javascript'>alert('Admin not Exists.');</script>");
                    return View();
                }

            }
            else
            {
                return View();
            }
        }
        [SessionExpireAdmin]
        public ActionResult AdminProfile()
        {
            var admindata = api.GetAdminDetail(Convert.ToInt32(Session["AdminId"]));
            return View(admindata);
        }
        [SessionExpireAdmin]
        [HttpPost]
        public ActionResult AdminProfile(LoginAdminData model, HttpPostedFileBase AdminPhoto)
        {
            ModelState["Password"].Errors.Clear();
            ModelState["ConfirmPassword"].Errors.Clear();
          
            if (ModelState.IsValid)
            {

                if (AdminPhoto != null && AdminPhoto.ContentLength > 0)
                {
                    string url = string.Empty;
                    string fileName = Guid.NewGuid().ToString();
                    string path = string.Empty;
                    path = Path.Combine(Constant.Path, fileName.ToString() + "." + Path.GetExtension(AdminPhoto.FileName).Substring(1));
                    AdminPhoto.SaveAs(path);
                    url = fileName + "." + Path.GetExtension(AdminPhoto.FileName).Substring(1);
                    model.AdminImage = url;
                }
                else
                {
                    var admin = api.GetAdminDetail(model.Id);
                    model.AdminImage = admin.AdminPhoto;
                }
                api.EditAdminRegister(model);
                return RedirectToAction("AdminProfile", "Login");
            }
            else
            {
                return RedirectToAction("AdminProfile", "Login");

            }
        }
        [SessionExpireAdmin]
        public ActionResult Editstatus(int Id)
        {
            api.EditAdminStatus(Id);
            return RedirectToAction("AdminRegistration");
        }
        [HttpPost]
        public ActionResult SendQuickMail(FormCollection frm)
        {

            api.SendEmail(frm["subject"], frm["message"], frm["emailto"]);
            return RedirectToAction("Dashboard");
        }

        public ActionResult Userlist()
        {
            var result = api.GetAllRegistartion();
            return View(result);
        }
    }
}
