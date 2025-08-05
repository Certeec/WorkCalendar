CREATE TABLE [dbo].[GameItemsReadings] (
[ReadingId] int NOT NULL IDENTITY(1,1),
[ItemId] int NOT NULL,
[Value] float(53) NULL ,
[ReadingAddDate] datetime NOT NULL ,
[ReadingAddedBy] int,
[IsConfirmed] bit NOT NULL 
)