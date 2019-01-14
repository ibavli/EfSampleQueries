using EfSampleQueries.VeritabaniModelleri;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EfSampleQueries.Manager
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Urun> Urun { get; set; }
        public DbSet<Musteri> Musteri { get; set; }
        public DbSet<Siparis> Siparis { get; set; }
        public DbSet<SiparisUrunleri> SiparisUrunleri { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//Çoğul -s takısı gelmemesi için.

            modelBuilder.Entity<Musteri>().HasKey(m => m.id);
            modelBuilder.Entity<Kategori>().HasKey(k => k.id);
            modelBuilder.Entity<Urun>().HasKey(u => u.id);
            modelBuilder.Entity<Siparis>().HasKey(s => s.id);
            modelBuilder.Entity<SiparisUrunleri>().HasKey(s => s.id);


            Database.SetInitializer(new VeritabaniOlusurkenTablolaraBaslangicKayitlariEkleme());

            //ZORUNLU DEMİŞTİK AMA NULLABLE YAPTIK DAHA SONRA BU HALE SOKTUK.
            //modelBuilder.Entity<Urun>().HasRequired<Kategori>(u => u.Kategori).WithMany(k => k.Urunler).HasForeignKey(u => u.KategoriId).WillCascadeOnDelete();
            modelBuilder.Entity<Urun>().HasOptional<Kategori>(u => u.Kategori).WithMany(k => k.Urunler).HasForeignKey(u => u.KategoriId).WillCascadeOnDelete();

            modelBuilder.Entity<SiparisUrunleri>().HasRequired<Siparis>(su => su.Siparis).WithMany(s => s.SiparisUrunleri).HasForeignKey(su => su.SiparisId).WillCascadeOnDelete();
            modelBuilder.Entity<SiparisUrunleri>().HasRequired<Urun>(su => su.Urun).WithMany(s => s.SiparisUrunleri).HasForeignKey(su => su.SiparisId).WillCascadeOnDelete();
            modelBuilder.Entity<Siparis>().HasRequired<Musteri>(s => s.Musteri).WithMany(m => m.Siparisleri).HasForeignKey(s => s.MusteriId).WillCascadeOnDelete();
        }
    }


    //Web.Config'e bu class'ı ekle !!
    public class VeritabaniOlusurkenTablolaraBaslangicKayitlariEkleme : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Kategori kategori1 = new Kategori()
            {
                id = 1,
                KategoriAciklama = "Bilgisayar Kategorisi",
                KategoriIsmi = "Bilgisayar",
                OlusturulmaTarihi = DateTime.Now,
                YayinDurumu = true
            };
            Kategori kategori2 = new Kategori()
            {
                id = 2,
                KategoriAciklama = "Telefon Kategorisi",
                KategoriIsmi = "Telefon",
                OlusturulmaTarihi = DateTime.Now,
                YayinDurumu = true
            };
            Kategori kategori3 = new Kategori()
            {
                id = 3,
                KategoriAciklama = "Monitör Kategorisi",
                KategoriIsmi = "Monitör",
                OlusturulmaTarihi = DateTime.Now,
                YayinDurumu = true
            };
            context.Kategori.Add(kategori1);
            context.Kategori.Add(kategori2);
            context.Kategori.Add(kategori3);
            context.SaveChanges();

            var kat1 = context.Kategori.Where(k => k.id == 1).FirstOrDefault();
            var kat2 = context.Kategori.Where(k => k.id == 2).FirstOrDefault();
            var kat3 = context.Kategori.Where(k => k.id == 3).FirstOrDefault();
            Urun urun = new Urun()
            {
                id = 1,
                Fiyat = 5450,
                Marka = "Asus",
                UrunAciklama = "Asus laptop açıklama",
                UrunIsmi = "Asus n553wt",
                YayindaMi = true,
                Kategori = kat1,
                Miktar = 47
            };
            Urun urun2 = new Urun()
            {
                id = 2,
                Fiyat = 4750,
                Marka = "Lenovo",
                UrunAciklama = "Lenovo laptop açıklama",
                UrunIsmi = "Lenovo L23Vm",
                YayindaMi = true,
                Kategori = kat1,
                Miktar = 33
            };
            Urun urun3 = new Urun()
            {
                id = 3,
                Fiyat = 3750,
                Marka = "Acer",
                UrunAciklama = "Acer laptop açıklama",
                UrunIsmi = "Acer ac781",
                YayindaMi = true,
                Kategori = kat1,
                Miktar = 12
            };
            Urun urun4 = new Urun()
            {
                id = 4,
                Fiyat = 6950,
                Marka = "Apple",
                UrunAciklama = "Apple laptop açıklama",
                UrunIsmi = "Apple ap123",
                YayindaMi = true,
                Kategori = kat1,
                Miktar = 25
            };

            context.Urun.Add(urun);
            context.Urun.Add(urun2);
            context.Urun.Add(urun3);
            context.Urun.Add(urun4);
            context.SaveChanges();

            Urun urun5 = new Urun()
            {
                id = 5,
                Fiyat = 1250,
                Marka = "Apple",
                UrunAciklama = "Apple telefon açıklama",
                UrunIsmi = "IPhone 5S",
                YayindaMi = true,
                Kategori = kat2,
                Miktar = 7
            };
            Urun urun6 = new Urun()
            {
                id = 6,
                Fiyat = 2250,
                Marka = "Apple",
                UrunAciklama = "Apple telefon açıklama",
                UrunIsmi = "IPhone 6S",
                YayindaMi = true,
                Kategori = kat2,
                Miktar = 11
            };
            Urun urun7 = new Urun()
            {
                id = 7,
                Fiyat = 3250,
                Marka = "Apple",
                UrunAciklama = "Apple telefon açıklama",
                UrunIsmi = "IPhone 7S",
                YayindaMi = true,
                Kategori = kat2,
                Miktar = 89
            };
            Urun urun8 = new Urun()
            {
                id = 8,
                Fiyat = 4250,
                Marka = "Apple",
                UrunAciklama = "Apple telefon açıklama",
                UrunIsmi = "IPhone 8S",
                YayindaMi = false,
                Kategori = kat2,
                Miktar = 55
            };

            Urun urun9 = new Urun()
            {
                id = 9,
                Fiyat = 1250,
                Marka = "Nokia",
                UrunAciklama = "Nokia 3310 telefon açıklama",
                UrunIsmi = "Nokia 3310",
                YayindaMi = false,
                Miktar = 17
            };

            context.Urun.Add(urun5);
            context.Urun.Add(urun6);
            context.Urun.Add(urun7);
            context.Urun.Add(urun8);
            context.Urun.Add(urun9);
            context.SaveChanges();

            Musteri musteri1 = new Musteri()
            {
                id = 1,
                Adi = "Ali",
                Soyadi = "Veli",
                Sehir = "İzmir",
                Ulke = "Türkiye",
                Yas = 20
            };
            Musteri musteri2 = new Musteri()
            {
                id = 2,
                Adi = "Hasan",
                Soyadi = "Hüseyin",
                Sehir = "İzmir",
                Ulke = "Türkiye",
                Yas = 22
            };
            Musteri musteri3 = new Musteri()
            {
                id = 3,
                Adi = "Ayşe",
                Soyadi = "Fatma",
                Sehir = "İstanbul",
                Ulke = "Türkiye",
                Yas = 34
            };
            Musteri musteri4 = new Musteri()
            {
                id = 4,
                Adi = "Hatice",
                Soyadi = "Fadime",
                Sehir = "Antalya",
                Ulke = "Türkiye",
                Yas = 27
            };
            context.Musteri.Add(musteri1);
            context.Musteri.Add(musteri2);
            context.Musteri.Add(musteri3);
            context.Musteri.Add(musteri4);
            context.SaveChanges();

            var mus1 = context.Musteri.Where(m => m.id == 1).FirstOrDefault();
            var mus2 = context.Musteri.Where(m => m.id == 2).FirstOrDefault();
            var mus3 = context.Musteri.Where(m => m.id == 3).FirstOrDefault();
            var ur1 = context.Urun.Where(u => u.id == 1).FirstOrDefault();
            var ur2 = context.Urun.Where(u => u.id == 2).FirstOrDefault();
            var ur3 = context.Urun.Where(u => u.id == 3).FirstOrDefault();
            var ur4 = context.Urun.Where(u => u.id == 4).FirstOrDefault();
            var ur5 = context.Urun.Where(u => u.id == 5).FirstOrDefault();
            var ur6 = context.Urun.Where(u => u.id == 6).FirstOrDefault();
            var ur7 = context.Urun.Where(u => u.id == 7).FirstOrDefault();
            var ur8 = context.Urun.Where(u => u.id == 8).FirstOrDefault();

            Siparis siparis1 = new Siparis()
            {
                id = 1,
                Musteri = mus1,
                SiparisTarihi = DateTime.Now
            };
            context.Siparis.Add(siparis1);
            context.SaveChanges();
            var sip1 = context.Siparis.Where(s => s.id == 1).FirstOrDefault();
            SiparisUrunleri siparisUrunleri1 = new SiparisUrunleri()
            {
                id = 1,
                UrunId = ur1.id,
                Siparis = sip1
            };
            SiparisUrunleri siparisUrunleri2 = new SiparisUrunleri()
            {
                id = 2,
                UrunId = ur2.id,
                Siparis = sip1
            };
            List<SiparisUrunleri> siparisUrunleriList1 = new List<SiparisUrunleri>();
            siparisUrunleriList1.Add(siparisUrunleri1);
            siparisUrunleriList1.Add(siparisUrunleri2);
            sip1.SiparisUrunleri = siparisUrunleriList1;
            context.SiparisUrunleri.Add(siparisUrunleri1);
            context.SiparisUrunleri.Add(siparisUrunleri2);
            context.SaveChanges();

            Siparis siparis2 = new Siparis()
            {
                id = 2,
                Musteri = mus1,
                SiparisTarihi = DateTime.Now
            };
            context.Siparis.Add(siparis2);
            context.SaveChanges();
            var sip2 = context.Siparis.Where(s => s.id == 2).FirstOrDefault();
            SiparisUrunleri siparisUrunleri3 = new SiparisUrunleri()
            {
                id = 3,
                UrunId = ur3.id,
                Siparis = sip2
            };
            SiparisUrunleri siparisUrunleri4 = new SiparisUrunleri()
            {
                id = 4,
                UrunId = ur4.id,
                Siparis = sip2
            };
            List<SiparisUrunleri> siparisUrunleriList2 = new List<SiparisUrunleri>();
            siparisUrunleriList2.Add(siparisUrunleri3);
            siparisUrunleriList2.Add(siparisUrunleri4);
            sip2.SiparisUrunleri = siparisUrunleriList2;
            context.SiparisUrunleri.Add(siparisUrunleri3);
            context.SiparisUrunleri.Add(siparisUrunleri4);
            context.SaveChanges();


            Siparis siparis3 = new Siparis()
            {
                id = 3,
                Musteri = mus2,
                SiparisTarihi = DateTime.Now
            };
            context.Siparis.Add(siparis3);
            context.SaveChanges();
            var sip3 = context.Siparis.Where(s => s.id == 3).FirstOrDefault();
            SiparisUrunleri siparisUrunleri5 = new SiparisUrunleri()
            {
                id = 5,
                UrunId = ur5.id,
                Siparis = sip3
            };
            List<SiparisUrunleri> siparisUrunleriList3 = new List<SiparisUrunleri>();
            siparisUrunleriList3.Add(siparisUrunleri5);
            sip3.SiparisUrunleri = siparisUrunleriList3;
            context.SiparisUrunleri.Add(siparisUrunleri5);
            context.SaveChanges();

            Siparis siparis4 = new Siparis()
            {
                id = 4,
                MusteriId = musteri3.id,
                SiparisTarihi = DateTime.Now
            };
            context.Siparis.Add(siparis4);
            context.SaveChanges();
            var sip4 = context.Siparis.Where(s => s.id == 4).FirstOrDefault();
            SiparisUrunleri siparisUrunleri6 = new SiparisUrunleri()
            {
                id = 6,
                UrunId = ur6.id,
                SiparisId = siparis4.id
            };
            List<SiparisUrunleri> siparisUrunleriList4 = new List<SiparisUrunleri>();
            siparisUrunleriList4.Add(siparisUrunleri6);
            sip4.SiparisUrunleri = siparisUrunleriList4;
            context.SiparisUrunleri.Add(siparisUrunleri6);
            context.SaveChanges();

        }
    }
}