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
    
    public partial class ExamAnswerSubmitMaster
    {
        public int AnswerSubmitID { get; set; }
        public Nullable<int> QuestionID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public Nullable<int> ExamID { get; set; }
        public Nullable<int> SubExamID { get; set; }
        public Nullable<int> SessionID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> SubjectID1 { get; set; }
        public Nullable<int> SubjectID2 { get; set; }
        public Nullable<int> SubjectID3 { get; set; }
        public Nullable<int> AnswerObjSubmit { get; set; }
        public string AnswerDscSubmit { get; set; }
        public string AnswerImageDescription { get; set; }
        public string AnswerImage { get; set; }
        public Nullable<int> MarksObtain { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public Nullable<int> UIDAdd { get; set; }
        public Nullable<int> UIDMod { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> CompID { get; set; }
        public Nullable<byte> BranchID { get; set; }
    }
}
