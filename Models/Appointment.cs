namespace PatiTakip.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        // randevunun ait oldugu hayvan
        public int PetId { get; set; }

        // randevuyu yapacak veteriner
        public int VeterinarianId { get; set; }

        // randevuda verilecek hizmet
        public int ServiceId { get; set; }

        // randevunun tarihi ve saati
        public DateTime AppointmentDate { get; set; }

        // randevunun mevcut durumu
        public string Status { get; set; } = "Bekliyor";

        // randevuyla iliskili hayvan
        public Pet Pet { get; set; } = null!;

        // randevuyla iliskili veteriner
        public Veterinarian Veterinarian { get; set; } = null!;

        // randevuyla iliskili hizmet
        public Service Service { get; set; } = null!;
    }
}
