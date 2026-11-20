
using Microsoft.AspNetCore.Mvc;
using PatiTakip.Models;

namespace PatiTakip.Controllers;

// evcil hayvanlarla ilgili sayfa isteklerini yonetir
// kullanılan konular: MVC, Controller, Action, Model
public class PetsController : Controller
{
    //evcil hayvanları listeleyen action metodu
    public IActionResult Index()
    {
        //pet modelinden gecici liste
        List<Pet> pets = new List<Pet>();

        pets.Add(new Pet //test verileri
        {
            Id = 1,
            Name = "Pamuk",
            Species = "Kedi",
            Breed = "Tekir",
            BirthDate = new DateTime(2023, 5, 10),
            Weight = 4.2
        });

        pets.Add(new Pet
        {
            Id = 2,
            Name = "Karabas",
            Species = "Köpek",
            Breed = "Golden Retriever",
            BirthDate = new DateTime(2022, 3, 15),
            Weight = 25.5
        });

        
        // listeyi Index.cshtml sayfasina gonderiyoruz
        return View(pets);
    }

    // GET isteği ile evcil hayvan ekleme formunu açiyor
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
}
