using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Business.Models.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resources;
using Web.Models.Showroom;
using Web.Utils;

namespace Web.Controllers
{
    [Authorize]
    public class ShowroomController : Controller
    {
        protected readonly IShowroomRepository _showroomModel;
        protected readonly ICarRepository _carModel;

        protected readonly IMapper _mapper;

        public ShowroomController(IShowroomRepository showroomRepository,
            ICarRepository carRepository,
            IMapper mapper)
        {
            _showroomModel = showroomRepository;
            _carModel = carRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var showrooms = await _showroomModel.GetAllActive();

            var model = new ShowroomListViewModel(showrooms, page, pageSize);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ShowroomEditViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShowroomEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Showroom showroom = _mapper.Map<Showroom>(model);

                await _showroomModel.Create(showroom);

                return RedirectToAction(nameof(Details), new { id = showroom.Id });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Showroom showroom = await _showroomModel.GetById(id);
            var model = _mapper.Map<ShowroomEditViewModel>(showroom);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ShowroomEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Showroom showroom = await _showroomModel.GetById(model.Id);

                showroom = _mapper.Map<Showroom>(model);

                await _showroomModel.Update(showroom);

                return RedirectToAction(nameof(Details), new { id = showroom.Id });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            Showroom showroom = await _showroomModel.GetById(id);
            var model = _mapper.Map<ShowroomDetailsViewModel>(showroom);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _showroomModel.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GenerateXML()
        {
            var showrooms = await _showroomModel.GetAllActive();

            return File(
                fileContents: showrooms.GenerateXml(),
                contentType: "application/xml",
                fileDownloadName: "Showrooms.xml");
        }

        [HttpGet]
        public async Task<IActionResult> GenerateExcel()
        {
            var showrooms = await _showroomModel.GetAllActive();

            return File(
                fileContents: showrooms.GenerateExcel(null, GetPath()),
                contentType: "application/vnd.ms-excel",
                fileDownloadName: "Showrooms.xlsx");

            string GetPath() => Path.GetFullPath(Paths.ShowroomPathLogo);

        }
        [HttpGet]
        public ActionResult UploadXML()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadXML(IFormFile file)
        {
            List<ShowroomListForXMLModel.Showroom> showroomList = Utils.UploadXML.UploadShowroomXML(file);
            Showroom tempShowroom;
            Car tempCar;
            foreach (var showroom in showroomList)
            {
                tempShowroom = _showroomModel.GetByName(showroom.Name).Result;
                if (tempShowroom != null)
                {
                    foreach (var car in showroom.Cars)
                    {
                        tempCar = _carModel.GetByVIN(car.VIN).Result;
                        if (tempCar != null)
                        {
                            await _carModel.Update(new Car
                            {
                                Brand = car.Brand,
                                Model = car.Model,
                                ShowroomId = tempShowroom.Id,
                                Id = tempCar.Id
                            });
                        }
                        else
                        {
                            await _carModel.Create(new Car
                            {
                                Brand = car.Brand,
                                Model = car.Model,
                                VIN = car.VIN,
                                ShowroomId = tempShowroom.Id
                            });
                        }
                    }
                }
                else
                {
                    tempShowroom = new Showroom { Name = showroom.Name };
                    await _showroomModel.Create(tempShowroom);

                    foreach (var car in showroom.Cars)
                    {
                        await _carModel.Create(new Car
                        {
                            Brand = car.Brand,
                            Model = car.Model,
                            VIN = car.VIN,
                            ShowroomId = tempShowroom.Id
                        });
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}