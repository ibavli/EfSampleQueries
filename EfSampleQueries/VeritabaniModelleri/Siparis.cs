using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfSampleQueries.VeritabaniModelleri
{
    public class Siparis
    {
        public Siparis()
        {
            SiparisUrunleri = new List<SiparisUrunleri>();
        }
        public int id { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public int MusteriId { get; set; }
        public virtual Musteri Musteri { get; set; }
        public virtual ICollection<SiparisUrunleri> SiparisUrunleri { get; set; }
    }
}