using System.Collections.Generic;
using RoomPlanner.Business.Services;
using RoomPlanner.WebHandlers.ClientData;
using RoomPlanner.WebHandlers.Mappers;

namespace RoomPlanner.WebHandlers.Handlers
{
	public class HistoryHandler : IWebHandler
	{
		private readonly HistoryService service;
		private readonly HistoryMapper mapper;

		public HistoryHandler(HistoryService service, HistoryMapper mapper)
		{
			this.service = service;
			this.mapper = mapper;
		}

		public List<RoomHistoryClientData> GetHistory(bool isShort)
		{
			var data = service.GetHistory();
			return mapper.Map(data, isShort);
		}
	}
}
