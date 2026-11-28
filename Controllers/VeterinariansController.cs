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

}
