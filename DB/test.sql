create table Genre(
Id int primary key identity,
Name nvarchar(300)
)

insert into Genre values ('Drama');
insert into Genre values ('Comedy');
insert into Genre values ('Fantasy');
insert into Genre values ('Mystery');

create table Actor(
Id int primary key identity,
Name nvarchar(500)
)

insert into Actor values ('Jack Nicholson');
insert into Actor values ('Marlon Brando');go
insert into Actor values ('Robert De Niro');
insert into Actor values ('Al Pacino');


create table Movie(
Id int primary key identity,
Title nvarchar(500),
GenreId int,
foreign key (GenreId) references Genre (Id)
)

insert into Movie values ('One Flew Over the Cuckoo''s Nest', 1);
insert into Movie values ('The Last Tycoon', 1);


create table dbo.MovieActors(
Id int primary key identity(1,1),
MovieId int,
ActorId int,
foreign key (MovieId) references dbo.Movie (Id),
foreign key (ActorId) references dbo.Actor (Id)
);

insert into MovieActors values (1, 1);
insert into MovieActors values (3, 1);
insert into MovieActors values (3, 3);

select * from Genre
select * from Actor
select * from Movie
select * from MovieActors

SELECT m.Id, m.Title, g.Name,
  STUFF
  (
    (SELECT ',' + a.Name
     FROM MovieActors AS ma
	 join Actor a on ma.ActorId = a.Id
     WHERE ma.MovieId = m.Id
	 order by a.Id desc
     FOR XML PATH('')), 1, 1, NULL
  ) AS Actors
FROM Movie m
join Genre g on g.Id = m.GenreId

select a.Name from Actor a
join MovieActors ma on ma.ActorId = a.Id
join Movie m on ma.MovieId = m.Id
where m.Title like '%Tycoon'
