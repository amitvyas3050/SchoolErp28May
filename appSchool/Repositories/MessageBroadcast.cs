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
    
    public partial class MessageBroadcast
    {
        public int MessageID { get; set; }
        public int TeacherID { get; set; }
        public int StudentID { get; set; }
        public int SessionID { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public Nullable<System.DateTime> Fromdate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<bool> ReadFlag { get; set; }
        public Nullable<System.DateTime> SendDate { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }
    }
}