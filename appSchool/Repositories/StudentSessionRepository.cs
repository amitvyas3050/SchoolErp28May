using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class StudentSessionRepository : GenericRepository<StudentSession>
    {
        public StudentSessionRepository() : base(new dbSchoolAppEntities()) { }
        public StudentSessionRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<StudentSession> GetStudentSessionList(int mSessionID,byte mCompID, byte mBranchID)
        {
            List<StudentSession> obj = new List<StudentSession>();
            obj = this.context.StudentSessions.Where(x => x.SessionID == mSessionID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }
        public List<vStudentSession> GetAllStudentSessionDetailForSendMessage(byte mClassID, byte mCompID, byte mBranchID, int msessionID)
        {

            List<vStudentSession> obj = new List<vStudentSession>();
            obj = this.context.vStudentSessions.Where(x => x.ClassID == mClassID && x.SessionID == msessionID && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(X => X.FullName).ToList();
            return obj;
        }

        public List<vStudentSession> GetStudentDetailForRemark(byte mClassID, byte mSessionID, byte mCompID, byte mBranchID)
        {


            List<vStudentSession> obj = new List<vStudentSession>();
            obj = this.context.vStudentSessions.Where(x => x.ClassID == mClassID && x.SessionID == mSessionID && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(X => X.FullName).ToList();
            return obj;

            //string sql = " SELECT * FROM dbo.vStudentSession WHERE BranchID='+cast(@BranchID as varchar(10))+' AND "+
            //       CompID='+cast(@CompID as varchar(10))+' AND SessionID='+CAST(@SessionID as varchar(10))+'AND "+
            //         ClassID='+CAST(@ClassID as varchar(10))+' AND "+ ORDER BY StudentSessionID " +
            //  " Where  dbo.SubjectAllotment.ClassID in (" + mClassesID + ")   and dbo.SubjectAllotment.CompID=" + mCompID + " AND dbo.SubjectAllotment.BranchID=" + mBranchID ;


            //List<vStudentSession> obj = this.context.vStudentSessions.SqlQuery(sql).ToList();
            //return obj;    


            //return obj;


        }


        public vStudentSession GetStudentDetailByStudentID(int mStudentID, byte sessionID, byte mCompID, byte mBranchID)
        {

            vStudentSession obj = this.context.vStudentSessions.Where(x => x.CompID == mCompID && x.StudentID == mStudentID && x.BranchID == mBranchID && x.SessionID == sessionID).FirstOrDefault();
            return obj;
        }

        public void AddNewStudentinSession(StudentRegistration obj)
        {
            if (obj.ClassID == null)
            {
                obj.ClassID = 0;

            }

            StudentSession newObj = new StudentSession();
                newObj.StudentID = obj.StudentID;
                newObj.SessionID = obj.SessionID;
                newObj.CompID = obj.CompID;
                newObj.BranchID = obj.BranchID;
                newObj.ClassID = int.Parse(obj.ClassID.ToString());
                newObj.BusFacility = false;
                newObj.FeesStructStatus = false;
                newObj.HostelFacility = false;
                newObj.IsNewStudent = false;
                newObj.SMSInHindi = false;
                newObj.IsTransfered = false;
                newObj.BusID = 0;
                newObj.ClassSetupID = 0;
                newObj.AddDate = DateTime.Now;
                newObj.HouseID = 0;
                newObj.RollNo = 0;
                newObj.UIDAdd = obj.UIDAdd;
             

            this.Insert(newObj);
        }

        public List<StudentSession> GetStudentForAttendance(int mClassSetupID, byte mCompID, byte mBranchID)
        {
            bool TCFlag = false;

            List<StudentSession> obj = this.context.StudentSessions.Where(x => x.ClassSetupID == mClassSetupID && x.CompID==mCompID && x.BranchID==mBranchID ).ToList(); 
            return obj;
        }
        public bool CheckDuplicateRollNoClassWise(int mRollNo, int mClassSetupID, int mSessionID)
        {
            bool res = false;
            int ID = this.context.StudentSessions.Where(x => x.RollNo == mRollNo && x.ClassSetupID == mClassSetupID && x.SessionID == mSessionID).Count();
            if (ID > 0)
            {
                res = true;
            }
            return res;
        }


        public void UpdateStudentRollNoByVStudentSession(vStudentSession Stud)
        {
            StudentSession editSession = this.GetByID(Stud.StudentSessionID);
            if (editSession != null)
            {
                editSession.RollNo = Stud.RollNo;

                this.Update(editSession);
            }

        }
        public List<vStudentSession> GetStudentForFeeClasswise(int ClassSetupID,int sessionID, byte mCompID, byte mBranchID)
        {
            List<vStudentSession> obj1 = this.context.vStudentSessions.Where(x => x.ClassSetupID == ClassSetupID && x.SessionID == sessionID && x.CompID==mCompID && x.BranchID==mBranchID).ToList();
            return obj1;
        }

        public void UpdateStudentRollNo(StudentSession Stud)
        {
            StudentSession editSession = this.GetByID(Stud.StudentSessionID);
            if (editSession != null)
            {
                editSession.RollNo = Stud.RollNo;

                this.Update(editSession);
            }
        }

        public List<vStudentSession> GetStudentForSessionID(int sessionID)
        {
            // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();

            //List<vStudentSession> obj1 = this.context.StudentSessions.SqlQuery("SELECT * FROM dbo.vStudentSession where ClassSetupID=" + ClassSetupID + " and SessionID="+sessionID +"").ToList();
            List<vStudentSession> obj1 = this.context.vStudentSessions.Where(x =>x.SessionID == sessionID).ToList();
            return obj1;
        }
        public List<vStudentSession> GetStudentSessionListByClassSetupIDs(string mClassSetupID, int mSessionID)
        {
            List<vStudentSession> obj = new List<vStudentSession>();

                        obj = this.context.vStudentSessions.SqlQuery("SELECT * FROM dbo.vStudentSession where ClassSetupID in (" + mClassSetupID + ") and SessionID=" + mSessionID + "").ToList();
            return obj;
        }


        public List<vStudentSession> GetAllStudentbyClassID(int mClassID, int msessionID, byte mCompID, byte mBranchID)
        {
            bool TCFlag = false;
            List<vStudentSession> obj = new List<vStudentSession>();
                              obj = this.context.vStudentSessions.Where(x => x.ClassID == mClassID && x.SessionID == msessionID && x.CompID== mCompID && x.BranchID== mBranchID && x.TCGiven==TCFlag).OrderBy(X => X.FullName).ToList();
            return obj;
        }

        public List<vStudentSession> GetAllStudentWithTCFalsebyClassandClasssetupID(int mClassID, int mClassSetupID, int msessionID, byte mCompID, byte mBranchID)
        {
            bool mflag = false;
            List<vStudentSession> obj = new List<vStudentSession>();
            obj = this.context.vStudentSessions.Where(x => x.ClassID == mClassID && x.ClassSetupID == mClassSetupID && x.TCGiven == mflag && x.SessionID == msessionID && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(X => X.FullName).ToList();
            return obj;
        }


        public List<vStudentSession> GetAllStudentWithTCFalsebyClassID(int mClassID, int msessionID, byte mCompID, byte mBranchID)
        {
            bool mflag=false;
            List<vStudentSession> obj = new List<vStudentSession>();
            obj = this.context.vStudentSessions.Where(x => x.ClassID == mClassID && x.TCGiven==mflag && x.SessionID == msessionID  && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(X => X.FullName).ToList();
            return obj;
        }

        public List<vStudentSession> GetAllStudentWithTCTruebyClassID(int mClassID, int msessionID, byte mCompID, byte mBranchID)
        {
            bool mflag = true;
            List<vStudentSession> obj = new List<vStudentSession>();
            obj = this.context.vStudentSessions.Where(x => x.ClassID == mClassID && x.TCGiven == mflag && x.SessionID == msessionID && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(X => X.FullName).ToList();
            return obj;
        }

        public vStudentSession GetStudentDetailByStudentSessionID(int mStudentSessionID, int msessionID, byte mCompID, byte mBranchID)
        {
            vStudentSession obj = new vStudentSession();
            obj = this.context.vStudentSessions.Where(x => x.StudentSessionID == mStudentSessionID && x.SessionID == msessionID && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(X => X.FullName).FirstOrDefault();
            return obj;
        }

        public List<vStudentSession> GetAllStudentForSessionTransfer(int mClassID, int mSectionID, int msessionID, byte mCompID, byte mBranchID)
        {
            List<vStudentSession> obj = new List<vStudentSession>();
            obj = this.context.vStudentSessions.Where(x => x.ClassID == mClassID && x.ClassSetupID==mSectionID && x.IsTransfered==false && x.TCGiven==false && x.SessionID == msessionID && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(X => X.FullName).ToList();
            return obj;
        }

        public List<vStudentSession> GetAllStudentbyClassSetupID(int mClassSetupID, int msessionID, byte mCompID, byte mBranchID)
        {
            bool TCFlag = false;
            List<vStudentSession> obj1 = this.context.vStudentSessions.Where(x => x.ClassSetupID == mClassSetupID && x.SessionID == msessionID && x.CompID == mCompID && x.BranchID == mBranchID && x.TCGiven==TCFlag).OrderBy(X => X.FullName).ToList();
            return obj1;
        }




        public List<vStudentSession> GetAllStudentbyClassIDwithFeeStructStatus(int ClassID, int sessionID)
        {

            List<vStudentSession> obj1 = this.context.vStudentSessions.Where(x => x.ClassID == ClassID && x.SessionID == sessionID && x.FeesStructStatus==false && x.TCGiven==false).ToList();
            return obj1;
        }

        public List<StudentSession> GetAllStudentbyClassIDwithFeeFlag(int ClassID, int sessionID, byte mCompID, byte mBranchID)
        {
            List<StudentSession> obj = this.context.StudentSessions.Where(x => x.ClassID == ClassID && x.SessionID == sessionID && x.FeesStructStatus==false && x.CompID==mCompID && x.BranchID==mBranchID).ToList();
            return obj;
        }

        public string GetClassDescriptionByStudentIDandClassIDandSessionID(int mStudentID, int mClassID, int mSessionID, byte mCompID, byte mBranchID)
        {
            string ClassDescription = string.Empty;

            ClassDescription = this.context.vStudentSessions.Where(x => x.StudentID == mStudentID && x.ClassID == mClassID && x.SessionID == mSessionID && x.CompID==mCompID && x.BranchID==mBranchID ).SingleOrDefault().ClassDescription;

            return ClassDescription;
        }


        public List<vStudentSession>  GetAllStudentbyClassIDwithFeeFlagFromViewofStudentSession(int ClassID, int sessionID, byte mCompID, byte mBranchID)
        {
            List<vStudentSession> obj = this.context.vStudentSessions.Where(x => x.ClassID == ClassID && x.SessionID == sessionID && x.FeesStructStatus == false && x.TCGiven==false  && x.CompID==mCompID && x.BranchID==mBranchID ).ToList();
            return obj;
        }


        public List<vStudentSession> GetAllStudentForSession(int sessionID ,byte mCompID, byte mBranchID )
        {
            List<vStudentSession> obj = this.context.vStudentSessions.Where(x=>x.CompID==mCompID && x.TCGiven==false && x.BranchID==mBranchID && x.SessionID==sessionID).ToList();
            return obj;
        }


        public List<Session> GetAllSessionList()
        {
            List<appSchool.Repositories.Session> List = new List<Session>();


            List = this.context.Sessions.ToList();

            return List;

        }


    //    public StudentSession GetStudent

        const string NorthwindDataContextKey = "DXNorthwindDataContext";

        public static dbSchoolAppEntities DB
        {
            get
            {
               
                return (dbSchoolAppEntities)HttpContext.Current.Items[NorthwindDataContextKey];
            }
        }

        public void UpdateStudentSession(vStudentSession Stud)
        {
            StudentSession editSession = this.GetByID(Stud.StudentSessionID);
            if (editSession != null)
            {
                //editSession.StudentID = Stud.StudentID;
                //editSession.SessionID  = Stud.SessionID;

                editSession.HouseID  = Stud.HouseID;
                editSession.HostelFacility  = Stud.HostelFacility ;
                editSession.BusFacility = Stud.BusFacility ;
                editSession.BusID = Stud.BusID;
                editSession.ClassSetupID  = Stud.ClassSetupID;
                editSession.SMSInHindi = Stud.SMSInHindi;
                editSession.IsNewStudent = Stud.IsNewStudent;
                //if (editSession.ClassSetupID > 0)
                //{
                //    editSession.ClassID = this.context.ClassSetups.Where(x => x.ClassSetupID == Stud.ClassSetupID).SingleOrDefault().ClassID;
                //}
                //else
                //{
                //    editSession.ClassID = 0;
                //}
                
                this.Update(editSession);
            }




        }


        public void UpdateStudentClassAllotment(int mStudentSessionID, int mClassIDTo,int mClassSetupID ,byte CompID, byte BranchID)
        {
            StudentSession editSession = this.GetByID(mStudentSessionID);
            if (editSession != null)
            {
                editSession.ClassID = mClassIDTo;
                editSession.ClassSetupID = mClassSetupID;
                this.Update(editSession);
            }

        }



        public void UpdateStudentSessionHousewise(StudentSession Stud, int rbtnChoice)
        {
            StudentSession editSession = this.GetByID(Stud.StudentSessionID);
            if (editSession != null)
            {
                if (rbtnChoice == 1)
                {
                    editSession.HouseID = Stud.HouseID;

                }
                if (rbtnChoice == 2)
                {
                    editSession.ClassSetupID = Stud.ClassSetupID;
                }
                if (rbtnChoice == 3)
                {
                    editSession.SMSInHindi = Stud.SMSInHindi;
                }
                this.Update(editSession);
            }




        }

        public void UpdateStudentHouse(StudentSession Stud)
        {
            StudentSession editSession = this.GetByID(Stud.StudentSessionID);
            if (editSession != null)
            {
                    editSession.HouseID = Stud.HouseID;
               
                this.Update(editSession);
            }




        }


        public void UpdateFeesStructStatusinStudentSession(StudentSession Stud)
        {
            StudentSession editSession = this.GetByID(Stud.StudentSessionID);
            if (editSession != null)
            {
                editSession.FeesStructStatus = Stud.FeesStructStatus;
                this.Update(editSession);
            }
        }


        public void DeleteProduct(vStudentSession Stud)
        {
            StudentSession editSession = this.GetByID(Stud.StudentSessionID);
            if (editSession != null)
            {
                this.Delete(editSession);
            }

        }
        public void InsertProduct(vStudentSession Stud)
        {
            StudentSession editSession = new StudentSession();
            if (editSession != null)
            {
                editSession.StudentID = Stud.StudentID;
                editSession.SessionID = Stud.SessionID;
                editSession.HouseID = Stud.HouseID;
                editSession.HostelFacility = Stud.HostelFacility;
                editSession.BusFacility = Stud.BusFacility;
                editSession.BusID = Stud.BusID;
                editSession.ClassSetupID = Stud.ClassSetupID;
                editSession.StudentSessionID = Stud.StudentSessionID;
                editSession.SMSInHindi = Stud.SMSInHindi;
                editSession.IsNewStudent = Stud.IsNewStudent;
                editSession.CompID = Stud.CompID;
                editSession.BranchID = Stud.BranchID;
                
                this.Insert(editSession);
            }

        }
       
    }


    #region METADATA
    [MetadataType(typeof(StudentSessionMetadata))]
    public partial class StudentSession
    {
    }

    public class StudentSessionMetadata
    {
        [Key]
        public int StudentSessionID { get; set; } // Has to have the same type and name as your model
        [Required(ErrorMessage = "ClassId is required")]
        public int ClassSetupID { get; set; } // Has to have the same type and name as your model
        public int BusID { get; set; }
        public int HouseID { get; set; }
        public int StudentID { get; set; }
        public int SessionID { get; set; }
    }
    #endregion
}