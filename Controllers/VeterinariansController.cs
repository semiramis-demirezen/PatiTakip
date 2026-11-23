
using Microsoft.AspNetCore.Mvc;
using PatiTakip.Models;

namespace PatiTakip.Controllers;

public class VeterinariansController : Controller
{
    // veteriner hekimleri listelemek icin bu metodu kullaniyoruz
    public IActionResult Index()
    {
        // simdilik ornek veteriner bilgileri kullaniyoruz
        List<Veterinarian> veterinarians = new List<Veterinarian>();

        // ilk veterineri listeye ekliyoruz
        veterinarians.Add(new Veterinarian
        {
            Id = 1,
            FullName = "Ayşe Yılmaz",
            Specialty = "Küçük Hayvanlar"
        });

        // ikinci veterineri listeye ekliyoruz
        veterinarians.Add(new Veterinarian
        {
            Id = 2,
            FullName = "Mehmet Demir",
            Specialty = "Cerrahi"
        });

        // veteriner listesini view sayfasina gonderiyoruz
        return View(veterinarians);
    }
}
