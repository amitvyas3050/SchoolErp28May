using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class StudentFeesMasterRepository: GenericRepository<StudentFeesMaster>
    {
        public StudentFeesMasterRepository() : base(new dbSchoolAppEntities()) { }
        public StudentFeesMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }




        public int GetFeesStructID(StudentFeesMaster obj)
        {
            int id = 0;
            //DateTime mAttendanceDate = obj.AttendanceDate.Date;
            StudentFeesMaster obj1 = this.context.StudentFeesMasters.Where(x => x.TermID == obj.TermID && x.SessionID == obj.SessionID && x.StudentClassId==obj.StudentClassId).FirstOrDefault();
            if (obj1 != null)
            {
                id = obj1.StudmasterID;
            }


            return id;
        }

        public int GetFeesStructIDForCheckDuplicateByStudentSession(StudentSession obj)
        {
            int id = 0;
            StudentFeesMaster obj1 = this.context.StudentFeesMasters.Where(x => x.StudentClassId == obj.ClassID && x.SessionID == obj.SessionID && x.StudentID == obj.StudentID && x.CompID==obj.CompID && x.BranchID==obj.BranchID ).FirstOrDefault();
            if (obj1 != null)
            {
                id = obj1.StudmasterID;
            }
            return id;
        }

        public StudentFeesMaster GetFeesDetailForStudentFeesReceipt(int mStudentID,int mTermID, int mSessionID, byte mCompID, byte mBranchID)
        {
            StudentFeesMaster obj = this.context.StudentFeesMasters.Where(x => x.StudentID == mStudentID && x.SessionID == mSessionID && x.TermID == mTermID  && x.CompID==mCompID && x.BranchID==mBranchID ).FirstOrDefault();
            return obj;
        }

        public void UpdateFeesPaidFlagINStudentFeesMaster(int StudentID, int TermID, decimal PaidAmount, int SessionID , byte mCompID, byte mBranchID)
        {

            StudentFeesMaster editFeeMaster = this.context.StudentFeesMasters.Where(x => x.StudentID == StudentID && x.TermID == TermID && x.SessionID == SessionID && x.CompID==mCompID && x.BranchID==mBranchID).SingleOrDefault();
            if (editFeeMaster != null)
            {

                editFeeMaster.PaidFlag = true;
                editFeeMaster.PaidAmount = PaidAmount;

                this.Update(editFeeMaster);
            }




        }

        public void UpdateTotalFeesAmount(int? StudentID, int? TermID, int? SessionID, decimal? FeesAmount)
        {

            StudentFeesMaster editFeeMaster = this.context.StudentFeesMasters.Where(x => x.StudentID == StudentID && x.TermID == TermID && x.SessionID == SessionID).SingleOrDefault();
            if (editFeeMaster != null)
            {

                editFeeMaster.Amount = FeesAmount;

                this.Update(editFeeMaster);
            }




        }





    }



}