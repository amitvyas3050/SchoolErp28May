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
    
    public partial class AccountMasterHistory
    {
        public int HistoryID { get; set; }
        public byte CompID { get; set; }
        public int AcCode { get; set; }
        public byte BranchId { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string NameH { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string Add3 { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string State { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Mobile1 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ContPerson { get; set; }
        public string Mobile2 { get; set; }
        public bool Ack { get; set; }
        public Nullable<short> CatCode { get; set; }
        public Nullable<decimal> OpBalance { get; set; }
        public Nullable<decimal> DrAmount { get; set; }
        public Nullable<decimal> CrAmount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> PageNo { get; set; }
        public Nullable<float> DelCom { get; set; }
        public Nullable<float> DepRate { get; set; }
        public Nullable<float> BiltyCharge { get; set; }
        public Nullable<float> HammaliWt { get; set; }
        public Nullable<float> HammaliPkg { get; set; }
        public Nullable<float> DDly { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> BillCode { get; set; }
        public string BillName { get; set; }
        public Nullable<int> CandFCode { get; set; }
        public string CAndFName { get; set; }
        public string TinNo { get; set; }
        public string PanNo { get; set; }
        public string TanNo { get; set; }
        public string STaxNo { get; set; }
        public Nullable<bool> STax { get; set; }
        public Nullable<bool> BillStop { get; set; }
        public Nullable<System.DateTime> BillStopDate { get; set; }
        public Nullable<float> BalanceLimit { get; set; }
        public string BalanceLimitType { get; set; }
        public Nullable<short> UserCode { get; set; }
        public Nullable<byte> UIdAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIdMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public Nullable<byte> UIdDel { get; set; }
        public Nullable<System.DateTime> DelDate { get; set; }
        public Nullable<bool> uploadflag { get; set; }
        public Nullable<decimal> TDSPer { get; set; }
        public Nullable<int> CityId { get; set; }
        public string DeliveryType { get; set; }
        public Nullable<byte> ChangedBy { get; set; }
        public System.DateTime ChangedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
