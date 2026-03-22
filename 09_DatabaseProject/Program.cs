using System;

namespace ClassOrnek
{
    // Bu class adres bilgisini tutar.
    // Yani tek görevi: şehir ve ülke bilgisini saklamak.
    public class Adres
    {
        // Sadece okunabilen property'ler.
        // Dışarıdan değer okunur ama sonradan değiştirilemez.
        public string Sehir { get; }
        public string Ulke { get; }

        // Constructor:
        // Nesne oluşturulurken ilk değerleri vermemizi sağlar.
        public Adres(string sehir, string ulke)
        {
            Sehir = sehir;
            Ulke = ulke;
        }

        // Nesneyi yazdırınca anlamlı bir metin dönsün diye override ettik.
        public override string ToString()
        {
            return $"{Sehir} / {Ulke}";
        }
    }

    // Bu class bir öğrenciyi temsil eder.
    public class Ogrenci
    {
        // private field:
        // Dışarıdan direkt erişilemez. Sadece class içinden kontrol edilir.
        private int _yas;

        // public property:
        // Dışarıdan okunabilir.
        // private set olmadığı için burada "Ad" sadece constructor içinde atanacak.
        public string Ad { get; }

        // Property içinde kontrol (validation) yapıyoruz.
        // Böylece biri yanlış yaş verirse engellemiş oluyoruz.
        public int Yas
        {
            get { return _yas; }
            set
            {
                if (value < 0 || value > 120)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Yaş 0 ile 120 arasında olmalıdır.");
                }

                _yas = value;
            }
        }

        // Composition örneği:
        // Ogrenci class'ı içinde bir Adres nesnesi var.
        // Yani "Öğrenci bir adrese sahiptir."
        public Adres EvAdresi { get; }

        // Static property:
        // Bu değer tüm öğrenciler için ortaktır.
        // Her nesnede ayrı ayrı tutulmaz.
        public static int ToplamOgrenci { get; private set; }

        // Constructor:
        // Ogrenci nesnesi oluşturulurken çalışır.
        public Ogrenci(string ad, int yas, Adres evAdresi)
        {
            Ad = ad;
            Yas = yas; // property üzerinden atama yaptığımız için kontrol çalışır
            EvAdresi = evAdresi;

            // Her yeni öğrenci oluştuğunda sayı artsın
            ToplamOgrenci++;
        }

        // Method:
        // Class'ın yaptığı işi temsil eder.
        public int DogumYiliTahmini()
        {
            return DateTime.Now.Year - Yas;
        }

        // Başka bir method:
        // Öğrencinin bilgilerini tek bir metin olarak döndürür.
        public string BilgiGetir()
        {
            return $"Ad: {Ad}, Yaş: {Yas}, Adres: {EvAdresi}, Tahmini Doğum Yılı: {DogumYiliTahmini()}";
        }

        // Nesne direkt yazdırılırsa güzel bir çıktı versin
        public override string ToString()
        {
            return BilgiGetir();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Önce bir Adres nesnesi oluşturuyoruz
                Adres adres1 = new Adres("İstanbul", "Türkiye");

                // Sonra bu adresi kullanarak bir Ogrenci nesnesi oluşturuyoruz
                Ogrenci ogrenci1 = new Ogrenci("Ahmet", 20, adres1);

                // Yeni bir nesne daha oluşturalım
                Adres adres2 = new Adres("Ankara", "Türkiye");
                Ogrenci ogrenci2 = new Ogrenci("Zeynep", 22, adres2);

                Console.WriteLine("=== Öğrenci Bilgileri ===");
                Console.WriteLine(ogrenci1);
                Console.WriteLine(ogrenci2);

                Console.WriteLine();
                Console.WriteLine("Toplam öğrenci sayısı: " + Ogrenci.ToplamOgrenci);

                Console.WriteLine();
                Console.WriteLine("=== Property değiştirme örneği ===");

                // Yaşı sonradan değiştirebiliriz
                ogrenci1.Yas = 21;
                Console.WriteLine($"{ogrenci1.Ad} için yeni yaş: {ogrenci1.Yas}");

                Console.WriteLine();
                Console.WriteLine("=== Hatalı değer örneği ===");

                // Bilerek hatalı değer veriyoruz.
                // Burada exception oluşacak.
                ogrenci2.Yas = -5;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Hata yakalandı: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Program bitti.");
        }
    }
}