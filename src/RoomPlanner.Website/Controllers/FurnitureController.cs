using System;
using System.Web.Mvc;
using RoomPlanner.WebHandlers;

namespace RoomPlanner.Website.Controllers
{
    public class FurnitureController : Controller
    {
	    private readonly PlanHandler planHandler;

	    public FurnitureController(PlanHandler planHandler)
	    {
		    this.planHandler = planHandler;
	    }

	    public ActionResult Create(string furnitureName, string roomName, DateTime date)
	    {
		    planHandler.CreateFurniture(furnitureName, roomName, date);
            return Json(true);
        }

        public ActionResult Move(string furnitureName, string roomNameMoveFrom, string roomNameMoveTo, DateTime date)
        {
            return Json(true);
        }
    }
}