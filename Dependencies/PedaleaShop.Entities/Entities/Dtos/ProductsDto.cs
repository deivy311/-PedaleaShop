﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedaleaShop.Entities.Dtos
{
    public class ProductsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }

        public string? CategoryName { get; set; }
        public string? ColorName { get; set; }
        public string? SizeName { get; set; }
    }
}
