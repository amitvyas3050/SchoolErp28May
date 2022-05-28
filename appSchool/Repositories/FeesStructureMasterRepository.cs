using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class FeesStructureMasterRepository: GenericRepository<FeesStructureMaster>
    {
        public FeesStructureMasterRepository() : base(new dbSchoolAppEntities()) { }
        public FeesStructureMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }





        //public List<VFeesCompulsory> GetTermHeadForFeeStructure()
        //{
        //    List<VFeesCompulsory> obj = new List<VFeesCompulsory>();
        //    obj = this.context.VFeesCompulsories.Where(i => i.FeeTermID > 0).ToList();

        //    return obj;
        //}

        public int GetFeesStructID(FeesStructureMaster obj)
        {
            int id = 0;
            FeesStructureMaster obj1 = this.context.FeesStructureMasters.Where(x => x.FeesStructClassID == obj.FeesStructClassID && x.FeeStructSessionID == obj.FeeStructSessionID   && x.CompID==obj.CompID && x.BranchID==obj.BranchID ).FirstOrDefault();
            if (obj1 != null)
            {
                id = obj1.FeeStructID;
            }
         
            return id;
        }

        public int GetClassExist(int classID,int sessionID, byte mCompID, byte mBranchID)
        {
            int id = 0;
            //DateTime mAttendanceDate = obj.AttendanceDate.Date;
            FeesStructureMaster obj1 = this.context.FeesStructureMasters.Where(x => x.FeesStructClassID == classID && x.FeeStructSessionID == sessionID && x.CompID==mCompID && x.BranchID==mBranchID).FirstOrDefault();
            if (obj1 != null)
            {
                id = obj1.FeeStructID;
            }
            return id;
        }

        public FeesStructureMaster GetFeesStructureMasterByClassIDandSessionID(int classID, int sessionID)
        {
            //DateTime mAttendanceDate = obj.AttendanceDate.Date;
            FeesStructureMaster obj1 = this.context.FeesStructureMasters.Where(x => x.FeesStructClassID == classID && x.FeeStructSessionID == sessionID).FirstOrDefault();
            return obj1;
        }
        public FeesStructureMaster GetFeesStructureMasterByFeeStructIDandSessionID(int feeStructID, int sessionID)
        {
            //DateTime mAttendanceDate = obj.AttendanceDate.Date;
            FeesStructureMaster obj1 = this.context.FeesStructureMasters.Where(x => x.FeeStructID == feeStructID && x.FeeStructSessionID == sessionID).FirstOrDefault();
            return obj1;
        }
        public void UpdateFeesAmountMaster(FeesStructureMaster objFSmaster)
        {
            FeesStructureMaster editFStructMaster = this.GetByID(objFSmaster.FeeStructID);
            if (editFStructMaster != null)
            {
                editFStructMaster.FeeStructAmount = objFSmaster.FeeStructAmount;
                //editFStructMaster.FeeHeadID = objFSDetail.FeeHeadID;
                //editFStructMaster.FeeTermID = objFSDetail.FeeTermID;
                this.Update(editFStructMaster);
            }

        }


     









        //public void InsertProduct(VFeesCompulsory objFSMaster,int mfeesClassID)
        //{
        //    FeesStructureDetail editFStructDetail = new FeesStructureDetail();
        //    if (editFStructDetail != null)
        //    {
        //        editFStructDetail.FeeStructID = mfeesClassID;
        //        editFStructDetail.FeeAmount = objFSMaster.DefaultAmount;
        //        editFStructDetail.FeeHeadID = objFSMaster.FeesHeadID;
        //        //editFStructDetail.FeeStructClassID = Stud.HostelFacility;
        //        //editFStructDetail.FeeStructSessionID = Stud.BusFacility;
        //        editFStructDetail.FeeTermID =objFSMaster.FeeTermID;
        //        this.Insert(editFStructDetail);
        //    }

        //}


    }


    public class modelFeeTransaction
    {
        public List<modelFeesStructureDetail> feesStructDetail{ get; set; }
        public modelFeeTransactionMaster FeesTrancMaster { get; set; }
    }

    


    public class modelFeeTransactionMaster
    {
        public int FeeTransactionID { get; set; }
        public Nullable<int> FeeStructSessionID { get; set; }
        public Nullable<decimal> FeeStructAmount { get; set; }
        public Nullable<decimal> OneTimeFeeAmount { get; set; }
        public Nullable<int> FeesStructClassID { get; set; }
    }



    public  class modelFeesStructureDetail
    {
        public int FeeStructDetailID { get; set; }
        public Nullable<int> FeeStructID { get; set; }
        public Nullable<int> FeeStructClassID { get; set; }
        public Nullable<int> FeeTermID { get; set; }
        public Nullable<int> FeeHeadID { get; set; }
        public Nullable<decimal> FeeAmount { get; set; }
        public Nullable<int> FeeStructSessionID { get; set; }
    }




}