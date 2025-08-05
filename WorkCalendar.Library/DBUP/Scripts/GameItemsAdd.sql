CREATE TABLE [dbo].[GameItems] (
[ItemId] int NOT NULL IDENTITY(1,1) ,
[ItemName] varchar(50) NOT NULL ,
[ItemDescription] varchar(100),
[ItemAddDate] datetime NOT NULL ,
[ItemAddedBy] int,
[IsActive] bit NOT NULL 
)