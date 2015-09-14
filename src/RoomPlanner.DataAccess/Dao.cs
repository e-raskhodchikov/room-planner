using System;
using System.Collections.Generic;
using System.Linq;
using RoomPlanner.Domain;

namespace RoomPlanner.DataAccess
{
	public class Dao : BaseDao
	{
		public void CreateRoom(Room room)
		{
			room.Id = Query<int>(@"
insert into Room(Name, CreateDate) values (@Name, @CreateDate);
select last_insert_rowid();
", room).FirstOrDefault();
		}

		public Room GetRoom(string name)
		{
			return Query<Room>(@"
select
	Id,
	Name,
	CreateDate,
	RemoveDate
	from Room
	where Name = @name
	limit 1;
", new
			{
				name
			}).FirstOrDefault();
		}

		public List<Room> GetExistingRooms(DateTime date)
		{
			return Query<Room>(@"
select
	Id,
	Name,
	CreateDate,
	RemoveDate
	from Room
	where @date >= CreateDate and
		@date < coalesce(RemoveDate, '9999-12-31 00:00:00')
	order by Name;
", new
			{
				date
			}).ToList();
		}

		public List<Room> GetRooms()
		{
			return Query<Room>(@"
select
	Id,
	Name,
	CreateDate,
	RemoveDate
	from Room;	
").ToList();
		}

		public List<FurnitureAction> GetFurnitureActions()
		{
			return Query<FurnitureAction>(@"
select
	Date,
	RoomId,
	Furniture,
	ActionType
	from FurnitureAction").ToList();
		}

		public List<FurnitureCount> GetFurnitureCount(DateTime date, int[] roomIds)
		{
			return Query<FurnitureCount>(@"
select
	RoomId,
	Furniture,
	sum(case when ActionType = 0 then 1 else -1 end) as Count
	from FurnitureAction
	where RoomId in @roomIds and
		Date <= @date
	group by RoomId, Furniture
	order by Furniture;
", new
			{
				date,
				roomIds
			}).ToList();
		}

		public void CreateFurnitureAction(FurnitureAction furnitureAction)
		{
			Execute(@"
insert into FurnitureAction (Date, Furniture, RoomId, ActionType)
values (@Date, @Furniture, @RoomId, @ActionType)
", furnitureAction);
		}
	}
}
