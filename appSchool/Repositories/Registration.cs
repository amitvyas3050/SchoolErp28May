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
    
    public partial class Registration
    {
        public int RegistrationID { get; set; }
        public string RegNo { get; set; }
        public Nullable<System.DateTime> RegDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string ResidenceNo { get; set; }
        public string BloodGroup { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }
}
