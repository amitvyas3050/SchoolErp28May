using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class AccountMasterRepository: GenericRepository<AccountMaster>
    {
        public AccountMasterRepository() : base(new dbSchoolAppEntities()) { }
        public AccountMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<AccountMaster> GetAccountMasterList(byte mCompID, byte mBranchID)
        {
            List<AccountMaster> obj = new List<AccountMaster>();
            obj = this.context.AccountMasters.Where(x => x.CompID == mCompID && x.BranchId == mBranchID).ToList();
            if (obj == null)
            {
                List<AccountMaster> objNew = new List<AccountMaster>();
                return objNew;
            }
            else
            {
                return obj;
            }
        }

    public List<modelAccountMaster> GetAccountMasterGridData()
        {
            List<modelAccountMaster> lst =
                (from xx in this.context.AccountMasters
                 select new modelAccountMaster()
                 {
                     AcCode = xx.AcCode,
                     Name = xx.Name,
                     //Address = xx.Address,
                     //FeesCategoryID = xx.FeesCategoryID,

                 }
                 ).ToList<modelAccountMaster>();
            return lst;

        }
        public void AddNewAccountMasterInfo(modelAccountMaster obj)
        {
            AccountMaster newObj = new AccountMaster()
            {
                Name = obj.Name,
                //Address= obj.Address,
                //FeesCategoryID = obj.FeesCategoryID,
            };
            this.Insert(newObj);
        }
        public void UpdateAccountMasterInfo(modelAccountMaster obj)
        {
            AccountMaster newObj = this.GetByID(obj.AcCode);
            newObj.Name = obj.Name;
            //newObj.Address = obj.Address;
            //newObj.FeesCategoryID = obj.FeesCategoryID;
            this.Update(newObj);
        }

        public modelAccountPersonalInfo GetPersonalInfo(int AcCode)
        {
            modelAccountPersonalInfo obj = (
                from xx in this.context.AccountMasters
                where xx.AcCode == AcCode
                select new modelAccountPersonalInfo()
                {
                     AcCode = xx.AcCode,
                     Name= xx.Name,
                    // Address = xx.Address,
                     City=xx.City,
                     Pin = xx.Pin,
                     Phone1= xx.Phone1,
                     Phone2=xx.Phone2,
                     Mobile1=xx.Mobile1,
                     Mobile2= xx.Mobile2,
                     Email=xx.Email,
                     //FeesCategoryID =xx.FeesCategoryID,
                     OpBalance= xx.OpBalance,
                     DrAmount= xx.DrAmount,
                     CrAmount=xx.CrAmount,
                     Balance=xx.Balance,
                     CreateDate=xx.CreateDate,
                     PanNo=xx.PanNo,




                }).FirstOrDefault<modelAccountPersonalInfo>();
            return obj;
         }
        public void UpdateAccountProfileInfo(modelAccountPersonalInfo obj)
        {
            try
            {
                AccountMaster newInfo = this.GetByID(obj.AcCode);
              //  newInfo.Address = obj.Address;
                newInfo.AcCode = obj.AcCode;
                newInfo.Name = obj.Name;
                newInfo.City = obj.City;
                newInfo.Pin = obj.Pin;
                newInfo.Phone1 = obj.Phone1;
                newInfo.Phone2 = obj.Phone2;
                newInfo.Mobile1 = obj.Mobile1;               
                newInfo.Mobile2 = obj.Mobile2;
                newInfo.Email = obj.Email;
                //newInfo.FeesCategoryID = obj.FeesCategoryID;
                newInfo.OpBalance = obj.OpBalance;
                newInfo.DrAmount = obj.DrAmount;
                newInfo.CrAmount = obj.CrAmount;
                newInfo.Balance = obj.Balance;
                newInfo.CreateDate = obj.CreateDate;
                newInfo.PanNo = obj.PanNo;
                
                                
                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }

       
        public int AcCode { get; set; }
    }

 #region METADATA

    
   public class modelAccountMaster
    {
        [Key]
        public int AcCode { get; set; } // Has to have the same type and name as your model
        [Required(ErrorMessage = "Account Name is required")]
        public string Name { get; set; } // Has to have the same type and name as your model
        public string Address { get; set; } // Has to have the same type and name as your model
        public int FeesCategoryID { get; set; } // Has to have the same type and name as your model

    }


    public class modelAccountPersonalInfo
    {

        [Key]
        public int AcCode { get; set; }
        [Required, StringLength(120, ErrorMessage = "Name can not be more than 120 characters")]
        public string Name { get; set; } // Has to have the same type and name as your model
        [Required, StringLength(200, ErrorMessage = "Can not be more than 200 chars.")]
        public string Address { get; set; }
        [Required, StringLength(50, ErrorMessage = "Can not be more than 50 chars.")]
        public string City { get; set; }
        public string Pin { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone2 { get; set; }
        [Display(Name = "Mobile")]
        [StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]
        public string Mobile1 { get; set; }
        [Display(Name = "Mobile")]
        [StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]
        public string Mobile2 { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        [Required]
        public int  FeesCategoryID { get; set; }

        public decimal? OpBalance { get; set; }
        public decimal? DrAmount { get; set; }
        public decimal? CrAmount { get; set; }
        public decimal? Balance { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> CreateDate { get; set; }
        [Required]
        public string PanNo { get; set; }
      
    }

    #endregion
}
   

   