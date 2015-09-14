using System;
using System.Web.Mvc;
using RoomPlanner.WebHandlers;

namespace RoomPlanner.Website.Controllers
{
    public class RoomController : Controller
    {
	    private readonly PlanHandler planHandler;

	    public RoomController(PlanHandler planHandler)
	    {
		    this.planHandler = planHandler;
	    }

	    public ActionResult Create(string roomName, DateTime date)
	    {
		    planHandler.CreateRoom(roomName, date);
		    return Json(true);
	    }

	    public ActionResult Remove(string roomNameToRemove, string roomNameMoveTo, DateTime date)
        {
            return Json(true);
        }
    }
}
