
using Microsoft.AspNetCore.Mvc;
using PatiTakip.Models;

namespace PatiTakip.Controllers;

public class VaccinationsController : Controller
{
    // asi kayitlarini listelemek icin bu metodu kullaniyoruz
    public IActionResult Index()
    {
        // simdilik ornek asi kayitlari olusturuyoruz
        List<Vaccination> vaccinations = new List<Vaccination>();

        // pamukun asi kaydini ekliyoruz
        vaccinations.Add(new Vaccination
        {
            Id = 1,
            PetId = 1,
            VaccineName = "Kuduz Asisi",
            DateAdministered = new DateTime(2026, 5, 10),
            NextDoseDate = new DateTime(2027, 5, 10)
        });

        // karabasin asi kaydini ekliyoruz
        vaccinations.Add(new Vaccination
        {
            Id = 2,
            PetId = 2,
            VaccineName = "Kuduz Asisi",
            DateAdministered = new DateTime(2026, 3, 15),
            NextDoseDate = new DateTime(2027, 3, 15)
        });

        // asi kayitlarini sayfaya gonderiyoruz
        return View(vaccinations);
    }
}
