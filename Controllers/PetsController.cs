using PatiTakip.ViewModels;
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


    // H04: Formdan gönderilen bilgileri karşılar.
    // H05: Anti-forgery token ile isteği doğrular.
    // H06: ModelState ile form verilerini kontrol eder.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PetCreateViewModel model)
    {
        // formda hata varsa aynı sayfayı tekrar gösterir
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // simdilik veritabanına kayıt yapmıyoruz
        // formun doğrulandıgını kullaniciya bildirecegiz
        TempData["Message"] = "Form doğrulandı. Veritabanına kayıt henüz eklenmedi.";

        // H04: PRG yontemi ile listeye yonlendirir
        return RedirectToAction(nameof(Index));
    }

    //URLden gelen hayvanlarin ID sine gore detay sayfasini aciyoruz
    public IActionResult Details(int id)
    {
        //veritabani yerine ornek bilgiler
        Pet? pet = null;

        if(id==1)
        {
            pet = new Pet
            {
                Id = 1,
                Name="Pamuk",
                Species="Kedi",
                Breed="Tekir",
                BirthDate=new DateTime(2023,5,10),
                Weight=4.2
            };
        }
        else if (id==2)
        {
            pet = new Pet
            {
                Id=2,
                Name="Karabas",
                Species="Kopek",
                Breed="Golden Retriever",
                BirthDate = new DateTime(2022,3,15),
                Weight=25.5
            };
        }

        //girilen id de hayvan yoksa 404 hatasi
        if(pet==null)
        {
            return NotFound();
        }

        return View(pet);
    }

}
