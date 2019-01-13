using EfSampleQueries.Manager;
using EfSampleQueries.Models;
using System;
using System.Collections.Generic;
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
        //AsNoTracking, AsQueryable
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
    }
}