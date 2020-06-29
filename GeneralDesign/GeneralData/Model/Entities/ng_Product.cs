using System;
using System.Collections.Generic;

namespace GeneralData.Model.Entities
{
    public partial class ng_Product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string productCode { get; set; }
        public DateTime releaseDate { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public decimal starRating { get; set; }
        public string imageUrl { get; set; }
    }
}
