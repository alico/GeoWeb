using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoPaint.Entities;
using GeoPaint.Settings;

namespace GeoPaint.Data.Contexts
{
    public class GeoPaintSQLDbContext :DbContext
    {
        public DbSet<ComplexShape> ComplexShapeSet { get; set; }

        public GeoPaintSQLDbContext(ConnectionSettings appSettings) : base(appSettings.DataConnectionString)
        {
            Database.SetInitializer<GeoPaintSQLDbContext>(new CreateDatabaseIfNotExists<GeoPaintSQLDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<ComplexShape>().HasKey<int>(e => e.Id).Ignore(e => e.EntityId);
        }
    }
}
