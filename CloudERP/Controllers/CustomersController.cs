using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudERP.Models;
using DatabaseAccess;

namespace CloudERP.Controllers
{
    public class CustomersController : Controller
    {
        private CloudDBEntities db = new CloudDBEntities();

        // GET: AllCustomers
        public ActionResult AllCustomers()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var tblCustomer = db.tblCustomer.Include(t => t.tblBranch).Include(t => t.tblUser).Include(t => t.tblCompany);
            return View(tblCustomer.ToList());
        }

        // GET: Customers
        public ActionResult Index()
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

            var tblCustomer = db.tblCustomer.Include(t => t.tblBranch).Include(t => t.tblUser).Include(t => t.tblCompany)
                                            .Where(c => c.CompanyID == companyID && c.BranchID == branchID);
            return View(tblCustomer.ToList());
        }

        // GET: SubBranchCustomers
        public ActionResult SubBranchCustomer()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            List<int> branchIDs = new List<int>();
            List<int> isSubBranchesFirst = new List<int>();
            List<int> isSubBranchesSecond = new List<int>();
            List<BranchesCustomerMV> branchCustomers = new List<BranchesCustomerMV>();

            int branchID = 0;
            branchID = Convert.ToInt32(Convert.ToString(Session["BranchID"]));

            var brnch = db.tblBranch.Where(b => b.BrchID == branchID);

            foreach (var item in brnch)
            {
                isSubBranchesFirst.Add(item.BranchID);
            }

        subBranchFinding:
            foreach (var item in isSubBranchesFirst)
            {
                branchIDs.Add(item);
                foreach (var subBranch in db.tblBranch.Where(b => b.BrchID == item))
                {
                    isSubBranchesSecond.Add(subBranch.BranchID);
                }
            }
            if (isSubBranchesSecond.Count > 0)
            {
                isSubBranchesFirst.Clear();
                foreach (var subBranch in isSubBranchesSecond)
                {
                    isSubBranchesFirst.Add(subBranch);
                }
                isSubBranchesSecond.Clear();

                goto subBranchFinding;
            }

            foreach (var item in branchIDs)
            {
                foreach (var customer in db.tblCustomer.Where(c => c.BranchID == item))
                {
                    var newCustomer = new BranchesCustomerMV()
                    {
                        BranchName = customer.tblBranch.BranchName,
                        CompanyName = customer.tblCompany.Name,
                        CustomerAddress = customer.CustomerAddress,
                        CustomerArea = customer.CustomerArea,
                        CustomerContact = customer.CustomerContact,
                        CustomerName = customer.CustomerName,
                        Description = customer.Description,
                        User = customer.tblUser.UserName
                    };
                    branchCustomers.Add(newCustomer);
                }
            }

            return View(branchCustomers);
        }

        // GET: Customers/Details/5
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
            tblCustomer tblCustomer = db.tblCustomer.Find(id);
            if (tblCustomer == null)
            {
                return HttpNotFound();
            }
            return View(tblCustomer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        // POST: Customers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblCustomer tblCustomer)
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

            tblCustomer.CompanyID = companyID;
            tblCustomer.BranchID = branchID;
            tblCustomer.UserID = userID;

            if (ModelState.IsValid)
            {
                var findCustomer = db.tblCustomer.Where(c => c.CustomerName == tblCustomer.CustomerName
                                                          && c.CustomerContact == tblCustomer.CustomerContact).FirstOrDefault();
                if (findCustomer == null)
                {
                    db.tblCustomer.Add(tblCustomer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Already Exist";
                }
            }
            return View(tblCustomer);
        }

        // GET: Customers/Edit/5
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
            tblCustomer tblCustomer = db.tblCustomer.Find(id);
            if (tblCustomer == null)
            {
                return HttpNotFound();
            }
            return View(tblCustomer);
        }

        // POST: Customers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblCustomer tblCustomer)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["CompanyID"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int userID = 0;
            userID = Convert.ToInt32(Convert.ToString(Session["UserID"]));
            tblCustomer.UserID = userID;

            if (ModelState.IsValid)
            {
                var findCustomer = db.tblCustomer.Where(c => c.CustomerName == tblCustomer.CustomerName
                                                          && c.CustomerContact == tblCustomer.CustomerContact
                                                          && c.CustomerID != tblCustomer.CustomerID).FirstOrDefault();
                if (findCustomer == null)
                {
                    db.Entry(tblCustomer).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Already Exist";
                }
            }
            return View(tblCustomer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCustomer tblCustomer = db.tblCustomer.Find(id);
            if (tblCustomer == null)
            {
                return HttpNotFound();
            }
            return View(tblCustomer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCustomer tblCustomer = db.tblCustomer.Find(id);
            db.tblCustomer.Remove(tblCustomer);
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
