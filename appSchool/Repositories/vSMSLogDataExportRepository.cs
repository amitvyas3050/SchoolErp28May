 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace appSchool.Repositories
{
    public class vSMSLogDataExportRepository : GenericRepository<vSMSLogDataExport> 
    {
        public vSMSLogDataExportRepository() : base(new dbSchoolAppEntities()) { }
        public vSMSLogDataExportRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public IEnumerable<vSMSLogDataExport> GetSMSLogDatewise(DateTime mFromDate, DateTime mToDate, int mSessionID, byte mCompID, byte mBranchID)
        {
            List<vSMSLogDataExport> objSMSDataExport = new List<vSMSLogDataExport>();

            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@FromDate", mFromDate),
                           new SqlParameter("@ToDate", mToDate.AddDays(1)),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),
                            };

            objSMSDataExport = this.context.Database.SqlQuery<vSMSLogDataExport>(
                                     "GetSMSLogListForDataExport @SessionID,@FromDate,@ToDate,@CompID,@BranchID ",
                                      param
                             ).ToList();
            
            return objSMSDataExport;
        }

    }

  
}
