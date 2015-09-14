using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace RoomPlanner.Website
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			ServiceConfig.RegisterServices();
		}

		private void Application_Error(object sender, EventArgs e)
		{

		}
	}
}
