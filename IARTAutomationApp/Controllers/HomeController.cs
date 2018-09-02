using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IARTAutomationApp.Models;
using IARTAutomationApp.ViewModels;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace IARTAutomationApp.Controllers
{    
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class HomeController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        public ActionResult ComingSoonAccount()
        {
            return View();
        }
        public ActionResult ComingSoonAudit()
        {
            return View();
        }
        public ActionResult UserIndex()
        {
            //var NoofEmp = (from a in db.EmployeeGIs select a).ToList().Count();
            //ViewBag.NoOfStaff = NoofEmp;
            //DateTime extDateRange = DateTime.Now.Date.AddMonths(6);
            //var NearToRetirementCount = (from a in db.EmployeeGIs where a.DateOfRetirement <= extDateRange select a).ToList().Count();
            //@ViewBag.NeartoRetirement = NearToRetirementCount;
            return View();
        }
        public ActionResult Index()
        {
            
            if (Session.SessionID!="")
            {

                var NoofEmp = (from a in db.EmployeeGIs select a).ToList().Count();
                ViewBag.NoOfStaff = NoofEmp;
                DateTime extDateRange = DateTime.Now.Date.AddMonths(6);
                var NearToRetirementCount = (from a in db.EmployeeGIs where a.DateOfRetirement <= extDateRange select a).ToList().Count();
                ViewBag.NeartoRetirement = NearToRetirementCount;


                var TenderAssesment = (from a in db.PrequalificationScorings select a).ToList().Count();
                ViewBag.TenderAssesment = TenderAssesment;
                //return View();

                DateTime today = DateTime.Now.Date;

                var emponleave = (from a in db.LeaveApplications where a.LeaveFromDate >= today && a.LeaveToDate >= today && a.IsApproved == true select a).Count();
                var empdueforleave = (from a in db.LeaveApplications where a.LeaveFromDate > today && a.IsApproved == true select a).Count();

                var emponduty = (from a in db.EmployeeGIs select a).Count();
                ViewBag.emponleave = emponleave;
                ViewBag.empdueforleave = empdueforleave;
                emponduty = emponduty - (emponleave + empdueforleave);

                ViewBag.emponduty = emponduty;

                ViewBag.Store = (from a in db.StoreMasters select a).Count();

                ViewBag.Central = (from a in db.ItemMasters where a.StoreId == 1 select a).Count();
                ViewBag.Stationary = (from a in db.ItemMasters where a.StoreId == 2 select a).Count();
                ViewBag.Chemical = (from a in db.ItemMasters where a.StoreId == 3 select a).Count();
                ViewBag.Fertilizer = (from a in db.ItemMasters where a.StoreId == 4 select a).Count();



                ViewBag.JrStaff = (from a in db.EmployeeGIs where a.Cadre == "Junior" select a).Count();
                ViewBag.SrStaff = (from a in db.EmployeeGIs where a.Cadre == "Senior" select a).Count();
                ViewBag.NyscStaff = (from a in db.EmployeeGIs where a.Cadre == "NYSC Members" select a).Count();
                ViewBag.OthersStaff = (from a in db.EmployeeGIs where a.Cadre == "Others" select a).Count();


                List<GraphData> GraphDataList = new List<GraphData>();

                var results = (from tn in db.EmployeeGIs
                               group tn by tn.DateOfRetirement.Year into bGroup
                               orderby bGroup.Key
                               select new
                               {
                                   label = bGroup.Key,
                                   value = bGroup.Count()
                               }).ToList();
                ViewBag.DataPoints = JsonConvert.SerializeObject(results);



                var resultstender = (from tn in db.TenderOpenings
                                     group tn by tn.YearofProject into bGroup
                                     orderby bGroup.Key
                                     select new
                                     {
                                         label = bGroup.Key,
                                         value = bGroup.Count()
                                     }).ToList();
                ViewBag.DataTender = JsonConvert.SerializeObject(resultstender);


                ////////////////////////
                EmployeeAll empall = new EmployeeAll();

                empall.employeegi = (from a in db.EmployeeGIs select a).ToList();
                empall.employeeai = (from a in db.EmployeeAIs select a).ToList();
                empall.employeepi = (from a in db.EmployeePIs select a).ToList();
                return View(empall);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Login");

            }

         }

        public ActionResult DataSync()
        {
            try
            {
                BulkInsertToDataBase();
                TempData["datasync"] = "Data is Successfully restored on Server";
            }
            catch(Exception ext)
            { TempData["datasync"] = "Data Connection Error!,Please try again"; }
            return RedirectToAction("index", "Home");

        }

        public ActionResult MidLevelIndex()
        {

            if (Session.SessionID != "")
            {

                var NoofEmp = (from a in db.EmployeeGIs select a).ToList().Count();
                ViewBag.NoOfStaff = NoofEmp;
                DateTime extDateRange = DateTime.Now.Date.AddMonths(6);
                var NearToRetirementCount = (from a in db.EmployeeGIs where a.DateOfRetirement <= extDateRange select a).ToList().Count();
                ViewBag.NeartoRetirement = NearToRetirementCount;


                var TenderAssesment = (from a in db.PrequalificationScorings select a).ToList().Count();
                ViewBag.TenderAssesment = TenderAssesment;
                //return View();

                DateTime today = DateTime.Now.Date;

                var emponleave = (from a in db.LeaveApplications where a.LeaveFromDate >= today && a.LeaveToDate >= today && a.IsApproved == true select a).Count();
                var empdueforleave = (from a in db.LeaveApplications where a.LeaveFromDate > today && a.IsApproved == true select a).Count();

                var emponduty = (from a in db.EmployeeGIs select a).Count();
                ViewBag.emponleave = emponleave;
                ViewBag.empdueforleave = empdueforleave;
                emponduty = emponduty - (emponleave + empdueforleave);

                ViewBag.emponduty = emponduty;

                ViewBag.Store = (from a in db.StoreMasters select a).Count();

                ViewBag.Central = (from a in db.ItemMasters where a.StoreId == 1 select a).Count();
                ViewBag.Stationary = (from a in db.ItemMasters where a.StoreId == 2 select a).Count();
                ViewBag.Chemical = (from a in db.ItemMasters where a.StoreId == 3 select a).Count();
                ViewBag.Fertilizer = (from a in db.ItemMasters where a.StoreId == 4 select a).Count();



                ViewBag.JrStaff = (from a in db.EmployeeGIs where a.Cadre == "Junior" select a).Count();
                ViewBag.SrStaff = (from a in db.EmployeeGIs where a.Cadre == "Senior" select a).Count();
                ViewBag.NyscStaff = (from a in db.EmployeeGIs where a.Cadre == "NYSC Members" select a).Count();
                ViewBag.OthersStaff = (from a in db.EmployeeGIs where a.Cadre == "Others" select a).Count();


                List<GraphData> GraphDataList = new List<GraphData>();

                var results = (from tn in db.EmployeeGIs
                               group tn by tn.DateOfRetirement.Year into bGroup
                               orderby bGroup.Key
                               select new
                               {
                                   label = bGroup.Key,
                                   value = bGroup.Count()
                               }).ToList();
                ViewBag.DataPoints = JsonConvert.SerializeObject(results);



                var resultstender = (from tn in db.TenderOpenings
                                     group tn by tn.YearofProject into bGroup
                                     orderby bGroup.Key
                                     select new
                                     {
                                         label = bGroup.Key,
                                         value = bGroup.Count()
                                     }).ToList();
                ViewBag.DataTender = JsonConvert.SerializeObject(resultstender);


                ////////////////////////
                EmployeeAll empall = new EmployeeAll();

                empall.employeegi = (from a in db.EmployeeGIs select a).ToList();
                empall.employeeai = (from a in db.EmployeeAIs select a).ToList();
                empall.employeepi = (from a in db.EmployeePIs select a).ToList();
                return View(empall);
            }
            else
            {
                return RedirectToAction("AdminLogin", "Login");

            }


        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void BulkInsertToDataBase()
        {
            DataTable dttblName = new DataTable();
            string msgError = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["IARTDBNEWEntitiesSrc"].ConnectionString);
            SqlConnection condest = new SqlConnection(ConfigurationManager.ConnectionStrings["IARTDBNEWEntitiesDest"].ConnectionString);
            con.Open();
            condest.Open();

            SqlDataAdapter adptbl = new SqlDataAdapter("select * from sys.tables order by object_id", con);
            adptbl.Fill(dttblName);

            foreach (DataRow drname in dttblName.Rows)

            {
                //if (drname[0].ToString() != "EmployeeMI" && drname[0].ToString() != "EmployeeSI" && drname[0].ToString() != "EmployeeAI" && drname[0].ToString() != "EmployeePI")
                if (drname[0].ToString() != "EmployeeGI")

                    {
                    try
                    {
                        string sqlTrunc1 = "TRUNCATE TABLE " + drname[0].ToString();
                        SqlCommand cmd1 = new SqlCommand(sqlTrunc1, condest);
                        cmd1.ExecuteNonQuery();
                    }
                    catch (Exception ext)
                    {
                        msgError = ext.Message.ToString();
                    }
            }
                }
               /////////
            try
            {
                string sqlTrunc11 = "delete from  EmployeeGI";
                SqlCommand cmd11 = new SqlCommand(sqlTrunc11, condest);
                cmd11.ExecuteNonQuery();
            }
            catch(Exception ext) { }
            //////////
            foreach (DataRow drname in dttblName.Rows)
            {
                 DataTable dt1 = new DataTable();

                    SqlDataAdapter adp1 = new SqlDataAdapter("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('dbo." + drname[0].ToString() + "')", con);
                    //creating object of SqlBulkCopy  
                    adp1.Fill(dt1);

                    adp1.Dispose();
                    adp1 = new SqlDataAdapter("select * from " + drname[0].ToString() + "", con);
                    DataTable dtdata1 = new DataTable();
                    adp1.Fill(dtdata1);

                    SqlBulkCopy objbulk1 = new SqlBulkCopy(condest);
                    //assigning Destination table name  
                    objbulk1.DestinationTableName = drname[0].ToString();
                    //Mapping Table column  
                    foreach (DataRow dr in dt1.Rows)
                    {
                        objbulk1.ColumnMappings.Add(dr[0].ToString(), dr[0].ToString());
                    }
                //inserting bulk Records into DataBase  
                try
                {
                    objbulk1.WriteToServer(dtdata1);
                }
                catch(Exception ext)
                { }

                }
            //}
            ///////////////
            //string sqlTrunc = "TRUNCATE TABLE EmployeeGI" ;
            //SqlCommand cmd = new SqlCommand(sqlTrunc, condest);
            //cmd.ExecuteNonQuery();

            //DataTable dt = new DataTable();

            //SqlDataAdapter adp = new SqlDataAdapter("SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('dbo.EmployeeGI')", con);
            ////creating object of SqlBulkCopy  
            //adp.Fill(dt);

            //adp.Dispose();
            //adp = new SqlDataAdapter("select * from EmployeeGI", con);
            //DataTable dtdata = new DataTable();
            //adp.Fill(dtdata);

            //SqlBulkCopy objbulk = new SqlBulkCopy(condest);
            ////assigning Destination table name  
            //objbulk.DestinationTableName = "EmployeeGI";
            ////Mapping Table column  
            //foreach (DataRow dr in dt.Rows)
            //{
            //    objbulk.ColumnMappings.Add(dr[0].ToString(), dr[0].ToString());
            //}
            ////inserting bulk Records into DataBase   
            //objbulk.WriteToServer(dtdata);
            //////////////
            con.Close();
            condest.Close();
        }
    }
}