﻿using System.ComponentModel.DataAnnotations;

namespace SupplyChainManagement.Models
{
    public class ProductType
    {
        public int ProductTypeId { get; set; }
        [Required]
        public string ProductTypeName { get; set; }
        public string Description { get; set; }
    }
}
