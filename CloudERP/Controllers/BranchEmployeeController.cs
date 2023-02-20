using CloudERP.Helpers;
using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CloudERP.Controllers
{
    public class BranchEmployeeController : Controller
    {
        private CloudDBEntities db = new CloudDBEntities();

        public ActionResult Employee()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int companyID = 0;
            int branchID = 0;

            companyID = Convert.ToInt32(Convert.ToString(Session["CompanyID"]));
            branchID = Convert.ToInt32(Convert.ToString(Session["BranchID"]));

            var tblEmployee = db.tblEmployee.Where(c => c.BranchID == branchID && c.CompanyID == companyID);

            return View(tblEmployee);
        }

        public ActionResult EmployeeRegistration()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeRegistration(tblEmployee employee)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int companyID = 0;
            int branchID = 0;

            companyID = Convert.ToInt32(Convert.ToString(Session["CompanyID"]));
            branchID = Convert.ToInt32(Convert.ToString(Session["BranchID"]));
            employee.CompanyID = companyID;
            employee.BranchID = branchID;
            employee.UserID = null;

            if (ModelState.IsValid)
            {
                db.tblEmployee.Add(employee);
                db.SaveChanges();

                if (employee.LogoFile != null)
                {
                    var folder = "~/Content/EmployeePhotos";
                    var file = string.Format("{0}.png", employee.EmployeeID);
                    var response = FileHelper.UploadPhoto(employee.LogoFile, folder, file);

                    if (response)
                    {
                        var picture = string.Format("{0}/{1}", folder, file);
                        employee.Photo = picture;
                        db.Entry(employee).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Employee");
            }

            return View(employee);
        }

        public ActionResult EmployeeUpdation(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = db.tblEmployee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeUpdation(tblEmployee employee)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int companyID = 0;
            int branchID = 0;

            companyID = Convert.ToInt32(Convert.ToString(Session["CompanyID"]));
            branchID = Convert.ToInt32(Convert.ToString(Session["BranchID"]));
            employee.CompanyID = companyID;
            employee.BranchID = branchID;
            employee.UserID = null;

            if (ModelState.IsValid)
            {
                if (employee.LogoFile != null)
                {
                    var folder = "~/Content/EmployeePhotos";
                    var file = string.Format("{0}.png", employee.EmployeeID);
                    var response = FileHelper.UploadPhoto(employee.LogoFile, folder, file);

                    if (response)
                    {
                        var picture = string.Format("{0}/{1}", folder, file);
                        employee.Photo = picture;
                        db.Entry(employee).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Employee");
            }

            return View(employee);
        }
    
        public ActionResult ViewProfile(int? id)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
                {
                    return RedirectToAction("Login", "Home");
                }
                if (id == null)
                {
                    return RedirectToAction("EP500", "EP");
                }
                int companyID = Convert.ToInt32(Convert.ToString(Session["CompanyID"]));
                var employee = db.tblEmployee.Where(e => e.CompanyID == companyID && e.EmployeeID == id).FirstOrDefault();
                if (employee == null)
                {
                    return RedirectToAction("EP404", "EP");
                }
                return View(employee);
            }
            catch
            {
                return RedirectToAction("EP500", "EP");
            }
        }
    }
}