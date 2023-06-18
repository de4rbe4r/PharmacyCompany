using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCompany.Models
{
    public class Storage
    {
        public int Id;
        public string Title;
        public int PharmacyId;
        public Pharmacy Pharmacy;
    }
}
