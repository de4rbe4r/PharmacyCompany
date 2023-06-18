using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyCompany.Models
{
    public class Batch
    {
        public int Id;
        public int ProductId;
        public int StorageId;
        public int Quantity;
        public Storage Storage;
        public Product Product;
    }
}
