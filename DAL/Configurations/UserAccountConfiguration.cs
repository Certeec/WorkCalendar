using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DatabaseModels;

namespace DAL.Configurations
{
    internal class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.HasKey(n => n.UserId);
            builder.Ignore(n => n.Token);
            builder.ToTable("Accounts");
        }
    }
}
