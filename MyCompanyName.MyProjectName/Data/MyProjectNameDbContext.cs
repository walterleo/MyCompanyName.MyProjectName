using Microsoft.EntityFrameworkCore;
using MyCompanyName.MyProjectName.Entities;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.Data;

public class MyProjectNameDbContext : AbpDbContext<MyProjectNameDbContext>
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<Persona> Personas { get; set; }
	public DbSet<Auto> Autos { get; set; }


	public MyProjectNameDbContext(DbContextOptions<MyProjectNameDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureIdentityServer();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
        
        /* Configure your own entities below */
        
        builder.Entity<Todo>(b =>
        {
            b.ToTable("Todos");
            b.ConfigureByConvention();
            b.Property(x => x.Text).IsRequired().HasMaxLength(128);
        });

        builder.Entity<Persona>(b =>
        {
            b.ToTable("Personas");
            b.ConfigureByConvention();
        });

		    builder.Entity<Auto>(b =>
		    {
			    b.ToTable("Autos");
			    b.ConfigureByConvention();
		    });
	}
}