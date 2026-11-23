# PatiTakip - Veteriner Kliniği Randevu ve Hasta Takip Sistemi

**Öğrenci Numarası:** B241200027  
**Ad Soyad:** Semiramis Demirezen  
**Ders Grubu:** A Grubu  
**GitHub:** https://github.com/semiramis-demirezen/PatiTakip

## Proje Hakkında

PatiTakip, veteriner klinikleri için geliştirdiğim bir randevu ve hasta takip sistemidir.

Projede üç farklı kullanıcı rolü bulunacak:
- Admin
- Veteriner
- Üye (Hayvan Sahibi)

Kullanıcılar rollerine göre hayvan bilgilerini, randevuları ve muayene kayıtlarını yönetebilecekler.

## Kullanılan Teknolojiler

- ASP.NET Core MVC (.NET 10)
- C#
- Entity Framework Core
- SQLite
- HTML, CSS
- Bootstrap 5
- Git ve GitHub

İlerleyen aşamalarda Identity, REST API ve MCP de kullanılacak.

## Proje Durumu

Projenin temel MVC yapısını oluşturdum. Şu anda evcil hayvanları listeleyebiliyorum ve yeni hayvan eklemek için bir form hazırladım.

Formda zorunlu alan kontrolleri ve hata mesajları çalışıyor. Ancak henüz veritabanına kayıt yapılmıyor.

## Şu Ana Kadar Yapılanlar

### MVC ve Veritabanı Hazırlığı
- ASP.NET Core MVC projesi oluşturuldu.
- Pet modeli ve PetsController hazırlandı.
- SQLite ve Entity Framework Core bağlantısı ayarlandı.
- İlk Migration oluşturuldu.

### Evcil Hayvan İşlemleri
- Örnek verilerle hayvan listeleme sayfası yapıldı.
- Yeni evcil hayvan ekleme formu oluşturuldu.
- GET ve POST işlemleri eklendi.
- Model Binding ve Validation kullanıldı.
- Hatalı girişler için Türkçe uyarılar eklendi.
- Sayfalar arası bağlantılar ve menü düzenlendi.

- Ana sayfa PatiTakip'e uygun şekilde düzenlendi.
- Evcil hayvan detay sayfası oluşturuldu.
- Hayvanların ID bilgisine göre detayları gösterildi.
- Geçersiz ID girildiğinde 404 hatası döndürüldü.
- Doğum tarihleri Gün.Ay.Yıl biçiminde düzenlendi.

### Klinik Hizmetleri
- Service modeli oluşturuldu.
- ServicesController hazırlandı.
- Örnek hizmetlerle listeleme sayfası yapıldı.
- Hizmetlerin süre ve ücret bilgileri tabloda gösterildi.
- Üst menüye Hizmetler bağlantısı eklendi.


### Hizmet Ekleme
- Hizmet ekleme formu oluşturuldu.
- GET ve POST işlemleri eklendi.
- Hizmet adı, süre ve ücret için doğrulama kuralları yazıldı.
- Hatalı girişlerde uyarı mesajları gösterildi.
- Form gönderme ve yönlendirme işlemleri test edildi.


## Kullanılan Ders Konuları

- **H02:** MVC, Controller, Action
- **H03:** Razor, Model, ViewData, foreach, Layout
- **H04:** GET, POST, Model Binding, ViewModel
- **H05:** Tag Helper, AntiForgeryToken
- **H06:** DataAnnotations, ModelState ve Validation

## Sonraki Aşama

- Evcil hayvan bilgilerini veritabanına kaydetmek
- Güncelleme ve silme işlemlerini yapmak
- Kullanıcı girişini ve rollerini oluşturmak
- Veteriner ve randevu işlemlerini geliştirmek
- REST API ve MCP özelliklerini eklemek

