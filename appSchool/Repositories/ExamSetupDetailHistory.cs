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
    
    public partial class ExamSetupDetailHistory
    {
        public int HistoryID { get; set; }
        public int ExamSetupDetailID { get; set; }
        public int ExamSetupID { get; set; }
        public int SubjectIDL1 { get; set; }
        public int SubjectIDL2 { get; set; }
        public int SubjectIDL3 { get; set; }
        public Nullable<System.DateTime> ExamDate { get; set; }
        public string ExamTime { get; set; }
        public int OrderNo { get; set; }
        public int MinMark { get; set; }
        public int MaxMark { get; set; }
        public Nullable<byte> ChangedBy { get; set; }
        public System.DateTime ChangedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> SessionId { get; set; }
        public string MarksType { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }
}
