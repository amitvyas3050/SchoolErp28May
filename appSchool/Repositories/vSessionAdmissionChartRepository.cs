using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;


namespace appSchool.Repositories
{
    public class vSessionAdmissionChartRepository : GenericRepository<vSessionwiseAdmission> 
    {
        public vSessionAdmissionChartRepository() : base(new dbSchoolAppEntities()) { }
        public vSessionAdmissionChartRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }





        public List<vSessionwiseAdmission> GetSessionwiseAdmissionStrength(int mSessionID, byte mCompID, byte mBranchID)
        {
            List<vSessionwiseAdmission> objSessionwiseAdmission = new List<vSessionwiseAdmission>();
            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),

                            };
            objSessionwiseAdmission = this.context.Database.SqlQuery<vSessionwiseAdmission>(
                                     "GetSessionwiseAdmission @SessionID, @CompID, @BranchID",
                                      param
                             ).ToList();

            return objSessionwiseAdmission;
        }








       
    }

  
}
