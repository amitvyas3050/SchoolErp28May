using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class ExamSetupMasterRepository: GenericRepository<ExamSetupMaster>
    {
        public ExamSetupMasterRepository() : base(new dbSchoolAppEntities()) { }
        public ExamSetupMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }




        public ExamSetupMaster GetExamSetupMasterData(ExamSetupMaster obj)
        {
            ExamSetupMaster objNew = new ExamSetupMaster();
            objNew = this.context.ExamSetupMasters.Where(x => x.ClassID == obj.ClassID && x.ExamID == obj.ExamID && x.SessionID==obj.SessionID && x.CompID == obj.CompID && x.BranchID == obj.BranchID ).SingleOrDefault();
            return objNew;
        }

        public List<ExamMaster> GetExamListFromExamSetupMaster(byte mSessionID, byte mBranchID, byte mCompID)
        {
            List<ExamMaster> objList = new List<ExamMaster>();
            string msql = "SELECT DISTINCT dbo.ExamSetupMaster.ExamID, dbo.ExamMaster.ExamName " +
                          " FROM    dbo.ExamSetupMaster INNER JOIN " + 
                          " dbo.ExamMaster ON dbo.ExamSetupMaster.ExamID = dbo.ExamMaster.ExamID AND dbo.ExamSetupMaster.BranchID = dbo.ExamMaster.BranchID AND  " +
                         " dbo.ExamSetupMaster.CompID = dbo.ExamMaster.CompID  " +
                         " Where dbo.ExamSetupMaster.SessionID=" + mSessionID + " AND dbo.ExamSetupMaster.BranchID=" + mBranchID + " AND dbo.ExamSetupMaster.CompID="+mCompID +"  ";

            //objList = this.context.ExamMasters.SqlQuery(msql).ToList();
            DataTable dt = new DataTable();
            dt = DB.ExecuteQuery(msql);
            foreach (DataRow dr in dt.Rows)
            {
                ExamMaster objEM=new ExamMaster();
                objEM.ExamName=dr["ExamName"].ToString();
                objEM.ExamID=int.Parse(dr["ExamID"].ToString());
                objList.Add(objEM);
            }

            return objList;
        }

        public List<vEXamSetUpTransfer> GetExamSetupMasterList(byte mSessionID, byte mBranchID, byte mCompID)
        {

            bool mflag = false;
            List<vEXamSetUpTransfer> objNew = new List<vEXamSetUpTransfer>();
            objNew = this.context.vEXamSetUpTransfers.Where(x => x.SessionID == mSessionID && x.CompID == mCompID && x.IsTransfer==mflag && x.BranchID == mBranchID).OrderBy(x => x.ClassID).ToList();
            return objNew;
        }



        public List<ExamMaster> GetExamListFromExamSetupMasterByClasswise(int mClassID,byte mSessionID, byte mBranchID, byte mCompID)
        {
            List<ExamMaster> objList = new List<ExamMaster>();
            string msql = "SELECT DISTINCT dbo.ExamSetupMaster.ExamID, dbo.ExamMaster.ExamName " +
                          " FROM    dbo.ExamSetupMaster INNER JOIN " +
                          " dbo.ExamMaster ON dbo.ExamSetupMaster.ExamID = dbo.ExamMaster.ExamID AND dbo.ExamSetupMaster.BranchID = dbo.ExamMaster.BranchID AND  " +
                         " dbo.ExamSetupMaster.CompID = dbo.ExamMaster.CompID  " +
                         " Where dbo.ExamSetupMaster.ClassID=" + mClassID + " AND dbo.ExamSetupMaster.SessionID=" + mSessionID + " AND dbo.ExamSetupMaster.BranchID=" + mBranchID + " AND dbo.ExamSetupMaster.CompID=" + mCompID + "  ";

            //objList = this.context.ExamMasters.SqlQuery(msql).ToList();
            DataTable dt = new DataTable();
            dt = DB.ExecuteQuery(msql);
            foreach (DataRow dr in dt.Rows)
            {
                ExamMaster objEM = new ExamMaster();
                objEM.ExamName = dr["ExamName"].ToString();
                objEM.ExamID = int.Parse(dr["ExamID"].ToString());
                objList.Add(objEM);
            }

            return objList;
        }

        public List<SubjectLevelOne> GetSubjectLevelOneListFromExamSetup(byte mBranchID, byte mCompID, byte mSessionID, int mClassID)
        {
            List<SubjectLevelOne> objList = new List<SubjectLevelOne>();

            string msql = " SELECT DISTINCT dbo.SubjectLevelOne.IdL1, dbo.SubjectLevelOne.SubjectNameL1 " +
                            " FROM   dbo.ExamSetupMaster INNER JOIN " +
                            " dbo.ExamSetupDetail ON dbo.ExamSetupMaster.SessionID = dbo.ExamSetupDetail.SessionId AND " +
                            " dbo.ExamSetupMaster.CompID = dbo.ExamSetupDetail.CompID AND dbo.ExamSetupMaster.BranchID = dbo.ExamSetupDetail.BranchID AND  " +
                            " dbo.ExamSetupMaster.ExamSetupID = dbo.ExamSetupDetail.ExamSetupID INNER JOIN " +
                            " dbo.SubjectLevelOne ON dbo.ExamSetupDetail.SubjectIDL1 = dbo.SubjectLevelOne.IdL1 " +
                            " Where   dbo.ExamSetupMaster.CompID=" + mCompID + " AND dbo.ExamSetupMaster.BranchID=" + mBranchID + " AND " +
                            " dbo.ExamSetupMaster.SessionID=" + mSessionID + " AND dbo.ExamSetupMaster.ClassID=" + mClassID;

            DataTable dt = new DataTable();
            dt = DB.ExecuteQuery(msql);
            foreach (DataRow dr in dt.Rows)
            {
                SubjectLevelOne objLO = new SubjectLevelOne();
                objLO.SubjectNameL1 = dr["SubjectNameL1"].ToString();
                objLO.IdL1 = int.Parse(dr["IdL1"].ToString());
                objList.Add(objLO);
            }

            return objList;

        }

        public List<Class> GetClassListFromExamSetupMaster(byte mSessionID, byte mBranchID, byte mCompID)
        {
            List<Class> objList = new List<Class>();
            string msql = "SELECT DISTINCT dbo.ExamSetupMaster.ClassID, dbo.Class.ClassName " +
                          " FROM    dbo.ExamSetupMaster INNER JOIN " +
                          " dbo.Class ON dbo.ExamSetupMaster.ClassID = dbo.Class.ClassID AND dbo.ExamSetupMaster.BranchID = dbo.Class.BranchID AND  " +
                         " dbo.ExamSetupMaster.CompID = dbo.Class.CompID  " +
                         " Where dbo.ExamSetupMaster.SessionID=" + mSessionID + " AND dbo.ExamSetupMaster.BranchID=" + mBranchID + " AND dbo.ExamSetupMaster.CompID=" + mCompID + "  ";

            //objList = this.context.ExamMasters.SqlQuery(msql).ToList();
            DataTable dt = new DataTable();
            dt = DB.ExecuteQuery(msql);
            foreach (DataRow dr in dt.Rows)
            {
                Class objEM = new Class();
                objEM.ClassName = dr["ClassName"].ToString();
                objEM.ClassID = int.Parse(dr["ClassID"].ToString());
                objList.Add(objEM);
            }

            return objList;
        }

        public void InsertExamSetupMaster(ExamSetupMaster obj)
        {
            this.Insert(obj);
        }
        public void UpdateExamSetupMaster(ExamSetupMaster obj)
        {
            ExamSetupMaster objnew = this.GetByID(obj.ExamSetupID);
            if (objnew != null)
            {
                objnew.UIDMod = obj.UIDMod;
                objnew.ModDate = obj.ModDate;
            }

        }


    }



}