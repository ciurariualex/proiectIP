using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Business.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Home;

namespace Web.Controllers
{
	[Authorize]
    public class HomeController : BaseController
    {
        protected readonly ICarRepository _carModel;
        protected readonly IShowroomRepository _showroomModel;
		protected readonly IUserRepository _userModel;

		public HomeController(ICarRepository carRepository,
            IShowroomRepository showroomRepository,
			IUserRepository userRepository)
		{
            _carModel = carRepository;
            _showroomModel = showroomRepository;
			_userModel = userRepository;
		}
        public IActionResult Index()
        {
			var model = new HomeViewModel();

			model.CarCount = _carModel.Count();
			model.ShowroomCount = _showroomModel.Count();
			model.UserCount = 1;

            return View(model);
        }
    }
}