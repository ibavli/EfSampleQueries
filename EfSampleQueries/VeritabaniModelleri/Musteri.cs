using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfSampleQueries.VeritabaniModelleri
{
    public class Musteri
    {
        public Musteri()
        {
            Siparisleri = new List<Siparis>();
        }
        public int id { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Sehir { get; set; }
        public string Ulke { get; set; }
        public DateTime DogumTarihi { get; set; }

        public string AdveSoyad { get; set; }

        public ICollection<Siparis> Siparisleri { get; set; }
    }
}