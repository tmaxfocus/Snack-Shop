using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SnackShop.InventoryManagement
{
    public class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description;

        private int maxItemsInStock = 0;

        private UnitType unitType;
        private int amountInStock = 0;
        private bool isBelowStockThreshold = false;

        public void UseProduct(int items)
        {
            if(items < amountInStock)
            {
                //use the items
                amountInStock -= items;
                UpdateLowStock();
                Log($"Amount in stock updated. Now {amountInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items on stock for {CreateSimpleProductRepresentation()}. {amountInStock} available but {items} requested.");
            }
        }

        public string DisplayDetailsShort()
        {
            return $"{id}. {name} \n{amountInStock} items in stock";
        }

        public string DisplayDetailsFull()
        {
            StringBuilder sb = new();
            //ToDo: add price here too
            sb.Append($"{id} {name} \n{description}\n{amountInStock} item(s) in stock");

            if(isBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");

            }
            return sb.ToString();
        }

        public void IncreaseStock()
        {
            amountInStock++;
        }

        private void UpdateLowStock()
        {
            if(amountInStock < 10) // for a fixed value
            {
                isBelowStockThreshold = true;
            }
        }

        private void DecreaseStock(int items, string reason)
        {
            if(items < amountInStock)
            {
                amountInStock -= items;
            }
            else
            {
                amountInStock = 0;
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
