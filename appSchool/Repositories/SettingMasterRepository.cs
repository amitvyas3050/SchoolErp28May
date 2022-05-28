using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class SettingMasterRepository : GenericRepository<SettingMaster>
    {
        public SettingMasterRepository() : base(new dbSchoolAppEntities()) { }
        public SettingMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        public SettingMaster GetSettingMaster()
        {
            SettingMaster obj1 = this.context.SettingMasters.FirstOrDefault();
            return obj1;
        }

        public void UpdateSettingMaster(SettingMaster obj, int UserID)
        {
            SettingMaster newObj = this.GetByID(obj.SettingID);
            newObj.IncludeNameInSMS = obj.IncludeNameInSMS;
            this.Update(newObj);
        }

    }
   
}