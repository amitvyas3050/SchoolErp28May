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
    
    public partial class PermitDetail
    {
        public int PermitId { get; set; }
        public int VehicleID { get; set; }
        public int SrNo { get; set; }
        public string PermitDocNo { get; set; }
        public Nullable<System.DateTime> PermitDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string PermitType { get; set; }
        public string State1 { get; set; }
        public string State2 { get; set; }
        public string State3 { get; set; }
        public Nullable<byte> BranchID { get; set; }
        public Nullable<byte> CompID { get; set; }
        public Nullable<byte> SessionID { get; set; }
        public Nullable<int> UIdAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<int> UIdMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
    }
}
