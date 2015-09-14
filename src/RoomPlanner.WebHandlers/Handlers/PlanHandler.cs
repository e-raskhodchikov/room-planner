using System;
using System.Collections.Generic;
using RoomPlanner.Business.Services;
using RoomPlanner.WebHandlers.ClientData;
using RoomPlanner.WebHandlers.Mappers;

namespace RoomPlanner.WebHandlers.Handlers
{
    public class PlanHandler : IWebHandler
    {
	    private readonly PlanService service;
	    private readonly PlanMapper mapper;

	    public PlanHandler(PlanService service, PlanMapper mapper)
	    {
		    this.service = service;
		    this.mapper = mapper;
	    }

	    public List<RoomPlanClientData> GetPlan(DateTime? date)
	    {
		    var rooms = service.GetExistingRooms(date ?? DateTime.Today);
		    return mapper.Map(rooms);
	    }

	    public void CreateRoom(string roomName, DateTime date)
	    {
		    service.CreateRoom(roomName, date);
	    }

	    public void CreateFurniture(string furnitureName, string roomName, DateTime date)
	    {
		    service.CreateFurniture(furnitureName, roomName, date);
	    }

	    public void RemoveRoom(string roomNameToRemove, string roomNameMoveTo, DateTime date)
	    {

	    }
    }
}
