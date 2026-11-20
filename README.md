
# PatiTakip - Veteriner Kliniği Randevu ve Hasta Takip Sistemi

**Öğrenci Numarası:** [B241200027]
**Ad Soyad:** [Semiramis Demirezen]
**Ders Grubu:** [A Grubu]
**GitHub:** [Depo bağlantın]

## Proje Hakkında

PatiTakip, veteriner kliniklerinin randevu ve hasta
takip işlemlerini yönetebilmesi için geliştirilen
bir web uygulamasıdır.

Sistemde üç kullanıcı rolü bulunacaktır:
- Admin
- Veteriner
- Üye (Hayvan Sahibi)

## Kullanılan Teknolojiler

- ASP.NET Core MVC (.NET 10)
- C#
- Entity Framework Core
- SQLite
- HTML, CSS, JavaScript
- Bootstrap 5
- REST API
- MCP

## Proje Durumu

Projenin temel ASP.NET Core MVC yapısı oluşturuldu.


## Geliştirme Notları

### MVC Altyapısı
- ASP.NET Core MVC ile proje oluşturuldu.
- Controller, Model ve View yapısı kullanıldı.

### Veritabanı Hazırlığı
- SQLite veritabanı seçildi.
- Entity Framework Core paketleri eklendi.
- Pet modeli ve AppDbContext oluşturuldu.
- İlk Migration hazırlandı.

### Evcil Hayvan Listeleme
- PetsController oluşturuldu.
- Index action metodu yazıldı.
- Test amacıyla geçici Pet listesi kullanıldı.
- Razor ve Bootstrap ile listeleme sayfası hazırlandı.

### Kullanılan Ders Konuları
- H02: MVC, Controller, Action
- H03: Razor, Model, ViewData, foreach

### Sonraki Aşama
- Evcil hayvan ekleme formu
- GET ve POST işlemleri
- Model Binding ve Validation
