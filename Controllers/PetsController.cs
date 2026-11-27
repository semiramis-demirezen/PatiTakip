using PatiTakip.ViewModels;
using Microsoft.AspNetCore.Mvc;
using PatiTakip.Models;
using PatiTakip.Data;

namespace PatiTakip.Controllers;

// evcil hayvanlarla ilgili sayfa isteklerini yonetir
// kullanılan konular: MVC, Controller, Action, Model
public class PetsController : Controller
{    

    // veritabanina erismek icin context kullaniyoruz
    private readonly AppDbContext _context;

    // context bilgisini constructor ile aliyoruz
    public PetsController(AppDbContext context)
    {
        _context = context;
    }

    // hayvan bilgilerini veritabanindan aliyoruz
    public IActionResult Index()
    {
        // veritabanindaki hayvanlari listeye aktariyoruz
        List<Pet> pets = _context.Pets.ToList();

        // hayvan listesini sayfaya gonderiyoruz
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

    // formdan gelen hayvan bilgilerini aliyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PetCreateViewModel model)
    {
        // girilen bilgilerin dogrulugunu kontrol ediyoruz
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // formdan gelen bilgilerle yeni hayvan olusturuyoruz
        Pet pet = new Pet
        {
            Name = model.Name,
            Species = model.Species,
            Breed = model.Breed,
            BirthDate = model.BirthDate,
            Weight = model.Weight
        };

        // yeni hayvani veritabanina ekliyoruz
        _context.Pets.Add(pet);

        // yapilan degisiklikleri veritabanina kaydediyoruz
        _context.SaveChanges();

        // kayit tamamlandiginda mesaj gosteriyoruz
        TempData["Message"] = "Evcil hayvan başarıyla kaydedildi.";

        // hayvan listesine geri donuyoruz
        return RedirectToAction(nameof(Index));
    }



    //URLden gelen hayvanlarin ID sine gore detay sayfasini aciyoruz
    

    // secilen hayvanin bilgilerini veritabanindan aliyoruz
    public IActionResult Details(int id)
    {
        // numarasina gore hayvani buluyoruz
        Pet? pet = _context.Pets.Find(id);

        // hayvan bulunamazsa 404 hatasi donduruyoruz
        if (pet == null)
        {
            return NotFound();
        }

        // bulunan hayvani detay sayfasina gonderiyoruz
        return View(pet);
    }


    // // ornek hayvan bilgilerini tek yerde tutuyoruz
    // private List<Pet> GetSamplePets()
    // {
    //     List<Pet> pets = new List<Pet>();

    //     pets.Add(new Pet
    //     {
    //         Id = 1,
    //         Name = "Pamuk",
    //         Species = "Kedi",
    //         Breed = "Tekir",
    //         BirthDate = new DateTime(2023, 5, 10),
    //         Weight = 4.2
    //     });

    //     pets.Add(new Pet
    //     {
    //         Id = 2,
    //         Name = "Karabaş",
    //         Species = "Köpek",
    //         Breed = "Golden Retriever",
    //         BirthDate = new DateTime(2022, 3, 15),
    //         Weight = 25.5
    //     });

    //     return pets;
    // }


    // duzenlenecek hayvanin bilgilerini getiriyoruz
    [HttpGet]
    public IActionResult Edit(int id)
    {
        // hayvani veritabaninda ariyoruz
        Pet? pet = _context.Pets.Find(id);

        // hayvan bulunamazsa hata donduruyoruz
        if (pet == null)
        {
            return NotFound();
        }

        // hayvan bilgilerini forma aktariyoruz
        PetCreateViewModel model = new PetCreateViewModel
        {
            Name = pet.Name,
            Species = pet.Species,
            Breed = pet.Breed,
            BirthDate = pet.BirthDate,
            Weight = pet.Weight
        };

        return View(model);
    }

    // formdan gelen yeni bilgileri kaydediyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, PetCreateViewModel model)
    {
        // duzenlenecek hayvani buluyoruz
        Pet? pet = _context.Pets.Find(id);

        // hayvan bulunamazsa hata donduruyoruz
        if (pet == null)
        {
            return NotFound();
        }

        // form bilgilerinin dogrulugunu kontrol ediyoruz
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // hayvanin bilgilerini guncelliyoruz
        pet.Name = model.Name;
        pet.Species = model.Species;
        pet.Breed = model.Breed;
        pet.BirthDate = model.BirthDate;
        pet.Weight = model.Weight;

        // degisiklikleri veritabanina kaydediyoruz
        _context.SaveChanges();

        TempData["Message"] = "Evcil hayvan bilgileri başarıyla güncellendi.";

        return RedirectToAction(nameof(Index));
    }


}
