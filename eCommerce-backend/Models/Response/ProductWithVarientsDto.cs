﻿namespace eCommerce_backend.Models.Response
{
    public class ProductWithVarientsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? Description { get; set; }
        public string SKU { get; set; }
        public decimal BasePrice { get; set; }
        public int CategoryId { get; set; }
        public int VendorId { get; set; }
        public List<ProductVariantDto> Variants { get; set; }
        public string ProductImage { get; set; }
    }
}
