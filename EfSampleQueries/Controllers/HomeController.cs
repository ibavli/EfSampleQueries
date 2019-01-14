using EfSampleQueries.Manager;
using EfSampleQueries.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EfSampleQueries.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            var kategoriler = db.Kategori.ToList();
            var musteriler = db.Musteri.ToList();
            var urunler = db.Urun.ToList();
            var siparisler = db.Siparis.ToList();
            var siparisUrunleri = db.SiparisUrunleri.ToList();
            return View();
        }
        //AsNoTracking, AsQueryable, Include, Join
        public ActionResult AllMetodu()
        {
            var sonuc = db.Urun.All(u => u.YayindaMi == false);
            TempData["AllSonucu"] = sonuc.ToString();
            return View();
        }

        public ActionResult AnyMetodu()
        {
            var sonuc = db.Urun.Any(u => u.YayindaMi == false);
            TempData["AnySonucu"] = sonuc.ToString();
            return View();
        }

        public ActionResult ConcatMetodu()
        {
            var birlestirilmisUrunler = db.Urun.Where(u => u.Fiyat > 5000).ToList().Concat(db.Urun.Where(u => u.Fiyat < 2000).ToList());
            return View(birlestirilmisUrunler);
        }

        public ActionResult ContainsMetodu()
        {
            var appleIcerenUrunler = db.Urun.Where(u => u.UrunAciklama.Contains("apple")).ToList();
            return View(appleIcerenUrunler);
        }

        public ActionResult DistinctMetodu()
        {
            var distinctUrunler = db.Urun.Select(x => x.Marka).Distinct().ToList();

            return View(distinctUrunler);
        }

        public ActionResult ElementAtOrDefaultMetodu()
        {
            var tumUrunler = db.Urun.ToList();
            var sifirdanBaslayarakIkinciUrun2 = tumUrunler.ElementAtOrDefault(2);

            return View(sifirdanBaslayarakIkinciUrun2);
        }

        public ActionResult ExceptMetodu()
        {
            int[] onaKadarSayilar = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] tekSayilar = { 1, 3, 5, 7, 9 };

            var result = onaKadarSayilar.Except(tekSayilar);//tek sayıları dışlayacak geriye kalan değerleri alacak.

            return View(result);
        }

        public ActionResult FindMetodu()
        {
            var sonuc = db.Urun.Find(2);

            return View(sonuc);
        }



        public ActionResult GroupByMetodu()
        {
            //Join
            var HangiKategorideKacUrunVar = db.Urun.GroupBy(u => u.Kategori.KategoriIsmi).Select(g => new GroupByModel
            {
                KategoriAdi = g.Key,
                ToplamStok = g.Sum(u => u.Miktar)
            }).ToList();

            return View(HangiKategorideKacUrunVar);
        }

        public ActionResult GroupJoinMetodu()
        {
            //left join
            var urunler = db.Urun.ToList();
            var kategoriler = db.Kategori.ToList();

            var GorupJoinSonuc = kategoriler.GroupJoin(urunler,
                kat => kat.id,
                urun => urun.KategoriId,
                (_kat, _urun) => new GroupByModel
                {
                    KategoriAdi = _kat.KategoriIsmi,
                    ToplamStok = _urun.Sum(x => x.Miktar)
                });
            
            return View(GorupJoinSonuc);
        }

        public ActionResult LastOrDefaultMetodu()
        {
            var sonurun = db.Urun.ToList().LastOrDefault();
            return View(sonurun);
        }


        public ActionResult RemoveRangeMetodu()
        {
            List<int> liste = new List<int>();
            liste.Add(1);
            liste.Add(2);
            liste.Add(3);
            liste.Add(4);
            liste.Add(5);
            liste.Add(6);

            liste.RemoveRange(1, 3);

            foreach (int i in liste)
            {
                var x = i;
            }

            return View(liste);
        }


        public ActionResult OfTypeMetodu()
        {
            IList liste = new ArrayList();
            liste.Add(0);
            liste.Add("Bir");
            liste.Add("İki");
            liste.Add(3);
           
            var stringList = from L in liste.OfType<string>() select L;

            var intList = from L in liste.OfType<int>() select L;

            OfTypeModel ofTypeModel = new OfTypeModel();

            ofTypeModel.OrjinalListe = liste;
            ofTypeModel.StringListe = stringList;
            ofTypeModel.IntListe = intList;


            return View(ofTypeModel);
        }

        public ActionResult SelectMetodu()
        {
            var kategoriler = db.Kategori.ToList();

            var kategoriIsimleri = db.Kategori.Select(a => a.KategoriIsmi).ToList();
            return View(kategoriIsimleri);
        }


       
        public ActionResult SelectManyMetodu()
        {
            var kategoriSelectMany = db.Kategori.SelectMany(x => x.Urunler).ToList();
            var kategoriSelect = db.Kategori.Select(x => x.Urunler).ToList();
            //Aradaki fark debug yapılarak incelenirse daha iyi olur.
            
            return View(kategoriSelectMany);
        }

        public ActionResult SkipMetodu()
        {
            List<int> liste = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var yeniListe = liste.Skip(4);
            return View(yeniListe);
        }

        public ActionResult SqlQueryMetod()
        {
            var sorguSonucu = db.Urun.SqlQuery("Select * from Urun").ToList();

            return View(sorguSonucu);
        }

        public ActionResult SqlQueryMetodParametreIle()
        {
            var sorguSonucu = db.Urun.SqlQuery("select * from Urun where Marka = @marka and KategoriId = @katid", new SqlParameter("@marka", "Apple"), new SqlParameter("@katid","2")).ToList();
            
            return View(sorguSonucu);
        }

        public ActionResult SqlQueryMetodCustomModel()
        {
            //Tablodan dönün isimlerin modeldeki ile aynı olmasına dikkat edilmeli.
            var sorguSonucu = db.Database.SqlQuery<GroupByModel>("select k.KategoriIsmi as 'KategoriAdi', sum(u.Miktar) as 'ToplamStok' from Kategori as k join Urun as u on k.id = u.KategoriId group by k.KategoriIsmi").ToList<GroupByModel>();
            return View(sorguSonucu);
        }


        
        public ActionResult ThenByMetodu()
        {
            IList<Uye> uyeListesi = new List<Uye>() {
                 new Uye() { isim = "ali", yas = 18 } ,
                 new Uye() { isim = "ali",  yas = 15 } ,
                 new Uye() { isim = "ahmet",  yas = 25 } ,
                 new Uye() { isim = "atılay" , yas = 20 } ,
                 new Uye() { isim = "oktay" , yas = 19 },
                 new Uye() { isim = "ali" , yas = 10 },
                 new Uye() { isim = "adnan" , yas = 18 }
            };

            //var _uyeListesi = uyeListesi.OrderBy(i => i.isim).ToList(); //Sadece isme göre sıralama
            //var _uyeListesi = uyeListesi.OrderBy(i => i.isim).OrderBy(x => x.yas).ToList(); //HATALI ÇIKTI
            var _uyeListesi = uyeListesi.OrderBy(i => i.isim).ThenBy(y => y.yas).ToList();//İsimden sonra yaşlarıda sıralama

            return View(_uyeListesi);
        }
    }
}