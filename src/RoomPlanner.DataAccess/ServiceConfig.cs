using LightInject;

namespace RoomPlanner.DataAccess
{
	public static class ServiceConfig
	{
		public static void RegisterServices(IServiceRegistry serviceRegistry)
		{
			var assembly = typeof (ServiceConfig).Assembly;
			var webHandlerType = typeof (IDao);

			serviceRegistry.RegisterAssembly(
				assembly,
				() => new PerRequestLifeTime(),
				(serviceType, implementingType) => webHandlerType.IsAssignableFrom(serviceType));
		}
	}
}
