using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class FacultyAllotmentMasterRepository: GenericRepository<FacultyAllotmentMaster>
    {
        public FacultyAllotmentMasterRepository() : base(new dbSchoolAppEntities()) { }
        public FacultyAllotmentMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }




        public FacultyAllotmentMaster GetFacultyAllotmentMasterData(FacultyAllotmentMaster obj)
        {
            FacultyAllotmentMaster objNew = new FacultyAllotmentMaster();
            objNew = this.context.FacultyAllotmentMasters.Where(x => x.ClassID == obj.ClassID && x.ClassSetupID == obj.ClassSetupID && x.FacultyID == obj.FacultyID && x.CompID==obj.CompID && x.BranchID==obj.BranchID).SingleOrDefault();
            return objNew;
        }

        public void InsertFacultyAllotmentMaster(FacultyAllotmentMaster obj)
        {
            this.Insert(obj);
        }
        public void UpdateFacultyAllotmentMaster(FacultyAllotmentMaster obj)
        {
            FacultyAllotmentMaster objnew = this.GetByID(obj.FacultyAllotmentID);
            if (objnew != null)
            {
                objnew.UIDMod = obj.UIDMod;
                objnew.ModDate = obj.ModDate;
            }

        }


    }



}