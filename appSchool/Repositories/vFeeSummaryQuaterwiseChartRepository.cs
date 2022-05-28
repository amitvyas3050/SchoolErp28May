using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;


namespace appSchool.Repositories
{
    public class vFeeSummaryQuaterwiseChartRepository : GenericRepository<vFeeSummaryQuarterwise> 
    {
        public vFeeSummaryQuaterwiseChartRepository() : base(new dbSchoolAppEntities()) { }
        public vFeeSummaryQuaterwiseChartRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<vFeeSummaryQuarterwise> GetFeeSummaryQuarterwiseForChart(int mSessionID,byte mCompID ,byte mBranchID)
        {
            List<vFeeSummaryQuarterwise> objfeeSummaryChart = new List<vFeeSummaryQuarterwise>();
            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),

                            };
            objfeeSummaryChart = this.context.Database.SqlQuery<vFeeSummaryQuarterwise>(
                                     "GetFeeSummaryQuarterwiseForChart @SessionID, @CompID, @BranchID ",
                                      param
                             ).ToList();

            return objfeeSummaryChart;
        }









       
    }

  
}
