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
    
    public partial class vStudentTopperList
    {
        public int TNoticeID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public string FullName { get; set; }
        public Nullable<byte> ClassID { get; set; }
        public string Session { get; set; }
        public string ClassName { get; set; }
        public string Gender { get; set; }
        public string Remark { get; set; }
        public string RankPoint { get; set; }
        public string Percentage { get; set; }
        public Nullable<bool> IsTopper { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }
        public string FatherName { get; set; }
        public string AppImageName { get; set; }
    }
}