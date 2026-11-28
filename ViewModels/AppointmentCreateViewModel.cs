using System.ComponentModel.DataAnnotations;

namespace PatiTakip.ViewModels;

// randevu formundaki bilgileri tutuyoruz
public class AppointmentCreateViewModel
{
    // randevunun hangi hayvana ait oldugunu aliyoruz
    [Display(Name = "Hayvan Numarası")]
    [Required(ErrorMessage = "Hayvan seçimi zorunludur.")]
    [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir hayvan numarası giriniz.")]
    public int? PetId { get; set; }

    // randevu icin veteriner numarasini aliyoruz
    [Display(Name = "Veteriner Numarası")]
    [Required(ErrorMessage = "Veteriner seçimi zorunludur.")]
    [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir veteriner numarası giriniz.")]
    public int? VeterinarianId { get; set; }

    // randevuda alinacak hizmeti belirliyoruz
    [Display(Name = "Hizmet Numarası")]
    [Required(ErrorMessage = "Hizmet seçimi zorunludur.")]
    [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir hizmet numarası giriniz.")]
    public int? ServiceId { get; set; }

    // randevunun tarihini ve saatini aliyoruz
    [Display(Name = "Randevu Tarihi ve Saati")]
    [Required(ErrorMessage = "Randevu tarihi ve saati zorunludur.")]
    [DataType(DataType.DateTime)]
    public DateTime? AppointmentDate { get; set; }

    // randevuyu olusturan kisinin adini aliyoruz
    [Required(ErrorMessage = "Ad soyad alanı zorunludur.")]
    [StringLength(100, ErrorMessage = "Ad soyad en fazla 100 karakter olabilir.")]
    public string? CreatedByName { get; set; }

}
