# Proje Adı: TemplateDapr

# Proje Açıklaması:

TemplateDapr, mikroservis mimarisi kullanılarak geliştirilen Safrantek tarafından başlatılan bir uygulama projesidir. Bu proje, büyük ve karmaşık bir uygulamayı daha küçük, bağımsız hizmetlere bölmek ve her bir hizmeti ayrı bir mikroservis olarak geliştirmek amacıyla oluşturulmuştur.

# Ana Amaç:

# Projenin temel amaçları;

Uygulama ölçeklenebilirliğini artırmak
Hizmetler arasındaki bağımsızlığı ve yeniden kullanılabilirliği teşvik etmek
Hızlı geliştirme ve dağıtım süreçlerini sağlamak
Karmaşık uygulamayı daha kolay yönetilebilir hale getirmek

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
