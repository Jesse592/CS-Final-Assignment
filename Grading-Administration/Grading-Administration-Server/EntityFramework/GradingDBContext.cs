using Grading_Administration_Server.EntityFramework.models;
using System.Data.Entity;

namespace Grading_Administration_Server.EntityFramework
{
    /// <summary>
    /// Context to acces the database with
    /// </summary>
    public class GradingDBContext : DbContext
    {

        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<LoginDetail> LoginDetails { get; set; }
        public virtual DbSet<ModuleContribution> moduleContributions { get; set; }
        public virtual DbSet<Grade> grades { get; set; }

        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=EFCore;Trusted_Connection=True;";

        /// <summary>
        /// Empty constructor for the grading context
        /// </summary>
        public GradingDBContext() : base("GradingDB")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
        }

    }

}