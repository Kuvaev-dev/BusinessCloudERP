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
    using System.ComponentModel.DataAnnotations;

    public partial class tblCustomer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCustomer()
        {
            this.tblCustomerInvoice = new HashSet<tblCustomerInvoice>();
            this.tblSaleCart = new HashSet<tblSaleCart>();
        }
    
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string CustomerContact { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string CustomerArea { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string CustomerAddress { get; set; }
        public string Description { get; set; }
        public int BranchID { get; set; }
        public int CompanyID { get; set; }
        public int UserID { get; set; }
    
        public virtual tblBranch tblBranch { get; set; }
        public virtual tblCompany tblCompany { get; set; }
        public virtual tblUser tblUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomerInvoice> tblCustomerInvoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSaleCart> tblSaleCart { get; set; }
    }
}
