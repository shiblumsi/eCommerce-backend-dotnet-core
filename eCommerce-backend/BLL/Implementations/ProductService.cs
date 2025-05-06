using eCommerce_backend.BLL.Interfaces;
using eCommerce_backend.Data.DAL.Interfaces;
using eCommerce_backend.Data.Entities;
using eCommerce_backend.Helper;
using eCommerce_backend.Migrations;
using eCommerce_backend.Models.Request;
using eCommerce_backend.Models.Response;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace eCommerce_backend.BLL.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        public ProductService(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor,IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }


        //---------------------Customer/Public -----------------------------------------------
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

        public async Task<ProductWithVarientsDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            var dto = new ProductWithVarientsDto
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


        


        // -------------------- Vendor Endpoints --------------------------------------------------------------------
        public async Task<ProductWithVarientsDto> AddProductAsync(int vendorId, ProductWithVarientCreateDto dto)
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
                VendorId = vendorId,
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

            return new ProductWithVarientsDto
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


        public async Task<List<ProductWithVarientsDto>> GetAllProductsForVendorWithVarients(int vendorId)
        {
            var products = await _productRepository.GetAllProductsForVendorWithVarients(vendorId);
            var dto = products.Select(product => new ProductWithVarientsDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                BasePrice = product.BasePrice,
                ProductImage = product.ProductImage?.ImageUrl, // Optional chaining

                Variants = product.Variants?.Select(varient => new ProductVariantDto
                {
                    Id = varient.Id,
                    Size = varient.Size,
                    Color = varient.Color,
                    Stock = varient.Stock,
                    Price = varient.Price,
                    VarientImage = varient.VarientImage?.ImageUrl // Safe null access
                }).ToList() ?? new List<ProductVariantDto>() // If null, return empty list
            }).ToList();

            return dto;
        }

        public async Task<ProductWithVarientsDto?> GetVendorProductByIdAsync(int id, int vendorId)
        {
            var product = await _productRepository.GetVendorProductByIdAsync(id);
            if (product == null) throw new Exception("Product Not Found!");

           
            if (product.VendorId != vendorId) return null;

            var dto = new ProductWithVarientsDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                BasePrice = product.BasePrice,
                ProductImage = product.ProductImage.ImageUrl,
                Variants = product.Variants?.Select(varient => new ProductVariantDto
                {
                    Id = varient.Id,
                    Size = varient.Size,
                    Color = varient.Color,
                    Stock = varient.Stock,
                    Price = varient.Price,
                    VarientImage = varient.VarientImage.ImageUrl
                }).ToList() ?? new List<ProductVariantDto>()
            };
            return dto;
        }

        public async Task<ProductUpdateDto> UpdateProductAsync(int id, int vendorId, ProductUpdateDto updateDto)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            
            if (product == null)
                throw new Exception("Product not found.");

            

            if (product.VendorId != vendorId)
            {
                return null;
            }


            if (!string.IsNullOrEmpty(updateDto.Name))
                product.Name = updateDto.Name;

            if (!string.IsNullOrEmpty(updateDto.Description))
                product.Description = updateDto.Description;

            if (updateDto.BasePrice.HasValue)
                product.BasePrice = updateDto.BasePrice.Value;

            if (updateDto.CategoryId.HasValue)
                product.CategoryId = updateDto.CategoryId.Value;

            // Handle Image upload if provided
            if (updateDto.ProductImage != null)
            {
                var imgUrl = await FileHelper.SaveImageAsync(updateDto.ProductImage, "uploads/image/Product");

                if (product.ProductImage == null)
                {
                    product.ProductImage = new ProductImage
                    {
                        ImageUrl = imgUrl,
                        ProductId = id
                    };
                }
                else
                {
                    product.ProductImage.ImageUrl = imgUrl;
                }

            }
            var updatedProduct = await _productRepository.UpdateProductAsync(product);

            return new ProductUpdateDto
            {
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
                BasePrice = updatedProduct.BasePrice,
                CategoryId = updatedProduct.CategoryId,
            };
        }

        public async Task<ProductVariantDto?> GetProductVariantByIdAsync(int variantId)
        {
            var pVarient = await _productRepository.GetProductVariantByIdAsync(variantId);
            if (pVarient == null) throw new Exception("not found");

            return new ProductVariantDto
            {
                Id = pVarient.Id,
                Size = pVarient.Size,
                Color = pVarient.Color,
                Price = pVarient.Price,
                Stock = pVarient.Stock,
                IsActive = pVarient.IsActive,
                VendorId = pVarient.Product.VendorId,
                VarientImage = pVarient.VarientImage.ImageUrl
            };
        }

        public Task UpdateProductVariantAsync(ProductVariant variant)
        {
            throw new NotImplementedException();
        }
    }
}
