using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IARTAutomationApp.Models;
using System.Web.Security;

namespace IARTAutomationApp.Controllers
{
    public class LoginController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();



        public ActionResult AdminLogin()
        {
            return View();
        }
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AdminLogin(string returnUrl, FormCollection fc)
        {
            if (!string.IsNullOrEmpty(fc["EmployeeCode"]) && !string.IsNullOrEmpty(fc["Password"]))
            {
                if (db.SuperAdmins.Any(sa => sa.LoginName.Equals(fc["EmployeeCode"]) && sa.Password.Equals(fc["Password"]) && sa.Id == 1))
                {
                    return RedirectToAction("", "");
                }
                int empCode = 0; int.TryParse(fc["EmployeeCode"], out empCode);
                var user = (db.UserMasters.Where(emp => emp.EmployeeCode == empCode && emp.Password == fc["Password"])).FirstOrDefault();
                if (user != null)
                {
                    TempData["wel"] = "Your Login is Successful";
                    {
                        var userName = user.EmployeeCode.ToString();
                        var empdetail = (from a in db.EmployeeGIs where a.EmployeeCode == user.EmployeeCode select new { empic = a.EmployeePhoto, empname = a.First_Name }).FirstOrDefault();
                        Session["name"] = empdetail.empname;
                        Session["employeecode"] = user.EmployeeCode.ToString();
                        Session.Timeout = 60000;
                        string roletext = (from a in db.RoleMasters where a.RoleId == user.RoleId select a.RoleName).FirstOrDefault();
                        var role = (from a in db.UserMasters where a.EmployeeCode == user.EmployeeCode select a.RoleId).SingleOrDefault();
                        var userType = (from a in db.RoleMasters where a.RoleId == role select a.RoleName).FirstOrDefault();
                        Session["Role"] = userType;
                        try
                        {
                            if (userType.Contains("Admin"))
                            {
                                EmployeeAttendance attendence = new EmployeeAttendance();
                                attendence.EmployeeCode = user.EmployeeCode;
                                attendence.OnDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                                string timelogin = String.Format("{0:T}", DateTime.Now);
                                attendence.LoginTime = Convert.ToDateTime(timelogin);  // "4:05 PM";
                                attendence.LogoutTime = Convert.ToDateTime(timelogin);
                                db.EmployeeAttendances.Add(attendence);
                                db.SaveChanges();
                            }
                        }
                        catch (System.Exception ext)
                        {
                        }

                        if (userType == "Super Level Admin" || userType == "Admin" || userType == "Super Admin")
                        {
                            FormsAuthenticationTicket authTicket = new
            FormsAuthenticationTicket(1, //version
            userName, // user name
            DateTime.Now,             //creation
            DateTime.Now.AddDays(2), //Expiration (you can set it to 1 month
            true,  //Persistent
            user.EmployeeCode.ToString()); // additional informations

                            FormsAuthentication.SetAuthCookie(userName, true);
                            //authCookie.Expires = authTicket.Expiration
                            //return RedirectToAction("index", new RouteValueDictionary(new { controller = "Admin", action = "index", Id = UrlParameter.Optional }));
                            //return RedirectToAction("/Home/index");
                            return RedirectToAction("index", "Home");
                        }

                        else if (userType == "HR Admin")
                        {

                            FormsAuthentication.SetAuthCookie(userName, true);
                            //return RedirectToAction("index", new RouteValueDictionary(new { controller = "Admin", action = "index", Id = UrlParameter.Optional }));
                            return RedirectToAction("index", "Home");
                        }
                        else if (userType == "Store Admin")
                        {

                            FormsAuthentication.SetAuthCookie(userName, true);
                            //return RedirectToAction("index", new RouteValueDictionary(new { controller = "Admin", action = "index", Id = UrlParameter.Optional }));
                            return RedirectToAction("index", "Home");
                        }
                        else if (userType == "Procurement Admin")
                        {

                            FormsAuthentication.SetAuthCookie(userName, true);
                            //return RedirectToAction("index", new RouteValueDictionary(new { controller = "Admin", action = "index", Id = UrlParameter.Optional }));
                            return RedirectToAction("index", "Home");
                        }


                        else
                        {

                            FormsAuthentication.SetAuthCookie(userName, true);
                            //return RedirectToAction("index", new RouteValueDictionary(new { controller = "Admin", action = "index", Id = UrlParameter.Optional }));
                            return RedirectToAction("UserIndex", "Home");
                        }

                    }
                }
                else
                {
                    TempData["wel"] = "Invalid UserName or Password";
                    return RedirectToAction("index", "Login");
                }
            }
            // else
            {
                TempData["wel"] = "Pls Enter UserName and Password";
                return RedirectToAction("index", "Login");
            }
        }
        public bool IsvalidUser(UserMaster user)
        {
            TempData["wel"] = string.Empty;
            bool isvalidUser = false;
            if (!string.IsNullOrEmpty(user?.UserName))
            {
                TempData["wel"] = "Your Login is Successful";
                isvalidUser = true;
            }
            else
            {
                TempData["wel"] = "Your Login is Successful";
                TempData["wel"] = "Pls Check your UserName & Password";
                isvalidUser = false;
            }
            return isvalidUser;
        }

        // GET: Login
        //public ActionResult Index()
        //{
        //    return View();
        //    //return RedirectToAction("Home/Index");


        //}
        //[HttpPost]
        //public ActionResult Index(int id)
        //{


        //    return RedirectToAction("Home/Index");


        //}


        [Route("Login/{index ?}")]
        public ActionResult index()
        {

            int role = (from a in db.UserMasters where a.UserName == User.Identity.Name select a.RoleId).SingleOrDefault();
            var usertype = (from b in db.RoleMasters where b.RoleId == role select b.RoleName).FirstOrDefault();
            //Pending,in-Progress,Confirmed,Completed
            if (usertype != "" && usertype != null && usertype == "Admin")
            {

                return RedirectToAction("Login/Index");
            }
            else
            {
                return RedirectToAction("AdminLogin", "Login");

            }
        }



        public ActionResult Logout()
        {
            UserMaster usermaster = new UserMaster();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            //RedirectToAction("Login/AdminLogin");
            return RedirectToAction("AdminLogin", "Login");
        }
    }
}