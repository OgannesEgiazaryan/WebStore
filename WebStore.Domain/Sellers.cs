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
    
    public partial class Sellers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sellers()
        {
            this.SoftWares = new HashSet<SoftWares>();
        }
    
        public int ID_Seller { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }
        public string ImageMimeType { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SoftWares> SoftWares { get; set; }
    }
}
