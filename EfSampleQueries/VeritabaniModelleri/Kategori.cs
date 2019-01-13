using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfSampleQueries.VeritabaniModelleri
{
    public class Kategori
    {
        public Kategori()
        {
            Urunler = new List<Urun>();
        }
        public int id { get; set; }
        public string KategoriIsmi { get; set; }
        public string KategoriAciklama { get; set; }
        public bool YayinDurumu { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }
        public virtual ICollection<Urun> Urunler { get; set; }
    }
}