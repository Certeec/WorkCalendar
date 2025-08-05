CREATE TABLE [dbo].[UserSchedulerPlaces] (
[PlaceId] int NOT NULL IDENTITY(1,1),
[UserId] int NOT NULL,
[PlaceName] varchar(50) NOT NULL ,
[IsActive] bit NOT NULL 
)