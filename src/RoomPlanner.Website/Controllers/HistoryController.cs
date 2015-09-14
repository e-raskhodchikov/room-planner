using System.Web.Mvc;
using RoomPlanner.WebHandlers.Handlers;

namespace RoomPlanner.Website.Controllers
{
	public class HistoryController : Controller
	{
		private readonly HistoryHandler handler;

		public HistoryController(HistoryHandler handler)
		{
			this.handler = handler;
		}

		public ActionResult GetHistory(bool isShort = false)
		{
			return Json(handler.GetHistory(isShort));
		}
	}
}
