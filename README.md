# Trafik Cezası Yönetim ve Onay Modülü

Trafik cezalarının araç bazında takip edildiği, belirli bir onay akışından (Yönetici → Finans) geçirilerek sonuçlandırıldığı bir **.NET Core MVC** uygulaması.

## İçindekiler

- [Özellikler](#özellikler)
- [Teknolojiler](#teknolojiler)
- [Mimari](#mimari)
- [Onay Süreci](#onay-süreci)
- [Kurulum](#kurulum)
- [Varsayılan Kullanıcılar](#varsayılan-kullanıcılar)
- [Proje Yapısı](#proje-yapısı)
- [Alınan Teknik Kararlar](#alınan-teknik-kararlar)

## Özellikler

**Araç Yönetimi**
- Araç ekleme, düzenleme ve listeleme
- Plaka, araç tipi (Binek / Çekici / Dorse / Kiralık Araç), marka ve model bilgisi
- Aynı plakanın tekrar kaydedilmesini engelleyen benzersizlik kontrolü

**Trafik Cezası Yönetimi**
- Araca bağlı ceza kaydı oluşturma ve düzenleme
- Cezaların araç bazında gruplanmış şekilde listelenmesi
- Onay sürecine girmiş bir cezanın düzenlenememesi

**Onay Süreci**
- `Yeni → Yönetici Onayı → Finans Onayı → Tamamlandı` akışı
- Her aşamayı yalnızca ilgili rolün onaylayabilmesi (Manager / Finance)
- Onay ve ret işlemlerinin desteklenmesi, ret için zorunlu açıklama alınması
- Her ceza için onay/ret geçmişinin (kim, ne zaman, hangi durumdan hangi duruma) tutulması

**Kimlik Doğrulama**
- ASP.NET Core Identity ile giriş/çıkış
- Rol bazlı yetkilendirme (Manager, Finance)

## Teknolojiler

- .NET 9 / ASP.NET Core MVC
- Entity Framework Core 9 (SQL Server)
- ASP.NET Core Identity
- FluentValidation (global action filter ile entegre)

## Mimari

Katmanlı bir yapı kullanılmıştır:

```
Controllers   → HTTP isteklerini karşılar, Service katmanına yönlendirir
Services      → İş kuralları burada uygulanır (durum geçişleri, yetki kontrolleri vb.)
Repositories  → EF Core üzerinden veri erişimi (Generic Repository + özelleşmiş repository'ler)
Dtos          → Controller/View ile Entity katmanı arasındaki veri taşıyıcıları
Validators    → FluentValidation kuralları, tüm POST action'larda global bir
                ActionFilter (FluentValidationFilter) aracılığıyla otomatik çalışır
Data/Entities → EF Core entity'leri
Data/Enums    → VehicleType, FineStatus, ApprovalAction
```

Controller'lar ModelState doğrulamasını elle yapmaz; `FluentValidationFilter`, action çalışmadan önce ilgili DTO için kayıtlı validator'ı bulup çalıştırır ve hata varsa isteği view'a (veya `ApproveTrafficFineDto` / `RejectTrafficFineDto` için ilgili detay sayfasına) geri yönlendirir.

## Onay Süreci

| Durum (FineStatus) | Açıklama                          | Onaylayabilecek Rol |
|---------------------|------------------------------------|----------------------|
| New                 | Ceza yeni oluşturulmuş             | Manager              |
| ManagerApproved     | Yönetici onayı verilmiş            | Finance              |
| Completed           | Finans onayı ile süreç tamamlanmış | —                     |
| Rejected            | Süreç herhangi bir aşamada reddedilmiş | —                 |

Kurallar:
- Bir kullanıcı yalnızca kendi rolüne karşılık gelen aşamadaki cezayı onaylayabilir/reddedebilir.
- Onay verildiğinde ceza bir sonraki aşamaya ilerler; ret verildiğinde `Rejected` durumuna geçer ve süreç orada sonlanır (tekrar sürece sokulmaz).
- Her onay/ret işlemi `ApprovalHistory` tablosuna; işlemi yapan kullanıcı, önceki/yeni durum, işlem tipi, tarih ve açıklama (ret nedeni) ile birlikte kaydedilir.
- `New` dışındaki bir durumdaki ceza artık düzenlenemez (onay sürecine girmiş bir kaydın veri bütünlüğünü korumak için).

## Kurulum

### Gereksinimler
- .NET 9 SDK
- SQL Server (LocalDB / Express / tam sürüm)

### Adımlar

1. Bağlantı dizesini kontrol edin (`appsettings.Development.json`):
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.\\SQLEXPRESS;Database=TrafficFineSystemDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
   Gerekirse kendi SQL Server instance'ınıza göre güncelleyin.

2. Bağımlılıkları yükleyin ve veritabanını oluşturun:
   ```bash
   dotnet restore
   dotnet ef database update --project TrafficFineSystem
   ```

3. Uygulamayı çalıştırın:
   ```bash
   dotnet run --project TrafficFineSystem
   ```

Uygulama ilk açılışta rolleri, varsayılan kullanıcıları ve örnek araç/ceza/onay geçmişi verilerini otomatik olarak seed eder (`Program.cs` içindeki `SeedRolesAsync`, `SeedUsersAsync`, `SeedDataAsync`).

## Varsayılan Kullanıcılar

| Rol      | Email                     | Şifre       |
|----------|---------------------------|-------------|
| Manager  | manager@trafficfine.com   | Manager123  |
| Finance  | finance@trafficfine.com   | Finance123  |

## Proje Yapısı

```
TrafficFineSystem/
├── Controllers/          # AccountController, VehicleController, TrafficFineController, ApprovalController
├── Data/
│   ├── Entities/          # Vehicle, TrafficFine, ApprovalHistory, AppUser
│   ├── Enums/              # VehicleType, FineStatus, ApprovalAction
│   └── Repositories/       # Generic + entity bazlı repository'ler
├── Dtos/                  # Create/Update/List/Detail DTO'ları
├── Extensions/            # DI kaydı, rol/kullanıcı/veri seed işlemleri
├── Filters/               # FluentValidationFilter (global validation)
├── Services/              # İş kuralları (VehicleService, TrafficFineService, ApprovalService, AccountService)
├── Validators/            # FluentValidation kuralları
├── Views/                 # Razor View'ları
└── Program.cs
```

## Alınan Teknik Kararlar

Case metninde serbest bırakılan konularda verilen kararlar:

- **Onay mekanizması**: Ayrı bir workflow motoru yerine `FineStatus` enum'u + `ApprovalService` içindeki rol/durum kontrolleriyle basit bir state machine uygulanmıştır. Küçük ölçekli, sabit adımlı bir süreç için yeterli görülmüştür.
- **Onay geçmişi**: Her durum değişikliği ayrı bir `ApprovalHistory` kaydı olarak tutulur; `TrafficFine` üzerinde geçmiş bilgisi tutulmaz, böylece denetlenebilirlik korunur.
- **Reddedilen kayıt**: `Rejected` durumu terminaldir, tekrar sürece sokulamaz. Yeniden değerlendirme gerekiyorsa yeni bir ceza kaydı açılması beklenir.
- **Düzenleme kısıtı**: Yalnızca `New` durumundaki cezalar düzenlenebilir; onay sürecine girmiş bir kayıt üzerinde tutarsızlık oluşmaması hedeflenmiştir.
