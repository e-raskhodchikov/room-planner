using System;
using System.Collections.Generic;

namespace RoomPlanner.Domain
{
	public class RoomHistory
	{
		public int RoomId { get; set; }

		public DateTime Date { get; set; }

		public string Name { get; set; }

		public RoomActionType ActionType { get; set; }

		public List<FurnitureCount> Furnitures { get; set; }
	}
}