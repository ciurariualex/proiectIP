using AutoMapper;
using Core.Business.Models.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Car;
using Web.Utils;

namespace Web.Controllers
{
    public class CarController : Controller
    {
        protected readonly ICarRepository _carModel;
        protected readonly IShowroomRepository _showroomModel;
        protected readonly IMapper _mapper;

        public CarController(ICarRepository carRepository,
            IShowroomRepository showroomRepository,
            IMapper mapper)
        {
            _carModel = carRepository;
            _showroomModel = showroomRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var cars = await _carModel.GetAllActive();
            var model = new CarListViewModel(cars, page, pageSize);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var showrooms = await _showroomModel.GetAllActive();

            return View(new CarEditViewModel(showrooms));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarEditViewModel uniqueModelName)
        {
            if (!ModelState.IsValid)
            {
                var showrooms = await _showroomModel.GetAllActive();
                uniqueModelName.SetList(showrooms);
                return View(uniqueModelName);
            }

            Car car = _mapper.Map<Car>(uniqueModelName);
            Showroom showroom = await _showroomModel.GetById(uniqueModelName.ShowroomId);
            await _carModel.Create(car);

            showroom.Cars.Add(car);

            await _showroomModel.Update(showroom);

            return RedirectToAction(nameof(Details), new { id = car.Id });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            Car car = await _carModel.GetById(id);
            var showrooms = await _showroomModel.GetAllActive(); ;

            var model = _mapper.Map<CarEditViewModel>(car);

            model.SetList(showrooms);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarEditViewModel uniqueModelName)
        {
            if (!ModelState.IsValid)
            {
                var showrooms = await _showroomModel.GetAllActive();
                uniqueModelName.SetList(showrooms);

                return View(uniqueModelName);
            }

            Car car = _carModel.GetById(uniqueModelName.Id).Result;
            car = _mapper.Map<Car>(uniqueModelName);

            await _carModel.Update(car);

            return RedirectToAction(nameof(Details), new { id = car.Id });
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            Car car = _carModel.GetById(id).Result;
            var model = _mapper.Map<CarDetailsViewModel>(car);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _carModel.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GenerateXML()
        {
            var cars = await _carModel.GetAllActive();

            return File(
                fileContents: cars.GenerateXml(),
                contentType: "application/xml",
                fileDownloadName: "Cars.xml");
        }

        [HttpGet]
        public async Task<IActionResult> GenerateExcel()
        {
            var cars = await _carModel.GetAllActive();

            return File(
                fileContents: cars.GenerateExcel(null, GetPath()),
                contentType: "application/vnd.ms-excel",
                fileDownloadName: "Cars.xlsx");

            string GetPath() => Path.GetFullPath(Paths.CarPathLogo);
        }

    }
}