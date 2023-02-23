using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseAccess;

namespace CloudERP.Controllers
{
    public class StocksController : Controller
    {
        private CloudDBEntities db = new CloudDBEntities();

        // GET: Stocks
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

            var tblStock = db.tblStock.Include(t => t.tblBranch).Include(t => t.tblCategory)
                                      .Include(t => t.tblUser).Include(t => t.tblCompany)
                                      .Where(t => t.CompanyID == companyID && t.BranchID == branchID);

            return View(tblStock.ToList());
        }

        // GET: Stocks/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStock tblStock = db.tblStock.Find(id);
            if (tblStock == null)
            {
                return HttpNotFound();
            }
            return View(tblStock);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int companyID = 0;
            int branchID = 0;

            companyID = Convert.ToInt32(Convert.ToString(Session["CompanyID"]));
            branchID = Convert.ToInt32(Convert.ToString(Session["BranchID"]));

            ViewBag.CategoryID = new SelectList(db.tblCategory.Where(c => c.BranchID == branchID && c.CompanyID == companyID), "CategoryID", "categoryName", "0");
            return View();
        }

        // POST: Stocks/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblStock tblStock)
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

            tblStock.UserID = userID;
            tblStock.CompanyID = companyID;
            tblStock.BranchID = branchID;

            if (ModelState.IsValid)
            {
                var findProduct = db.tblStock.Where(p => p.CompanyID == companyID
                                                      && p.BranchID == branchID
                                                      && p.ProductName == tblStock.ProductName).FirstOrDefault();
                if (findProduct == null)
                {
                    db.tblStock.Add(tblStock);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Already in Stock";
                }
            }

            ViewBag.CategoryID = new SelectList(db.tblCategory.Where(c => c.BranchID == branchID && c.CompanyID == companyID), "CategoryID", "categoryName", tblStock.CategoryID);
            return View(tblStock);
        }

        // GET: Stocks/Edit/5
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
            tblStock tblStock = db.tblStock.Find(id);
            if (tblStock == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryID = new SelectList(db.tblCategory.Where(c => c.BranchID == tblStock.BranchID && c.CompanyID == tblStock.CompanyID), "CategoryID", "categoryName", tblStock.CategoryID);
            return View(tblStock);
        }

        // POST: Stocks/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblStock tblStock)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userID = 0;
            userID = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            tblStock.UserID = userID;

            if (ModelState.IsValid)
            {
                var findProduct = db.tblStock.Where(p => p.CompanyID == tblStock.CompanyID
                                                      && p.BranchID == tblStock.BranchID
                                                      && p.ProductName == tblStock.ProductName
                                                      && p.ProductID != tblStock.ProductID).FirstOrDefault();
                if (findProduct == null)
                {
                    db.Entry(tblStock).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Already in Stock";
                }
            }
            
            ViewBag.CategoryID = new SelectList(db.tblCategory.Where(c => c.BranchID == tblStock.BranchID && c.CompanyID == tblStock.CompanyID), "CategoryID", "categoryName", tblStock.CategoryID);
            return View(tblStock);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStock tblStock = db.tblStock.Find(id);
            if (tblStock == null)
            {
                return HttpNotFound();
            }
            return View(tblStock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }
            tblStock tblStock = db.tblStock.Find(id);
            db.tblStock.Remove(tblStock);
            db.SaveChanges();
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
    }
}
