
using System.ComponentModel.DataAnnotations;

namespace PatiTakip.ViewModels;

// veteriner ekleme formundaki bilgileri tutuyoruz
public class VeterinarianCreateViewModel
{
    // veterinerin adi ve soyadi bos birakilamaz
    [Display(Name = "Ad Soyad")]
    [Required(ErrorMessage = "Ad soyad zorunludur.")]
    [StringLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir.")]
    public string FullName { get; set; } = string.Empty;

    // veterinerin uzmanlik alanini tutuyoruz
    [Display(Name = "Uzmanlık Alanı")]
    [Required(ErrorMessage = "Uzmanlık alanı zorunludur.")]
    [StringLength(100, ErrorMessage = "Uzmanlık alanı en fazla 100 karakter olabilir.")]
    public string Specialty { get; set; } = string.Empty;
}
