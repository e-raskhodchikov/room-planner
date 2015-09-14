create table if not exists FurnitureAction (
	Date 		date not null,
	RoomId 		integer not null,
	Furniture 	varchar(100) not null,
    ActionType	integer not null
);

create table if not exists Room (
	Id			integer primary key autoincrement not null,
    Name		varchar(100),
    CreateDate	date not null,
    RemoveDate	date null
);
