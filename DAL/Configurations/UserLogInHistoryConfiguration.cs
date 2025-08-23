using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DatabaseModels;

namespace DAL.Configurations
{
    internal class UserLogInHistoryConfiguration : IEntityTypeConfiguration<UserLogInHistory>
    {
        public void Configure(EntityTypeBuilder<UserLogInHistory> builder)
        {
            builder.HasKey(n => n.LogId);
            builder.Property(e => e.Duration).HasDefaultValue(DateTime.MinValue);
            builder.ToTable("UserLoginLogs");
        }
    }
}
