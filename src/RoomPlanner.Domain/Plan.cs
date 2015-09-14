using System.Collections.Generic;

namespace RoomPlanner.Domain
{
	public class Plan
	{
		public List<Room> Rooms { get; set; }

		public List<FurnitureCount> FurnitureCount { get; set; }
	}
}