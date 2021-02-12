### UContact

##### My Contact API
Kişilerin iletişim bilgilerini saklar<br>
Kişilerin CRUD işlemleri yapılır.
Rabbit MQ üzerinde istekleri dinler.
Rabbit MQ üzerinde gelen istekleri yanıtlar.

#### My Report API
Rapor datası saklanır.<br>
Mevcut raporları listeler.<br>
İstediği raporların oluşturulup oluşturulmadığına bakar.<br>
Oluşturulan raporları veritabanına kaydeder..

#### Web Application
İletişim bilgisi ekle/güncelle/sil yapabilir.<br>
Mevcut iletişim bilgileri listesini görebilir.<br>
Rapor talebinde bulunabilir.<br>
Kayıtlı raporları görebilir.

### Projenin çalıştırılması hakkında
* Her API kendisi için ayrı veritabanına sahiptir.
* Web application herhangi bir veritabanı ile haberleşmez. RabbitMQ den gelen mesajları dinleyebilir veya API'lara http isteği yapabilir.
* API'lar birbirlerinin veritabanlarına erişmezler. Arada iletişim gerekli olması durumunda RabbitMQ üzerinden mesajlarını iletirler.

UContact.Web : https://localhost:5001<br>
UContact.MyContactApi : https://localhost:6001<br>
UContact.MyReportApi : https://localhost:7001

#### Teknolojiler
* .Net 5.0
* EntityFramework Core 5.0
* Swagger(OpenAPI v3 UI)
* Mapster
* xUnit
* RabbitMQ
* PostgreSQL 12.5
