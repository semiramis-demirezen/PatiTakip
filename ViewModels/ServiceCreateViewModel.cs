using System.ComponentModel.DataAnnotations;

namespace PatiTakip.ViewModels;

// hizmet ekleme formundaki bilgileri tutuyoruz
public class ServiceCreateViewModel
{
    // hizmet adi bos birakilamaz
    [Display(Name = "Hizmet Adı")]
    [Required(ErrorMessage = "Hizmet adı zorunludur.")]
    [StringLength(50, ErrorMessage = "Hizmet adı en fazla 50 karakter olabilir.")]
    public string Name { get; set; } = string.Empty;

    // hizmet suresi 1 ile 240 dakika arasinda olmali
    [Display(Name = "Süre (Dakika)")]
    [Required(ErrorMessage = "Hizmet süresi zorunludur.")]
    [Range(1, 240, ErrorMessage = "Süre 1 ile 240 dakika arasında olmalıdır.")]
    public int? DurationMinutes { get; set; }

    // hizmet ucreti belirlenen aralikta olmali
    [Display(Name = "Ücret (TL)")]
    [Required(ErrorMessage = "Hizmet ücreti zorunludur.")]
    [Range(1, 100000, ErrorMessage = "Ücret 1 ile 100000 TL arasında olmalıdır.")]
    public decimal? Price { get; set; }
}
