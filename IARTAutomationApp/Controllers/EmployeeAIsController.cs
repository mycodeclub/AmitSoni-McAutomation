using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IARTAutomationApp.Models;
using IARTAutomationApp.ViewModels;
using System.Data.SqlClient;

namespace IARTAutomationApp.Controllers
{
    public class EmployeeAIsController : Controller
    {
        private IARTDBNEWEntities db = new IARTDBNEWEntities();

        [HttpPost]

        public JsonResult Edit(EmployeeAI obj, List<EmpAIAssociation> ObjAssociation, List<EmpAIConference> ObjConferenece)
        {
            int insertedRecords = 0;
            try
            {
                var id = (from a in db.EmployeeAIs where a.EmployeeCode == obj.EmployeeCode select a.EmployeeAIId).FirstOrDefault();
                var idgI = (from a in db.EmployeeGIs where a.EmployeeCode == obj.EmployeeCode select a.EmployeeGIId).FirstOrDefault();
                EmployeeGI objempGi = db.EmployeeGIs.Find(idgI);
                EmployeeAI objdest = db.EmployeeAIs.Find(id);

                objdest.EmployeeCode = obj.EmployeeCode;
                objdest.InstitutionAttended1 = obj.InstitutionAttended1;
                objdest.InstitutionAttended2 = obj.InstitutionAttended2;
                objdest.InstitutionAttended3 = obj.InstitutionAttended3;
                objdest.Qualification1 = obj.Qualification1;
                objdest.Qualification2 = obj.Qualification2;
                objdest.Qualification3 = obj.Qualification3;
                objdest.YearOfGraduation1 = obj.YearOfGraduation1;
                objdest.YearOfGraduation2 = obj.YearOfGraduation2;
                objdest.YearOfGraduation3 = obj.YearOfGraduation3;
                objdest.CreatedDate = objdest.CreatedDate;
                objdest.IsDeleted = objdest.IsDeleted;
                objdest.ConferenceAttendedDate = DateTime.Now.Date;
                objdest.ConferenceAttendedName = "NA";
                objdest.ConferenceAttendedTitle = "NA";
                objdest.ProfessionalAssociationsDate = DateTime.Now.Date;
                objdest.ProfessionalAssociationsIdNo = "1";
                objdest.ProfessionalAssociationsTitle = "NA";
                objdest.EmployeeGI = objempGi;
                db.SaveChanges();
                int i = (from a in db.EmployeeAIs where a.EmployeeCode == obj.EmployeeCode select a).Count();
                if (i == 0)
                    //entities.EmployeeAIs.Add(obj);

                    //Check for NULL.
                    if (ObjAssociation == null)
                    {
                        ObjAssociation = new List<EmpAIAssociation>();
                    }

                //Loop and insert records.
                foreach (EmpAIAssociation item in ObjAssociation)
                {
                    EmpAIAssociation Existed_Emp = db.EmpAIAssociations.Find(item.AssociationsId);
                    Existed_Emp.IDnumber = item.IDnumber;
                    Existed_Emp.Title = item.Title;
                    Existed_Emp.AttendedDate = item.AttendedDate;
                    Existed_Emp.IsDeleted = false;

                }
                db.SaveChanges();

                if (ObjConferenece == null)
                {
                    ObjConferenece = new List<EmpAIConference>();
                }

                //Loop and insert records.
                foreach (EmpAIConference item in ObjConferenece)
                {
                    EmpAIConference Existed_Emp = db.EmpAIConferences.Find(item.ConferenceId);
                    Existed_Emp.Name = item.Name;
                    Existed_Emp.Title = item.Title;
                    Existed_Emp.AttendedDate = item.AttendedDate;
                    Existed_Emp.IsDeleted = false;
                }
                db.SaveChanges();

                insertedRecords = id;
            }

            catch (Exception ext)
            {

            }
            return Json(insertedRecords);
        }

        [HttpPost]
        public JsonResult InsertCustomers(EmployeeAI obj, List<EmpAIAssociation> ObjAssociation, List<EmpAIConference> ObjConferenece)
        {
            int insertedRecords = 0;
            try
            {
                using (IARTDBNEWEntities entities = new IARTDBNEWEntities())
                {

                    int i = (from a in entities.EmployeeAIs where a.EmployeeCode == obj.EmployeeCode select a).Count();
                    if (i == 0)
                        entities.EmployeeAIs.Add(obj);

                    //Check for NULL.
                    if (ObjAssociation == null)
                    {
                        ObjAssociation = new List<EmpAIAssociation>();
                    }

                    //Loop and insert records.
                    foreach (EmpAIAssociation item in ObjAssociation)
                    {
                        entities.EmpAIAssociations.Add(item);
                    }


                    if (ObjConferenece == null)
                    {
                        ObjConferenece = new List<EmpAIConference>();
                    }

                    //Loop and insert records.
                    foreach (EmpAIConference item in ObjConferenece)
                    {
                        entities.EmpAIConferences.Add(item);
                    }

                    insertedRecords = entities.SaveChanges();
                }
            }
            catch (Exception ext)
            {

            }
            return Json(insertedRecords);

        }

