using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class vStudentCreateLoginRepository : GenericRepository<vStudentForCreateLoginID>
    {
        public vStudentCreateLoginRepository() : base(new dbSchoolAppEntities()) { }
        public vStudentCreateLoginRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<vStudentForCreateLoginID> GetStudentListForCreateLoginID(byte mCompID, byte mBranchID)
        {
            List<vStudentForCreateLoginID> obj = new List<vStudentForCreateLoginID>();
            obj = this.context.vStudentForCreateLoginIDs.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        public vStudentForCreateLoginID GetStudentDetailForLogin(int StudentID, byte BranchID, byte CompID)
        {

            vStudentForCreateLoginID obj = new vStudentForCreateLoginID();
            obj = this.context.vStudentForCreateLoginIDs.Where(x => x.StudentID == StudentID && x.BranchID == BranchID && x.CompID == CompID).FirstOrDefault();
            return obj;
        }



        //public void AddNewStudentinSession(StudentRegistration obj)
        //{
        //    if (obj.ClassID == null)
        //    {
        //        obj.ClassID = 0;
        //    }

        //    StudentSession newObj = new StudentSession();
        //        newObj.StudentID = obj.StudentID;
        //        newObj.SessionID = obj.SessionID;
        //        newObj.CompID = obj.CompID;
        //        newObj.BranchID = obj.BranchID;
        //        newObj.IsNewStudent = true;
        //        newObj.ClassID = byte.Parse(obj.ClassID.ToString());
        //        newObj.MCourseID = obj.MCourseID;
        //        newObj.CourseID =obj.CourseID;
        //        newObj.YearID = 1;
        //        newObj.SemID = 1;
        //        newObj.BusFacility = false;
        //        newObj.FeesStructStatus = false;
        //        newObj.HostelFacility = false;
        //        newObj.IsNewStudent = false;
        //        newObj.SMSInHindi = false;
        //        newObj.BusID = 0;
        //        newObj.AddDate = DateTime.Now;
        //        newObj.HouseID = 0;
        //        newObj.RollNo = 0;
        //        newObj.UIDAdd = obj.UIDAdd;
             

        //    this.Insert(newObj);
        //}

        //public List<StudentSession> GetStudentForAttendance(int mClassSetupID, byte mCompID, byte mBranchID)
        //{
        //    List<StudentSession> obj = this.context.StudentSessions.Where(x => x.CourseID == mClassSetupID && x.CompID==mCompID && x.BranchID==mBranchID).ToList(); 
        //    return obj;
        //}

        //public List<vStudentSession> GetAllStudentSessionDetail(byte mCourseID, byte courseID, byte yearID, byte semID, byte mSemStatus, byte mCompID, byte mBranchID, byte mSessionID)
        //{
        //    bool SemTransFlag = false;
        //    List<vStudentSession> obj = new List<vStudentSession>();
        //    if (mSemStatus ==2)
        //    {
        //        SemTransFlag = true;
        //    }
        //    obj = this.context.vStudentSessions.Where(x => x.MCourseID == mCourseID && x.CourseID == courseID && x.YearID == yearID && x.SemID==semID && x.SemTransflag==SemTransFlag   && x.CompID == mCompID && x.BranchID == mBranchID && x.SessionID==mSessionID).ToList();
        //    return obj;
        //}

        //public List<vStudentSession> GetStudentDetailForRemark(byte mCourseID,byte mYearID,byte mSemID  ,byte mSessionID, byte mCompID, byte mBranchID )
        //{

        //    List<vStudentSession> obj = new List<vStudentSession>();

        //    obj = this.context.vStudentSessions.Where(x => x.MCourseID == mCourseID && x.YearID==mYearID && x.SemID==mSemID  && x.CompID == mCompID && x.BranchID == mBranchID && x.SessionID == mSessionID).ToList();
        //    return obj;
        //}

        //public bool CheckDuplicateRollNoClassWise(int mRollNo, int mClassSetupID, int mSessionID)
        //{
        //    bool res = false;
        //    int ID = this.context.StudentSessions.Where(x => x.RollNo == mRollNo && x.CourseID == mClassSetupID && x.SessionID == mSessionID).Count();
        //    if (ID > 0)
        //    {
        //        res = true;
        //    }
        //    return res;
        //}


        //public void UpdateStudentRollNoByVStudentSession(vStudentSession Stud)
        //{
        //    StudentSession editSession = this.GetByID(Stud.StudentSessionID);
        //    if (editSession != null)
        //    {
        //        editSession.RollNo = Stud.RollNo;

        //        this.Update(editSession);
        //    }

        //}
        //public List<vStudentSession> GetStudentForFeeClasswise(int ClassSetupID,int sessionID, byte mCompID, byte mBranchID)
        //{
        //    List<vStudentSession> obj1 = this.context.vStudentSessions.Where(x => x.CourseID == ClassSetupID && x.SessionID == sessionID && x.CompID==mCompID && x.BranchID==mBranchID).ToList();
        //    return obj1;
        //}

        //public void UpdateStudentRollNo(StudentSession Stud)
        //{
        //    StudentSession editSession = this.GetByID(Stud.StudentSessionID);
        //    if (editSession != null)
        //    {
        //        editSession.RollNo = Stud.RollNo;

        //        this.Update(editSession);
        //    }
        //}

        //public List<vStudentSession> GetStudentForSessionID(int sessionID)
        //{
        //    // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();

        //    //List<vStudentSession> obj1 = this.context.StudentSessions.SqlQuery("SELECT * FROM dbo.vStudentSession where ClassSetupID=" + ClassSetupID + " and SessionID="+sessionID +"").ToList();
        //    List<vStudentSession> obj1 = this.context.vStudentSessions.Where(x =>x.SessionID == sessionID).ToList();
        //    return obj1;
        //}
        //public List<vStudentSession> GetStudentSessionListByClassSetupIDs(string mClassSetupID, int mSessionID)
        //{
        //    List<vStudentSession> obj = new List<vStudentSession>();

        //                obj = this.context.vStudentSessions.SqlQuery("SELECT * FROM dbo.vStudentSession where ClassSetupID in (" + mClassSetupID + ") and SessionID=" + mSessionID + "").ToList();
        //    return obj;
        //}

       
        //public List<vStudentSession> GetAllStudentbyClassID(int mClassID, int msessionID, byte mCompID, byte mBranchID)
        //{
        //    List<vStudentSession> obj = new List<vStudentSession>();
        //                      obj = this.context.vStudentSessions.Where(x => x.CourseID == mClassID && x.SessionID == msessionID && x.CompID== mCompID && x.BranchID== mBranchID).OrderBy(X => X.FullName).ToList();
        //    return obj;
        //}

      



        //public List<vStudentSession> GetAllStudentWithTCFalsebyClassID(int mClassID, int msessionID, byte mCompID, byte mBranchID)
        //{
        //    bool mflag=false;
        //    List<vStudentSession> obj = new List<vStudentSession>();
        //    obj = this.context.vStudentSessions.Where(x => x.CourseID == mClassID && x.TCGiven==mflag && x.SessionID == msessionID  && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(X => X.FullName).ToList();
        //    return obj;
        //}

        //public List<vStudentSession> GetAllStudentWithTCTruebyClassID(int mClassID, int msessionID, byte mCompID, byte mBranchID)
        //{
        //    bool mflag = true;
        //    List<vStudentSession> obj = new List<vStudentSession>();
        //    obj = this.context.vStudentSessions.Where(x => x.CourseID == mClassID && x.TCGiven == mflag && x.SessionID == msessionID && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(X => X.FullName).ToList();
        //    return obj;
        //}

        //public vStudentSession GetStudentDetailByStudentSessionID(int mStudentSessionID, int msessionID, byte mCompID, byte mBranchID)
        //{
        //    vStudentSession obj = new vStudentSession();
        //    obj = this.context.vStudentSessions.Where(x => x.StudentSessionID == mStudentSessionID && x.SessionID == msessionID && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(X => X.FullName).FirstOrDefault();
        //    return obj;
        //}

        //public List<vStudentSession> GetAllStudentForSessionTransfer(int mClassID, int mSectionID, int msessionID, byte mCompID, byte mBranchID)
        //{
        //    List<vStudentSession> obj = new List<vStudentSession>();
        //    obj = this.context.vStudentSessions.Where(x => x.MCourseID == mClassID && x.CourseID == mSectionID && x.IsTransfered == false && x.SessionID == msessionID && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(X => X.FullName).ToList();
        //    return obj;
        //}

        //public List<vStudentSession> GetAllStudentbyClassSetupID(int mClassSetupID, int msessionID, byte mCompID, byte mBranchID)
        //{

        //    List<vStudentSession> obj1 = this.context.vStudentSessions.Where(x => x.CourseID == mClassSetupID && x.SessionID == msessionID && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(X => X.FullName).ToList();
        //    return obj1;
        //}




        //public List<vStudentSession> GetAllStudentbyClassIDwithFeeStructStatus(int ClassID, int sessionID)
        //{
        //    List<vStudentSession> obj1 = this.context.vStudentSessions.Where(x => x.CourseID == ClassID && x.SessionID == sessionID && x.FeesStructStatus==false).ToList();
        //    return obj1;
        //}

        //public List<StudentSession> GetAllStudentbyMCourseIDwithFeeFlag(int MCourseID,int YearID ,int sessionID, byte mCompID, byte mBranchID)
        //{
        //    List<StudentSession> obj = this.context.StudentSessions.Where(x => x.MCourseID == MCourseID && x.YearID == YearID && x.SessionID == sessionID && x.FeesStructStatus==false && x.CompID==mCompID && x.BranchID==mBranchID).ToList();
        //    return obj;
        //}

        //public string GetClassDescriptionByStudentIDandClassIDandSessionID(int mStudentID, int mClassID, int mSessionID, byte mCompID, byte mBranchID)
        //{
        //    string ClassDescription = string.Empty;

        //    ClassDescription = this.context.vStudentSessions.Where(x => x.StudentID == mStudentID && x.CourseID == mClassID && x.SessionID == mSessionID && x.CompID==mCompID && x.BranchID==mBranchID ).FirstOrDefault().MCourseName;

        //    return ClassDescription;
        //}


        //public List<vStudentSession>  GetAllStudentbyMCourseIDwithFeeFlagFromViewofStudentSession(int mCourseID,int yearID ,int sessionID, byte mCompID, byte mBranchID)
        //{
        //    List<vStudentSession> obj = this.context.vStudentSessions.Where(x => x.MCourseID == mCourseID && x.YearID==yearID && x.SessionID == sessionID && x.FeesStructStatus == false  && x.CompID==mCompID && x.BranchID==mBranchID ).ToList();
        //    return obj;
        //}


        //public List<vStudentSession> GetAllStudentForSession(int sessionID ,byte mCompID, byte mBranchID )
        //{
        //    List<vStudentSession> obj = this.context.vStudentSessions.Where(x=>x.CompID==mCompID && x.BranchID==mBranchID && x.SessionID==sessionID).ToList();
        //    return obj;
        //}
        //public List<vStudentSession> GetAllStudentForFeeTransaction(int sessionID, byte mCompID, byte mBranchID)
        //{
        //    bool FeesFlag = true;
        //    List<vStudentSession> obj = this.context.vStudentSessions.Where(x => x.CompID == mCompID && x.FeesStructStatus==FeesFlag && x.BranchID == mBranchID && x.SessionID == sessionID).ToList();
        //    return obj;
        //}

        //public vStudentSession GetStudentDetailByStudentID(int mStudentID,byte sessionID, byte mCompID, byte mBranchID)
        //{

        //    vStudentSession obj = this.context.vStudentSessions.Where(x => x.CompID == mCompID && x.StudentID == mStudentID && x.BranchID == mBranchID && x.SessionID == sessionID).FirstOrDefault();
        //    return obj;
        //}



     










    //    public StudentSession GetStudent

        //const string NorthwindDataContextKey = "DXNorthwindDataContext";

        //public static JngaCollegeEntities DB
        //{
        //    get
        //    {
               
        //        return (JngaCollegeEntities)HttpContext.Current.Items[NorthwindDataContextKey];
        //    }
        //}

        //public void UpdateStudentSession(vStudentSession Stud)
        //{
        //    StudentSession editSession = this.GetByID(Stud.StudentSessionID);
        //    if (editSession != null)
        //    {
        //        //editSession.StudentID = Stud.StudentID;
        //        //editSession.SessionID  = Stud.SessionID;

        //        editSession.HouseID  = Stud.HouseID;
        //        editSession.HostelFacility  = Stud.HostelFacility ;
        //        editSession.BusFacility = Stud.BusFacility ;
        //        editSession.BusID = Stud.BusID;
        //        editSession.SMSInHindi = Stud.SMSInHindi;
        //        editSession.IsNewStudent = bool.Parse(Stud.IsNewStudent.ToString());
                
                
        //        this.Update(editSession);
        //    }




        //}


        //public void UpdateStudentClassAllotment(int mStudentSessionID, int mClassIDTo,int mClassSetupID ,byte CompID, byte BranchID)
        //{
        //    StudentSession editSession = this.GetByID(mStudentSessionID);
        //    if (editSession != null)
        //    {
        //        editSession.MCourseID = byte.Parse(mClassIDTo.ToString());
        //        editSession.CourseID = byte.Parse(mClassSetupID.ToString());
        //        this.Update(editSession);
        //    }

        //}



        //public void UpdateStudentSessionHousewise(StudentSession Stud, int rbtnChoice)
        //{
        //    StudentSession editSession = this.GetByID(Stud.StudentSessionID);
        //    if (editSession != null)
        //    {
        //        if (rbtnChoice == 1)
        //        {
        //            editSession.HouseID = Stud.HouseID;
        //        }
        //        if (rbtnChoice == 2)
        //        {
        //            editSession.CourseID = Stud.CourseID;
        //        }
        //        if (rbtnChoice == 3)
        //        {
        //            editSession.SMSInHindi = Stud.SMSInHindi;
        //        }
        //        this.Update(editSession);
        //    }




        //}

        //public void UpdateStudentHouse(StudentSession Stud)
        //{
        //    StudentSession editSession = this.GetByID(Stud.StudentSessionID);
        //    if (editSession != null)
        //    {
        //            editSession.HouseID = Stud.HouseID;
               
        //        this.Update(editSession);
        //    }




        //}


        //public void UpdateFeesStructStatusinStudentSession(StudentSession Stud)
        //{
        //    StudentSession editSession = this.GetByID(Stud.StudentSessionID);
        //    if (editSession != null)
        //    {
        //        editSession.FeesStructStatus = Stud.FeesStructStatus;
        //        this.Update(editSession);
        //    }
        //}


        //public void DeleteProduct(vStudentSession Stud)
        //{
        //    StudentSession editSession = this.GetByID(Stud.StudentSessionID);
        //    if (editSession != null)
        //    {
        //        this.Delete(editSession);
        //    }

        //}
        //public void InsertProduct(vStudentSession Stud)
        //{
        //    StudentSession editSession = new StudentSession();
        //    if (editSession != null)
        //    {
        //        editSession.StudentID = Stud.StudentID;
        //        editSession.SessionID = Stud.SessionID;
        //        editSession.HouseID = Stud.HouseID;
        //        editSession.HostelFacility = Stud.HostelFacility;
        //        editSession.BusFacility = Stud.BusFacility;
        //        editSession.BusID = Stud.BusID;
        //        editSession.CourseID = Stud.CourseID;
        //        editSession.StudentSessionID = Stud.StudentSessionID;
        //        editSession.SMSInHindi = Stud.SMSInHindi;
        //        editSession.IsNewStudent = bool.Parse(Stud.IsNewStudent.ToString());
        //        editSession.CompID = Stud.CompID;
        //        editSession.BranchID = Stud.BranchID;
                
        //        this.Insert(editSession);
        //    }

        //}
       
    }


   
}