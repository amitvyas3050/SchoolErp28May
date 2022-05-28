using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class AdmissionFormSaleRepository: GenericRepository<AdmissionFormSale>
    {
        public AdmissionFormSaleRepository() : base(new dbSchoolAppEntities()) { }
        public AdmissionFormSaleRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<AdmissionFormSale> GetAdmissionFormSaleList(byte mCompID, byte mBranchID)
        {
            List<AdmissionFormSale> obj = new List<AdmissionFormSale>();
            obj = this.context.AdmissionFormSales.Where(x => x.FormID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        public void UpdateAdmissionFormSale(AdmissionFormSale obj)
        {
            AdmissionFormSale c = this.GetByID(obj.FormID);
            c.FormNo = obj.FormNo;
            c.Rate = obj.Rate;
            c.FirstName = obj.FirstName;
            c.MiddleName = obj.MiddleName;
            c.LastName = obj.LastName;
            c.FatherName = obj.FatherName;
            c.MotherName = obj.MotherName;
            c.ClassID = obj.ClassID;
            c.DateOfBirth = obj.DateOfBirth;
            c.Gender = obj.Gender;
            c.MobileNo = obj.MobileNo;
            c.EmailId = obj.EmailId;
            c.Address = obj.Address;
            c.IsAdmitted = obj.IsAdmitted;
            c.AdmitDate = obj.AdmitDate;
            c.UIDMod = obj.UIDMod;
            c.ModDate = DateTime.Now;
            this.Update(c);
            return;
        }

    }
    #region METADATA
    //[MetadataType(typeof(DepartmentMasterMetadata))]
    //public partial class DepartmentMaster
    //{
    //}

    //public class DepartmentMasterMetadata
    //{
    //    [Key]
    //    public short DepartmentID { get; set; } // Has to have the same type and name as your model
    //    [Required(ErrorMessage = "Department Name is required")]
    //    public string DepartmentName { get; set; } // Has to have the same type and name as your model

    //}
    #endregion
}