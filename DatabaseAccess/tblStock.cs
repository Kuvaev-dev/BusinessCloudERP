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
    
    public partial class tblStock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblStock()
        {
            this.tblCustomerInvoiceDetail = new HashSet<tblCustomerInvoiceDetail>();
            this.tblSupplierInvoiceDetail = new HashSet<tblSupplierInvoiceDetail>();
        }
    
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double SaleUnitPrice { get; set; }
        public double CurrentPurchaseUnitPrice { get; set; }
        public System.DateTime ExpiryDate { get; set; }
        public System.DateTime Manufacture { get; set; }
        public int StockTreshHoldQuantity { get; set; }
        public string Description { get; set; }
        public int UserID { get; set; }
        public bool IsActive { get; set; }
    
        public virtual tblBranch tblBranch { get; set; }
        public virtual tblCategory tblCategory { get; set; }
        public virtual tblCompany tblCompany { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomerInvoiceDetail> tblCustomerInvoiceDetail { get; set; }
        public virtual tblUser tblUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSupplierInvoiceDetail> tblSupplierInvoiceDetail { get; set; }
    }
}
