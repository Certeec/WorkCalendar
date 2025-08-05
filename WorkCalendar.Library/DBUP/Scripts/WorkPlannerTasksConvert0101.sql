ALTER TABLE [dbo].[WorkPlannerTasks] 
ADD Revenue float(53) NULL,
RevenueParticipation float(53) NULL,
PremiumPoints float(53) NULL;

GO
UPDATE [dbo].[WorkPlannerTasks] SET Revenue = 0 WHERE Revenue IS NULL;
UPDATE [dbo].[WorkPlannerTasks] SET RevenueParticipation = 0 WHERE RevenueParticipation IS NULL;
UPDATE [dbo].[WorkPlannerTasks] SET PremiumPoints = 0 WHERE PremiumPoints IS NULL;
GO
ALTER TABLE [dbo].[WorkPlannerTasks] Alter Column Revenue float(53) NOT NULL;
ALTER TABLE [dbo].[WorkPlannerTasks] Alter Column RevenueParticipation float(53) NOT NULL;
ALTER TABLE [dbo].[WorkPlannerTasks] Alter Column PremiumPoints float(53) NOT NULL;
GO