
namespace PatiTakip.Models;

// veteriner hekimlerin bilgilerini tutuyoruz
public class Veterinarian
{
    // her veterinerin kendine ait numarasi
    public int Id { get; set; }

    // veterinerin adi ve soyadi
    public string FullName { get; set; } = string.Empty;

    // veterinerin uzmanlik alani
    public string Specialty { get; set; } = string.Empty;
}
