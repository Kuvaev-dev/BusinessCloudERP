using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudERP.Controllers
{
    public class UserSettingController : Controller
    {
        private CloudDBEntities db = new CloudDBEntities();

        public ActionResult CreateUser(int? employeeID)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }
            Session["CEmployeeID"] = employeeID;
            var employee = db.tblEmployee.Find(employeeID);
            var user = new tblUser()
            {
                Email = employee.Email,
                ContactNo = employee.ContactNo,
                FullName = employee.Name,
                IsActive = true,
                Password = employee.ContactNo,
                UserName = employee.Email
            };
            ViewBag.UserTypeID = new SelectList(db.tblUserType.ToList(), "UserTypeID", "UserType");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(tblUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var guser = db.tblUser.Where(u => u.Email == user.Email && u.UserID != user.UserID);
                    if (guser.Count() > 0)
                    {
                        ViewBag.Message = "Email is Already Registered";
                    }
                    else
                    {
                        db.tblUser.Add(user);
                        db.SaveChanges();

                        int? employeeID = Convert.ToInt32(Convert.ToString(Session["CEmployeeID"]));
                        var employee = db.tblEmployee.Find(employeeID);
                        employee.UserID = user.UserID;

                        db.Entry(employee).State = EntityState.Modified;
                        db.SaveChanges();

                        Session["CEmployeeID"] = null;

                        return RedirectToAction("Index", "Users");
                    }
                }

                if (user == null)
                {
                    ViewBag.UserTypeID = new SelectList(db.tblUserType.ToList(), "UserTypeID", "UserType");
                }
                else
                {
                    ViewBag.UserTypeID = new SelectList(db.tblUserType.ToList(), "UserTypeID", "UserType", user.UserTypeID);
                }

                return View(user);
            }
            catch
            {
                return RedirectToAction("EP500", "EP");
            }
        }

        public ActionResult UpdateUser(int? userID)
        {
            var user = db.tblUser.Find(userID);

            ViewBag.UserTypeID = new SelectList(db.tblUserType.ToList(), "UserTypeID", "UserType", user.UserTypeID);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(tblUser user)
        {
            if (ModelState.IsValid)
            {
                var guser = db.tblUser.Where(u => u.Email == user.Email && u.UserID != user.UserID);
                if (guser.Count() > 0)
                {
                    ViewBag.Message = "Email is Already Registered";
                }
                else
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index", "Users");
                }
            }

            if (user == null)
            {
                ViewBag.UserTypeID = new SelectList(db.tblUserType.ToList(), "UserTypeID", "UserType");
            }
            else
            {
                ViewBag.UserTypeID = new SelectList(db.tblUserType.ToList(), "UserTypeID", "UserType", user.UserTypeID);
            }

            return View(user);
        }
    }
}