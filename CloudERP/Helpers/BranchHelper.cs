using CloudERP.Models;
using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudERP.Helpers
{
    public class BranchHelper
    {
        public static List<int> GetBranchIDs(int? branchIDParam, CloudDBEntities db)
        {
            List<int> branchIDs = new List<int>();
            List<int> isSubBranchesFirst = new List<int>();
            List<int> isSubBranchesSecond = new List<int>();
            
            int branchID = 0;
            branchID = Convert.ToInt32(branchIDParam);

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

            return branchIDs;
        }
    }
}