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
    
    public partial class vStudentFeesStructDataExport
    {
        public int StudmasterID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public bool PaidFlag { get; set; }
        public string ClassName { get; set; }
        public string FeeTermType { get; set; }
        public string FeeTermName { get; set; }
        public System.DateTime FeeTermFromDate { get; set; }
        public System.DateTime FeeTermToDate { get; set; }
        public Nullable<decimal> FineAmount { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string EnrollmentNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string SMSMobileNo { get; set; }
        public string FullName { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }
}
