
using Microsoft.AspNetCore.Mvc;
using PatiTakip.Models;

namespace PatiTakip.Controllers;

public class ServicesController : Controller
{
    // H02: Hizmetleri listeleme sayfasını açıyoruz.
    public IActionResult Index()
    {
        // Şimdilik veritabanı yerine örnek hizmetler kullanıyoruz.
        List<Service> services = new List<Service>();

        services.Add(new Service
        {
            Id = 1,
            Name = "Genel Muayene",
            DurationMinutes = 30,
            Price = 500
        });

        services.Add(new Service
        {
            Id = 2,
            Name = "Aşı",
            DurationMinutes = 15,
            Price = 350
        });

        services.Add(new Service
        {
            Id = 3,
            Name = "Diş Temizliği",
            DurationMinutes = 60,
            Price = 900
        });

        // Hizmet listesini View sayfasına gönderiyoruz.
        return View(services);
    }
}
