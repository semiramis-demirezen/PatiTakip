using PatiTakip.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatiTakip.Models;
using PatiTakip.Data;

namespace PatiTakip.Controllers;

public class AppointmentsController : Controller
{

    // veritabani islemleri icin context kullaniyoruz
    private readonly AppDbContext _context;

    // veritabani baglantisini controller a aliyoruz
    public AppointmentsController(AppDbContext context)
    {
        _context = context;
    }

    
    // randevulari veritabanindan listeliyoruz
    public IActionResult Index()
    {
        // kayitli randevulari listeye aliyoruz
        List<Appointment> appointments = _context.Appointments.ToList();

        // randevu listesini sayfaya gonderiyoruz
        return View(appointments);
    }



    // randevu olusturma formunu aciyoruz
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    
    // formdan gelen randevu bilgilerini aliyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AppointmentCreateViewModel model)
    {
        // form bilgilerinin dogrulugunu kontrol ediyoruz
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // secilen hayvanin kayitli olup olmadigini kontrol ediyoruz
        bool petExists = _context.Pets
            .Any(p => p.Id == model.PetId.Value);

        if (!petExists)
        {
            ModelState.AddModelError(
                "PetId",
                "Girilen numaraya ait evcil hayvan bulunamadı."
            );

            return View(model);
        }

        // secilen veterinerin kayitli olup olmadigini kontrol ediyoruz
        bool veterinarianExists = _context.Veterinarians
            .Any(v => v.Id == model.VeterinarianId.Value);

        if (!veterinarianExists)
        {
            ModelState.AddModelError(
                "VeterinarianId",
                "Girilen numaraya ait veteriner hekim bulunamadı."
            );

            return View(model);
        }

        // secilen hizmetin kayitli olup olmadigini kontrol ediyoruz
        bool serviceExists = _context.Services
            .Any(s => s.Id == model.ServiceId.Value);

        if (!serviceExists)
        {
            ModelState.AddModelError(
                "ServiceId",
                "Girilen numaraya ait hizmet bulunamadı."
            );

            return View(model);
        }

        // secilen hizmetin bilgilerini aliyoruz
        Service? selectedService = _context.Services.Find(model.ServiceId.Value);

        // hizmet bulunamazsa hata gosteriyoruz
        if (selectedService == null)
        {
            ModelState.AddModelError(
                "ServiceId",
                "Seçilen hizmet bulunamadı."
            );

            return View(model);
        }

        // yeni randevunun baslangic ve bitis saatini hesapliyoruz
        DateTime newStart = model.AppointmentDate.Value;
        DateTime newEnd = newStart.AddMinutes(selectedService.DurationMinutes);

        // ayni veterinerin reddedilmemis randevularini aliyoruz
        List<Appointment> existingAppointments = _context.Appointments
            .Include(a => a.Service)
            .Where(a => a.VeterinarianId == model.VeterinarianId.Value
                     && a.Status != "Reddedildi")
            .ToList();

        // randevu saatleri birbiriyle cakisiyor mu kontrol ediyoruz
        bool hasConflict = existingAppointments.Any(a =>
            newStart < a.AppointmentDate.AddMinutes(a.Service.DurationMinutes)
            && newEnd > a.AppointmentDate
        );

        // cakisma varsa yeni randevuya izin vermiyoruz
        if (hasConflict)
        {
            ModelState.AddModelError(
                "AppointmentDate",
                "Bu veteriner hekimin seçilen saat aralığında başka bir randevusu bulunuyor."
            );

            return View(model);
        }


        // form bilgileriyle yeni randevu olusturuyoruz
        Appointment appointment = new Appointment
        {
            PetId = model.PetId.Value,
            VeterinarianId = model.VeterinarianId.Value,
            ServiceId = model.ServiceId.Value,
            AppointmentDate = model.AppointmentDate.Value,
            Status = "Bekliyor"
        };

        // randevuyu veritabanina ekliyoruz
        _context.Appointments.Add(appointment);

        // kaydi veritabanina uyguluyoruz
        _context.SaveChanges();

        // kullaniciya basari mesaji gosteriyoruz
        TempData["Message"] = "Randevu başarıyla oluşturuldu.";

        // randevu listesine geri donuyoruz
        return RedirectToAction(nameof(Index));
    }


}
