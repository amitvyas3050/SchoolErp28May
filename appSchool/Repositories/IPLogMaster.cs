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
    
    public partial class IPLogMaster
    {
        public int ID { get; set; }
        public string IPAddress { get; set; }
        public string HostName { get; set; }
        public Nullable<System.DateTime> LoginDateTime { get; set; }
        public Nullable<System.DateTime> LogoutDateTime { get; set; }
        public string UserID { get; set; }
        public string PortalName { get; set; }
        public string Country { get; set; }
        public string SessionID { get; set; }
        public Nullable<byte> BranchID { get; set; }
        public Nullable<byte> CompID { get; set; }
    }
}
