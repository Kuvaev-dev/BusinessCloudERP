//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblSupplierPayment
    {
        public int SupplierPaymentID { get; set; }
        public int SupplierID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public string InvoiceNo { get; set; }
        public double TotalAmount { get; set; }
        public double PaymentAmount { get; set; }
        public double RemainingBalance { get; set; }
        public int UserID { get; set; }
    
        public virtual tblSupplier tblSupplier { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
