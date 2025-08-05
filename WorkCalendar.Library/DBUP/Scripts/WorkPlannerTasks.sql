CREATE TABLE [dbo].[WorkPlannerTasks] (
[TaskId] int NOT NULL IDENTITY(1,1) ,
[UserId] int NOT NULL ,
[DateStart] datetime NOT NULL ,
[DateEnd] datetime NOT NULL ,
[TimeLength] float(53) NULL ,
[TaskType] varchar(50) NOT NULL ,
[Description] varchar(100) NULL ,
[Place] varchar(50) NULL ,
[MoneyPerHour] float(53) NULL ,
[Bonus] float(53) NULL ,
[IsActive] bit NOT NULL 
)