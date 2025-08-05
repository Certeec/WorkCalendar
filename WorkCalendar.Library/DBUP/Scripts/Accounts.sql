CREATE TABLE [dbo].[Accounts] (
[UserId] int NOT NULL IDENTITY(1,1) ,
[Login] varchar(50) NOT NULL ,
[Password] varchar(50) NOT NULL ,
[Status] tinyint NOT NULL ,
[Email] varchar(50) NULL ,
[CreateDate] datetime NOT NULL ,
[LastLogin] datetime NULL ,
[Power] tinyint NULL 
)