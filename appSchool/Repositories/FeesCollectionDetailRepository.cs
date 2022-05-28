using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class FeesCollectionDetailRepository: GenericRepository<FeesCollectionDetail>
    {
        public FeesCollectionDetailRepository() : base(new dbSchoolAppEntities()) { }
        public FeesCollectionDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }




        //public int GetFeesStructID(StudentFeesMaster obj)
        //{
        //    int id = 0;
        //    //DateTime mAttendanceDate = obj.AttendanceDate.Date;
        //    StudentFeesMaster obj1 = this.context.StudentFeesMasters.Where(x => x.TermID == obj.TermID && x.SessionID == obj.SessionID && x.StudentClassId==obj.StudentClassId).FirstOrDefault();
        //    if (obj1 != null)
        //    {
        //        id = obj1.StudmasterID;
        //    }


        //    return id;
        //}

        //public int GetFeesStructIDForCheckDuplicateByStudentSession(StudentSession obj)
        //{
        //    int id = 0;
        //    //DateTime mAttendanceDate = obj.AttendanceDate.Date;
        //    StudentFeesMaster obj1 = this.context.StudentFeesMasters.Where(x => x.StudentClassId == obj.ClassID && x.SessionID == obj.SessionID && x.StudentID == obj.StudentID).FirstOrDefault();
        //    if (obj1 != null)
        //    {
        //        id = obj1.StudmasterID;
        //    }


        //    return id;
        //}

        //public StudentFeesMaster GetFeesDetailForStudentFeesReceipt(int mStudentID,int mTermID, int mSessionID)
        //{
        //    StudentFeesMaster obj = this.context.StudentFeesMasters.Where(x => x.StudentID == mStudentID && x.SessionID == mSessionID && x.TermID == mTermID).FirstOrDefault();
        //    return obj;
        //}



    }



}