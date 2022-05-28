using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class ContectListRepository: GenericRepository<ContactList>
    {
        public ContectListRepository() : base(new dbSchoolAppEntities()) { }
        public ContectListRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<ContactList> GetContactList(byte mCompID, byte mBranchID)
        {
            List<ContactList> obj = new List<ContactList>();
            obj = this.context.ContactLists.Where(x => x.ContactID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(x => x.Department).ToList();
            return obj;
        }



        public void UpdateContectList(ContactList obj)
        {
            ContactList c = this.GetByID(obj.ContactID);
            c.Department = obj.Department;
            c.Description = obj.Description;
            c.MobileNo = obj.MobileNo;
            c.PhoneNo1 = obj.PhoneNo1;
            c.PhoneNo2 = obj.PhoneNo2;
            
            this.Update(c);
            return;
        }

    }
    //#region METADATA
    //[MetadataType(typeof(DepartmentMasterMetadata))]
    //public partial class DepartmentMaster
    //{
    //}

    //public class ContectMetadata
    //{
    //    [Key]
    //    public short ContectID { get; set; } // Has to have the same type and name as your model
    //    [Required(ErrorMessage = "Department is required")]
    //    public string Department { get; set; } // Has to have the same type and name as your model

    //}
    //#endregion
}