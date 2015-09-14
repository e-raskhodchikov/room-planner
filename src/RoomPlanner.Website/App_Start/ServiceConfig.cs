using LightInject;

namespace RoomPlanner.Website
{
    public static class ServiceConfig
    {
        public static void RegisterServices()
        {
            var container = new ServiceContainer();

            WebHandlers.ServiceConfig.RegisterServices(container);

            container.RegisterControllers();
            container.EnableMvc();
        }
    }
}
