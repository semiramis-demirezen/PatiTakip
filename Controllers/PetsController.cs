using PatiTakip.ViewModels;
using Microsoft.AspNetCore.Mvc;
using PatiTakip.Models;

namespace PatiTakip.Controllers;

// evcil hayvanlarla ilgili sayfa isteklerini yonetir
// kullanılan konular: MVC, Controller, Action, Model
public class PetsController : Controller
{    
    // hayvan listesini sayfaya gonderiyoruz
    public IActionResult Index()
    {
        List<Pet> pets = GetSamplePets();

        return View(pets);
    }


    // GET isteği ile evcil hayvan ekleme formunu açiyor
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    // formdan gönderilen bilgileri karşılar
    // anti-forgery token ile isteği doğrular
    // modelState ile form verilerini kontrol eder
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
    
    // secilen hayvanin bilgilerini gosteriyoruz
    public IActionResult Details(int id)
    {
        // hayvan listesini aliyoruz
        List<Pet> pets = GetSamplePets();

        // verilen numaraya ait hayvani buluyoruz
        Pet? pet = pets.Find(p => p.Id == id);

        // hayvan bulunamazsa hata donduruyoruz
        if (pet == null)
        {
            return NotFound();
        }

        return View(pet);
    }

    // ornek hayvan bilgilerini tek yerde tutuyoruz
    private List<Pet> GetSamplePets()
    {
        List<Pet> pets = new List<Pet>();

        pets.Add(new Pet
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
            Name = "Karabaş",
            Species = "Köpek",
            Breed = "Golden Retriever",
            BirthDate = new DateTime(2022, 3, 15),
            Weight = 25.5
        });

        return pets;
    }


}
