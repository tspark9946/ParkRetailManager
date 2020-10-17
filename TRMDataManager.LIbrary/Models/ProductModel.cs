﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDataManager.LIbrary.Models
{
    public class ProductModel
    {
        /// <summary>
        /// The unique Identifier for a given product.
        /// </summary>
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal RetailPrice { get; set; }
        public int QuantityInStock { get; set; }

    }
}
