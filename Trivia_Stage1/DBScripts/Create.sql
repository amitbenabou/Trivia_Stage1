use master
drop database TriviaDB
Go

create database TriviaDB
go

Use [TriviaDB]
Go

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
    [QuestionId] int identity(1,1) not null primary key
    [StatusId] int NOT NULL foreign key references QuestionStatus(StatusId),
    [PlayerId] int NOT NULL foreign key references Player(PlayerId),
    [SubjectId] int NOT NULL foreign key references QuestionSubject(SubjectId),
    [correctAnswer] NVARCHAR(256) NOT NULL,  
    [wrongAnswer] NVARCHAR(256) NOT NULL,
    [wrongAnswer] NVARCHAR(256) NOT NULL,
    [wrongAnswer] NVARCHAR(256) NOT NULL,
    [Text] NVARCHAR(256) NOT NUll,    
);
go

create table PlayerType (
[TypeId] int  not null primary key,
[name] NVARCHAR(20) NOT NULL
);

GO
create table QuestionStatus (
[StatusId] int not null primary key,
[name] NVARCHAR(20) NOT NULL
);
go
create table QuestionSubject (
[SubjectId] int identity (1,1) not null primary key,
[name] NVARCHAR(20) NOT NULL
);
go