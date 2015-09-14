using System;
using System.Collections.Generic;
using System.Linq;
using RoomPlanner.Domain;
using RoomPlanner.WebHandlers.ClientData;

namespace RoomPlanner.WebHandlers.Mappers
{
	public class Mapper : IMapper
	{
		public List<RoomPlanClientData> Map(Plan plan)
		{
			return plan.Rooms.Select(r => Map(r, plan.FurnitureCount.Where(f => f.RoomId == r.Id))).ToList();
		}

		private static RoomPlanClientData Map(Room data, IEnumerable<FurnitureCount> furnitures)
		{
			return new RoomPlanClientData
			{
				Name = data.Name,
				Furnitures = Map(furnitures),
				RemoveDate = Map(data.RemoveDate)
			};
		}

		private static List<FurniturePlanClientData> Map(IEnumerable<FurnitureCount> furnitures)
		{
			return furnitures.Select(Map).ToList();
		}

		private static FurniturePlanClientData Map(FurnitureCount furnitureCount)
		{
			return new FurniturePlanClientData {Name = furnitureCount.Furniture, Count = furnitureCount.Count};
		}

		private static string Map(DateTime? date)
		{
			return date != null ? date.Value.ToString("dd.MM.yyyy") : string.Empty;
		}
	}
}
