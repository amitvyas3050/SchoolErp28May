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
    
    public partial class FeesVoucherDetail
    {
        public int VocherDetailID { get; set; }
        public Nullable<int> FeesVoucherNo { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> FeesHeadID { get; set; }
        public string Narration { get; set; }
        public Nullable<byte> CompID { get; set; }
        public Nullable<byte> BranchID { get; set; }
        public Nullable<int> SrNo { get; set; }
        public Nullable<int> SessionID { get; set; }
    }
}