using LightInject;

namespace RoomPlanner.Business
{
	public static class ServiceConfig
	{
		public static void RegisterServices(IServiceRegistry serviceRegistry)
		{
			DataAccess.ServiceConfig.RegisterServices(serviceRegistry);

			var assembly = typeof (ServiceConfig).Assembly;
			var webHandlerType = typeof (IService);

			serviceRegistry.RegisterAssembly(
				assembly,
				() => new PerRequestLifeTime(),
				(serviceType, implementingType) => webHandlerType.IsAssignableFrom(serviceType));
		}
	}
}
