using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;



namespace appSchool.Repositories
{
    public class vFeesStructureDataExportRepository: GenericRepository<vFeesStructureDataExport>
    {
        public vFeesStructureDataExportRepository() : base(new dbSchoolAppEntities()) { }
        public vFeesStructureDataExportRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }





        public IEnumerable<vFeesStructureDataExport> GetFeesStructureDataExportBySessionWise( int mSessionID, byte mCompID, byte mBranchID)
        {

            List<vFeesStructureDataExport> objFeesStructureDataExport = new List<vFeesStructureDataExport>();


            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),

                            };

            objFeesStructureDataExport = this.context.Database.SqlQuery<vFeesStructureDataExport>(
                                     "GetFeesStructureDataExport @SessionID, @CompID, @BranchID",
                                      param
                             ).ToList();

            return objFeesStructureDataExport;
        }


        
    }


     




}