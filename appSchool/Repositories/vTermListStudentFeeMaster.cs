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
    
    public partial class vTermListStudentFeeMaster
    {
        public int FeeTermID { get; set; }
        public string FeeTermName { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<int> SessionID { get; set; }
        public Nullable<int> StudentClassId { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }
}
