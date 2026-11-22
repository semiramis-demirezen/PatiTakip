namespace PatiTakip.Models;
//verilen hizmetin bilgileri
public class Service
{
    public int Id {get;set;}
    public string Name {get;set;} = string.Empty;
    //randevu saatlerinin cakismasini kontrol etmek icin 
    public int DurationMinutes {get;set;}
    public decimal Price {get;set;}
}