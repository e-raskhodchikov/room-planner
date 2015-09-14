using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.Hosting;
using Dapper;
using DapperExtensions.Sql;

namespace RoomPlanner.DataAccess
{
	public class BaseDao : IDao
	{
		protected readonly string FilePath;

		public BaseDao()
		{
			var databaseName = WebConfigurationManager.AppSettings["DatabaseName"];
			FilePath = string.Format("{0}{1}.sqlite", HostingEnvironment.ApplicationPhysicalPath, databaseName);
			DapperExtensions.DapperExtensions.SqlDialect = new SqliteDialect();
		}

		protected string ConnectionString
		{
			get { return string.Format("Data Source={0};Version=3;", FilePath); }
		}

		private DbConnection Connection
		{
			get
			{
				if (!File.Exists(FilePath))
				{
					SQLiteConnection.CreateFile(FilePath);

					Execute(Sql.Scripts.CreateSchema);
				}

				return new SQLiteConnection(ConnectionString);
			}
		}

		protected IList<T> Query<T>(string sql, object parameters = null)
		{
			IList<T> result;

			using (var connection = Connection)
			{
				connection.Open();
				result = connection.Query<T>(sql, parameters).ToList();
				connection.Close();
			}

			return result;
		}

		protected void Execute(string sql, object parameters = null)
		{
			using (var connection = Connection)
			{
				connection.Open();
				connection.Execute(sql, parameters);
				connection.Close();
			}
		}
	}
}
