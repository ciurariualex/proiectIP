using AutoMapper;
using Core.Business.Models.Interfaces;
using Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Models.Car;
using Xunit;
using static System.Linq.Enumerable;

namespace xUnitTest
{
    [Trait("Category", "# Calendar Controller")]
    public class CarControllerTest
    {
        #region Setup
        protected Mock<ICarRepository> MockCarData { get; }
        protected Mock<IShowroomRepository> MockShowroomData { get; }
        protected Mock<IMapper> MockMapper { get; }
        protected CarController ControllerUnderTest { get; }

        public CarControllerTest()
        {
            MockCarData = new Mock<ICarRepository>();
            MockShowroomData = new Mock<IShowroomRepository>();
            MockMapper = new Mock<IMapper>();

            ControllerUnderTest = new CarController(
               MockCarData.Object,
               MockShowroomData.Object,
               MockMapper.Object);
        }
        #endregion

        #region Utils
        private Car GetCar()
        {
            return new Car
            {
                Brand = "Brand",
                Model = "Car",
                VIN = "VIN",
                ShowroomId = new Guid("02b8650c-e52b-4f29-bdab-da8950d98b51"),
                Showroom = new Showroom
                {
                    Name = "ShowroomName",
                    Id = new Guid("02b8650c-e52b-4f29-bdab-da8950d98b51"),
                    IsDeleted = false,
                    Edited = DateTime.UtcNow,
                    Created = DateTime.UtcNow,
                    Cars = new List<Car>()
                },
                Id = new Guid("02b8651c-e52b-4f29-bdab-da8950d98b51"),
                Edited = DateTime.UtcNow,
                Created = DateTime.UtcNow,
                IsDeleted = false
            };
        }

        private Showroom GetShowroom()
        {
            return new Showroom
            {
                Name = "ShowroomName",
                Id = new Guid("02b8650c-e52b-4f29-bdab-da8950d98b51"),
                IsDeleted = false,
                Edited = DateTime.UtcNow,
                Created = DateTime.UtcNow,
                Cars = new List<Car>()
            };
        }

        private CarEditViewModel GetCarEditViewModel(Car car)
        {
            return new CarEditViewModel
            {
                Brand = car.Brand,
                Model = car.Model,
                VIN = car.VIN,
                ShowroomId = car.ShowroomId.HasValue ? car.ShowroomId.Value : Guid.Empty,
                Id = car.Id
            };
        }

        private CarDetailsViewModel GetCarDetailsViewModel(Car car)
        {
            return new CarDetailsViewModel
            {
                Brand = car.Brand,
                Model = car.Model,
                VIN = car.VIN,
                Showroom = car.Showroom,
                Id = car.Id
            };
        }

        private List<Car> GetListCar(int count) => Repeat(GetCar(), count).ToList();
        #endregion

        #region Tests
        [Trait("Car", "IndexGET")]
        [Fact]
        private async Task TestIndexGET()
        {
            // Arrange
            int carsCount = 3;
            var cars = GetListCar(carsCount).AsQueryable();
            var model = new CarListViewModel(cars, 1, 10);

            MockCarData
                .Setup(_ => _.GetAllActive())
                .ReturnsAsync(cars);

            // Act
            dynamic response = await ControllerUnderTest.Index(1, 3);

            // Assert
            Assert.True(response.Model.Count.Equals(carsCount));
        }

        [Trait("Car", "CreateGET")]
        [Fact]
        private async Task TestCreateGET()
        {
            // Arrange
            MockShowroomData
                .Setup(_ => _.GetAllActive())
                .ReturnsAsync(new List<Showroom>().AsEnumerable());

            // Act
            dynamic response = await ControllerUnderTest.Create();

            // Assert
            MockShowroomData.Verify(_ => _.GetAllActive(), Times.Once);
            Assert.True(response.Model.GetType() == typeof(CarEditViewModel));
        }

