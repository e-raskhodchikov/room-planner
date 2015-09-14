using System.Collections.Generic;

namespace RoomPlanner.WebHandlers.ClientData
{
	public class RoomPlanClientData
	{
		public string Name { get; set; }

		public string RemoveDate { get; set; }

		public bool IsReadonly { get; set; }

		public string ReadonlyMessage { get; set; }

		public List<FurniturePlanClientData> Furnitures { get; set; }
	}
}