        [HttpPost]
        public JsonResult AutoEmployeeCode(string Prefix)
        {
            var user = (UserMaster)Session["User"];

            //Note : you can bind same list from database  
            ViewBag.emp = new SelectList(db.EmployeeGIs.Where(e => e.CustomerId == user.CustomerId), "EmployeeCode", "EmployeeCode").ToList();
            List<SelectListItem> objlist = ViewBag.emp;
            //Searching records from list using LINQ query  
            var emplist = (from N in objlist
                           where N.Value.ToString().StartsWith(Prefix)
                           select new { N.Value });
            return Json(emplist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserIndex()
        {
            try
            {
                int empcode = Convert.ToInt32(@Session["employeecode"]);



                var employeeAIs = db.EmployeeAIs.Where(a => a.EmployeeCode == empcode).ToList();
                return View(employeeAIs.ToList());

                //if(     User.Identity.Name)


            }
            catch (Exception ext)
            {
                return RedirectToAction("index", "Login");

            }
            return View();
        }

        public ActionResult FullDetails(int? id)
        {
            EmployeeAll empall = new EmployeeAll();

            empall.employeegi = (from a in db.EmployeeGIs where a.EmployeeCode == id select a).ToList();
            empall.employeeai = (from a in db.EmployeeAIs where a.EmployeeCode == id select a).ToList();
            empall.employeemi = (from a in db.EmployeeMIs where a.EmployeeCode == id select a).ToList();
            empall.employeesi = (from a in db.EmployeeSIs where a.EmployeeCode == id select a).ToList();
            empall.employeepi = (from a in db.EmployeePIs where a.EmployeeCode == id select a).ToList();
            empall.employeeassociation = (from a in db.EmpAIAssociations where a.EmployeeCode == id select a).ToList();
            empall.employeeconference = (from a in db.EmpAIConferences where a.EmployeeCode == id select a).ToList();

            empall.employeeleaveledger = (from a in db.LeaveLedgers where a.EmployeeCode == id select a).ToList();

            return View(empall);

        }
        // GET: EmployeeAIs
        //[HttpPost, ActionName("MidLevelIndex")]
        public ActionResult Index()
        {
            var user = (UserMaster)Session["User"];
            var employeeAIs = db.EmployeeAIs.Where(e => e.CustomerId == user.CustomerId).Include(e => e.EmployeeGI);
            ViewBag.NoOfStaff = employeeAIs.Count();
            return View(employeeAIs.ToList());
        }

        public ActionResult MidLevelIndex()
        {
            var NoofEmp = (from a in db.EmployeeAIs select a).ToList().Count();
            ViewBag.NoOfStaff = NoofEmp;
            var employeeAIs = db.EmployeeAIs.Include(e => e.EmployeeGI);
            return View(employeeAIs.ToList());
        }

        // GET: EmployeeAIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAI employeeAI = db.EmployeeAIs.Find(id);
            if (employeeAI == null)
            {
                return HttpNotFound();
            }
            return View(employeeAI);
        }

        // GET: EmployeeAIs/Create
        public ActionResult Create()
        {
            var user = (UserMaster)Session["User"];

            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode");
            ///////


            ///////
            int year = DateTime.Now.Year;
            var list = new List<int>();
            for (int i = 1950; i <= year; i++)
            {
                list.Add(i);
            }
            List<SelectListItem> YearOfGraduation1 = new List<SelectListItem>();
            YearOfGraduation1.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1950; i <= year; i++)
            {
                YearOfGraduation1.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.YearOfGraduation1s = new SelectList(YearOfGraduation1, "Text", "Text");
            ViewBag.YearOfGraduation2s = new SelectList(YearOfGraduation1, "Text", "Text");
            ViewBag.YearOfGraduation3s = new SelectList(YearOfGraduation1, "Text", "Text");

            //List<QualificationMaster> QualificationList = new List<QualificationMaster>();
            //QualificationList = (from a in db.QualificationMasters select a).ToList();
            ////ViewBag.CountryList = CountryList;
            //ViewBag.Qualification1 = new SelectList(QualificationList, "QualificationName", "QualificationName");
            //ViewBag.Qualification2 = new SelectList(QualificationList, "QualificationName", "QualificationName");
            //ViewBag.Qualification3 = new SelectList(QualificationList, "QualificationName", "QualificationName");

            ///////
            ViewBag.Qualification1s = new SelectList(db.QualificationMasters, "QualificationName", "QualificationName");
            ViewBag.Qualification2s = new SelectList(db.QualificationMasters, "QualificationName", "QualificationName");
            ViewBag.Qualification3s = new SelectList(db.QualificationMasters, "QualificationName", "QualificationName");


            return View();
        }

        // POST: EmployeeAIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,EmployeeAIId,EmployeeCode,InstitutionAttended1,InstitutionAttended2,InstitutionAttended3,Qualification1,Qualification2,Qualification3,YearOfGraduation1,YearOfGraduation2,YearOfGraduation3,ProfessionalAssociationsTitle,ProfessionalAssociationsIdNo,ProfessionalAssociationsDate,ConferenceAttendedName,ConferenceAttendedTitle,ConferenceAttendedDate,CreatedDate,IsDeleted")] EmployeeAI employeeAI)
        {
            try
            {
                EmpAIAssociation empa = new Models.EmpAIAssociation();
                EmpAIConference empc = new Models.EmpAIConference();
                if (ModelState.IsValid)
                {
                    // string strConnString = "IARTDBNEWEntities"; // get it from Web.config file  
                    // SqlTransaction objTrans = null;

                    using (System.Data.Entity.DbContextTransaction objTrans = db.Database.BeginTransaction())
                    {
                        {

                            int empidcount = (from a in db.EmployeeAIs where a.EmployeeCode == employeeAI.EmployeeCode select a).Count();
                            if (empidcount == 0)
                            {


                                employeeAI.CreatedDate = DateTime.Now.Date;
                                db.EmployeeAIs.Add(employeeAI);
                                db.SaveChanges();

                                //association
                                empa.AttendedDate = employeeAI.ProfessionalAssociationsDate;
                                empa.IDnumber = employeeAI.ProfessionalAssociationsIdNo;
                                empa.EmployeeCode = employeeAI.EmployeeCode;

                                empa.Title = employeeAI.ProfessionalAssociationsTitle;
                                empa.IsDeleted = false;
                                empa.CreatedDate = DateTime.Now.Date;
                                db.EmpAIAssociations.Add(empa);
                                db.SaveChanges();
                                //////Prof summary

                                empc.AttendedDate = employeeAI.ConferenceAttendedDate;
                                empc.Name = employeeAI.ConferenceAttendedName;
                                empc.EmployeeCode = employeeAI.EmployeeCode;
                                empc.Title = employeeAI.ConferenceAttendedTitle;
                                empc.IsDeleted = false;
                                empc.CreatedDate = DateTime.Now.Date;
                                db.EmpAIConferences.Add(empc);
                                db.SaveChanges();
                            }

                            try
                            {

                                objTrans.Commit();
                            }
                            catch (Exception)
                            {
                                objTrans.Rollback();
                            }
                            finally
                            {

                            }

                            ////////


                            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeAI.EmployeeCode);
                            TempData["successmsg"] = "Record is Successfully Added";
                            return RedirectToAction("Index");
                        }
                    }


                }
            }
            catch (Exception ext)
            {
                TempData["msg"] = "Record is not Added,Try again";

            }
            return View(employeeAI);
        }

