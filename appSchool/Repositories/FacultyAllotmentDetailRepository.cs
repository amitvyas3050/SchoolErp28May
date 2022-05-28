using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;

namespace appSchool.Repositories
{
    public class FacultyAllotmentDetailRepository : GenericRepository<FacultyAllotmentDetail>
    {
        public FacultyAllotmentDetailRepository() : base(new dbSchoolAppEntities()) { }
        public FacultyAllotmentDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<FacultyAllotmentDetail> GetFacultyAllotmentDetailListByFacultyAllotmentID(int mFacultyAllotmentID, byte mCompID, byte MBranchID)
        {
            List<FacultyAllotmentDetail> obj = new List<FacultyAllotmentDetail>();
            obj = this.context.FacultyAllotmentDetails.Where(x => x.FacultyAllotmentID == mFacultyAllotmentID && x.CompID == mCompID && x.BranchID == MBranchID).ToList();
            return obj;
        }

        public void InsertFacultyAllotmentDetail(FacultyAllotmentDetail obj)
        {
            this.Insert(obj);
        }

        public void UpdateFacultyAllotmentDetail(FacultyAllotmentDetail obj)
        {
            FacultyAllotmentDetail objNew = this.GetByID(obj.ID);
            if (objNew != null)
            {
                objNew.SubjectIDL1 = obj.SubjectIDL1;
                objNew.SubjectIDL2 = obj.SubjectIDL2;
                objNew.SubjectIDL3 = obj.SubjectIDL3;

                this.Update(objNew);
            }

        }


        public List<vSubjectListByClassID> GetSubjectListByClassID(int mClassID, byte mCompID, byte mBranchID )
        {
            List<vSubjectListByClassID> obj = new List<vSubjectListByClassID>();

            obj = this.context.vSubjectListByClassIDs.Where(x => x.ClassID == mClassID).Distinct().OrderBy(x => x.Order1).ToList();

            return obj;
        }


        public List<SubjectLevelOne> GetSubjectListByClassIDDistinct(int mClassID, byte mCompID, byte mBranchID)
        {
            List<SubjectLevelOne> obj = new List<SubjectLevelOne>();

            //obj = this.context.vSubjectListByClassIDs.Where(x => x.ClassID == mClassID).Distinct().OrderBy(x => x.Order1).ToList();

            string sql = " SELECT DISTINCT  dbo.SubjectAllotment.IDL1, dbo.SubjectAllotment.ClassID, dbo.SubjectLevelOne.SubjectCodeL1, dbo.SubjectLevelOne.SubjectNameL1,  " +
                         " dbo.SubjectLevelOne.Order1 FROM   dbo.SubjectAllotment INNER JOIN   dbo.SubjectLevelOne ON dbo.SubjectAllotment.IDL1 = dbo.SubjectLevelOne.IdL1 " +
                         " WHERE  dbo.SubjectAllotment.ClassID=" + mClassID + " AND dbo.SubjectAllotment.BranchID=" + mBranchID + " AND dbo.SubjectAllotment.CompID=" + mCompID + "    ";

            DataTable dt = new DataTable();

            dt = DB.ExecuteQuery(sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SubjectLevelOne objnew = new SubjectLevelOne();
                    objnew.IdL1 = int.Parse(dr["IDL1"].ToString());
                    objnew.SubjectNameL1 = dr["SubjectNameL1"].ToString();

                    obj.Add(objnew);
                }

            }



            return obj;
        }
  


    }



  

}