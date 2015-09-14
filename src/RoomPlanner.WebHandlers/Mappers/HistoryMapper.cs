using System.Collections.Generic;
using System.Linq;
using RoomPlanner.Domain;
using RoomPlanner.WebHandlers.ClientData;

namespace RoomPlanner.WebHandlers.Mappers
{
	public class HistoryMapper : Mapper
	{
		public List<RoomHistoryClientData> Map(List<RoomHistory> data, bool isShort)
		{
			return data.Select(x => Map(x, isShort)).ToList();
		}

		private static RoomHistoryClientData Map(RoomHistory data, bool isShort)
		{
			var result = new RoomHistoryClientData
			{
				Date = Map(data.Date)
			};

			if (!isShort)
			{
				result.Name = data.Name;
				result.Description = Map(data.Furnitures);
				result.ActionType = data.ActionType;
			}

			return result;
		}

		private static string Map(IEnumerable<FurnitureCount> furnitureCounts)
		{
			return string.Join(", ", furnitureCounts.Select(Map));
		}

		private static string Map(FurnitureCount furnitureCount)
		{
			return string.Format("{0} × {1}", furnitureCount.Furniture, furnitureCount.Count);
		}
	}
}
