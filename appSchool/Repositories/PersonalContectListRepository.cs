using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class PersonalContectListRepository: GenericRepository<PersonalContactList>
    {
        public PersonalContectListRepository() : base(new dbSchoolAppEntities()) { }
        public PersonalContectListRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<PersonalContactList> GetContactList(byte mCompID, byte mBranchID)
       {
            List<PersonalContactList> obj = new List<PersonalContactList>();
            obj = this.context.PersonalContactLists.Where(x => x.PersonalPersonID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
       }

        //public PersonalContactList GetContactListByID(int mPersonalPersonID, byte mCompID, byte mBranchID)
        //{
        //    PersonalContactList obj = new PersonalContactList();
        //    obj = this.context.PersonalContactLists.Where(x => x.PersonalPersonID == mPersonalPersonID && x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();

        //    return obj;
        //}



        public void UpdateContectList(PersonalContactList obj)
        {
            PersonalContactList c = this.GetByID(obj.PersonalPersonID);
            c.PName = obj.PName;
            c.EmailID = obj.EmailID;
            c.MobileNO = obj.MobileNO;
            c.Description = obj.Description;
            //c.PhoneNo2 = obj.PhoneNo2;
            
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