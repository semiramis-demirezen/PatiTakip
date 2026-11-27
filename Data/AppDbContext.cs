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

}