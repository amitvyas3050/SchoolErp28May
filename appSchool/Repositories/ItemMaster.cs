//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace appSchool.Repositories
{
    using System;
    using System.Collections.Generic;
    
    public partial class ItemMaster
    {
        public int ItemID { get; set; }
        public Nullable<int> ItemGroupID { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public Nullable<decimal> OpeningQty { get; set; }
        public Nullable<decimal> PurchaseQty { get; set; }
        public Nullable<decimal> DamageQty { get; set; }
        public Nullable<decimal> AllotQty { get; set; }
        public Nullable<decimal> ReturnQty { get; set; }
        public string Remark { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }
        public Nullable<byte> UIdAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIdMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
    }
}
