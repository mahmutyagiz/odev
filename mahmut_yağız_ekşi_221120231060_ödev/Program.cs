using System;
using System.Collections.Generic;

public enum AraçDurumu
{
    Boşta,
    Kiralanmış,
    Satılmış
}

public class Araç
{
    public string Plaka { get; set; }
    public string Model { get; set; }
    public double Fiyat { get; set; }
    public AraçDurumu Durum { get; set; }

    public Araç(string plaka, string model, double fiyat)
    {
        Plaka = plaka;
        Model = model;
        Fiyat = fiyat;
        Durum = AraçDurumu.Boşta;
    }

    public void AraçBilgisi()
    {
        Console.WriteLine($"Plaka: {Plaka}, Model: {Model}, Fiyat: {Fiyat}, Durum: {Durum}");
    }
}

public class Program
{
    private static List<Araç> araçlar = new List<Araç>();
    private static double globalIndirim = 0; // Genel indirim oranı

    public static void Main(string[] args)
    {
        Console.Write("Genel indirim yüzdesini girin (0-100): ");
        globalIndirim = Convert.ToDouble(Console.ReadLine());

        while (true)
        {
            Console.WriteLine("\nAraç Yönetim Sistemi");
            Console.WriteLine("1. Araç Ekle");
            Console.WriteLine("2. Araç Sil");
            Console.WriteLine("3. Araçları Listele");
            Console.WriteLine("4. Araç Sat");
            Console.WriteLine("5. Araç Kirala");
            Console.WriteLine("6. Çıkış");
            Console.Write("Seçiminizi yapın: ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    AraçEkle();
                    break;
                case "2":
                    AraçSil();
                    break;
                case "3":
                    AraçlarıListele();
                    break;
                case "4":
                    AraçSat();
                    break;
                case "5":
                    AraçKirala();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyin.");
                    break;
            }
        }
    }

    private static void AraçEkle()
    {
        Console.Write("Plaka girin: ");
        string plaka = Console.ReadLine();
        Console.Write("Model girin: ");
        string model = Console.ReadLine();
        Console.Write("Fiyat girin: ");
        double fiyat = Convert.ToDouble(Console.ReadLine());
        araçlar.Add(new Araç(plaka, model, fiyat));
        Console.WriteLine("Araç eklendi.");
    }

    private static void AraçSil()
    {
        Console.Write("Silmek istediğiniz aracın plakasını girin: ");
        string plaka = Console.ReadLine();
        Araç araç = araçlar.Find(a => a.Plaka == plaka);

        if (araç != null)
        {
            araçlar.Remove(araç);
            Console.WriteLine("Araç silindi.");
        }
        else
        {
            Console.WriteLine("Araç bulunamadı.");
        }
    }

    private static void AraçlarıListele()
    {
        foreach (var araç in araçlar)
        {
            araç.AraçBilgisi();
        }
    }

    private static void AraçSat()
    {
        Console.Write("Satmak istediğiniz aracın plakasını girin: ");
        string plaka = Console.ReadLine();
        Araç araç = araçlar.Find(a => a.Plaka == plaka);

        if (araç != null && araç.Durum == AraçDurumu.Boşta)
        {
            double indirimliFiyat = araç.Fiyat - (araç.Fiyat * globalIndirim / 100);
            araç.Durum = AraçDurumu.Satılmış;
            Console.WriteLine($"Araç satıldı. İndirimli fiyat: {indirimliFiyat}");
        }
        else if (araç == null)
        {
            Console.WriteLine("Araç bulunamadı.");
        }
        else
        {
            Console.WriteLine("Araç zaten satıldı.");
        }
    }

    private static void AraçKirala()
    {
        Console.Write("Kiralamak istediğiniz aracın plakasını girin: ");
        string plaka = Console.ReadLine();
        Araç araç = araçlar.Find(a => a.Plaka == plaka);

        if (araç != null && araç.Durum == AraçDurumu.Boşta)
        {
            Console.Write("Kiralama süresi (gün) girin: ");
            int gün = Convert.ToInt32(Console.ReadLine());
            double toplamTutar = araç.Fiyat * gün;
            double indirimliTutar = toplamTutar - (toplamTutar * globalIndirim / 100);
            araç.Durum = AraçDurumu.Kiralanmış;
            Console.WriteLine($"Araç kiralandı. Toplam tutar: {indirimliTutar}");
        }
        else if (araç == null)
        {
            Console.WriteLine("Araç bulunamadı.");
        }
        else
        {
            Console.WriteLine("Araç zaten kiralandı.");
        }
    }
}