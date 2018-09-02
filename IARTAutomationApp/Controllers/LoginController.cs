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
        //[ValidateAntiForgeryToken]
        public ActionResult AdminLogin(UserMaster user, string returnUrl)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, false, shouldLockout: false);
            var result = IsvalidUser(user);


            if (result == true)
            {

                var userName = user.EmployeeCode.ToString(); //(from a in db.UserMasters where a.EmployeeCode == user.EmployeeCode select a.UserName).SingleOrDefault();
                Session["email"] = (from a in db.UserMasters where a.EmployeeCode == user.EmployeeCode select a.EmailId).SingleOrDefault(); ;
                Session["employeecode"] = user.EmployeeCode.ToString();
                Session["Role"] = (from a in db.RoleMasters where a.RoleId == user.RoleId select a.RoleName).FirstOrDefault();
             
                var empdetail = (from a in db.EmployeeGIs where a.EmployeeCode == user.EmployeeCode select new { empic = a.EmployeePhoto, empname = a.First_Name }).FirstOrDefault();
                Session["profilepic"] = empdetail.empic;
                Session["name"] = empdetail.empname;

                Session.Timeout = 60000;

                //Session.Peek("name");
                //Session.Peek("employeecode");
                //Session.Peek("Role");
                //Session.Peek("profilepic");
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

                //else if (userType == "Mid Level Admin")
                //{
                //    FormsAuthentication.SetAuthCookie(userName, true);

                //    //return RedirectToAction("index", new RouteValueDictionary(new { controller = "Admin", action = "index", Id = UrlParameter.Optional }));
                //    return RedirectToAction("index", "Home");
                //}

                //else if (userType == "Low Level Admin")
                //{

                //    FormsAuthentication.SetAuthCookie(userName, true);
                //    //return RedirectToAction("index", new RouteValueDictionary(new { controller = "Admin", action = "index", Id = UrlParameter.Optional }));
                //    return RedirectToAction("index", "Home");
                //}

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
            else
            {
                return RedirectToAction("index", "Login");
            }
            //switch (result)
            //{
            //    case true:
            //        return RedirectToLocal(returnUrl);
            //    //case SignInStatus.LockedOut:
            //    //    return View("Lockout");
            //    //case SignInStatus.RequiresVerification:
            //        //return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, false});
            //    case false:
            //        return RedirectToLocal("admin/login");
            //    default:
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        return View(model);
            //}
        }
        public bool IsvalidUser(UserMaster user)
        {
            TempData["wel"] = "";
            var userName = "";
            bool flag = false;
            if (user.Password != null && user.Password != "")
            {
                userName = (from a in db.UserMasters where a.EmployeeCode == user.EmployeeCode && a.Password == user.Password select a.UserName).SingleOrDefault();
                user.UserName = userName;
                if (userName != null && userName != "0")
                {
                    TempData["wel"] = "Your Login is Successful";
                    userName = "";
                    flag = true;
                }
                else
                {
                    TempData["wel"] = "Pls Check your UserName & Password";
                    flag = false;
                }
                //userName = "";
            }
            return flag;
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