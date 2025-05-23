﻿using eCommerce_backend.Data.DAL.Interfaces;
using eCommerce_backend.Data.Entities;
using eCommerce_backend.Models.Request;
using eCommerce_backend.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_backend.Data.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }


        // -------------------- Public (Customer-facing) Endpoints --------------------
        public async Task<List<Product>> GetAllProductAsync()
        {
            return await _context.Products
                .Include(p => p.ProductImage)
                .ToListAsync();
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            var product = _context.Products
                .Include(p => p.ProductImage)
                .Include(p => p.Variants)
                .ThenInclude(v => v.VarientImage)
                .FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }





        // -------------------- Vendor Endpoints --------------------

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }


        public Task<List<Product>> GetAllProductsForVendorWithVarients(int vendorId)
        {
            return _context.Products
                .Where(p => p.VendorId == vendorId)
                .Include(p => p.ProductImage)
                .Include(p => p.Variants)
                .ThenInclude(v => v.VarientImage)
                .ToListAsync();
        }


        public Task<Product?> GetVendorProductByIdAsync(int id)
        {
            return _context.Products
                .Include(p => p.ProductImage)
                .Include(p => p.Variants)
                .ThenInclude(v => v.VarientImage)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        // ----------- Variants (Vendor) -----------
        public async Task<ProductVariant> AddProductVariantAsync(ProductVariant variant)
        {
            _context.ProductVariants.Add(variant);
            await _context.SaveChangesAsync();
            return variant;
        }

        public Task<ProductVariant?> GetProductVariantByIdAsync(int variantId)
        {
            return _context.ProductVariants
                .Include(v => v.VarientImage)
                .Include(v => v.Product)
                .FirstOrDefaultAsync(v => v.Id == variantId);
        }

        public async Task UpdateProductVariantAsync(ProductVariant variant)
        {
            _context.ProductVariants.Update(variant);
            await _context.SaveChangesAsync();
        }

        
        public Task DeleteProductVariantAsync(int variantId)
        {
            throw new NotImplementedException();
        }


        //Image
        public async Task<ProductImage> AddProductImageAsync(ProductImage image)
        {
            _context.ProductImages.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }
    }
}
