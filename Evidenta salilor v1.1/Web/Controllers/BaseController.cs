using Core.Business.Models.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
	public abstract class BaseController : Controller
	{

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{

			base.OnActionExecuting(filterContext);

		}
	}
}