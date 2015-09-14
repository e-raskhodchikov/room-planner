using System;
using System.Collections.Generic;
using RoomPlanner.Business;
using RoomPlanner.WebHandlers.ClientData;
using RoomPlanner.WebHandlers.Mappers;

namespace RoomPlanner.WebHandlers
{
    public class PlanHandler : IWebHandler
    {
	    private readonly Service service;
	    private readonly Mapper mapper;

	    public PlanHandler(Service service, Mapper mapper)
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

	    public RoomHistoryClientData[] GetHistory(bool isShort)
	    {
		    return new[]
		    {
			    new RoomHistoryClientData
			    {
				    Date = "12.09.2015",
				    Name = isShort ? "" : "Кухня",
				    Description = isShort ? "" : "стол × 1, стул × 2"
			    },
			    new RoomHistoryClientData
			    {
				    Date = "13.09.2015",
				    Name = isShort ? "" : "Спальня",
				    Description = isShort ? "" : "диван × 1, стол × 1, стул × 2"
			    }
		    };
	    }
    }
}
