//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Moq;
//using RACE2.DataAccess.Repository;
//using RACE2.DatabaseProvider.Data;
//using RACE2.Dto;
//using RACE2.Services;
//using System.IO;

//namespace RACE2.UnitTest
//{
//    public class ReservoirServiceTest
//    {
//        private readonly Mock<IUserService> userService;
//        private readonly Mock<IUserRepository> userRepository;
//        private readonly IConfiguration configuration;

//        public ReservoirServiceTest()
//        {
//            userRepository = new Mock<IUserRepository>();
//            userService = new Mock<IUserService>();
//        }



//        //private UserRepository userRepository;
//        //public  DbContextOptions<ApplicationDbContext> dbContextOptions { get; }
//        //string connectionString = configuration["SqlConnectionString"];

//        //public ReservoirServiceTest(IConfiguration _configuration)
//        //{
//        //    dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
//        //        .UseSqlServer8
//        //        .Options;
//        //}

//        [Fact]
//        public async void GetUserByEmailID_Should_Return_Value()
//        {
//            //arrange
//            var userList =await GetUsers();
//            userService.Setup(x => x.GetUserByEmailID("mahalakshmi.alagarsamy@capgemini.com")).Returns((Task<UserSpecificDto>)userList);

//            //var result = userRepository.GetUserByEmailID("mahalakshmi.alagarsamy@capgemini.com");
                
//           //// var result  = userService.
//           // //act
//           // var productResult = productController.ProductList();
//           // //assert
//           // Assert.NotNull(productResult);
//           // Assert.Equal(GetProductsData().Count(), productResult.Count());
//           // Assert.Equal(GetProductsData().ToString(), productResult.ToString());
//           // Assert.True(productList.Equals(productResult));
//        }
       
//        private Task<IEnumerable<UserSpecificDto>> GetUsers()
//        {
//            IEnumerable<UserSpecificDto> usersData = new List<UserSpecificDto>()
//            {
//                new UserSpecificDto
//                {
//                     Id = 1,
//                     cFirstName = "Test User1",
//                     cLastName = "Name 1",
//                     Email = "test.user1@race.test",
//                     cBackEndUserId = "1",
//                     cMobile = "01234567890"
//                },
//                new UserSpecificDto
//                {
//                     Id = 2,
//                     cFirstName = "Test User2",
//                     cLastName = "Name 2",
//                     Email = "test.user2@race.test",
//                     cBackEndUserId = "2",
//                     cMobile = "01234567890"
//                }
//            };

//            return Task.FromResult(usersData); ;
//        }

//        //private List<reservoir> GetReservoirs()
//        //{
//        //    List<ReservoirDetailsDTO> reservoirData = new List<ReservoirDetailsDTO>
//        //{
//        //    new ReservoirDetailsDTO
//        //    {
//        //        ProductId = 1,
//        //        ProductName = "IPhone",
//        //        ProductDescription = "IPhone 12",
//        //        ProductPrice = 55000,
//        //        ProductStock = 10
//        //    },
//        //     new ReservoirDetailsDTO
//        //    {
//        //        ProductId = 2,
//        //        ProductName = "Laptop",
//        //        ProductDescription = "HP Pavilion",
//        //        ProductPrice = 100000,
//        //        ProductStock = 20
//        //    },
//        //     new ReservoirDetailsDTO
//        //    {
//        //        ProductId = 3,
//        //        ProductName = "TV",
//        //        ProductDescription = "Samsung Smart TV",
//        //        ProductPrice = 35000,
//        //        ProductStock = 30
//        //    },
//        //};
//        //    return reservoirData;
//        //}

//    }
//}