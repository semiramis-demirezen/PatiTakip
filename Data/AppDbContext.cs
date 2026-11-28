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

    // randevu kayitlarini veritabaninda tutuyoruz
    public DbSet<Appointment> Appointments { get; set; }



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
        // randevuyu evcil hayvan ile iliskilendiriyoruz
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Pet)
            .WithMany()
            .HasForeignKey(a => a.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        // randevuyu veteriner ile iliskilendiriyoruz
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Veterinarian)
            .WithMany()
            .HasForeignKey(a => a.VeterinarianId)
            .OnDelete(DeleteBehavior.Restrict);

        // randevuyu hizmet ile iliskilendiriyoruz
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Service)
            .WithMany()
            .HasForeignKey(a => a.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}