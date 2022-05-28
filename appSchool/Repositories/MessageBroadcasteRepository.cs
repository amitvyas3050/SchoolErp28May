using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.Data.SqlClient;

namespace appSchool.Repositories
{
    public class MessageBroadcasteRepository : GenericRepository<Section> 
    {
        public MessageBroadcasteRepository() : base(new dbSchoolAppEntities()) { }
        public MessageBroadcasteRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }
        //public IEnumerable<vClass> GetSectionsView()
        //{
        //    return this.context.vClasses.ToList<vClass>();
        //}

        public List<Section> GetSectionList(byte mCompID,byte mBranchID)
        {
            List<Section> obj = new List<Section>();
            obj = this.context.Sections.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        
        }
        public StudentSession GetStudentSessionListtbyStudentSessionID(int StudSessionID, byte BranchID, byte CompID)
        {

            StudentSession obj = new StudentSession();
            obj = this.context.StudentSessions.Where(x => x.StudentSessionID == StudSessionID && x.BranchID == BranchID && x.CompID == CompID).FirstOrDefault();
            return obj;
        }
        public List<vStudentDataExport> GetStudentListForMessagebroadcast(byte mClassID, byte mClassSetupID, byte mCompID, byte mBranchID, byte mSessionID)
        {

            List<vStudentDataExport> obj1 = this.context.vStudentDataExports.Where(x => x.ClassID == mClassID && x.ClassSetupID == mClassSetupID && x.SessionID == mSessionID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();

            return obj1;

        }

        public void AddNewSection(Section obj,byte UserID) 
        {
            this.Insert(new Section() { SectionName = obj.SectionName, UIDAdd = UserID, AddDate = DateTime.Now, BranchID=obj.BranchID, CompID=obj.CompID });
            return;
           
        }
        public void UpdateSection(Section obj, byte UserID)
        {
            Section c = this.GetByID(obj.SectionID);
            c.SectionName = obj.SectionName;
            c.UIDMod = UserID;
            c.ModDate = DateTime.Now;
            this.Update(c);
            return;
        }
        public void DeleteSection(Section obj)
        {
            this.Delete(obj);
            return;
        }
        public int CheckDelete(int mID)
        {
            int ID = 0;
            ID = this.context.ClassSetups.Where(x => x.SectionID == mID).Count();
            return ID;
        }
        public List<vStudentSession> GetAllStudentSessionDetailForTransfer(byte mCourseID, byte courseID, byte yearID, byte semID, byte mCompID, byte mBranchID, byte mSessionID)
        {
            byte Mode = 15;  //Mode=15 for filter by CompID,BranchID,SessionID,MCourseID,CourseID,YearID,SemID,IsTransfered=False  in vStudentSession
            byte pCompID = mCompID;
            byte pBranchID = mBranchID;
            byte pSessionID = mSessionID;
            byte pClassID = 0;
            byte pCourseID = courseID;
            byte pMCourseID = mCourseID;
            byte pYearID = yearID;
            byte pSemID = semID;
            bool pFeesStructStatus = false;
            bool pSemTransFlag = false;

            List<vStudentSession> obj = new List<vStudentSession>();
            var param = new[] { 
                          
                          new SqlParameter("@CompID", pCompID),
                           new SqlParameter("@BranchID", pBranchID),
                           new SqlParameter("@SessionID", pSessionID),
                           new SqlParameter("@ClassID", pClassID),
                           new SqlParameter("@CourseID", pCourseID),
                           new SqlParameter("@MCourseID", pMCourseID),
                           new SqlParameter("@YearID", pYearID),
                           new SqlParameter("@SemID", pSemID),
                           new SqlParameter("@FeesStructStatus", pFeesStructStatus),
                           new SqlParameter("@SemTransFlag", pSemTransFlag),
                           new SqlParameter("@Mode", Mode),
                            };

            obj = this.context.Database.SqlQuery<vStudentSession>(
                                   "GetStudentSessionList @CompID,@BranchID,@SessionID,@ClassID,@CourseID,@MCourseID,@YearID,@SemID,@FeesStructStatus,@SemTransFlag,@Mode",
                                    param
                           ).ToList();

            return obj;

        }


        //public List<vMessageBroadcast> GetAllStudentMessageBroadcast(byte mCompID, byte mBranchID, byte mSessionID)
        //{
        //    bool SemTransferFlag = false;

        //    List<vMessageBroadcast> obj1 = this.context.vMessageBroadcasts.SqlQuery("SELECT * FROM dbo.vMessageBroadcast where  SessionID=" + mSessionID + " and CompID=" + mCompID + " AND BranchID=" + mBranchID + "   ").ToList();

        //    return obj1;


        //}


        //public List<vMessageBroadcast> GetAllStudentMessageBroadcast(byte mCourseID, byte courseID, byte yearID, byte semID, byte mCompID, byte mBranchID, byte mSessionID)
        //{
        //    List<vMessageBroadcast> obj = new List<vMessageBroadcast>();
        //    obj = this.context.vMessageBroadcasts.Where(x => x. &&  > 0).ToList();
        //    return obj;
        //}
    

    }
}
