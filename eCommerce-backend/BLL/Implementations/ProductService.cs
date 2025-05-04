using eCommerce_backend.BLL.Interfaces;
using eCommerce_backend.Data.DAL.Interfaces;
using eCommerce_backend.Data.Entities;
using eCommerce_backend.Helper;
using eCommerce_backend.Models.Request;
using eCommerce_backend.Models.Response;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace eCommerce_backend.BLL.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<ProductDto> AddProductAsync(ProductCreateDto dto)
        {

            Console.WriteLine("DTO JSON: " + JsonSerializer.Serialize(dto));
            var productObj = new Product
            {
                Name = dto.Name,
                Slug = dto.Slug,
                SKU = dto.SKU,
                Description = dto.Description,
                BasePrice = dto.BasePrice,
                CategoryId = dto.CategoryId,
                VendorId = dto.VendorId,
            };
            var newProduct = await _productRepository.AddProductAsync(productObj);


            if (dto.ProductImage == null) throw new Exception("Product Image Required!");
            var imgUrl =  await FileHelper.SaveImageAsync(dto.ProductImage, "uploads/image/Product");
            var productImage = new ProductImage
            {
                ProductId = newProduct.Id,
                ImageUrl = imgUrl
            };
            await _productRepository.AddProductImageAsync(productImage);


            var varientDtoList = new List<ProductVariantDto>();

            //debugging
            if (dto.Variants == null)
                Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%----> Variants is NULL");
            else
                Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% Received variants: " + dto.Variants.Count);


            if (dto.Variants != null && dto.Variants.Any() == true)
            {
                foreach (var variant in dto.Variants)
                {
                    var varientObj = new ProductVariant
                    {
                        Size = variant.Size,
                        Color = variant.Color,
                        Price = variant.Price,
                        Stock = variant.Stock,
                        IsActive = true,
                        ProductId = newProduct.Id,

                    };
                    await _productRepository.AddProductVariantAsync(varientObj);

                    ProductImage variantImageObj = null;
                    if (variant.ProductVarientImage != null)
                    {
                        var variantImageUrl = await FileHelper.SaveImageAsync(variant.ProductVarientImage, "uploads/image/ProductVariant");
                        variantImageObj = new ProductImage
                        {
                            ProductVarientId = varientObj.Id,
                            ImageUrl = variantImageUrl
                        };
                        await _productRepository.AddProductImageAsync(variantImageObj);
                    }

                    varientDtoList.Add(new ProductVariantDto
                    {
                        Id = varientObj.Id,
                        Size = varientObj.Size,
                        Color = varientObj.Color,
                        Stock = varientObj.Stock,
                        Price = varientObj.Price,
                        IsActive = varientObj.IsActive,
                        VarientImage = varientObj.VarientImage.ImageUrl
                    });
                }
            }

            return new ProductDto
            {
                Id = newProduct.Id,
                Name = newProduct.Name,
                Slug = newProduct.Slug,
                SKU = newProduct.SKU,
                BasePrice = newProduct.BasePrice,
                CategoryId = newProduct.CategoryId,
                VendorId = newProduct.VendorId,
                Description = newProduct.Description,
                ProductImage = newProduct.ProductImage.ImageUrl,
                Variants = varientDtoList,

            };
        }

        public async Task<List<ProductListDto>> GetAllProductAsync()
        {
            var allProducts = await _productRepository.GetAllProductAsync();
            var productListDto = allProducts.Select(p => new ProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                BasePrice = p.BasePrice,
                ProductImage = p.ProductImage?.ImageUrl ?? string.Empty

            }).ToList();

            return productListDto;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            var dto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                BasePrice = product.BasePrice,
                CategoryId = product.CategoryId,
                VendorId = product.VendorId,
                SKU = product.SKU,
                Slug = product.Slug,
                ProductImage = product.ProductImage?.ImageUrl ?? string.Empty,
                Variants = product.Variants?.Select(v => new ProductVariantDto
                {
                    Id = v.Id,
                    Size = v.Size,
                    Color = v.Color,
                    Price = v.Price,
                    Stock = v.Stock,
                    IsActive = v.IsActive,
                    VarientImage = v.VarientImage?.ImageUrl ?? string.Empty

                }).ToList() ?? new List<ProductVariantDto>()

            };
            return dto;
        }
    }
}