        // GET: EmployeeAIs/Edit/5



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ///////
            int year = DateTime.Now.Year;
            var list = new List<int>();
            for (int i = 1950; i <= year; i++)
            {
                list.Add(i);
            }
            List<SelectListItem> YearOfGraduation1 = new List<SelectListItem>();
            //YearOfGraduation1.Add(new SelectListItem { Text = "Select", Value = "0" });
            for (int i = 1950; i <= year; i++)
            {
                YearOfGraduation1.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.YearOfGraduation1s = new SelectList(YearOfGraduation1, "Text", "Text");
            ViewBag.YearOfGraduation2s = new SelectList(YearOfGraduation1, "Text", "Text");
            ViewBag.YearOfGraduation3s = new SelectList(YearOfGraduation1, "Text", "Text");

            ///////
            ViewBag.Qualification1s = new SelectList(db.QualificationMasters, "QualificationName", "QualificationName");
            ViewBag.Qualification2s = new SelectList(db.QualificationMasters, "QualificationName", "QualificationName");
            ViewBag.Qualification3s = new SelectList(db.QualificationMasters, "QualificationName", "QualificationName");
            EmployeeAI employeeAI = db.EmployeeAIs.Find(id);
            if (employeeAI == null)
            {
                return HttpNotFound();
            }
            EmployeeAll empall = new EmployeeAll();

            var empid = (from a in db.EmployeeAIs where a.EmployeeAIId == id select a.EmployeeCode).FirstOrDefault();
            empall.employeeai = (from a in db.EmployeeAIs where a.EmployeeAIId == id select a).ToList();
            empall.employeeassociation = (from a in db.EmpAIAssociations where a.EmployeeCode == empid select a).ToList();
            empall.employeeconference = (from a in db.EmpAIConferences where a.EmployeeCode == empid select a).ToList();



            ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeAI.EmployeeCode);
            return View(empall);
            //return View(employeeAI);
        }

