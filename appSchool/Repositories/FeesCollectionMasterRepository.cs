using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class FeesCollectionMasterRepository: GenericRepository<FeesCollectionMaster>
    {
        public FeesCollectionMasterRepository() : base(new dbSchoolAppEntities()) { }
        public FeesCollectionMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }




        public int GetReceiptNo()
        {
            int? ReceiptNo = 0;

               //var    mReceiptNo = this.context.FeesCollectionMasters.Max(e => e.ReceiptNo).ToString();
               //ReceiptNo = int.Parse(mReceiptNo.ToString());
               //if (mReceiptNo != null)
               //{
               //    ReceiptNo = ReceiptNo + 1;
               //}


          //  ReceiptNo = this.context.FeesCollectionMasters.SqlQuery("Select MAX(ReceiptNo) from FeesCollectionMaster where ReceiptNo>=0 ").SingleOrDefault().ReceiptNo;
        //  var  aa=this.context.FeesCollectionMasters.Max(o => o == null ? 0 : o.ReceiptNo);

          //FeesCollectionMaster obj = this.context.FeesCollectionMasters.SqlQuery("Select from FeesCollectionMaster").SingleOrDefault();
          //if (obj == null)
          //{
          //    ReceiptNo = 1;

          //}
          //else
          //{
          //    ReceiptNo = obj.ReceiptNo + 1;
          //}


            // this.context.FeesCollectionMasters.Select(r => r.ReceiptNo).Max().ToString();

           // ReceiptNo = ReceiptNo + 1;



            return  int.Parse(ReceiptNo.ToString());
        }


        public bool CheckDuplicateData(int StudentID, int TermID, int SessionID, byte mCompID, byte mBranchID)
        {
            bool isDuplicate = false;

            FeesCollectionMaster obj = this.context.FeesCollectionMasters.Where(x => x.StudentID == StudentID && x.TermID == TermID && x.SessionID == SessionID && x.CompID==mCompID && x.BranchID==mBranchID ).SingleOrDefault();

            if (obj != null && obj.ReceiptNo > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }
            return isDuplicate;
        }








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