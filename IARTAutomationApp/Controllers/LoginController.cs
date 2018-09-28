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
                var sa = db.SuperAdmins.Find(1);
                if (sa.LoginName.Equals(fc["EmployeeCode"]) && sa.Password.Equals(fc["Password"]))
                {
                    FormsAuthentication.SetAuthCookie(sa.LoginName, true);
                    return RedirectToAction("Dashboard", "SuperAdmin");
                }
                int empCode = 0; int.TryParse(fc["EmployeeCode"], out empCode);
                string password = fc["Password"];
                UserMaster user = db.UserMasters.Where(u => u.EmployeeCode == empCode && u.Password.Equals(password)).FirstOrDefault();
                if (user != null)
                {
                    TempData["wel"] = "Your Login is Successful";
                    {
                        var userName = user.EmployeeCode.ToString();
                        var empdetail = (from a in db.EmployeeGIs where a.EmployeeCode == user.EmployeeCode select new { empic = a.EmployeePhoto, empname = a.First_Name }).FirstOrDefault();
                        Session["name"] = empdetail.empname;
                        Session["employeecode"] = user.EmployeeCode.ToString();
                        Session["User"] = user;

                        Session.Timeout = 60000;
                        string roletext = (from a in db.RoleMasters where a.RoleId == user.RoleId select a.RoleName).FirstOrDefault();
                        var role = (from a in db.UserMasters where a.EmployeeCode == user.EmployeeCode select a.RoleId).FirstOrDefault();
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
                        catch { }
                        FormsAuthentication.SetAuthCookie(userName, true);
                        return RedirectToAction("index", "Home");
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
            return RedirectToAction("AdminLogin", "Login");
        }
    }
}