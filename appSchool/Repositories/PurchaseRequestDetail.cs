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
    
    public partial class PurchaseRequestDetail
    {
        public int ID { get; set; }
        public int RequestID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> AprovedQty { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }
        public byte SessionID { get; set; }
    }
}
