using EcommerceProductModule.Controllers;
using EcommerceProductModule.Models.Dtos;
using EcommerceProductModule.Models.Dtos.ProductDto;
using EcommerceProductModule.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ECommersProduct.Test
{
    public class ProductCotrollerTest
    {
        private readonly Mock<IProductService> _productService;
        private readonly ProductController _productController;
        public ProductCotrollerTest()
        {
            _productService = new Mock<IProductService>();
            _productController = new ProductController(_productService.Object);
        }
        [Fact]
        public async void CreateProduct_Success()
        {
            // Act 
            var mockIProduct = new Mock<IProductService>();
            var prod = new ProductCreateDto
            {
                Id = 1,
                Name = "Test",
                StockQuantity = 1,
                Description = "Test",
                CategoryId = 1,
                DiscountPercentage = 1,
                ImageUrl = "TestUrl", 
                IsAvailable = true,
                Price = 1,
            };
            mockIProduct.Setup(mockObj => mockObj.CreateProductAsync(prod)).ReturnsAsync(
                new ApiResponse<ProductResponseDto>()
                {
                    Data = new ProductResponseDto()
                    {
                        Id = 1,
                        Name = "Test",
                        StockQuantity = 1,
                        Description = "Test",
                        CategoryId = 1,
                        DiscountPercentage = 1,
                        ImageUrl = "TestUrl",
                        IsAvailable = true,
                        Price = 1,
                    },
                    Message = "Product details added successfully.",
                    Status = true,
                    StatusCode = 200
                });

            // Arrange

            var controller = new ProductController(mockIProduct.Object);
            var result = await controller.CreateProduct(prod);
            var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            var respProduct = Assert.IsType<ApiResponse<ProductResponseDto>>(okObjectResult.Value);
            // Asssert
            Assert.NotNull(result);
            Assert.Equal(prod.Name, respProduct.Data.Name);
        }
        [Fact]
        public async void CreateProduct_Failed()
        {
            //Act
            ProductCreateDto prod = null;


            var mock = new Mock<IProductService>();
            mock.Setup(mockobj => mockobj.CreateProductAsync(prod));

            //Arrange
            var controller = new ProductController(mock.Object);
            var result = await controller.CreateProduct(prod);

            //Assert
            var okResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.Equal(400, okResult.StatusCode);
        }
        [Fact]
        public async void UpdateProduct_sucess()
        {
            //Arrange
            var updateProd = new ProductUpdateDto
            {
                Id = 1,
                Name = "Test",
                StockQuantity = 1,
                Description = "Test",
                CategoryId = 1,
                DiscountPercentage = 1,
                ImageUrl = "TestUrl",
                IsAvailable = true,
                Price = 1,
            };
            var mock = new Mock<IProductService>();
            mock.Setup(mockobj => mockobj.UpdateProductAsync(updateProd)).ReturnsAsync(
                new ApiResponse<ProductResponseDto>
                {
                    Data = new ProductResponseDto {
                        Id = 1,
                        Name = "Test",
                        StockQuantity = 1,
                        Description = "Test",
                        CategoryId = 1,
                        DiscountPercentage = 1,
                        ImageUrl = "TestUrl",
                        IsAvailable = true,
                        Price = 1,
                    },
                    Message = "Data updatated successfully",
                    Status = true,
                    StatusCode = 200
                });
            //Act
            var controller = new ProductController(mock.Object);
            var result = await controller.UpdateProduct(updateProd);

            //Asset
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var productResult = Assert.IsType<ApiResponse<ProductResponseDto>>(okResult.Value);
            Assert.Equal(updateProd.Id, productResult.Data.Id);
        }
        [Fact]
        public async void UpdateProduct_failed()
        {
            ProductUpdateDto productUpdate = null;
            var mock = new Mock<IProductService>();
            mock.Setup(u => u.UpdateProductAsync(productUpdate));

            var controller = new ProductController(mock.Object);
            var result = await controller.UpdateProduct(productUpdate);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }
        [Fact]
        public async void DeleteProduct_Success()
        {
            //Arrange
            _productService.Setup(u => u.DeleteProductAsync(1)).ReturnsAsync(
                new ApiResponse<ProductResponseDto>
                {
                    Data = new ProductResponseDto
                    {
                        Id = 1,
                        Name = "Test",
                        StockQuantity = 1,
                        Description = "Test",
                        CategoryId = 1,
                        DiscountPercentage = 1,
                        ImageUrl = "TestUrl",
                        IsAvailable = true,
                        Price = 1,
                    },
                    Status = true,
                    StatusCode = 200,
                    Message = "Product deleted successfully."
                });

            //act
            var result = await _productController.DeleteProduct(1);

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(result.Result);
            var productResult = Assert.IsType<ApiResponse<ProductResponseDto>>(okObjResult.Value);

            Assert.Equal(1,productResult.Data.Id);
        }
        [Fact]
        public async void DeleteProduct_Failed()
        {

            _productService.Setup(u => u.DeleteProductAsync(0));

            var result = await _productController.DeleteProduct(1);

            var okObjResult = Assert.IsType<BadRequestResult>(result.Result);
            Assert.Equal(400, okObjResult.StatusCode);
        }
    }
}