        [Trait("Car", "CreatePOST")]
        [Fact]
        private async Task TestCreatePOST()
        {

            // Arrange
            var id = Guid.Empty;
            var showroom = GetShowroom();
            var car = GetCar();
            var model = GetCarEditViewModel(car);

            MockMapper
              .Setup(_ => _.Map<Car>(model))
              .Returns(car);

            MockShowroomData
              .Setup(_ => _.GetById(model.ShowroomId))
              .ReturnsAsync(showroom);

            // Act
            //ControllerUnderTest.ModelState.AddModelError("key", "error"); // force invalid input
            dynamic response = await ControllerUnderTest.Create(model);

            // Assert
            MockShowroomData.Verify(_ => _.GetById(model.ShowroomId), Times.Once);
            MockMapper.Verify(_ => _.Map<Car>(model), Times.Once);

            Assert.True(response.RouteValues.Values[0].Equals(car.Id));
            Assert.True(response.ActionName.Equals("Details"));

        }

        [Trait("Car", "CreatePOSTInvalidModelState")]
        [Fact]
        private async Task TestCreatePOSTInvalidModelState()
        {
            // Arrange
            var car = GetCar();
            var model = GetCarEditViewModel(car);

            // Act
            ControllerUnderTest.ModelState.AddModelError("key", "error"); // force invalid input
            dynamic response = await ControllerUnderTest.Create(model);

            // Assert
            MockShowroomData.Verify(_ => _.GetAllActive(), Times.Once);

            Assert.True(response.Model != null);

        }

        [Trait("Car", "EditGET")]
        [Fact]
        private async Task TestEditGET()
        {

            // Arrange
            var id = Guid.Empty;
            var car = GetCar();
            var model = GetCarEditViewModel(car);

            MockCarData
                .Setup(_ => _.GetById(id))
                .ReturnsAsync(car);
            MockMapper
               .Setup(_ => _.Map<CarEditViewModel>(car))
               .Returns(model);
            MockShowroomData
                .Setup(_ => _.GetAllActive())
                .ReturnsAsync(new List<Showroom>().AsEnumerable());

            // Act
            dynamic response = await ControllerUnderTest.Edit(id);

            // Assert
            MockCarData.Verify(_ => _.GetById(id), Times.Once);
            Assert.True(response.Model.Brand.Equals(model.Brand));

        }

        [Trait("Car", "EditPOST")]
        [Fact]
        private async Task TestEditPOST()
        {

            // Arrange
            var id = Guid.Empty;
            var car = GetCar();
            var model = GetCarEditViewModel(car);

            MockCarData
                .Setup(_ => _.GetById(id))
                .ReturnsAsync(car);
            MockMapper
               .Setup(_ => _.Map<Car>(model))
               .Returns(car);

            // Act
            dynamic response = await ControllerUnderTest.Edit(model);

            // Assert
            MockCarData.Verify(_ => _.GetById(model.Id), Times.Once);
            MockCarData.Verify(_ => _.Update(car), Times.Once);
            Assert.True(response.RouteValues.Values[0].Equals(car.Id));

        }

        [Trait("Car", "DetailsGET")]
        [Fact]
        private void TestDetailsGET()
        {

            // Arrange
            var id = Guid.Empty;
            var car = GetCar();
            var model = GetCarDetailsViewModel(car);

            MockCarData
                .Setup(_ => _.GetById(id))
                .ReturnsAsync(car);
            MockMapper
               .Setup(_ => _.Map<CarDetailsViewModel>(car))
               .Returns(model);

            // Act
            dynamic response = ControllerUnderTest.Details(id);

            // Assert
            MockCarData.Verify(_ => _.GetById(id), Times.Once);
            MockMapper.Verify(_ => _.Map<CarDetailsViewModel>(car), Times.Once);

            Assert.True(response.Model.Id.Equals(model.Id));
            Assert.True(response.Model.Brand.Equals(model.Brand));
            Assert.True(response.Model.Model.Equals(model.Model));
            Assert.True(response.Model.VIN.Equals(model.VIN));

        }

        [Trait("Car", "DeletePOST")]
        [Fact]
        private async Task TestDeletePOST()
        {
            // Arrange

            // Act
            dynamic response = await ControllerUnderTest.Delete(Guid.Empty);

            // Assert
            Assert.True(response.ActionName.Equals("Index"));
        }
        #endregion
    }
}
