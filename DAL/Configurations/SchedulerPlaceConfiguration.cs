
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DatabaseModels;

namespace DAL.Configurations
{
    public class SchedulerPlaceConfiguration : IEntityTypeConfiguration<SchedulerPlace>
    {
        public void Configure(EntityTypeBuilder<SchedulerPlace> builder)
        {
            builder.HasKey(n => n.PlaceId);
            builder.ToTable("UserSchedulerPlaces");
        }
    }
}
