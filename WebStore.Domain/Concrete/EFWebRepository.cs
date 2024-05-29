using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain.Abstract;
using WebStore.Domain.Concrete;
using System.Data.Entity;
using System.Linq;

namespace GameStore.Domain.Concrete
{
    public class EFWebRepository : IWebRepository
    {
        EFDbContext context = new EFDbContext();


        public IQueryable<SoftWares> App1
        {
            get { return context.App; }
        }

        public void Save()
        {
            context.SaveChanges();
        }

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
                    dbEntry.LongDescription = software.LongDescription;
                    dbEntry.OS = software.OS;
                    dbEntry.RAM = software.RAM;
                    dbEntry.Image1 = software.Image1;
                    dbEntry.Image2 = software.Image2;
                    dbEntry.Image3 = software.Image3;
                    dbEntry.Copy_Count= software.Copy_Count;
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

        public void SaveCat(Categorys category)
        {
            if (category.ID_Category == 0)
                context.Category.Add(category);
            else
            {
                Categorys dbEntry = context.Category.Find(category.ID_Category);
                if (dbEntry != null)
                {
                    dbEntry.Name = category.Name;
                    dbEntry.Description = category.Description;
                    dbEntry.Photo = category.Photo;
                    dbEntry.ImageMimeType= category.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Categorys DeleteCat(int catID)
        {
            Categorys dbEntry = context.Category.Find(catID);
            if (dbEntry != null)
            {
                context.Category.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public IEnumerable<Sellers> Seller
        {
            get { return context.Seller; }

        }

        public void SaveSeller(Sellers seller)
        {
            if (seller.ID_Seller == 0)
                context.Seller.Add(seller);
            else
            {
                Sellers dbEntry = context.Seller.Find(seller.ID_Seller);
                if (dbEntry != null)
                {
                    dbEntry.Name = seller.Name;
                    dbEntry.Email = seller.Email;
                    dbEntry.Photo = seller.Photo;
                    dbEntry.ImageMimeType = seller.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Sellers DeleteSeller(int sellerID)
        {
            Sellers dbEntry = context.Seller.Find(sellerID);
            if (dbEntry != null)
            {
                context.Seller.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public IEnumerable<Orders> Order
        {
            get { return context.Order; }

        }

        public void SaveOrder(Orders order)
        {
            if (order.Order_ID == 0)
            {
                context.Order.Add(order);
            }
            else
            {
                Orders dbEntry = context.Order.Find(order.Order_ID);
                if (dbEntry != null)
                {
                    dbEntry.Order_Name = order.Order_Name;
                    dbEntry.Order_Email = order.Order_Email;
                    dbEntry.Order_Soft_ID = order.Order_Soft_ID;
                    dbEntry.Order_Quantity = order.Order_Quantity;
                }
            }

            // Decrement the Copy_Count for the ordered software
            var orderedSoftware = context.App.Find(order.Order_Soft_ID);
            if (orderedSoftware != null)
            {
                orderedSoftware.Copy_Count -= order.Order_Quantity;
                orderedSoftware.Count_Sale += order.Order_Quantity;
            }

            context.SaveChanges();
        }


        public Orders DeleteOrder(int orderID)
        {
            Orders dbEntry = context.Order.Find(orderID);
            if (dbEntry != null)
            {
                context.Order.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public IEnumerable<Reviews> Review
        {
            get { return context.Review; }

        }

        public void SaveReview(Reviews review)
        {
            if (review.ID_Review == 0)
                context.Review.Add(review);
            else
            {
                Reviews dbEntry = context.Review.Find(review.ID_Review);
                if (dbEntry != null)
                {
                    dbEntry.Date = review.Date;
                    dbEntry.Text = review.Text;
                    dbEntry.Rating = review.Rating;
                    dbEntry.ID_Software = review.ID_Software;
                    dbEntry.User = review.User;
                }
            }
            context.SaveChanges();
        }

        public Reviews DeleteReview(int reviewID)
        {
            Reviews dbEntry = context.Review.Find(reviewID);
            if (dbEntry != null)
            {
                context.Review.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public IEnumerable<Events> Event
        {
            get { return context.Event; }

        }

        public void SaveEvent(Events event_)
        {
            if (event_.Event_ID == 0)
                context.Event.Add(event_);
            else
            {
                Events dbEntry = context.Event.Find(event_.Event_ID);
                if (dbEntry != null)
                {
                    dbEntry.Event_Name = event_.Event_Name;
                    dbEntry.Event_Description = event_.Event_Name;
 
                }
            }
            context.SaveChanges();
        }

        public Events DeleteEvent(int eventID)
        {
            Events dbEntry = context.Event.Find(eventID);
            if (dbEntry != null)
            {
                context.Event.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}