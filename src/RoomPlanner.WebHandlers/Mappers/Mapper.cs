using System;

namespace RoomPlanner.WebHandlers.Mappers
{
	public class Mapper : IMapper
	{
		protected static string Map(DateTime? date)
		{
			return date != null ? date.Value.ToString("dd.MM.yyyy") : string.Empty;
		}
	}
}
