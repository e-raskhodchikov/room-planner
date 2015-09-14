using System.Linq;
using System.Web.Optimization;
using WebGrease.Css.Extensions;

namespace RoomPlanner.Website
{
	public class BundleConfig
	{
		public static class Urls
		{
			public static string Content = "~/Content/Styles/";
			public static string Scripts = "~/Scripts/";
		}

		public static void RegisterBundles(BundleCollection bundles)
		{
			new[]
			{
				new StyleBundle(Urls.Content).Include(MakeUrls(
					Urls.Content,
					"*.css")),

				new ScriptBundle(Urls.Scripts).Include(MakeUrls(
					Urls.Scripts,
					"modules.js",
					"urls.js",
					"dialogs/*.js",
					"tabs/*.js",
					"*.js"))

			}.ForEach(bundles.Add);
		}

		private static string[] MakeUrls(string url, params string[] filePath)
		{
			return filePath.Select(x => url + x).ToArray();
		}
	}
}
