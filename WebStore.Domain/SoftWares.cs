//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebStore.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class SoftWares
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SoftWares()
        {
            this.Orders = new HashSet<Orders>();
            this.Reviews = new HashSet<Reviews>();
        }
    
        public int ID_SoftWare { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> Weight_GB { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> Count_Sale { get; set; }
        public Nullable<int> ID_Category { get; set; }
        public Nullable<int> ID_Seller { get; set; }
        public Nullable<int> ID_Event { get; set; }
        public byte[] Photo1 { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
        public string Image_Mime_Type { get; set; }
        public string LongDescription { get; set; }
        public string OS { get; set; }
        public Nullable<int> RAM { get; set; }
    
        public virtual Categorys Categorys { get; set; }
        public virtual Events Events { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reviews> Reviews { get; set; }
        public virtual Sellers Sellers { get; set; }
    }
}
