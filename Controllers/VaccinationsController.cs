using PatiTakip.ViewModels;
using Microsoft.AspNetCore.Mvc;
using PatiTakip.Models;
using PatiTakip.Data;

namespace PatiTakip.Controllers;

public class VaccinationsController : Controller
{
    
    // asi kayitlarini veritabanindan listeliyoruz
    public IActionResult Index()
    {
        // kayitli asilari listeye aliyoruz
        List<Vaccination> vaccinations = _context.Vaccinations.ToList();

        // asi listesini sayfaya gonderiyoruz
        return View(vaccinations);
    }



    // asi kaydi ekleme formunu aciyoruz
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    
    // formdan gelen asi bilgilerini aliyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(VaccinationCreateViewModel model)
    {
        // form bilgilerinin dogrulugunu kontrol ediyoruz
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // girilen numaraya ait hayvan var mi kontrol ediyoruz
        bool petExists = _context.Pets.Any(p => p.Id == model.PetId.Value);

        if (!petExists)
        {
            // hayvan bulunamazsa formda hata gosteriyoruz
            ModelState.AddModelError(
                "PetId",
                "Girilen numaraya ait evcil hayvan bulunamadı."
            );

            return View(model);
        }

        // form bilgileriyle yeni asi kaydi olusturuyoruz
        Vaccination vaccination = new Vaccination
        {
            PetId = model.PetId.Value,
            VaccineName = model.VaccineName,
            DateAdministered = model.DateAdministered.Value,
            NextDoseDate = model.NextDoseDate.Value
        };

        // asi kaydini veritabanina ekliyoruz
        _context.Vaccinations.Add(vaccination);

        // degisiklikleri veritabanina kaydediyoruz
        _context.SaveChanges();

        // kullaniciya basari mesaji gosteriyoruz
        TempData["Message"] = "Aşı kaydı başarıyla oluşturuldu.";

        // asi kayitlari listesine donuyoruz
        return RedirectToAction(nameof(Index));
    }


    // veritabani islemleri icin context kullaniyoruz
    private readonly AppDbContext _context;

    // veritabani baglantisini controller a aliyoruz
    public VaccinationsController(AppDbContext context)
    {
        _context = context;
    }

    // duzenlenecek asi kaydinin bilgilerini getiriyoruz
    [HttpGet]
    public IActionResult Edit(int id)
    {
        // asi kaydini veritabaninda ariyoruz
        Vaccination? vaccination = _context.Vaccinations.Find(id);

        // kayit bulunamazsa hata donduruyoruz
        if (vaccination == null)
        {
            return NotFound();
        }

        // mevcut bilgileri duzenleme formuna aktariyoruz
        VaccinationCreateViewModel model = new VaccinationCreateViewModel
        {
            PetId = vaccination.PetId,
            VaccineName = vaccination.VaccineName,
            DateAdministered = vaccination.DateAdministered,
            NextDoseDate = vaccination.NextDoseDate
        };

        return View(model);
    }

    // formdan gelen yeni asi bilgilerini kaydediyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, VaccinationCreateViewModel model)
    {
        // duzenlenecek asi kaydini buluyoruz
        Vaccination? vaccination = _context.Vaccinations.Find(id);

        // kayit bulunamazsa hata donduruyoruz
        if (vaccination == null)
        {
            return NotFound();
        }

        // girilen bilgilerin dogrulugunu kontrol ediyoruz
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // girilen numaraya ait hayvan var mi kontrol ediyoruz
        bool petExists = _context.Pets.Any(p => p.Id == model.PetId.Value);

        if (!petExists)
        {
            // hayvan bulunamazsa formda hata gosteriyoruz
            ModelState.AddModelError(
                "PetId",
                "Girilen numaraya ait evcil hayvan bulunamadı."
            );

            return View(model);
        }

        // mevcut asi kaydinin bilgilerini guncelliyoruz
        vaccination.PetId = model.PetId.Value;
        vaccination.VaccineName = model.VaccineName;
        vaccination.DateAdministered = model.DateAdministered.Value;
        vaccination.NextDoseDate = model.NextDoseDate.Value;

        // degisiklikleri veritabanina kaydediyoruz
        _context.SaveChanges();

        // basari mesajini hazirliyoruz
        TempData["Message"] = "Aşı kaydı başarıyla güncellendi.";

        // asi kayitlari listesine donuyoruz
        return RedirectToAction(nameof(Index));
    }


    // silinecek asi kaydinin bilgilerini getiriyoruz
    [HttpGet]
    public IActionResult Delete(int id)
    {
        // asi kaydini veritabaninda ariyoruz
        Vaccination? vaccination = _context.Vaccinations.Find(id);

        // asi kaydi bulunamazsa hata donduruyoruz
        if (vaccination == null)
        {
            return NotFound();
        }

        // asi bilgilerini silme onay sayfasina gonderiyoruz
        return View(vaccination);
    }

    // onaylanan asi kaydini veritabanindan siliyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        // silinecek asi kaydini buluyoruz
        Vaccination? vaccination = _context.Vaccinations.Find(id);

        // asi kaydi bulunamazsa hata donduruyoruz
        if (vaccination == null)
        {
            return NotFound();
        }

        // asi kaydini silinmek uzere isaretliyoruz
        _context.Vaccinations.Remove(vaccination);

        // degisikligi veritabanina kaydediyoruz
        _context.SaveChanges();

        // kullaniciya basari mesaji gosteriyoruz
        TempData["Message"] = "Aşı kaydı başarıyla silindi.";

        // asi kayitlari listesine geri donuyoruz
        return RedirectToAction(nameof(Index));
    }


}
