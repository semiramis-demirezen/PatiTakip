using PatiTakip.ViewModels;
using Microsoft.AspNetCore.Mvc;
using PatiTakip.Models;

namespace PatiTakip.Controllers;

public class AppointmentsController : Controller
{
    // randevulari listelemek icin bu metodu kullaniyoruz
    public IActionResult Index()
    {
        // simdilik ornek randevu bilgileri kullaniyoruz
        List<Appointment> appointments = new List<Appointment>();

        // ilk randevuyu listeye ekliyoruz
        appointments.Add(new Appointment
        {
            Id = 1,
            PetId = 1,
            VeterinarianId = 1,
            ServiceId = 1,
            AppointmentDate = new DateTime(2026, 12, 5, 10, 0, 0),
            Status = "Bekliyor"
        });

        // ikinci randevuyu listeye ekliyoruz
        appointments.Add(new Appointment
        {
            Id = 2,
            PetId = 2,
            VeterinarianId = 2,
            ServiceId = 2,
            AppointmentDate = new DateTime(2026, 12, 5, 11, 30, 0),
            Status = "Onaylandı"
        });

        // randevu listesini sayfaya gonderiyoruz
        return View(appointments);
    }


    // randevu olusturma formunu aciyoruz
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // formdan gonderilen randevu bilgilerini aliyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AppointmentCreateViewModel model)
    {
        // girilen bilgilerin dogru olup olmadigini kontrol ediyoruz
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // veritabani kaydini daha sonra ekleyecegiz
        TempData["Message"] = "Form doğrulandı ancak randevu henüz kaydedilmedi.";

        // randevu listesine geri donuyoruz
        return RedirectToAction(nameof(Index));
    }

}
