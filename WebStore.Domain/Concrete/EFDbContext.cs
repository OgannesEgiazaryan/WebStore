using System;
using System.Data.Entity;
using WebStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {


        public DbSet<SoftWares> App { get; set; }

        public DbSet<Categorys> Category { get; set; }
        
        public DbSet<Sellers> Seller { get; set; }

        public DbSet<Orders> Order { get; set; }
        
        public DbSet<Reviews> Review { get; set; }
        
        public DbSet<Events> Event { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SoftWares>()
                .HasMany(s => s.Review)
                .WithRequired(r => r.Software   )
                .HasForeignKey(r => r.ID_Software);
        }

    }
}
