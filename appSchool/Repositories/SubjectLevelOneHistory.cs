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
    
    public partial class SubjectLevelOneHistory
    {
        public int HistoryIDL1 { get; set; }
        public int IdL1 { get; set; }
        public string SubjectCodeL1 { get; set; }
        public string SubjectNameL1 { get; set; }
        public Nullable<int> Order1 { get; set; }
        public Nullable<byte> UIDAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIDMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public Nullable<byte> ChangedBy { get; set; }
        public Nullable<System.DateTime> ChangedDate { get; set; }
        public bool IsDeleted { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }
}
