using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class EmployeeShiftMasterRepository : GenericRepository<EmployeeShiftMaster>
    {
        public EmployeeShiftMasterRepository() : base(new dbSchoolAppEntities()) { }
        public EmployeeShiftMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        public EmployeeShiftMaster GetEmployeeShiftMaster()
        {
            EmployeeShiftMaster obj1 = this.context.EmployeeShiftMasters.FirstOrDefault();
            return obj1;
        }

        public void UpdateShifttime(EmployeeShiftMaster objtime)
        {
            //this.Insert(objtime);
            EmployeeShiftMaster newObj = this.GetByID(objtime.ShiftID);
            newObj.ShiftTime = objtime.ShiftTime;
            this.Update(newObj);
        }

    }
   
    public class modelEmployeeShiftMaster
    {
        public int ShiftID { get; set; }
        public string ShiftTime { get; set; }
        public DateTime ShiftDateTime { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<byte> CompID { get; set; }
        public Nullable<byte> BranchID { get; set; }
    }
         
}