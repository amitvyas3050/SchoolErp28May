using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;

namespace appSchool.Repositories
{
    public class DriverMasterRepository : GenericRepository<DriverMaster> 
    {
        public DriverMasterRepository() : base(new dbSchoolAppEntities()) { }
        public DriverMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<DriverMaster> GetDriverMasterList(byte mCompID, byte mBranchID)
        {
            List<DriverMaster> obj = new List<DriverMaster>();
            obj = this.context.DriverMasters.Where(i => i.DriverId > 0 && i.CompID == mCompID && i.BranchID == mBranchID).OrderBy(i => i.Dname).ToList();

            return obj;
        }

        public List<DriverMaster> GetOnlyCleanerList(byte mCompID, byte mBranchID)
        {
            string mType="Cleaner";

            List<DriverMaster> obj = new List<DriverMaster>();
            obj = this.context.DriverMasters.Where(i => i.DriverId > 0 && i.EmpType ==mType && i.CompID == mCompID && i.BranchID == mBranchID).OrderBy(i => i.Dname).ToList();

            return obj;
        }

        public List<DriverMaster> GetOnlyDriverList(byte mCompID, byte mBranchID)
        {
            string mType = "Driver";

            List<DriverMaster> obj = new List<DriverMaster>();
            obj = this.context.DriverMasters.Where(i => i.DriverId > 0 && i.EmpType == mType && i.CompID == mCompID && i.BranchID == mBranchID).OrderBy(i => i.Dname).ToList();

            return obj;
        }


        public void AddNewDriverMaster(DriverMaster obj)
        {
            Insert(obj);
            return;
        }
        public void UpdateDriverMaster(DriverMaster obj)
        {
            DriverMaster objNew = this.GetByID(obj.DriverId);
            if (objNew != null)
            {
                objNew.Address = obj.Address;
                objNew.Birthdate = obj.Birthdate;
                objNew.City = obj.City;
                objNew.CityID = obj.CityID;
                objNew.Dname= obj.Dname;
                objNew.DrEmailID = obj.DrEmailID;
                objNew.DrMobileNo = obj.DrMobileNo;
                objNew.DrMobileNo2 = obj.DrMobileNo2;
                objNew.EmpType = obj.EmpType;
                objNew.Gender = obj.Gender;
                objNew.IsActive = obj.IsActive;
                objNew.Licence = obj.Licence;
                objNew.LorryNo = obj.LorryNo;
                objNew.PAN = obj.PAN;
                objNew.Phone = obj.Phone;
                objNew.PinCode = obj.PinCode;
                objNew.RefAddress = obj.RefAddress;
                objNew.RefBy = obj.RefBy;
                objNew.RefMobileNo = obj.RefMobileNo;
                objNew.Remark = obj.Remark;
                objNew.RTO = obj.RTO;
                objNew.StateId = obj.StateId;
                
                objNew.uploadflag = obj.uploadflag;
                objNew.Validupto = obj.Validupto;
                objNew.UIDMod = obj.UIDMod;
                objNew.ModDate = DateTime.Now;

                Update(objNew);
            }
            return;
        }

        //private string SetDescription(ClassSetup obj)
        //{
        //    string strDescription = string.Empty;
        //    strDescription += this.context.Classes.Where(x => x.ClassID == obj.ClassID).FirstOrDefault().ClassName+" ";
        //    strDescription += this.context.Sections.Where(x => x.SectionID == obj.SectionID).FirstOrDefault().SectionName + "  ";
        //    //if(obj.ClassCategoryID!=null)
        //    //    strDescription += this.context.ClassCategories.Where(x => x.ClassCategoryID == obj.ClassCategoryID).FirstOrDefault().ClassCategoryName;
        //    return strDescription;
        //}

        //public IEnumerable<listItem> GetClassSetupItemsforClass(int typeID)
        //{
        //    List<listItem> lst = new List<listItem>();
        //    lst.AddRange(
        //        from xx in this.context.ClassSetups
        //        where xx.ClassID == typeID
        //        orderby xx.DisplayOrder 
        //        select new listItem() { Value = xx.ClassSetupID, Description = xx.ClassDescription }
        //        );
        //    return lst;
        //}
        //public IEnumerable<listItem> GetClassSetupforSelectWithNone()
        //{
        //    List<listItem> lst = new List<listItem>();
        //    lst.Add(new listItem() { Value = -1, Description = "(None)" });
        //    lst.AddRange(
        //        from xx in this.context.ClassSetups
        //        select new listItem() { Value = xx.ClassSetupID, Description = xx.ClassDescription }
        //        );
        //    return lst;
        //}
        //public IEnumerable<listItem> GetClassSetupforSelect()
        //{
        //    IEnumerable<listItem> lst = null;
        //    lst =(
        //        from xx in this.context.ClassSetups
        //        select new listItem() { Value = xx.ClassSetupID, Description = xx.ClassDescription }
        //        );
        //    return lst;
        //}
        //public List<vClass> GetAllClassForSMS()
        //{
        //    List<vClass> obj = new List<vClass>();
        //    obj = this.context.vClasses.Where(i => i.ClassID >0).ToList();

        //    return obj;
        //}

        //public List<ClassSetup> GetAllClassNameByClassID(int mClassID)
        //{
        //    List<ClassSetup> obj = new List<ClassSetup>();
        //    obj = this.context.ClassSetups.Where(i => i.ClassID == mClassID).OrderBy(i => i.DisplayOrder).ToList();

        //    return obj;
        //}

        //public string GetClassNameByClassID(int classID)
        //{
           
        //    string mclassName = this.context.vClasses.Where(x => x.ClassID == classID).SingleOrDefault().ClassName;

        //    return mclassName ;
            
        //}
        
      

        //public int CheckDelete(int mID)
        //{
        //    int ID = 0;
        //    ID = this.context.StudentSessions.Where(x => x.ClassSetupID == mID).Count();
        //    return ID;
        //}

        
    }








}
