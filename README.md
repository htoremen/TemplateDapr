# Proje Adı: TemplateDapr

# Proje Açıklaması:

TemplateDapr, mikroservis mimarisi kullanılarak geliştirilen Safrantek tarafından başlatılan bir uygulama projesidir. Bu proje, büyük ve karmaşık bir uygulamayı daha küçük, bağımsız hizmetlere bölmek ve her bir hizmeti ayrı bir mikroservis olarak geliştirmek amacıyla oluşturulmuştur. Geleneksel monolitik uygulama yapılarının dezavantajlarını aşmak ve daha etkili bir yazılım geliştirme ve dağıtma süreci sunmak için tasarlanmıştır.

# Ana Amaç:

# Projenin temel amaçları;

- Monolitik yapılardan kaynaklanan sıkı bağımlılıkları azaltmak.
- Uygulama bileşenlerini bağımsız olarak geliştirmek ve dağıtmak için mikroservisler kullanmak.
- Uygulama ölçeklenebilirliğini artırmak
- Hizmetler arasındaki bağımsızlığı ve yeniden kullanılabilirliği teşvik etmek
- İş süreçlerini parçalara ayırarak geliştirme sürecini hızlandırmak.
- Hızlı geliştirme ve dağıtım süreçlerini sağlamak
- Farklı teknolojileri bir arada kullanarak dil ve çerçeve bağımsızlığı sağlamak.
- Karmaşık uygulamayı daha kolay yönetilebilir hale getirmek

# Proje Özellikleri:

- Mikroservis Mimarisi: Proje, bağımsız hizmetler olarak çalışan mikroservislerden oluşur. Her bir mikroservis, belirli bir işlevi yerine getirir ve kendi verilerini yönetir.
- Dil ve Çerçeve Bağımsızlığı: Farklı programlama dilleri ve çerçeveler kullanarak her mikroservisi geliştirebiliriz. Bu, geliştiricilere esneklik sağlar.
- API ve Etkinlik Tabanlı İletişim: Mikroservisler arasında iletişim, API çağrıları ve etkinlik tabanlı mesajlaşma yoluyla sağlanır.
- Veri Yönetimi ve Veritabanları: Her mikroservis, kendi veritabanını kullanır ve veri yönetimini bağımsız olarak gerçekleştirir.
- Konteynerizasyon ve Orkestrasyon: Docker konteynerleri kullanılarak mikroservislerin paketlenmesi ve Kubernetes gibi orkestrasyon araçlarıyla dağıtılması sağlanır.

# Proje Bileşenleri:

TemplateDapr, aşağıdaki anahtar bileşenleri içermektedir: Mikroservislerin doğru şekilde tasarlanması, projenin başarısı için çok önemlidir, geliştirilmesi için doğru teknolojilerin seçilmesi ve yönetimi için doğru süreçlerin ve araçların kullanılması gerekir. Geliştirilecek olan Microservis uygulaması için ihtiyaç duyulduğu kadar servisin oluşturulmasına olanak sağlamaktadır.

# Service 1
Kullanılan Teknolojiler: Dapr, Redis 
# Service 2
Kullanılan Teknolojiler: Dapr, RabbitMQ, Zeebe
# Service 3
Kullanılan Teknolojiler: Dapr, Kafka, Zeebe
# Service 4
Kullanılan Teknolojiler: Dapr, RabbitMQ, Kafka, Zeebe
# Service 5
Kullanılan Teknolojiler: Dapr, RabbitMQ, Kafka, Zeebe
# Service 6
Kullanılan Teknolojiler: Dapr, Redis, RabbitMQ, Kafka, Zeebe

# Kullanılan Teknolojiler:

# Bu proje, aşağıdaki temel teknolojileri kullanmaktadır:

.Net Core 
Redis
RabbitMQ
Kafka
Zeebe

# Nasıl Çalışır:

Bu proje, her bir mikroservisin belirli bir işlevi yerine getirdiği bir dizi bağımsız hizmetten oluşur. Her servis DAPR iletişim protokollerini kullanarak birbirleriyle iletişim kurar.


# Teknolojiler:

- Dil ve Çerçeveler: Mikroservislerin geliştirilmesi için uygun programlama dilleri ve çerçeveler.
- Konteynerleştirme: Docker ve Kubernetes gibi konteynerleştirme teknolojileri.
- API Geçidi: Mikroservislerin dış dünyayla etkileşimini kolaylaştırmak için bir API geçidi.
- Veri Depolama: Mikroservisler arasında veri paylaşımını desteklemek için veri tabanları ve veri depolama çözümleri.

# Beklenen Sonuçlar:
- Mikroservislerin başarılı bir şekilde geliştirilmesi ve işletilmesi.
- Daha iyi ölçeklenebilirlik, bakım ve hizmet sürekliliği.
- Kullanıcı deneyiminde iyileştirmeler.
- Geliştirme süreçlerinde hızlanma.


# TemplateDapr Architecture

![alt text](https://raw.githubusercontent.com/htoremen/TemplateDapr/master/Files/TemplateDapr.drawio.png)

# Dapr Dashboard

![alt text](https://raw.githubusercontent.com/htoremen/TemplateDapr/master/Files/DaprDashboard.png)
