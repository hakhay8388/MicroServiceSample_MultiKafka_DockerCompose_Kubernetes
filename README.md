# MicroService Sample Kafka
 
## Merhaba

Bu proje basit bir şekilde, bir REST servise gelen event/log olaylarını Kafka aracılığı ile mesaj lar haline getirip kuyruğa ekler. Kuyruğa bağlanmış olan uygulamalar ise bu mesajları dinliyor ve temsili olarak Elastic Search ve Postgres basıyor olacak.

### Senaryo

Birçok mikroservisin olduğu bir sistemde, diğer servislerde oluşan olay/eventleri toparlayacak yeni bir mikroservis oluşturacağız.

Yeni servisimiz üzerindeki tek REST API endpoint aracılığı ile diğer servislerden olayı detaylarıyla birlikte alacak ve Kafka'ya iletecek.

Kafka ise aldığı mesajları minimum 2 farklı sisteme dağıtacak. Bu iki sistemde de bu mesajları 2 farklı sistem karşılıcak.

#### Test Mesajı Yollayıcı  : TestSender         -> Producer(Üretici)
#### WEB Uygulamamız 	   : MicroServiceSample -> Producer(Üretici)
#### Temsili Elastic Search : ElasticSearchApp   -> Consumer(Alıcı)
#### Temsili Postgres App   : PostgresApp        -> Consumer(Alıcı)

### Teknik Tasarım

<div align="center">
  <img src="./images/screenshot-01.png" alt="Diagram" title="" />
</div>

- Kaynak sistemde oluşan olay/eventler, oluşturulacak yeni bir servise HTTP kanalı üzerinden bir dizi/array biçiminde iletilecektir.

- Servise iletilen her bir olay/event mesaj kuyruğuna bir mesaj nesnesi olarak eklenecektir.

- İlk abone olayı detayları ile ElasticSearch üzerine yazacak,

- İkinci abone olayı Postgres’de oluşturulmuş tablo üzerine kaydedecek,


### Teknik Bilgiler

- Kullanılan Teknolojiler
  - .NET Core
  - Kafka
  - Docker
  - Git

  - Mesajları diğer servislerden toplayan ve kanallarda karşılayan tüm uygulamaların docker-compose dosyasına işlenmesi
  - `docker-compose up` komutu ile tüm sistemin ayağa kaldırılabilirliği
  - Projenin nasıl çalıştırılacağına dair README.md dokümantasyonu



### API URL Örneği : https://localhost:44303/WebApi/WebApi
Siz URL içerisindeki port bilgisini kendinize göre özelleştiriyor olmalısınız.


#### POST / - Event İletme

#### Postman ile POST tipinde BODY'yi seçerek RAW formatında JSON tipini seçerek aşağıdaki veriyi göndererek test edebilirsiniz.

Request:

```http
POST /
Content-Type: application/json

{
  "events": [
    {
      "app": "277cdc8c-b0ea-460b-a7d2-592126f5bbb0",
      "type": "HOTEL_CREATE",
      "time": "2020-02-10T13:40:27.650Z",
      "isSucceeded": true,
      "meta": {},
      "user": {
        "isAuthenticated": true,
        "provider": "b2c-internal",
        "email": "test1@hotmail.com",
		"id": 1
      },
      "attributes": {
        "hotelId": 1,
        "hotelRegion": "Kıbrıs",
        "hotelName": "Rixos"
      }
    },
    {
      "app": "277cdc8c-b0ea-460b-a7d2-592126f5bbb1",
      "type": "HOTEL_CREATE",
      "time": "2020-02-10T13:40:27.650Z",
      "isSucceeded": true,
      "meta": {},
      "user": {
        "isAuthenticated": true,
        "provider": "b2c-internal",
        "id": 2,
        "email": "test2@hotmail.com"
      },
      "attributes": {
        "hotelId": 1,
        "hotelRegion": "Kıbrıs",
        "hotelName": "Rixos"
      }
    },
    {
      "app": "277cdc8c-b0ea-460b-a7d2-592126f5bbb2",
      "type": "HOTEL_CREATE",
      "time": "2020-02-10T13:40:27.650Z",
      "isSucceeded": true,
      "meta": {},
      "user": {
        "isAuthenticated": true,
        "provider": "b2c-internal",
        "id": 3,
        "email": "test2@hotmail.com"
      },
      "attributes": {
        "hotelId": 1,
        "hotelRegion": "Kıbrıs",
        "hotelName": "Rixos"
      }
    }
  ]
}


```

Response:

```http
HTTP 200 OK
```

Alanlar:

|Alan                 |Açıklama                                                 |Tip      |Zorunluluk |
|---------------------|---------------------------------------------------------|---------|-----------|
|app                  |Guid cinsinden uygulamanın kimliği                       |Guid     |Evet       |
|type                 |Event/olay kategorisi                                    |Enum     |Evet       |
|time                 |Olayın gerçekleştiği zaman                               |DateTime |Evet       |
|isSucceeded          |İlgili olay başarıyla sonlanmış mı?                      |Boolean  |Evet       |
|meta.*               |Olaya ait ek detaylar                                    |Object   |Hayır      |
|user.isAuthenticated |Olayı gerçekleştiren kullanıcı giriş yapmış mı?          |Boolean  |Evet       |
|user.provider        |Olayı gerçekleştiren kullanıcının kaydı hangi sistemde?  |String   |Evet       |
|user.id              |Olayı gerçekleştiren kullanıcının id’si                  |Long     |Evet       |
|user.email           |Olayı gerçekleştiren kullanıcının e-mail bilgisi         |String   |Hayır      |
|attributes.*         |Olaya kategorisine özel detaylar                         |Object   |Hayır      |


### Nasıl başlayabilirim?

- Öncelikle docker programını yüklemelisiniz. Docker CAAS teknolojilerini araştırarak bilgi sahibi olabilirsiniz.

- docker programını yükledikten sonra aşağıdaki kodu projenin klasörüne konsol açıp çalıştırıyoruz.
 
- `docker-compose -f docker-compose.yml up`
  
- Yukarıdaki komut öncelikle Kafka servisini ayağa kaldırıyor olacak. Ardından "topic" adında bir kafka kanalı oluşturacak.
  bu kanal/kanallar oluştuktan sonra ElasticSearhApp ve PostgresApp ayağa kalkıp kafkaya Consumer(Alıcı) olarak bağlanmış olacak.

- Ardından Test Gönderici Projemiz olan TestSender projemizi başlatabiliriz. Bu projemizi çalıştırıp enter'a bastığınızda mesajınız Kafka üzerinden diğer alıcılara dağıtılacaktır.

- Yukarıdaki test sonucunda başarılı olduğunda son adım olan WEB projemiz'e geçebilirsiniz. Web projemiz olan MicroServiceSample Visual Studio içinde açtıktan sonra Play dediğimizde "Microservice Started..." mesajını aldığınızda servis veri almaya hazır demektir.

- Yukarıdaki bahsi geçen postman ile ilgili JSON'ı https://localhost:"sizin_portunuz_yazılmalı"/WebApi/WebApi adresini çağırdığınızda ilgili verileri Kafka üzerinden ilgili uygulamalara iletmiş olduğunu göreceksiniz. :)


## Sorularınız

Değerlendirmelerle ilgili sorularınızı [hakhay8388@hotmail.com](mailto:hakhay8388@hotmail.com) adresine iletebilirsiniz.



