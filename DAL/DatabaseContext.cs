using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.DatabaseModels;

namespace DAL
{
    public class DatabaseContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<UserAccount> Accounts { get; set; }
        public DbSet<UserLogInHistory> UsersLogHistory { get; set; }
        public DbSet<UserSchedulerDefaultHourIncome> DefaultIncomes { get; set; }
        public DbSet<SchedulerPlace> schedulerPlaces { get; set; }
        public DbSet<SchedulerTask> schedulerTasks { get; set; }

        public DatabaseContext(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //jeśli nie skonfigurowano opcji, to dodajemy domyślną konfigurację na podstawie connectionString
            if (!optionsBuilder.IsConfigured)
            {
                if (!string.IsNullOrWhiteSpace(_connectionString))
                    optionsBuilder.UseSqlServer(_connectionString);
                else
                    optionsBuilder.UseSqlServer();
            }
        }
    }
}
