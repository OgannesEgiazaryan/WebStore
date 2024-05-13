using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain.Abstract;
using WebStore.Domain.Concrete;
using System.Data.Entity;

namespace GameStore.Domain.Concrete
{
    public class EFWebRepository : IWebRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<SoftWares> App
        {
            get { return context.App; }
        }

        public void SaveSoft(SoftWares software)
        {
            if (software.ID_SoftWare == 0)
                context.App.Add(software);
            else
            {
                SoftWares dbEntry = context.App.Find(software.ID_SoftWare);
                if (dbEntry != null)
                {
                    dbEntry.Name = software.Name;
                    dbEntry.Description = software.Description;
                    dbEntry.Price = software.Price;
                    dbEntry.ID_Category = software.ID_Category;
                    dbEntry.ID_Seller = software.ID_Seller;
                    dbEntry.Count_Sale = software.Count_Sale;
                    dbEntry.Weight_GB = software.Weight_GB;
                    dbEntry.Photo1 = software.Photo1;
                    dbEntry.Image_Mime_Type = software.Image_Mime_Type;
                    dbEntry.ID_Event = software.ID_Event;
                }
            }
            context.SaveChanges();
        }

        public SoftWares DeleteSoft(int softID)
        {
            SoftWares dbEntry = context.App.Find(softID);
            if (dbEntry != null)
            {
                context.App.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }


        public IEnumerable<Categorys> Category
        {
            get { return context.Category; }

        }
        


        public IEnumerable<Sellers> Seller
        {
            get { return context.Seller; }

        }

        public IEnumerable<Orders> Order
        {
            get { return context.Order; }

        }

        public IEnumerable<Reviews> Review
        {
            get { return context.Review; }

        }

        public IEnumerable<Events> Event
        {
            get { return context.Event; }

        }


    }
}