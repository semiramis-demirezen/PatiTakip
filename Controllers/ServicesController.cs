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

   


}
