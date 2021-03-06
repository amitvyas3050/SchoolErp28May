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
    
    public partial class InsuranceDetailHistory
    {
        public int HistoryID { get; set; }
        public int InsuranceID { get; set; }
        public int VehicleID { get; set; }
        public int SrNo { get; set; }
        public string InsuranceCertificateNo { get; set; }
        public Nullable<System.DateTime> InsuranceDate { get; set; }
        public string PolicyNo { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public string InsuranceCompany { get; set; }
        public string InsuranceBranch { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNo { get; set; }
        public Nullable<byte> BranchID { get; set; }
        public Nullable<byte> CompID { get; set; }
        public Nullable<byte> ChangedBy { get; set; }
        public System.DateTime ChangedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<byte> SessionID { get; set; }
        public Nullable<int> UIdAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<int> UIdMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
    }
}
