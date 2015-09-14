using System;
using System.Web.Mvc;
using RoomPlanner.WebHandlers.Handlers;

namespace RoomPlanner.Website.Controllers
{
	public class PlanController : Controller
	{
		private readonly PlanHandler planHandler;

		public PlanController(PlanHandler planHandler)
		{
			this.planHandler = planHandler;
		}

		public ActionResult GetPlan(DateTime? date)
		{
			return Json(planHandler.GetPlan(date));
		}
	}
}
