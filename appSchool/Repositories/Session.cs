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
    
    public partial class Session
    {
        public byte SessionId { get; set; }
        public string SessionName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<byte> UIDAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIDMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public Nullable<bool> MkRd { get; set; }
        public Nullable<decimal> FormRate { get; set; }
        public bool IscurrentYear { get; set; }
    }
}
