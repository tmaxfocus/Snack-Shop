﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackShop.InventoryManagement.Domain.OrderManagement
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;
        public int AmountOrdered { get; set; }
    }
}