//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bootcamp32Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_M_Item
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int stock { get; set; }
        public Nullable<int> Supplier_Id { get; set; }
    
        public virtual TB_M_Supplier TB_M_Supplier { get; set; }
    }
}