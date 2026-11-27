using PatiTakip.ViewModels;
using Microsoft.AspNetCore.Mvc;
using PatiTakip.Models;
using PatiTakip.Data;

namespace PatiTakip.Controllers;

public class ServicesController : Controller
{

    // veritabani islemleri icin context kullaniyoruz
    private readonly AppDbContext _context;

    // veritabani baglantisini controller a aliyoruz
    public ServicesController(AppDbContext context)
    {
        _context = context;
    }

    
    // hizmetleri veritabanindan listeliyoruz
    public IActionResult Index()
    {
        // kayitli hizmetleri listeye aliyoruz
        List<Service> services = _context.Services.ToList();

        // listeyi sayfaya gonderiyoruz
        return View(services);
    }


    // hizmet ekleme sayfasini aciyoruz
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }



    // formdan gelen hizmet bilgilerini aliyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ServiceCreateViewModel model)
    {
        // girilen bilgileri kontrol ediyoruz
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // form bilgileriyle yeni hizmet olusturuyoruz
        Service service = new Service
        {
            Name = model.Name,
            DurationMinutes = model.DurationMinutes.Value,
            Price = model.Price.Value
        };

        // hizmeti veritabanina ekliyoruz
        _context.Services.Add(service);

        // degisiklikleri veritabanina kaydediyoruz
        _context.SaveChanges();

        // basarili kayit mesajini hazirliyoruz
        TempData["Message"] = "Hizmet başarıyla kaydedildi.";

        // hizmetler listesine donuyoruz
        return RedirectToAction(nameof(Index));
    }



    // duzenlenecek hizmetin bilgilerini getiriyoruz
    [HttpGet]
    public IActionResult Edit(int id)
    {
        // hizmeti veritabaninda ariyoruz
        Service? service = _context.Services.Find(id);

        // hizmet bulunamazsa hata donduruyoruz
        if (service == null)
        {
            return NotFound();
        }

        // mevcut hizmet bilgilerini forma aktariyoruz
        ServiceCreateViewModel model = new ServiceCreateViewModel
        {
            Name = service.Name,
            DurationMinutes = service.DurationMinutes,
            Price = service.Price
        };

        return View(model);
    }

    // formdan gelen yeni hizmet bilgilerini kaydediyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, ServiceCreateViewModel model)
    {
        // duzenlenecek hizmeti veritabaninda buluyoruz
        Service? service = _context.Services.Find(id);

        // hizmet bulunamazsa hata donduruyoruz
        if (service == null)
        {
            return NotFound();
        }

        // girilen bilgilerin dogrulugunu kontrol ediyoruz
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // mevcut hizmetin bilgilerini guncelliyoruz
        service.Name = model.Name;
        service.DurationMinutes = model.DurationMinutes.Value;
        service.Price = model.Price.Value;

        // degisiklikleri veritabanina kaydediyoruz
        _context.SaveChanges();

        // kullaniciya basari mesajini gosteriyoruz
        TempData["Message"] = "Hizmet bilgileri başarıyla güncellendi.";

        // hizmetler listesine donuyoruz
        return RedirectToAction(nameof(Index));
    }


    // silinecek hizmetin bilgilerini getiriyoruz
    [HttpGet]
    public IActionResult Delete(int id)
    {
        // hizmeti veritabaninda ariyoruz
        Service? service = _context.Services.Find(id);

        // hizmet bulunamazsa hata donduruyoruz
        if (service == null)
        {
            return NotFound();
        }

        // hizmeti silme onay sayfasina gonderiyoruz
        return View(service);
    }

    // onaylanan hizmeti veritabanindan siliyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        // silinecek hizmeti veritabaninda buluyoruz
        Service? service = _context.Services.Find(id);

        // hizmet bulunamazsa hata donduruyoruz
        if (service == null)
        {
            return NotFound();
        }

        // hizmeti silinmek uzere isaretliyoruz
        _context.Services.Remove(service);

        // silme islemini veritabanina kaydediyoruz
        _context.SaveChanges();

        // kullaniciya basari mesaji gosteriyoruz
        TempData["Message"] = "Hizmet başarıyla silindi.";

        // hizmetler listesine geri donuyoruz
        return RedirectToAction(nameof(Index));
    }

   


}
