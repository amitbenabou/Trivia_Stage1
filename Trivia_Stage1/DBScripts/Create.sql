use master
drop database TriviaDB
Go

create database TriviaDB
go

Use [TriviaDB]
Go

create table QuestionSubject (
[SubjectId] int identity (1,1) not null primary key,
[name] NVARCHAR(20) NOT NULL
);
go

create table QuestionStatus (
[StatusId] int not null primary key,
[name] NVARCHAR(20) NOT NULL
);
go

create table PlayerType (
[TypeId] int  not null primary key,
[name] NVARCHAR(20) NOT NULL
);
GO


CREATE TABLE Player(
    [PlayerId] INT IDENTITY (1, 1) NOT NULL primary key,
    [PlayerName] NVARCHAR (20) NOT NULL,
    [PlayerMail]  NVARCHAR (20) NOT NULL unique,
    [Password] NVARCHAR (20) NOT NULL,
    [typeID] int NOT NULL foreign key references PlayerType(TypeId),
    [PlayerScore] int NOT NULL,
);
go 

CREATE TABLE Questions(
    [QuestionId] int identity(1,1) not null primary key,
    [StatusId] int NOT NULL foreign key references QuestionStatus(StatusId),
    [PlayerId] int NOT NULL foreign key references Player(PlayerId),
    [SubjectId] int NOT NULL foreign key references QuestionSubject(SubjectId),
    [correctAnswer] NVARCHAR(256) NOT NULL,  
    [wrongAnswer1] NVARCHAR(256) NOT NULL,
    [wrongAnswer2] NVARCHAR(256) NOT NULL,
    [wrongAnswer3] NVARCHAR(256) NOT NULL,
    [Text] NVARCHAR(256) NOT NUll,    
);
go

insert into PlayerType (TypeId,name) values (1,'Manager');
insert into PlayerType (TypeId,name) values (2,'Master');
insert into PlayerType (TypeId,name) values (3,'Rookie');
select * from PlayerType
go

insert into QuestionStatus (StatusId, name) values (1, 'Approved');
insert into QuestionStatus (StatusId, name) values (2, 'Wating');
insert into QuestionStatus (StatusId, name) values (3, 'Rejected');
select * from QuestionStatus
go

insert into QuestionSubject (name) values ('History')
insert into QuestionSubject (name) values ( 'Sports')
insert into QuestionSubject (name) values ('Politics')
insert into QuestionSubject (name) values ( 'Ramon')
insert into QuestionSubject (name) values ('Science')
select * from QuestionSubject
go

insert into Player(typeID,PlayerName,PlayerMail,PlayerScore,Password) values(1,'Hadas','hadas@gmail.com',0,'Hg2501');
select * from Player
go
 insert into  Questions   (StatusId,PlayerId,SubjectId,correctAnswer,wrongAnswer1,wrongAnswer2,wrongAnswer3,Text) values (1,1,4,'ilan ramon','amit ramon', 'hadas ramon','adar ramon','what is the name of the school?');
 select * from Player
go
select * from Questions

update PLayer set PlayerScore=100 where PlayerId=2