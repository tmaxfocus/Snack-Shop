using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackShop.InventoryManagement.Domain.ProductManagement
{
   public partial class Product
    {
        private void UpdateLowStock()
        {
            if (AmountInStock < 10) // for a fixed value
            {
                IsBelowStockThreshold = true;
            }
        }

        private void DecreaseStock(int items, string reason)
        {
            if (items < AmountInStock)
            {
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }

            UpdateLowStock();
            Log(reason);
        }

        private void Log(string message)
        {
            //this could be written in a file
            Console.WriteLine(message);
        }

        private string CreateSimpleProductRepresentation()
        {
            return $"Product {id} ({name})";
        }
    }
}
