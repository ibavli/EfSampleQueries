using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfSampleQueries.Models
{
    public class OfTypeModel
    {
        public IEnumerable OrjinalListe { get; set; }

        public IEnumerable<string> StringListe { get; set; }

        public IEnumerable<int> IntListe { get; set; }
    }
}