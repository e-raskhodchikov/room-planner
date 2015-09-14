using System;
using System.Collections.Generic;
using System.Linq;
using RoomPlanner.DataAccess;
using RoomPlanner.Domain;

namespace RoomPlanner.Business.Services
{
	public class HistoryService : IService
	{
		private readonly Dao dao;

		private List<Room> rooms;
		private List<FurnitureCountOnDate> furnitureActions;

		private List<RoomHistory> roomHistories;

		private Dictionary<int, List<FurnitureCount>> furnituresRunningTotal; 

		public HistoryService(Dao dao)
		{
			this.dao = dao;
		}

		public List<RoomHistory> GetHistory()
		{
			LoadData();

			roomHistories = new List<RoomHistory>();
			furnituresRunningTotal = new Dictionary<int, List<FurnitureCount>>();

			PopulateHistory();

			return roomHistories.OrderByDescending(x => x.Date).ThenBy(x => x.Name).ToList();
		}

		private void LoadData()
		{
			rooms = dao.GetRooms();
			furnitureActions = dao.GetFurnitureCountOnDates();
		}

		private void PopulateHistory()
		{
			foreach (var room in rooms)
			{
				AddToHistory(new RoomHistory
				{
					RoomId = room.Id.GetValueOrDefault(),
					Name = room.Name,
					Date = room.CreateDate,
					ActionType = RoomActionType.Created,
					Furnitures = GetFurnitureCount(room.Id.GetValueOrDefault(), room.CreateDate)
				});

				var furnitureActionsOnRoom = furnitureActions
					.Where(x => x.RoomId == room.Id && x.Date != room.CreateDate && x.Date != room.RemoveDate.GetValueOrDefault())
					.GroupBy(x => x.Date)
					.OrderBy(x => x.Key);

				foreach (var action in furnitureActionsOnRoom)
				{
					AddToHistory(new RoomHistory
					{
						RoomId = room.Id.GetValueOrDefault(),
						Name = room.Name,
						Date = action.Key,
						ActionType = RoomActionType.None,
						Furnitures = action.Cast<FurnitureCount>().ToList()
					});
				}

				if (room.RemoveDate.HasValue)
				{
					AddToHistory(new RoomHistory
					{
						RoomId = room.Id.GetValueOrDefault(),
						Name = room.Name,
						Date = room.RemoveDate.Value,
						ActionType = RoomActionType.Removed,
						Furnitures = GetFurnitureCount(room.Id.GetValueOrDefault(), room.RemoveDate.Value)
					});
				}
			}
		}

		private void AddToHistory(RoomHistory history)
		{
			var runningTotal = furnituresRunningTotal.ContainsKey(history.RoomId) ? furnituresRunningTotal[history.RoomId] : null;
			if (runningTotal != null)
			{
				foreach (var furniture in history.Furnitures)
				{
					var furnitureRunningTotal = runningTotal.FirstOrDefault(x => x.Furniture == furniture.Furniture);
					if (furnitureRunningTotal != null)
					{
						furnitureRunningTotal.Count += furniture.Count;
					}
					else
					{
						runningTotal.Add(furniture);
					}
				}

				history.Furnitures = runningTotal.ToList();
			}
			else
			{
				furnituresRunningTotal[history.RoomId] = history.Furnitures;
			}

			history.Furnitures = history.Furnitures.Select(x => new FurnitureCount
			{
				Furniture = x.Furniture,
				RoomId = x.RoomId,
				Count = x.Count
			}).ToList();
			roomHistories.Add(history);
		}

		private List<FurnitureCount> GetFurnitureCount(int roomId, DateTime date)
		{
			return furnitureActions.Where(x => x.RoomId == roomId && x.Date == date).Cast<FurnitureCount>().ToList();
		}
	}
}
