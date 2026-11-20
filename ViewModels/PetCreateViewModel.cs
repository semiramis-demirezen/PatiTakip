
using System.ComponentModel.DataAnnotations;

namespace PatiTakip.ViewModels;

// H04-H06: Evcil hayvan ekleme formunun veri modeli.
// Kullanıcıdan yalnızca gerekli bilgileri alır.
public class PetCreateViewModel
{
    [Display(Name = "Hayvan Adı")]
    [Required(ErrorMessage = "Hayvan adı zorunludur.")]
    [StringLength(50, ErrorMessage = "En fazla 50 karakter girilebilir.")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Tür")]
    [Required(ErrorMessage = "Hayvan türü zorunludur.")]
    [StringLength(50, ErrorMessage = "En fazla 50 karakter girilebilir.")]
    public string Species { get; set; } = string.Empty;

    [Display(Name = "Irk")]
    [Required(ErrorMessage = "Irk bilgisi zorunludur.")]
    [StringLength(50, ErrorMessage = "En fazla 50 karakter girilebilir.")]
    public string Breed { get; set; } = string.Empty;

    [Display(Name = "Doğum Tarihi")]
    [Required(ErrorMessage = "Doğum tarihi zorunludur.")]
    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    [Display(Name = "Kilo (kg)")]
    [Required(ErrorMessage = "Kilo bilgisi zorunludur.")]
    [Range(0.1, 200, ErrorMessage = "Kilo 0.1 ile 200 arasında olmalıdır.")]
    public double? Weight { get; set; }
}
