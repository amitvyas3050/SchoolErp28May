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
    
    public partial class TeacherSyllabusLog
    {
        public int ID { get; set; }
        public int ClassID { get; set; }
        public int IdL1 { get; set; }
        public string Unit { get; set; }
        public string Syllabus { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CompleteDate { get; set; }
        public int TeacherID { get; set; }
        public byte UIDAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIDMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }
}
