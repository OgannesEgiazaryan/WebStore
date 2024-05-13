using System.Collections.Generic;
using System.Linq;

namespace WebStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(SoftWares soft, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Soft.ID_SoftWare == soft.ID_SoftWare)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Soft = soft,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(SoftWares soft)
        {
            lineCollection.RemoveAll(l => l.Soft.ID_SoftWare == soft.ID_SoftWare);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Soft.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public SoftWares Soft { get; set; }
        public int Quantity { get; set; }
    }
}