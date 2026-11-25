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



### Veteriner Hekimler
- Veteriner modeli ve listeleme sayfası oluşturuldu.
- Örnek veteriner bilgileri tabloda gösterildi.
- Veteriner ekleme formu hazırlandı.
- GET ve POST işlemleri eklendi.
- Ad soyad ve uzmanlık alanı için doğrulama yapıldı.
- Form işlemleri test edildi.


### Aşı Kayıtları
- Vaccination modeli oluşturuldu.
- Örnek aşı kayıtlarıyla listeleme sayfası hazırlandı.
- Aşı kaydı ekleme formu oluşturuldu.
- GET ve POST işlemleri eklendi.
- Form alanları için doğrulama kuralları yazıldı.
- Boş ve hatalı girişler test edildi.
- Geçerli form gönderiminde yönlendirme kontrol edildi.



### Randevu İşlemleri
- Appointment modeli oluşturuldu.
- Örnek randevularla listeleme sayfası hazırlandı.
- Randevu oluşturma formu yapıldı.
- GET ve POST işlemleri eklendi.
- Hayvan, veteriner ve hizmet numaraları için doğrulama yapıldı.
- Hata mesajları Türkçeleştirildi.
- Form gönderme işlemleri test edildi.
- Tekrar eden örnek hayvan bilgileri ortak bir metotta toplandı.
Randevular henüz veritabanına kaydedilmiyor.

