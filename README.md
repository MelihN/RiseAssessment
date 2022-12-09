# RiseAssessment
Uygulama .net 7 ile hazırlanmıştır. İki ana projeden oluşur.
.Web API Servis projesi (RAServices) ve 
.Kullanıcı arayüzü (RAUI)
kullanılacak referans modelleri için ortak bir model projesi kullanılmıştır.(RaModels)

Projenin çalışabilmesi için
 - MongoDB
 - Kafka
gerekmektedir.
Projeler içerisinde bağlantı ayarları için appsettings.json 
dosyası kullanılabilir. Projeler docker container olarak hazırlanmıştır.
Bu nedenle docker üzerinde çalışırılabilir. 
Bunun için uygulama ayarlarının ve container ayarlarının yapılması gerekir.
Aynı sunucu üzerinde docker ile çalışabilmeleri için aralarında network yapısı kurulması gerekir.

Servis projesi içersinde bulunan bütün servisler POST metoduna göre hazırlanmıştır.
uygulama test amaçlı olduğu için giriş işlemleri authentication ve authorization için çalışma yapılmamıştır.
Kullanıcı arayüzü bootstrap ve jquery ile hazırlanmıştır. Arayüz aksiyonları POST metodu ile çalışmaktadır.
Ancak güvenlik ihtiyacı bulunmadığından AntiforgeryToken yapısı kurulmamıştır.