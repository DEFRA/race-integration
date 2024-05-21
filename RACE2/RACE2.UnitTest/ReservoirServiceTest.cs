//using Microsoft.Extensions.Configuration;
//using Moq;
//using RACE2.DataAccess.Repository;
//using RACE2.Dto;
//using RACE2.Services;
//using System.IO;

//namespace RACE2.UnitTest
//{
//    public class ReservoirServiceTest
//    {
//        private readonly Mock<IReservoirService> reservoirService;
//        private readonly Mock<IReservoirRepository> reservoirRepository;
        
//        public ReservoirServiceTest()
//        {
//            reservoirService = new Mock<IReservoirService>();
//            reservoirRepository = new Mock<IReservoirRepository>();

//        }
//        [Fact]
//        public void GetProductList_ProductList()
//        {
//            //arrange
//            var productList = GetProductsData();
//            productService.Setup(x => x.GetProductList())
//                .Returns(productList);
//            var productController = new ProductController(productService.Object);
//            //act
//            var productResult = productController.ProductList();
//            //assert
//            Assert.NotNull(productResult);
//            Assert.Equal(GetProductsData().Count(), productResult.Count());
//            Assert.Equal(GetProductsData().ToString(), productResult.ToString());
//            Assert.True(productList.Equals(productResult));
//        }

//        private List<UserSpecificDto>  GetUsers()
//        {
//            List<UserSpecificDto> usersData = new List<UserSpecificDto>()
//            {
//                new UserSpecificDto
//                {
//                     Id = 1,
//                     cFirstName = "Test User",
//                     cLastName = "Name 1",
//                     Email = "test.user1@race.test",
//                     cBackEndUserId = "1",
//                     cMobile = "01234567890"    
//                },
//                new UserSpecificDto
//                {
//                     Id = 1,
//                     cFirstName = "Test User",
//                     cLastName = "Name 2",
//                     Email = "test.user2@race.test",
//                     cBackEndUserId = "2",
//                     cMobile = "01234567890"
//                }
//            };

//            return usersData;
//        }

//        private List<reservoir> GetReservoirs()
//        {
//            List<Product> productsData = new List<Product>
//        {
//            new Product
//            {
//                ProductId = 1,
//                ProductName = "IPhone",
//                ProductDescription = "IPhone 12",
//                ProductPrice = 55000,
//                ProductStock = 10
//            },
//             new Product
//            {
//                ProductId = 2,
//                ProductName = "Laptop",
//                ProductDescription = "HP Pavilion",
//                ProductPrice = 100000,
//                ProductStock = 20
//            },
//             new Product
//            {
//                ProductId = 3,
//                ProductName = "TV",
//                ProductDescription = "Samsung Smart TV",
//                ProductPrice = 35000,
//                ProductStock = 30
//            },
//        };
//            return productsData;
//        }

//    }
//}