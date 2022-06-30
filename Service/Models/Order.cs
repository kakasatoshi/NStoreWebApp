﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Service.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipEmail { get; set; }
        public string ShipPhoneNumber { get; set; }
        public int Status { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
