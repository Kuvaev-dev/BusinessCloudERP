using CloudERP.Models;
using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;

namespace CloudERP.Helpers
{ 
    public class ComboHelper
    {
        //private CloudDBEntities db = new CloudDBEntities();

        //public List<AccountControlMV> AccountControl()
        //{
        //    var tblAccountControls = db.tblAccountControl.Include(t => t.tblBranch).Include(t => t.tblUser).Include(t => t.tblCompany)
        //                                                 .Where(a => a.CompanyID == companyID && a.BranchID == branchID);

        //    foreach (var item in tblAccountControls)
        //    {
        //        accountControls.Add(new AccountControlMV
        //        {
        //            AccountControlID = item.AccountControlID,
        //            AccountControlName = item.AccountControlName,
        //            AccountHeadID = item.AccountHeadID,
        //            AccountHeadName = db.tblAccountHead.Find(item.AccountHeadID).AccountHeadName,
        //            BranchID = item.BranchID,
        //            BranchName = item.tblBranch.BranchName,
        //            CompanyID = item.CompanyID,
        //            Name = item.tblCompany.Name,
        //            UserID = item.UserID,
        //            UserName = item.tblUser.UserName
        //        });
        //    }
        //}
    }
}