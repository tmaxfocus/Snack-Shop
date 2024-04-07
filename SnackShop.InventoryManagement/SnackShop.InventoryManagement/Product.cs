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
        public Product(int id) : this(id, string.Empty) { }
        public Product(int id, string name) { Id = id; Name = name; }

        public Product(int id, string name, string? description, UnitType unitType, int maxAmountInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            UnitType = unitType;

            maxAmountInStock = maxAmountInStock;

            UpdateLowStock();
        }

       

        private int id;
        private string name = string.Empty;
        private string? description;

        private int maxItemsInStock = 0;


        public int Id { get { return id; }  set{ id = value; } }

        public string Name { get { return name; } set { name = value.Length > 50 ? value[..50] : value; } }

        public string Description { get { return description; } set { if(value == null) { description = string.Empty; } else { description = value.Length > 50 ? value[..250] : value; } } }

        public UnitType UnitType { get; set; }

        public int AmountInStock { get; private set; }


        public bool IsBelowStockThreshold { get; private set; }
        public void UseProduct(int items)
        {
            if(items < AmountInStock)
            {
                //use the items
                AmountInStock -= items;
                UpdateLowStock();
                Log($"Amount in stock updated. Now {AmountInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items on stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} requested.");
            }
        }

        public string DisplayDetailsShort()
        {
            return $"{id}. {name} \n{AmountInStock} items in stock";
        }

        public string DisplayDetailsFull()
        {
            StringBuilder sb = new();
            //ToDo: add price here too
            sb.Append($"{id} {name} \n{description}\n{AmountInStock} item(s) in stock");

            if(IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");

            }
            return sb.ToString();
        }


        public string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new();
            //ToDo: add price here too
            sb.Append($"{id} {name} \n{description}\n{AmountInStock} item(s) in stock");
            sb.Append(extraDetails);

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");

            }
            return sb.ToString();
        }
        public void IncreaseStock()
        {
            AmountInStock++;
        }

        public void IncreaseStrock(int amount)
        {
            int newStock = AmountInStock + amount;

            if(newStock < maxItemsInStock)
            {
                AmountInStock += amount;
            }
            else
            {
                AmountInStock = maxItemsInStock;
                Log($"{CreateSimpleProductRepresentation} stock overflow. {newStock - AmountInStock} item(s) ordered that couldn't be stored.");
            }

            if(AmountInStock > 10)
            {
                IsBelowStockThreshold = false;
            }
        }

        private void UpdateLowStock()
        {
            if(AmountInStock < 10) // for a fixed value
            {
                IsBelowStockThreshold = true;
            }
        }

        private void DecreaseStock(int items, string reason)
        {
            if(items < AmountInStock)
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
