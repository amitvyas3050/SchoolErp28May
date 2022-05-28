using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;


namespace appSchool.Repositories
{
    public class vStudentStrengthChartRepository : GenericRepository<vStudentStrengthChart> 
    {
        public vStudentStrengthChartRepository() : base(new dbSchoolAppEntities()) { }
        public vStudentStrengthChartRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<vStudentStrengthChart> GetStudentStrengthClasswise(int mSessionID,byte mCompID, byte mBranchID )
        {
            List<vStudentStrengthChart> objStudentStrengthChart = new List<vStudentStrengthChart>();
            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),

                            };
            objStudentStrengthChart = this.context.Database.SqlQuery<vStudentStrengthChart>(
                                     "GetStudentStrengthClasswise @SessionID,@CompID,@BranchID",
                                      param
                             ).ToList();

            return objStudentStrengthChart;
        }









       
    }

  
}
