# Swing It - Fizik Tabanlı Parkur Oyunu

**Geliştirici:** Taylan Özer  
**Öğrenci Numarası:** 032490029  

Swing It, Unity motoru kullanılarak geliştirilmiş, oyuncunun haritanın altındaki ölümcül boşluğa (Killzone) düşmeden, kanca mekaniğini kullanarak en kısa sürede bitiş çizgisine ulaşmaya çalıştığı 3 boyutlu bir zamana karşı yarış (speedrun) platform oyunudur.


## 🛠️ Programın Kurulumu ve Çalıştırılması

Oyunun derlenmiş (Build) sürümünü bilgisayarınızda çalıştırmak için lütfen aşağıdaki adımları takip edin:

1. `Swing It Build.zip` arşiv dosyasını bilgisayarınıza indirin. (https://drive.google.com/file/d/1JfVzEk4ZmOREgExa3WNZZDOM0onuHm_w/view?usp=drive_link)
2. İndirilen ZIP dosyasına sağ tıklayarak **"Klasöre Çıkar"** seçeneğiyle tüm dosyaları arşivden temiz bir klasöre çıkartın.
3. Oyunun çalışabilmesi için `.exe` dosyası ile `Swing It_Data` klasörü aynı dizinde bulunmalıdır. Bu nedenle `.exe` dosyasını tek başına klasör dışına taşımayınız.
4. Klasör içerisindeki `Swing It.exe` dosyasına çift tıklayarak oyunu doğrudan başlatabilirsiniz. Herhangi bir ek kurulum veya setup yüklemesi gerektirmez.


## 🎮 Oyun İçi Kontroller

* **W, A, S, D:** Karakter Hareketi / Yürüme
* **SPACE (Zemindeyken):** Zıplama
* **MOUSE SOL TIK:** Kanca Atma / Hedefe Tutunma
* **SPACE (İp üzerindeyken):** Yukarı Tırmanma (İpi Çekme)
* **SOL CTRL (İp üzerindeyken):** Aşağı İnme (İpi Salma)
* **ESC (Oyun içi):** Herhangi bir anda anlık olarak Ana Menüye Dönüş


## 📦 Kullanılan Assetler ve Kütüphaneler

Projenin geliştirilme sürecinde görsel çeşitliliği artırmak ve teknik altyapıyı kurmak adına aşağıdaki asset paketleri ve bileşenler kullanılmıştır:

* **Cyberpunk Neon City (Low Poly Sci-Fi Urban Environment):** Oyundaki alternatif alanlardan biri olan siber-fütüristik şehrin mekanlarını, dikey binalarını ve neon ışıklandırmalı platformlarını oluşturmak için dekoratif olarak kullanılmıştır.
* **Lava Plants (Free Low Poly Pack):** Haritalardaki çevre detaylarını zenginleştirmek, ortama daha parıltılı ve canlı bir hava katmak amacıyla kullanılmıştır.

* **Unity New Input System:** Klavye ve özellikle fare deltasının (bakış açısı hassasiyetinin) yeni nesil optimizasyonla ve yüksek hassasiyetle okunmasını sağlamıştır.
* **TextMeshPro (TMP):** Ana menü, kontroller paneli ve oyun içi çift zamanlayıcı (Toplam Süre / Mevcut Deneme) sayaçlarının yüksek çözünürlüklü, esnek fontlarla ekrana yazdırılmasında kullanılmıştır.
* **Unity Physics (Rigidbody & SpringJoint):** Karakterin yerçekimi etkileri, platform sürtünmeleri ve halat salınım fiziği hazır fizik motoru elementleriyle simüle edilmiştir.
