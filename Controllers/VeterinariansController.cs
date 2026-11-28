using PatiTakip.ViewModels;
using Microsoft.AspNetCore.Mvc;
using PatiTakip.Models;
using PatiTakip.Data;

namespace PatiTakip.Controllers;

public class VeterinariansController : Controller
{
    
    // veterinerleri veritabanindan listeliyoruz
    public IActionResult Index()
    {
        // kayitli veterinerleri listeye aliyoruz
        List<Veterinarian> veterinarians = _context.Veterinarians.ToList();

        // listeyi sayfaya gonderiyoruz
        return View(veterinarians);
    }



    // veteriner ekleme formunu aciyoruz
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    
    // formdan gelen veteriner bilgilerini aliyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(VeterinarianCreateViewModel model)
    {
        // girilen bilgilerin dogrulugunu kontrol ediyoruz
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // form bilgileriyle yeni veteriner olusturuyoruz
        Veterinarian veterinarian = new Veterinarian
        {
            FullName = model.FullName,
            Specialty = model.Specialty
        };

        // veterineri veritabanina ekliyoruz
        _context.Veterinarians.Add(veterinarian);

        // kaydi veritabanina uyguluyoruz
        _context.SaveChanges();

        // kullaniciya basari mesaji gosteriyoruz
        TempData["Message"] = "Veteriner hekim başarıyla kaydedildi.";

        // veterinerler listesine donuyoruz
        return RedirectToAction(nameof(Index));
    }



    // veritabani islemleri icin context kullaniyoruz
    private readonly AppDbContext _context;

    // veritabani baglantisini controller a aliyoruz
    public VeterinariansController(AppDbContext context)
    {
        _context = context;
    }


    // duzenlenecek veterinerin bilgilerini getiriyoruz
    [HttpGet]
    public IActionResult Edit(int id)
    {
        // veterineri veritabaninda ariyoruz
        Veterinarian? veterinarian = _context.Veterinarians.Find(id);

        // veteriner bulunamazsa hata donduruyoruz
        if (veterinarian == null)
        {
            return NotFound();
        }

        // mevcut veteriner bilgilerini forma aktariyoruz
        VeterinarianCreateViewModel model = new VeterinarianCreateViewModel
        {
            FullName = veterinarian.FullName,
            Specialty = veterinarian.Specialty
        };

        return View(model);
    }

    // formdan gelen yeni veteriner bilgilerini kaydediyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, VeterinarianCreateViewModel model)
    {
        // duzenlenecek veterineri veritabaninda buluyoruz
        Veterinarian? veterinarian = _context.Veterinarians.Find(id);

        // veteriner bulunamazsa hata donduruyoruz
        if (veterinarian == null)
        {
            return NotFound();
        }

        // girilen bilgilerin dogrulugunu kontrol ediyoruz
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // veterinerin bilgilerini guncelliyoruz
        veterinarian.FullName = model.FullName;
        veterinarian.Specialty = model.Specialty;

        // degisiklikleri veritabanina kaydediyoruz
        _context.SaveChanges();

        // kullaniciya basari mesaji gosteriyoruz
        TempData["Message"] = "Veteriner hekim bilgileri başarıyla güncellendi.";

        // veterinerler listesine donuyoruz
        return RedirectToAction(nameof(Index));
    }


    // silinecek veterinerin bilgilerini getiriyoruz
    [HttpGet]
    public IActionResult Delete(int id)
    {
        // veterineri veritabaninda ariyoruz
        Veterinarian? veterinarian = _context.Veterinarians.Find(id);

        // veteriner bulunamazsa hata donduruyoruz
        if (veterinarian == null)
        {
            return NotFound();
        }

        // veterineri silme onay sayfasina gonderiyoruz
        return View(veterinarian);
    }

    // onaylanan veterineri veritabanindan siliyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        // silinecek veterineri veritabaninda buluyoruz
        Veterinarian? veterinarian = _context.Veterinarians.Find(id);

        // veteriner bulunamazsa hata donduruyoruz
        if (veterinarian == null)
        {
            return NotFound();
        }

        // veterineri silinmek uzere isaretliyoruz
        _context.Veterinarians.Remove(veterinarian);

        // silme islemini veritabanina kaydediyoruz
        _context.SaveChanges();

        // kullaniciya basari mesaji gosteriyoruz
        TempData["Message"] = "Veteriner hekim başarıyla silindi.";

        // veterinerler listesine donuyoruz
        return RedirectToAction(nameof(Index));
    }



}
