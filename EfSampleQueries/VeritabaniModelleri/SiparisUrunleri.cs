using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfSampleQueries.VeritabaniModelleri
{
    public class SiparisUrunleri
    {
        public int id { get; set; }
        public int SiparisId { get; set; }
        public virtual Siparis Siparis { get; set; }
        public int UrunId { get; set; }
        public virtual Urun Urun { get; set; }
    }
}