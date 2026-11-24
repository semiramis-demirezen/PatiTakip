
using System.ComponentModel.DataAnnotations;

namespace PatiTakip.ViewModels;

// asi ekleme formundaki bilgileri tutuyoruz
public class VaccinationCreateViewModel
{
    // asinin hangi hayvana ait oldugunu belirtiyoruz
    [Display(Name = "Hayvan Numarası")]
    [Required(ErrorMessage = "Hayvan numarası zorunludur.")]
    [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir hayvan numarası giriniz.")]
    public int? PetId { get; set; }

    // asi adini kontrol ediyoruz
    [Display(Name = "Aşı Adı")]
    [Required(ErrorMessage = "Aşı adı zorunludur.")]
    [StringLength(100, ErrorMessage = "Aşı adı en fazla 100 karakter olabilir.")]
    public string VaccineName { get; set; } = string.Empty;

    // asinin yapildigi tarihi aliyoruz
    [Display(Name = "Aşının Yapıldığı Tarih")]
    [Required(ErrorMessage = "Aşı tarihi zorunludur.")]
    [DataType(DataType.Date)]
    public DateTime? DateAdministered { get; set; }

    // bir sonraki doz tarihini aliyoruz
    [Display(Name = "Sonraki Doz Tarihi")]
    [Required(ErrorMessage = "Sonraki doz tarihi zorunludur.")]
    [DataType(DataType.Date)]
    public DateTime? NextDoseDate { get; set; }
}
