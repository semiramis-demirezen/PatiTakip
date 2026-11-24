
namespace PatiTakip.Models;

// evcil hayvanlarin asi bilgilerini tutuyoruz
public class Vaccination
{
    // her asi kaydinin numarasi
    public int Id { get; set; }

    // asinin hangi hayvana ait oldugunu belirtiyoruz
    public int PetId { get; set; }

    // yapilan asinin adi
    public string VaccineName { get; set; } = string.Empty;

    // asinin yapildigi tarih
    public DateTime DateAdministered { get; set; }

    // bir sonraki asi tarihi
    public DateTime NextDoseDate { get; set; }
}
