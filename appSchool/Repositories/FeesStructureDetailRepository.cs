using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace appSchool.Repositories
{
    public class FeesStructureDetailRepository: GenericRepository<FeesStructureDetail>
    {
        public FeesStructureDetailRepository() : base(new dbSchoolAppEntities()) { }
        public FeesStructureDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<FeesStructureDetail> GetFeeStructureDetailbyClassID(int mClassID , byte mCompID, byte mBranchID)
        {
            List<FeesStructureDetail> obj = new List<FeesStructureDetail>();
            obj = this.context.FeesStructureDetails.Where(i => i.FeeStructClassID ==mClassID && i.CompID==mCompID && i.BranchID==mBranchID).ToList();
            return obj;
        }


        public List<FeesStructureDetail> GetFeeStructureDetailbyClassIDAndSessionID(int mClassID, int mSessionID, byte mCompID, byte mBranchID)
        {
            List<FeesStructureDetail> obj = new List<FeesStructureDetail>();
            obj = this.context.FeesStructureDetails.Where(i => i.FeeStructClassID == mClassID && i.FeeStructSessionID==mSessionID  && i.CompID==mCompID && i.BranchID==mBranchID ).ToList();
            return obj;
        }

        public List<FeesStructureDetail> GetFeeStructureDetailbyClassIDAndSessionIDAndTermID(int mClassID, int mSessionID, int? mTermID, byte mCompID, byte mBranchID)
        {
            List<FeesStructureDetail> obj = new List<FeesStructureDetail>();
            obj = this.context.FeesStructureDetails.Where(i => i.FeeStructClassID == mClassID && i.FeeStructSessionID == mSessionID && i.FeeTermID==mTermID  && i.CompID==mCompID && i.BranchID==mBranchID).ToList();
            return obj;
        }


        public List<int?> GetFSDetailbyClassIDAndSessionIDwithDistinctTermID(int mClassID, int mSessionID, byte mCompID, byte mBranchID)
        {
            var result = this.context.FeesStructureDetails.Where(i=>i.FeeStructClassID==mClassID && i.FeeStructSessionID==mSessionID && i.CompID==mCompID && i.BranchID==mBranchID ).Select(m => m.FeeTermID).Distinct();
            List<int?> objlist = result.ToList();
            return objlist;
        }






        public List<FeesStructureDetail> GetFeeStructureDetailbyFeeStructID(int mFeeStructID)
        {
            List<FeesStructureDetail> obj = new List<FeesStructureDetail>();
            obj = this.context.FeesStructureDetails.Where(i => i.FeeStructID == mFeeStructID).ToList();
            return obj;
        }


        public decimal GetTotalFeesAmountByClassID(int? FeeStructClassID,int? FeeStructSessionID)
        {
            decimal? id = 0;
            //DateTime mAttendanceDate = obj.AttendanceDate.Date;
            // id = this.context.FeesStructureDetails.SqlQuery("SELECT SUM(FeeAmount) AS FeeAmount FROM  FeesStructureDetail  WHERE (FeeStructClassID = " +FeeStructClassID + ") AND (FeeStructSessionID = " +FeeStructSessionID +"  GroupBy FeeStructClassID )").FirstOrDefault().FeeAmount;
             decimal amount = Convert.ToDecimal(id);
            return amount;
        }


        public decimal GetTotalFeesAmountByClassIDwithTermIDwithSessionID(int? FeeStructClassID, int? FeeStructSessionID,int? TermID,bool? IsNewStudent)
        {
          
            //var result = this.context.FeesStructureDetails.Where(i => i.FeeStructClassID == FeeStructClassID && i.FeeStructSessionID == FeeStructSessionID && i.FeeTermID==TermID).Select(m => m.FeeAmount).Sum();

            //decimal objlist = result.GetValueOrDefault();

            //return objlist;

            var result = this.context.FeesStructureDetails.SqlQuery("SELECT  SUM(dbo.FeesStructureDetail.FeeAmount) AS FeeAmount " +
                                                                     " FROM            dbo.FeesStructureDetail INNER JOIN " +
                                                                     "  dbo.FeesHead ON dbo.FeesStructureDetail.FeeHeadID = dbo.FeesHead.FeesHeadID " +
                                                                     " WHERE (dbo.FeesStructureDetail.FeeStructClassID = (" + FeeStructClassID  + ")) AND " + 
                                                                     " (dbo.FeesStructureDetail.FeeStructSessionID = (" + FeeStructSessionID  + ")) AND " + 
                                                                     " (dbo.FeesStructureDetail.FeeTermID =(" + TermID  + ")) AND  " +
                                                                     " (dbo.FeesHead.FeesHeadType <> 'OneTime')");

            decimal objlist = decimal.Parse(result.ToString());

            return objlist;


          //  FeesStructureDetail objFeesStructureDataExport = new FeesStructureDetail();

          //  var param = new[] { 
          //                 new SqlParameter("@FeeStructClassID", FeeStructClassID),
          //                 new SqlParameter("@FeeStructSessionID", FeeStructSessionID),
          //                 new SqlParameter("@FeeTermID", TermID),
          //                  new SqlParameter("@IsNewStudent", IsNewStudent),
          //                  };

          //   objFeesStructureDataExport = this.context.Database.SqlQuery<FeesStructureDetail>(
          //                           "GetTotalFeeStructureAmountTermWise @FeeStructClassID,@FeeStructSessionID,@FeeTermID,@IsNewStudent",
          //                            param
          //                   ).SingleOrDefault();

          ////  decimal objlist = decimal.Parse(result.FeeAmount.ToString());
          //   decimal objlist = decimal.Parse(objFeesStructureDataExport.FeeAmount.ToString());

          //  return objlist;






        }




        public int GetFeesStructID(FeesStructureMaster obj)
        {
            int id = 0;
            //DateTime mAttendanceDate = obj.AttendanceDate.Date;
            FeesStructureMaster obj1 = this.context.FeesStructureMasters.Where(x => x.FeesStructClassID == obj.FeesStructClassID && x.FeeStructSessionID == obj.FeeStructSessionID).FirstOrDefault();
            if (obj1 != null)
            {
                id = obj1.FeeStructID;
            }


            return id;
        }


        public void InsertProduct(VFeesCompulsory objFSMaster,int mFeesStructID,int mfeesClassID,int mStructSessionID, byte mCompID, byte mBranchID)
        {
            FeesStructureDetail editFStructDetail = new FeesStructureDetail();
            if (editFStructDetail != null)
            {
                editFStructDetail.FeeStructID = mFeesStructID;
                editFStructDetail.FeeAmount = objFSMaster.DefaultAmount;
                editFStructDetail.FeeHeadID = objFSMaster.FeesHeadID;
                editFStructDetail.FeeStructClassID =mfeesClassID;
                editFStructDetail.FeeStructSessionID =mStructSessionID;
                editFStructDetail.CompID = mCompID;
                editFStructDetail.BranchID = mBranchID;
                editFStructDetail.FeeTermID =objFSMaster.FeeTermID;
                this.Insert(editFStructDetail);
            }

        }



        public void InsertFeesStructureDetail(vListFeesStructure objFSDetail, int mfeeStructID, int sessionid, byte CompID, byte BranchID)
        {
          
            FeesStructureMaster objFSMaster = new UnitOfWork().feesStructureMasterService.GetFeesStructureMasterByFeeStructIDandSessionID(mfeeStructID,sessionid);


            FeesStructureDetail editFStructDetail = new FeesStructureDetail();
            if (editFStructDetail != null)
            {
                editFStructDetail.FeeStructID = objFSMaster.FeeStructID;
                editFStructDetail.FeeAmount = objFSDetail.FeeAmount;
                editFStructDetail.FeeHeadID = objFSDetail.FeeHeadID;
                editFStructDetail.FeeStructClassID = objFSMaster.FeesStructClassID;
                editFStructDetail.FeeStructSessionID = objFSMaster.FeeStructSessionID;
                editFStructDetail.FeeTermID = objFSDetail.FeeTermID;
                editFStructDetail.CompID = CompID;
                editFStructDetail.BranchID = BranchID;

                this.Insert(editFStructDetail);
            }

        }


        public void UpdateFeesStructureDetail(vListFeesStructure objFSDetail,byte CompID,byte BranchID)
        {
            FeesStructureDetail editFStructDetail =this.GetByID(objFSDetail.FeeStructDetailID);
            if (editFStructDetail != null)
            {
                editFStructDetail.FeeAmount = objFSDetail.FeeAmount;
                editFStructDetail.FeeHeadID = objFSDetail.FeeHeadID;
                editFStructDetail.FeeTermID = objFSDetail.FeeTermID;
                editFStructDetail.CompID = CompID;
                editFStructDetail.BranchID = BranchID;
                
                this.Update(editFStructDetail);
            }

        }

        public void DeleteFeesStructureDetail(vListFeesStructure objFSDetail)
        {
            FeesStructureDetail editSession = this.GetByID(objFSDetail.FeeStructDetailID);
            if (editSession != null)
            {
                this.Delete(editSession);
            }

        }
    }


     




}