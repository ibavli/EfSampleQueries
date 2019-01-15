using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfSampleQueries.Models
{
    public class UyeModel
    {
        public string isim { get; set; }
        public string soyisim { get; set; }
        public DateTime dogumTarihi { get; set; }
        public int? yas { get; set; }
    }
}