using System;
using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;

namespace DXApplication48.Module.BusinessObjects {
    // This code allows our Model Editor to get relevant EF Core metadata at design time.
    // For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
	public class DXApplication48ContextInitializer : DbContextTypesInfoInitializerBase {
		protected override DbContext CreateDbContext() {
			var optionsBuilder = new DbContextOptionsBuilder<DXApplication48EFCoreDbContext>()
                .UseSqlServer(@";");
            return new DXApplication48EFCoreDbContext(optionsBuilder.Options);
		}
	}
	//This factory creates DbContext for design-time services. For example, it is required for database migration.
	public class DXApplication48DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DXApplication48EFCoreDbContext> {
		public DXApplication48EFCoreDbContext CreateDbContext(string[] args) {
			throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
			//var optionsBuilder = new DbContextOptionsBuilder<DXApplication48EFCoreDbContext>();
			//optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DXApplication48");
			//return new DXApplication48EFCoreDbContext(optionsBuilder.Options);
		}
	}
	[TypesInfoInitializer(typeof(DXApplication48ContextInitializer))]
	public class DXApplication48EFCoreDbContext : DbContext {
		public DXApplication48EFCoreDbContext(DbContextOptions<DXApplication48EFCoreDbContext> options) : base(options) {
		}
		public DbSet<ModuleInfo> ModulesInfo { get; set; }
		public DbSet<ModelDifference> ModelDifferences { get; set; }
		public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
	    public DbSet<PermissionPolicyRole> Roles { get; set; }
	    public DbSet<DXApplication48.Module.BusinessObjects.ApplicationUser> Users { get; set; }
        public DbSet<DXApplication48.Module.BusinessObjects.ApplicationUserLoginInfo> UserLoginInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<DXApplication48.Module.BusinessObjects.ApplicationUserLoginInfo>(b => {
                b.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName), nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
            });
        }
	}
}
