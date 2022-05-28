using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;

namespace appSchool.Repositories
{
    public class SMSTypeRepository: GenericRepository<SMSType>
    {
        public SMSTypeRepository() : base(new dbSchoolAppEntities()) { }
        public SMSTypeRepository(dbSchoolAppEntities dbContext): base(dbContext) {}


        public List<SMSType> GetSMSTypeList()
        {
            List<SMSType> obj = new List<SMSType>();
            obj = this.context.SMSTypes.Where(x => x.SMSTypeID>0).ToList();
            return obj;
        }

        public void UpdateSMSType(SMSType obj)
        {
            SMSType newObj = this.GetByID(obj.SMSTypeID);
            newObj.SMSTypeName = obj.SMSTypeName;
            newObj.UIDMod = obj.UIDMod;
            newObj.ModDate = obj.ModDate;
            this.Update(newObj);
        }




        public IEnumerable<listItem> GetSMSTypesItems()
        {
            List<listItem> lst = new List<listItem>();
            lst.AddRange(
                from xx in this.context.SMSTypes
                select new listItem() { Value = xx.SMSTypeID, Description = xx.SMSTypeName }
                );
            return lst;
        }

    }
}