using PatiTakip.ViewModels;
using Microsoft.AspNetCore.Mvc;
using PatiTakip.Models;

namespace PatiTakip.Controllers;

public class VeterinariansController : Controller
{
    // veteriner hekimleri listelemek icin bu metodu kullaniyoruz
    public IActionResult Index()
    {
        // simdilik ornek veteriner bilgileri kullaniyoruz
        List<Veterinarian> veterinarians = new List<Veterinarian>();

        // ilk veterineri listeye ekliyoruz
        veterinarians.Add(new Veterinarian
        {
            Id = 1,
            FullName = "Ayşe Yılmaz",
            Specialty = "Küçük Hayvanlar"
        });

        // ikinci veterineri listeye ekliyoruz
        veterinarians.Add(new Veterinarian
        {
            Id = 2,
            FullName = "Mehmet Demir",
            Specialty = "Cerrahi"
        });

        // veteriner listesini view sayfasina gonderiyoruz
        return View(veterinarians);
    }


    // veteriner ekleme formunu aciyoruz
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    // formdan gonderilen veteriner bilgilerini aliyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(VeterinarianCreateViewModel model)
    {
        // girilen bilgileri kontrol ediyoruz
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // veritabani kaydini daha sonra ekleyecegiz
        TempData["Message"] = "Form doğrulandı ancak veteriner henüz kaydedilmedi";

        // veteriner listesine geri donuyoruz
        return RedirectToAction(nameof(Index));
    }


}
