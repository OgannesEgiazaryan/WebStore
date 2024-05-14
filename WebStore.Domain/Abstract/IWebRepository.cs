using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Domain.Abstract
{
    public interface IWebRepository
    {
        IEnumerable<SoftWares> App { get; }
        void SaveSoft(SoftWares soft);
        SoftWares DeleteSoft(int softID);

        IEnumerable<Categorys> Category { get; }
        void SaveCat(Categorys cat);
        Categorys DeleteCat(int catID);

        IEnumerable<Sellers> Seller { get; }
        void SaveSeller(Sellers seller);
        Sellers DeleteSeller(int sellerID);

        IEnumerable<Orders> Order { get; }
        void SaveOrder(Orders order);
        Orders DeleteOrder(int orderID);

        IEnumerable<Reviews> Review { get; }
        void SaveReview(Reviews review);
        Reviews DeleteReview(int reviewID);

        IEnumerable<Events> Event { get; }
        void SaveEvent(Events event_);
        Events DeleteEvent(int eventID);

    }
}
