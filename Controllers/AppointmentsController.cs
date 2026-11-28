using PatiTakip.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatiTakip.Models;
using PatiTakip.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    

    // randevulari iliskili bilgilerle listeliyoruz
    public IActionResult Index()
    {
        // hayvan veteriner ve hizmet bilgilerini birlikte aliyoruz
        List<Appointment> appointments = _context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Veterinarian)
            .Include(a => a.Service)
            .OrderBy(a => a.AppointmentDate)
            .ToList();

        // randevu listesini sayfaya gonderiyoruz
        return View(appointments);
    }




    // randevu olusturma formunu gosteriyoruz
    [HttpGet]
    public IActionResult Create()
    {
        // hayvan veteriner ve hizmet listelerini hazirliyoruz
        LoadFormOptions();
        return View();
    }

    
    // formdan gelen randevu bilgilerini aliyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AppointmentCreateViewModel model)
    {
        // hata durumunda listelerin yeniden gosterilmesini sagliyoruz
        LoadFormOptions();
        // form bilgilerinin dogrulugunu kontrol ediyoruz
        if (!ModelState.IsValid)
        {
            return View(model);
        }


        // turkiye saatine gore su anki zamani aliyoruz
        TimeZoneInfo turkeyTimeZone =
            TimeZoneInfo.FindSystemTimeZoneById("Europe/Istanbul");

        DateTime currentDate = TimeZoneInfo.ConvertTimeFromUtc(
            DateTime.UtcNow,
            turkeyTimeZone
        );

        // gecmis tarih ve saatlere randevu verilmesini engelliyoruz
        if (model.AppointmentDate.Value <= currentDate)
        {
            ModelState.AddModelError(
                "AppointmentDate",
                "Randevu tarihi ve saati gelecekte olmalıdır."
            );

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

    // randevu formundaki secim listelerini hazirliyoruz
    private void LoadFormOptions()
    {
        // kayitli hayvanlari listeye aliyoruz
        ViewBag.Pets = new SelectList(
            _context.Pets.OrderBy(p => p.Name).ToList(),
            "Id",
            "Name"
        );

        // kayitli veterinerleri listeye aliyoruz
        ViewBag.Veterinarians = new SelectList(
            _context.Veterinarians.OrderBy(v => v.FullName).ToList(),
            "Id",
            "FullName"
        );

        // hizmetlerin adlarini ve surelerini gosteriyoruz
        ViewBag.Services = _context.Services
            .OrderBy(s => s.Name)
            .ToList()
            .Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.Name} ({s.DurationMinutes} dakika)"
            })
            .ToList();
    }

    // duzenlenecek randevunun bilgilerini getiriyoruz
    [HttpGet]
    public IActionResult Edit(int id)
    {
        // randevuyu veritabaninda ariyoruz
        Appointment? appointment = _context.Appointments.Find(id);

        // randevu bulunamazsa hata donduruyoruz
        if (appointment == null)
        {
            return NotFound();
        }

        // mevcut bilgileri duzenleme formuna aktariyoruz
        AppointmentCreateViewModel model = new AppointmentCreateViewModel
        {
            PetId = appointment.PetId,
            VeterinarianId = appointment.VeterinarianId,
            ServiceId = appointment.ServiceId,
            AppointmentDate = appointment.AppointmentDate
        };

        // secim listelerini hazirliyoruz
        LoadFormOptions();

        return View(model);
    }

    // formdan gelen randevu bilgilerini guncelliyoruz
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, AppointmentCreateViewModel model)
    {
        // randevuyu veritabaninda buluyoruz
        Appointment? appointment = _context.Appointments.Find(id);

        if (appointment == null)
        {
            return NotFound();
        }

        // formdaki secim listelerini hazirliyoruz
        LoadFormOptions();

        // form bilgilerini kontrol ediyoruz
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // turkiye saatine gore su anki zamani buluyoruz
        TimeZoneInfo turkeyTimeZone =
            TimeZoneInfo.FindSystemTimeZoneById("Europe/Istanbul");

        DateTime currentDate = TimeZoneInfo.ConvertTimeFromUtc(
            DateTime.UtcNow,
            turkeyTimeZone
        );

        // gecmis bir tarihe randevu verilmesini engelliyoruz
        if (model.AppointmentDate.Value <= currentDate)
        {
            ModelState.AddModelError(
                "AppointmentDate",
                "Randevu tarihi ve saati gelecekte olmalıdır."
            );

            return View(model);
        }

        // secilen hayvanin kayitli oldugunu kontrol ediyoruz
        bool petExists = _context.Pets
            .Any(p => p.Id == model.PetId.Value);

        if (!petExists)
        {
            ModelState.AddModelError(
                "PetId",
                "Seçilen evcil hayvan bulunamadı."
            );

            return View(model);
        }

        // secilen veterinerin kayitli oldugunu kontrol ediyoruz
        bool veterinarianExists = _context.Veterinarians
            .Any(v => v.Id == model.VeterinarianId.Value);

        if (!veterinarianExists)
        {
            ModelState.AddModelError(
                "VeterinarianId",
                "Seçilen veteriner hekim bulunamadı."
            );

            return View(model);
        }

        // secilen hizmeti veritabanindan aliyoruz
        Service? service = _context.Services.Find(model.ServiceId.Value);

        if (service == null)
        {
            ModelState.AddModelError(
                "ServiceId",
                "Seçilen hizmet bulunamadı."
            );

            return View(model);
        }

        // yeni baslangic ve bitis saatlerini hesapliyoruz
        DateTime newStart = model.AppointmentDate.Value;
        DateTime newEnd = newStart.AddMinutes(service.DurationMinutes);

        // ayni veterinerin diger randevularini getiriyoruz
        List<Appointment> otherAppointments = _context.Appointments
            .Include(a => a.Service)
            .Where(a => a.Id != id
                     && a.VeterinarianId == model.VeterinarianId.Value
                     && a.Status != "Reddedildi")
            .ToList();

        // diger randevularla saat cakismasi var mi bakiyoruz
        bool hasConflict = otherAppointments.Any(a =>
            newStart < a.AppointmentDate.AddMinutes(a.Service.DurationMinutes)
            && newEnd > a.AppointmentDate
        );

        if (hasConflict)
        {
            ModelState.AddModelError(
                "AppointmentDate",
                "Bu veteriner hekimin seçilen saat aralığında başka bir randevusu bulunuyor."
            );

            return View(model);
        }

        // mevcut randevu bilgilerini guncelliyoruz
        appointment.PetId = model.PetId.Value;
        appointment.VeterinarianId = model.VeterinarianId.Value;
        appointment.ServiceId = model.ServiceId.Value;
        appointment.AppointmentDate = model.AppointmentDate.Value;

        // degisiklikleri veritabanina kaydediyoruz
        _context.SaveChanges();

        TempData["Message"] = "Randevu başarıyla güncellendi.";

        return RedirectToAction(nameof(Index));
    }

}
