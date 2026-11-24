
namespace PatiTakip.Models;

// randevu bilgilerini tutuyoruz
public class Appointment
{
    // her randevunun kendine ait numarasi
    public int Id { get; set; }

    // randevunun hangi hayvana ait oldugunu tutuyoruz
    public int PetId { get; set; }

    // randevuya atanacak veterinerin numarasi
    public int VeterinarianId { get; set; }

    // randevuda alinacak hizmetin numarasi
    public int ServiceId { get; set; }

    // randevunun tarihini ve saatini tutuyoruz
    public DateTime AppointmentDate { get; set; }

    // randevunun durumunu tutuyoruz
    public string Status { get; set; } = "Bekliyor";
}
