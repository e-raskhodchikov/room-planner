using System;

namespace RoomPlanner.Domain
{
	public class Room
	{
		public int? Id { get; set; }

		public string Name { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime? RemoveDate { get; set; }
	}
}
