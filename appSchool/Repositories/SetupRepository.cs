using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class SetupRepository: GenericRepository<Setup>
    {
        public SetupRepository() : base(new dbSchoolAppEntities()) { }
        public SetupRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }




      

        public void UpdateReceiptNo(int mSessionID,long mReceiptNo, byte mCompID, byte mBranchID)
        {

            Setup editsetup = this.context.Setups.Where(x => x.SessionID == mSessionID && x.CompID==mCompID && x.BranchID==mBranchID).SingleOrDefault();
            if (editsetup != null)
            {

                editsetup.ReceiptNo = mReceiptNo;

                this.Update(editsetup);
            }




        }




    }



}