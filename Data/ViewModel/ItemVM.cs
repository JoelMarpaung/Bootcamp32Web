using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ViewModel
{
    public class ItemVM
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int stock { get; set; }
        public int Supplier_Id { get; set; }
    }
}
