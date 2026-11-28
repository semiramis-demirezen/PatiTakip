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

}
