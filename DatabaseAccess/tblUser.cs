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
    
    public partial class tblUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblUser()
        {
            this.tblAccountControl = new HashSet<tblAccountControl>();
            this.tblAccountHead = new HashSet<tblAccountHead>();
            this.tblAccountSubControl = new HashSet<tblAccountSubControl>();
            this.tblCategory = new HashSet<tblCategory>();
            this.tblCustomerInvoice = new HashSet<tblCustomerInvoice>();
            this.tblCustomerPayment = new HashSet<tblCustomerPayment>();
            this.tblFinancialYear = new HashSet<tblFinancialYear>();
            this.tblPayroll = new HashSet<tblPayroll>();
            this.tblStock = new HashSet<tblStock>();
            this.tblSupplier = new HashSet<tblSupplier>();
            this.tblSupplierInvoice = new HashSet<tblSupplierInvoice>();
            this.tblSupplierPayment = new HashSet<tblSupplierPayment>();
            this.tblTransaction = new HashSet<tblTransaction>();
            this.tblCustomer = new HashSet<tblCustomer>();
        }
    
        public int UserID { get; set; }
        public int UserTypeID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAccountControl> tblAccountControl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAccountHead> tblAccountHead { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAccountSubControl> tblAccountSubControl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCategory> tblCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomerInvoice> tblCustomerInvoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomerPayment> tblCustomerPayment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblFinancialYear> tblFinancialYear { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPayroll> tblPayroll { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblStock> tblStock { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSupplier> tblSupplier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSupplierInvoice> tblSupplierInvoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSupplierPayment> tblSupplierPayment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTransaction> tblTransaction { get; set; }
        public virtual tblUserType tblUserType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomer> tblCustomer { get; set; }
    }
}
