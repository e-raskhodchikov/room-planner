using System;
using System.Web.Mvc;

namespace RoomPlanner.Website.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