        // POST: EmployeeAIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "EmployeeAIId,EmployeeCode,InstitutionAttended1,InstitutionAttended2,InstitutionAttended3,Qualification1,Qualification2,Qualification3,YearOfGraduation1,YearOfGraduation2,YearOfGraduation3,ProfessionalAssociationsTitle,ProfessionalAssociationsIdNo,ProfessionalAssociationsDate,ConferenceAttendedName,ConferenceAttendedTitle,ConferenceAttendedDate,CreatedDate,IsDeleted")] EmployeeAI employeeAI)
        //{
        //    try { 
        //    if (ModelState.IsValid)
        //    {
        //        employeeAI.EmployeeAIId = (from a in db.EmployeeAIs where a.EmployeeCode == employeeAI.EmployeeCode select a.EmployeeAIId).FirstOrDefault();
        //        employeeAI.CreatedDate = DateTime.Now;
        //        employeeAI.IsDeleted = false;

        //        db.Entry(employeeAI).State = EntityState.Modified;
        //        db.SaveChanges();
        //     }
        //    ViewBag.EmployeeCode = new SelectList(db.EmployeeGIs, "EmployeeCode", "EmployeeCode", employeeAI.EmployeeCode);
        //        TempData["successmsg"] = "Record is Successfully Updated";
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ext)
        //    {
        //        TempData["msg"] = "Record is not Updated,Try again";

        //    }

        //    return View(employeeAI);
        //}

        // GET: EmployeeAIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeAI employeeAI = db.EmployeeAIs.Find(id);
            if (employeeAI == null)
            {
                return HttpNotFound();
            }
            return View(employeeAI);
        }

        // POST: EmployeeAIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                EmployeeAI employeeAI = db.EmployeeAIs.Find(id);
                db.EmployeeAIs.Remove(employeeAI);
                db.SaveChanges();
                TempData["successmsg"] = "Record is Successfully Deleted";
                return RedirectToAction("Index");
            }
            catch (Exception ext)
            {
                TempData["msg"] = "Record is not Deleted,Try again";

            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [System.Web.Services.WebMethod]
        public static string loadFields(string fields, string table)
        {
            string sConnString = "Data Source=DNA;Persist Security Info=False;" +
                "Initial Catalog=DNA_Classified;User Id=sa;Password=demo;Connect Timeout=30;";

            string msg = "";        // A MESSAGE TO BE RETURNED TO THE AJAX CALL.

            try
            {
                // EXTRACT VALUES FROM THE "fields" STRING FOR THE COLUMNS.

                int iCnt = 0;
                string sColumns = "";
                for (iCnt = 0; iCnt <= fields.Split(',').Length - 1; iCnt++)
                {
                    if (string.IsNullOrEmpty(sColumns))
                    {
                        sColumns = "[" + fields.Split(',')[iCnt].Replace(" ", "") + "] VARCHAR (100)";
                    }
                    else
                    {
                        sColumns = sColumns + ", [" + fields.Split(',')[iCnt].Replace(" ", "") + "] VARCHAR (100)";
                    }
                }

                using (SqlConnection con = new SqlConnection(sConnString))
                {
                    // CREATE TABLE STRUCTURE USING THE COLUMNS AND TABLE NAME.

                    string sQuery = null;
                    sQuery = "IF OBJECT_ID('dbo." + table.Replace(" ", "_") + "', 'U') IS NULL " +
                        "BEGIN " +
                        "CREATE TABLE [dbo].[" + table.Replace(" ", "_") + "](" +
                        "[" + table.Replace(" ", "_") + "_ID" + "] INT IDENTITY(1,1) NOT NULL CONSTRAINT pk" +
                            table.Replace(" ", "_") + "_ID" + " PRIMARY KEY, " +
                        "[CreateDate] DATETIME, " + sColumns + ")" +
                        " END";

                    using (SqlCommand cmd = new SqlCommand(sQuery))
                    {
                        cmd.Connection = con;
                        con.Open();

                        cmd.ExecuteNonQuery();
                        con.Close();

                        msg = "Table created successfuly.";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "There was an error.";
            }
            finally
            { }

            return msg;
        }

    }
}
