

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DatabaseModels;

namespace DAL.Configurations
{
    public class UserSchedulerDefaultHourIncomeConfiguration : IEntityTypeConfiguration<UserSchedulerDefaultHourIncome>

    {
        public void Configure(EntityTypeBuilder<UserSchedulerDefaultHourIncome> builder)
        {
            builder.HasKey(n => n.UserId);
            builder.Property(n => n.UserId).ValueGeneratedNever();
        }
    }
}
