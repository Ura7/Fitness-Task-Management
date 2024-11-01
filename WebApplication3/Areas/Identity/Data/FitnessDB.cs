using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Areas.Identity.Data;
using WebApplication3.Models;

namespace WebApplication3.Areas.Identity.Data;

public class FitnessDB : IdentityDbContext<AppUser>
{
    public DbSet<MusteriProgress> MusteriProgresses { get; set; }
    public DbSet<AntrenmanProgramları> AntrenmanProgramlarıs { get; set; }  
    public DbSet<DanismanMusteriAtama> DanismanMusteriAtamas { get; set; }

    public DbSet<BeslenmePlanı> BeslenmePlan {  get; set; }

   public DbSet<DanışmanBilgileri> danışmanBilgileris { get; set; }

    public DbSet<Mesajlar> mesajlars { get; set; }
    public FitnessDB(DbContextOptions<FitnessDB> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

    }

    public DbSet<WebApplication3.Models.BeslenmePlanı>? BeslenmePlanı { get; set; }

    public DbSet<WebApplication3.Models.Mesajlar>? Mesajlar { get; set; }



}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder) 
    {
		builder.Property(u=> u.Firstname).HasMaxLength(255);
		builder.Property(u=> u.Lastname).HasMaxLength(255);
		builder.Property(u=> u.Number).HasMaxLength(255);
		builder.Property(u=> u.Gender).HasMaxLength(2);
		builder.Property(u=> u.Birthday).HasMaxLength(255);
        builder.Property(u => u.ProfilePhoto).HasColumnType("bytea").HasMaxLength(int.MaxValue);
	}
    
}