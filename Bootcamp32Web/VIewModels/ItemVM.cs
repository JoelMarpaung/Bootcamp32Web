using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Bootcamp32Web.Models;
using Newtonsoft.Json;

namespace Bootcamp32Web.VIewModels
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