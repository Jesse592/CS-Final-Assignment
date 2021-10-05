using GradingAdministration_server.models;
using System.Data.Entity;

namespace Grading_Administration_Server.data
{

    public class GradingDBContext : DbContext
    {

        public DbSet<Module> Modules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LoginDetail> LoginDetails { get; set; }
        public DbSet<ModuleContribution> moduleContributions { get; set; }

        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EFCore;Trusted_Connection=True;";

        public GradingDBContext() : base("GradingDB")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
        }

    }

}
