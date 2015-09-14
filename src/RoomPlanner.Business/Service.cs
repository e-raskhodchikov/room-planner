using System;
using System.Collections.Generic;
using System.Linq;
using RoomPlanner.DataAccess;
using RoomPlanner.Domain;

namespace RoomPlanner.Business
{
	public class Service : IService
	{
		private readonly Dao dao;

		public Service(Dao dao)
		{
			this.dao = dao;
		}

		public void CreateRoom(string name, DateTime date)
		{
			var room = dao.GetRoom(name);
			if (room != null)
			{
				return;
			}

			room = new Room
			{
				Name = name,
				CreateDate = date
			};
			dao.CreateRoom(room);
		}

		public Plan GetExistingRooms(DateTime date)
		{
			var existingRooms = dao.GetExistingRooms(date);
			var roomIds = existingRooms.Where(x => x.Id.HasValue).Select(x => x.Id.Value).ToArray();
			var furnitureCount = dao.GetFurnitureCount(date, roomIds);

			return new Plan
			{
				Rooms = existingRooms,
				FurnitureCount = furnitureCount
			};
		}

		public void CreateFurniture(string furnitureName, string roomName, DateTime date)
		{
			var room = dao.GetRoom(roomName);
			if (room == null || !room.Id.HasValue || room.RemoveDate.HasValue)
			{
				return;
			}

			var action = new FurnitureAction
			{
				RoomId = room.Id.Value,
				Furniture = furnitureName,
				Date = date,
				ActionType = FurnitureActionType.Create
			};
			dao.CreateFurnitureAction(action);
		}
	}
}
