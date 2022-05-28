using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class vListFeesStructureRepository: GenericRepository<vListFeesStructure>
    {
        public vListFeesStructureRepository() : base(new dbSchoolAppEntities()) { }
        public vListFeesStructureRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<vListFeesStructure> GetFeesStructureList(int mSessionID,byte mCompID, byte mBranchID)
        {
            //DateTime mAttendanceDate = obj.AttendanceDate.Date;
            List<vListFeesStructure> obj1 = this.context.vListFeesStructures.Where(x => x.FeeStructSessionID == mSessionID && x.CompID == mCompID && x.BranchID==mBranchID).ToList();

            return obj1;
        }
        public List<vListFeesStructure> GetFeesStructureByClassID(int mClassID,int mSessionID)
        {
            
            //DateTime mAttendanceDate = obj.AttendanceDate.Date;
           List<vListFeesStructure> obj1 = this.context.vListFeesStructures.Where(x => x.FeesStructClassID == mClassID && x.FeeStructSessionID ==mSessionID).ToList();

            return obj1;
        }

        public List<vListFeesStructure> GetFeesStructureByFeeStructID(int mFeeStructID, int mSessionID)
        {

            //DateTime mAttendanceDate = obj.AttendanceDate.Date;
            List<vListFeesStructure> obj1 = this.context.vListFeesStructures.Where(x => x.FeeStructID == mFeeStructID && x.FeeStructSessionID == mSessionID).ToList();

            return obj1;
        }





        //public List<VFeesCompulsory> GetTermHeadForFeeStructure()
        //{
        //    List<VFeesCompulsory> obj = new List<VFeesCompulsory>();
        //    obj = this.context.VFeesCompulsories.Where(i => i.FeeTermID > 0).ToList();

        //    return obj;
        //}

        //public int GetFeesStructID(FeesStructureMaster obj)
        //{
        //    int id = 0;
        //    //DateTime mAttendanceDate = obj.AttendanceDate.Date;
        //    FeesStructureMaster obj1 = this.context.FeesStructureMasters.Where(x => x.FeesStructClassID == obj.FeesStructClassID && x.FeeStructSessionID == obj.FeeStructSessionID).FirstOrDefault();
        //    if (obj1 != null)
        //    {
        //        id = obj1.FeeStructID;
        //    }

         
        //    return id;
        //}

        //public int GetClassExist(int classID,int sessionID)
        //{
        //    int id = 0;
        //    //DateTime mAttendanceDate = obj.AttendanceDate.Date;
        //    FeesStructureMaster obj1 = this.context.FeesStructureMasters.Where(x => x.FeesStructClassID == classID && x.FeeStructSessionID == sessionID).FirstOrDefault();
        //    if (obj1 != null)
        //    {
        //        id = obj1.FeeStructID;
        //    }
        //    return id;
        //}




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


     




}