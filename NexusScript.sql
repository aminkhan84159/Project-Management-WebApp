CREATE DATABASE Nexus;
USE Nexus;


CREATE TABLE [User](
[UserId] INT CONSTRAINT PK_User_UserId PRIMARY KEY IDENTITY(101,1),
[Email] NVARCHAR(80) CONSTRAINT UQ_User_Email UNIQUE NOT NULL,
[Username] NVARCHAR(50) CONSTRAINT UQ_User_Username UNIQUE NOT NULL,
[Password] NVARCHAR(256) NOT NULL,
[JoinedDate] DATE CONSTRAINT DF_User_JoinedDate DEFAULT GETDATE(),
[IsActive] BIT CONSTRAINT DF_User_IsActive DEFAULT 1,
[CreatedBy] INT CONSTRAINT DF_User_CreatedBy DEFAULT 1,
[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_User_CreatedOn DEFAULT CURRENT_TIMESTAMP,
[ChangedBy] INT,
[ChangedOn] DATETIME
);


CREATE TABLE [UserDetails](
[UserDetailId] INT CONSTRAINT PK_UserDetails_UserDetail_Id PRIMARY KEY IDENTITY(101,1),
[UserId] INT CONSTRAINT FK_UserDetails_UserId__Users_UserId FOREIGN KEY(UserId) REFERENCES [User](UserId) ON DELETE CASCADE 
CONSTRAINT UQ_UserDetails_UserId 
UNIQUE CONSTRAINT DF_UserDetails_UserId DEFAULT 101,
[FirstName] NVARCHAR(30) NOT NULL,
[LastName] NVARCHAR(30)NOT NULL,
[Gender] NVARCHAR(6) NOT NULL,
[Age] INT NOT NULL,
[PhoneNo] NVARCHAR(10) NOT NULL CONSTRAINT UQ_UserDetails_PhoneNo UNIQUE,
[Address] NVARCHAR(255)NOT NULL,
[City] NVARCHAR(50) NOT NULL,
[State] NVARCHAR(50) NOT NULL,
[IsActive] BIT CONSTRAINT DF_UserDetails_IsActive DEFAULT 1,
[CreatedBy] INT CONSTRAINT DF_UserDetails_CreatedBy DEFAULT 1,
[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_UserDetails_CreatedOn DEFAULT CURRENT_TIMESTAMP,
[ChangedBy] INT,
[ChangedOn] DATETIME
);
alter table [UserDetails]
drop constraint DF_UserDetails_Age

CREATE TABLE [Status](
[StatusId] INT CONSTRAINT PK_Status_StatusId PRIMARY KEY IDENTITY(101,1),
[Type] NVARCHAR(30),
[IsActive] BIT CONSTRAINT DF_Status_IsActive DEFAULT 1,
[CreatedBy] INT CONSTRAINT DF_Status_CreatedBy DEFAULT 1,
[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_Status_CreatedOn DEFAULT CURRENT_TIMESTAMP,
[ChangedBy] INT,
[ChangedOn] DATETIME
);


CREATE TABLE [Project](
[ProjectId] INT CONSTRAINT PK_Project_ProjectId PRIMARY KEY IDENTITY (101,1),
[ProjectName] NVARCHAR(80) NOT NULL CONSTRAINT UQ_Project_ProjectName UNIQUE,
[StatusId] INT CONSTRAINT FK_Project_StatusId__Status_StatusId FOREIGN KEY (StatusId) REFERENCES [Status](StatusId), CONSTRAINT DF_Project_StatusId DEFAULT 101,
[DueDate] DATE,
[IsActive] BIT CONSTRAINT DF_Project_IsActive DEFAULT 1,
[CreatedBy] INT CONSTRAINT DF_Project_CreatedBy DEFAULT 1,
[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_Project_CreatedOn DEFAULT CURRENT_TIMESTAMP,
[ChangedBy] INT,
[ChangedOn] DATETIME,
[Description] NVARCHAR(255)
);


CREATE TABLE [Task](
[TaskId] INT CONSTRAINT PK_Task_TaskId PRIMARY KEY IDENTITY(101,1),
[TaskName] VARCHAR(50) NOT NULL,
[Priority] VARCHAR(30) NOT NULL,
[Type] NVARCHAR(50),
[StatusId] INT NOT NULL CONSTRAINT FK_Task_StatusId__Status_StatusId FOREIGN KEY (StatusId) REFERENCES [Status](StatusId)
CONSTRAINT DF_Task_StatusId DEFAULT 101,
[UserId] INT NOT NULL CONSTRAINT FK_Task_UserId__User_UserId FOREIGN KEY (UserId) REFERENCES [User](UserId) ON DELETE CASCADE
CONSTRAINT DF_Task_UserId DEFAULT 101 ,
[ProjectId] INT NOT NULL CONSTRAINT FK_Task_ProjectId__Project_ProjectId FOREIGN KEY (ProjectId) REFERENCES [Project](ProjectId) ON DELETE CASCADE,
[StartDate] DATE NOT NULL CONSTRAINT DF_Task_StartDate DEFAULT GETDATE(),
[EndtDate] DATE,
[IsActive] BIT CONSTRAINT DF_Task_IsActive DEFAULT 1,
[CreatedBy] INT CONSTRAINT DF_Task_CreatedBy DEFAULT 1,
[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_Task_CreatedOn DEFAULT CURRENT_TIMESTAMP,
[ChangedBy] INT,
[ChangedOn] DATETIME,
[Description] NVARCHAR(255)
);
 

CREATE TABLE [Issue](
[IssueId] INT CONSTRAINT PK_Issue_IssueId PRIMARY KEY IDENTITY(101,1),
[ProjectId] INT NOT NULL CONSTRAINT FK_Issue_ProjectId__Project_ProjectId FOREIGN KEY (ProjectId) REFERENCES [Project](ProjectId) ON DELETE CASCADE,
[TaskId] INT NOT NULL CONSTRAINT FK_Issue_TaskId__Task_TaskId FOREIGN KEY (TaskId) REFERENCES [Task](TaskId),
[IsActive] BIT CONSTRAINT DF_Issue_IsActive DEFAULT 1,
[CreatedBy] INT CONSTRAINT DF_Issue_CreatedBy DEFAULT 1,
[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_Issue_CreatedOn DEFAULT CURRENT_TIMESTAMP,
[ChangedBy] INT,
[ChangedOn] DATETIME,
[Description] NVARCHAR(255)
);
 


CREATE TABLE [Relation](
[RelationId] INT CONSTRAINT PK_Relation_RelationId PRIMARY KEY IDENTITY(101,1),
[UserId] INT NOT NULL CONSTRAINT FK_User_UserId FOREIGN KEY (UserId) REFERENCES [User](UserId) ON DELETE CASCADE,
[ProjectId] INT NOT NULL CONSTRAINT FK_Relation_ProjectId__Project_ProjectId FOREIGN KEY (ProjectId) REFERENCES [Project](ProjectId) ON DELETE CASCADE,
[Role] NVARCHAR(30) NOT NULL,
[IsActive] BIT CONSTRAINT DF_Relation_IsActive DEFAULT 1,
[CreatedBy] INT CONSTRAINT DF_Relation_CreatedBy DEFAULT 1,
[CreatedOn] DATETIME NOT NULL CONSTRAINT DF_Relation_CreatedOn DEFAULT CURRENT_TIMESTAMP,
[ChangedBy] INT,
[ChangedOn] DATETIME
);

