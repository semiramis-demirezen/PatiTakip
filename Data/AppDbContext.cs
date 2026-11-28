using Microsoft.EntityFrameworkCore;
using PatiTakip.Models;
namespace PatiTakip.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<Pet> Pets {get; set;}

    // hizmet kayitlarini veritabaninda tutuyoruz
    public DbSet<Service> Services { get; set; }

    // veteriner hekim kayitlarini veritabaninda tutuyoruz
    public DbSet<Veterinarian> Veterinarians { get; set; }

    // asi kayitlarini veritabaninda tutuyoruz
    public DbSet<Vaccination> Vaccinations { get; set; }


    // veritabani iliskilerini tanimliyoruz
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // asi kaydini evcil hayvan ile iliskilendiriyoruz
        modelBuilder.Entity<Vaccination>()
            .HasOne(v => v.Pet)
            .WithMany()
            .HasForeignKey(v => v.PetId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}