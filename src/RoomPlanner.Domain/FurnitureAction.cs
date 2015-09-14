using System;

namespace RoomPlanner.Domain
{
	public class FurnitureAction
	{
		public DateTime Date { get; set; }

		public string Furniture { get; set; }

		public int RoomId { get; set; }

		public FurnitureActionType ActionType { get; set; }
	}
}
