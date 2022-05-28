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
    
    public partial class TeacherHistory
    {
        public int ID { get; set; }
        public int TeacherID { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<System.DateTime> DOJ { get; set; }
        public Nullable<bool> IsClassTeacher { get; set; }
        public Nullable<byte> Age { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string MaritalStatus { get; set; }
        public string EmailID { get; set; }
        public string LocalAddress { get; set; }
        public string ParmanentAddress { get; set; }
        public Nullable<bool> Transport { get; set; }
        public Nullable<bool> Hostel { get; set; }
        public string BloodGroup { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string HusbandWifeName { get; set; }
        public Nullable<System.DateTime> AnniversaryDate { get; set; }
        public Nullable<int> DesignationId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<byte> UIDAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIDMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public byte[] AppImage { get; set; }
        public string AppImageName { get; set; }
        public int SessionID { get; set; }
        public Nullable<byte> ChangedBy { get; set; }
        public Nullable<System.DateTime> ChangedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string Color { get; set; }
        public byte CompID { get; set; }
        public byte BranchId { get; set; }
    }
}