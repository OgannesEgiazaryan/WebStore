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
        //void SaveCat(Categorys cat);

        IEnumerable<Sellers> Seller { get; }
        //void SaveSeller(Sellers seller);

        IEnumerable<Orders> Order { get; }
        //void SaveOrder(Orders order);

        IEnumerable<Reviews> Review { get; }
        //void SaveReview(Reviews review);

        IEnumerable<Events> Event { get; }

    }
}
