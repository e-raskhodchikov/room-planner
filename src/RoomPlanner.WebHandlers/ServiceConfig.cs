using LightInject;
using RoomPlanner.WebHandlers.Handlers;
using RoomPlanner.WebHandlers.Mappers;

namespace RoomPlanner.WebHandlers
{
	public static class ServiceConfig
	{
		public static void RegisterServices(IServiceRegistry serviceRegistry)
		{
			Business.ServiceConfig.RegisterServices(serviceRegistry);

			var assembly = typeof (ServiceConfig).Assembly;
			var webHandlerType = typeof (IWebHandler);
			var mapperType = typeof (IMapper);

			serviceRegistry.RegisterAssembly(
				assembly,
				() => new PerRequestLifeTime(),
				(serviceType, implementingType) => webHandlerType.IsAssignableFrom(serviceType));

			serviceRegistry.RegisterAssembly(
				assembly,
				() => new PerRequestLifeTime(),
				(serviceType, implementingType) => mapperType.IsAssignableFrom(serviceType));
		}
	}
}
