using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfSampleQueries.VeritabaniModelleri
{
    public class Urun
    {
        public Urun()
        {
            SiparisUrunleri = new List<SiparisUrunleri>();
        }
        public int id { get; set; }
        public string UrunIsmi { get; set; }
        public string UrunAciklama { get; set; }
        public bool YayindaMi { get; set; }
        public decimal Fiyat { get; set; }
        public string Marka { get; set; }
        public int? KategoriId { get; set; }
        public virtual Kategori Kategori { get; set; }
        public int Miktar { get; set; }
        //public int SiparisUrunleriId { get; set; }
        //public virtual SiparisUrunleri SiparisUrunleri { get; set; }
        public ICollection<SiparisUrunleri> SiparisUrunleri { get; set; }
    }
}