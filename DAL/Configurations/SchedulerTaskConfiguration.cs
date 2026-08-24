using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DatabaseModels;

namespace DAL.Configurations
{
    internal class SchedulerTaskConfiguration : IEntityTypeConfiguration<SchedulerTask>
    {
        public void Configure(EntityTypeBuilder<SchedulerTask> builder)
        {
            builder.HasKey(n => n.TaskId);
            builder.ToTable("WorkPlannerTasks");
            builder.Ignore("ShowDetails");
        }
    }
}
