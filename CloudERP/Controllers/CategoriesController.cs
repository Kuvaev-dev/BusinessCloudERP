using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccess;

namespace CloudERP.Controllers
{
    public class CategoriesController : Controller
    {
        private CloudDBEntities db = new CloudDBEntities();

        // GET: Categories
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int companyID = 0;
            int branchID = 0;

            companyID = Convert.ToInt32(Convert.ToString(Session["CompanyID"]));
            branchID = Convert.ToInt32(Convert.ToString(Session["BranchID"]));

            var tblCategory = db.tblCategory.Include(t => t.tblBranch).Include(t => t.tblUser).Include(t => t.tblCompany)
                                            .Where(c => c.CompanyID == companyID && c.BranchID == branchID);
            return View(tblCategory.ToList());
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        // POST: Categories/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblCategory tblCategory)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int companyID = 0;
            int branchID = 0;
            int userID = 0;

            companyID = Convert.ToInt32(Convert.ToString(Session["CompanyID"]));
            branchID = Convert.ToInt32(Convert.ToString(Session["BranchID"]));
            userID = Convert.ToInt32(Convert.ToString(Session["UserID"]));

            tblCategory.BranchID = branchID;
            tblCategory.CompanyID = companyID;
            tblCategory.UserID = userID;

            if (ModelState.IsValid)
            {
                var findCategory = db.tblCategory.Where(c => c.CompanyID == companyID 
                                                        && c.BranchID == branchID
                                                        && c.categoryName == tblCategory.categoryName).FirstOrDefault();

                if (findCategory == null)
                {
                    db.tblCategory.Add(tblCategory);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Already Exist";
                }
            }

            return View(tblCategory);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategory tblCategory = db.tblCategory.Find(id);
            if (tblCategory == null)
            {
                return HttpNotFound();
            }

            return View(tblCategory);
        }

        // POST: Categories/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblCategory tblCategory)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userID = 0;
            userID = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            tblCategory.UserID = userID;

            if (ModelState.IsValid)
            {
                var findCategory = db.tblCategory.Where(c => c.CompanyID == tblCategory.CompanyID
                                                        && c.BranchID == tblCategory.BranchID
                                                        && c.categoryName == tblCategory.categoryName
                                                        && c.CategoryID != tblCategory.CategoryID).FirstOrDefault();

                if (findCategory == null)
                {
                    db.Entry(tblCategory).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Already Exist";
                }
            }

            return View(tblCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
