using AutoMapper;
using Core.Business.Models.Interfaces;
using Core.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Models.User;

namespace Web.Controllers
{
	public class UserController : BaseController
	{
		protected readonly IUserRepository _userModel;
		protected readonly IMapper _mapper;
		protected readonly UserManager<User> _userManager;
		protected readonly SignInManager<User> _singinManager;

		public UserController(IMapper mapper,
			IUserRepository userModel,
			UserManager<User> userManager,
			SignInManager<User> signInManager)
		{
			_mapper = mapper;
			_userModel = userModel;
			_userManager = userManager;
			_singinManager = signInManager;
		}

		[Authorize]
		public async Task<IActionResult> Index()
		{
			return View();
		}

		[HttpGet]
		[Route("Login")]
		public async Task<IActionResult> Login(string returnUrl)
		{
			var model = new UserLoginViewModel()
			{
				ReturnUrl = returnUrl
			};
		
			return View(model);
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login(UserLoginViewModel uniqueModelName)
		{
			if (ModelState.IsValid)
			{

				var result = await _singinManager.PasswordSignInAsync(uniqueModelName.Email, uniqueModelName.Password, uniqueModelName.IsPersistent, false);

				if (result.Succeeded)
				{
					if (!String.IsNullOrEmpty(uniqueModelName.ReturnUrl))
					{
						return Redirect(uniqueModelName.ReturnUrl);
					}

					return RedirectToAction("Index", "Home");
				}
			}

			return View(uniqueModelName);
		}

		[HttpGet]
		public async Task<IActionResult> Register()
		{
			return View(new UserRegisterViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserRegisterViewModel uniqueModelName)
		{
			if (ModelState.IsValid)
			{
				User user = _mapper.Map<User>(uniqueModelName);

				var result= await _userManager.CreateAsync(user, uniqueModelName.Password);

				if (result.Succeeded)
				{
					return Redirect(nameof(Index));
				}
			}

			return View(uniqueModelName);
		}

		public async Task<IActionResult> Logout()
		{
			await _singinManager.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}
	}
}