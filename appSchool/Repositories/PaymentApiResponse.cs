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
    
    public partial class PaymentApiResponse
    {
        public long ResponseID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public string TxnID { get; set; }
        public Nullable<System.DateTime> Txndate { get; set; }
        public string TxnStatus { get; set; }
        public string SaltKey { get; set; }
        public string SaltMerchant { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remark { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Myhpayid { get; set; }
        public string UDF5 { get; set; }
        public string HashValue { get; set; }
    }
}
