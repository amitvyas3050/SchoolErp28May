using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class SMSTemplateRepository : GenericRepository<SMSTemplate> 
    {
        public SMSTemplateRepository () : base(new dbSchoolAppEntities()) { }
        public SMSTemplateRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<SMSTemplate> GetSMSTemplateList(byte mCompID, byte mBranchID)
        {
            List<SMSTemplate> obj = new List<SMSTemplate>();
            obj = this.context.SMSTemplates.Where(x => x.TemplateID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public void UpdateSMSTemplate(SMSTemplate obj)
        {
            SMSTemplate objNew = this.GetByID(obj.TemplateID);
            if (objNew != null)
            {
                objNew.LanguageID = obj.LanguageID;
                objNew.ModDate = obj.ModDate;
                objNew.PreFix = obj.PreFix;
                objNew.PreFixH = obj.PreFixH;
                objNew.SMSTypeID = obj.SMSTypeID;
                objNew.TemplateMessage = obj.TemplateMessage;
                objNew.TemplateMessageH = obj.TemplateMessageH;
                objNew.TemplateName = obj.TemplateName;
                objNew.UIDMod = obj.UIDMod;

                this.Update(objNew);
            }
            return;
        }

        public IEnumerable<listItem> GetSMSTemplateItemsforSMSType(int typeID)
        {
            List<listItem> lst = new List<listItem>();
            lst.AddRange(
                from xx in this.context.SMSTemplates
                where xx.SMSTypeID==typeID
                select new listItem() { Value = xx.TemplateID, Description = xx.TemplateName }
                );
            return lst;
        }

        public SMSTemplate GetSMSTemplateEmployeeAbsent(int templateID, byte mCompID, byte mBranchID)
        {
            SMSTemplate obj = this.context.SMSTemplates.SqlQuery("SELECT * FROM dbo.SMSTemplate where TemplateID=" + templateID + "  AND CompID=" + mCompID + " AND BranchID=" + mBranchID + " ").FirstOrDefault();
            return obj;
        }

        public SMSTemplate GetSMSTemplateEmployeeLate(int typeID, byte mCompID, byte mBranchID)
        {
            SMSTemplate obj = this.context.SMSTemplates.SqlQuery("SELECT * FROM dbo.SMSTemplate where SMSTypeID=" + typeID + " And TemplateName='Employee Late' AND CompID=" + mCompID + " AND BranchID=" + mBranchID + " ").FirstOrDefault();
            return obj;
        }

        public IEnumerable<SMSTemplate> GetSMSTemplateforSMSType(int typeID, byte mCompID, byte mBranchID)
        {
            IEnumerable<SMSTemplate> obj = this.context.SMSTemplates.SqlQuery("SELECT * FROM dbo.SMSTemplate where SMSTypeID=" + typeID + " AND CompID="+mCompID +" AND BranchID="+mBranchID +" ").ToList();
            return obj;
        }


        public IEnumerable<SMSTemplate> GetSMSTemplateListwithoutAbsent(byte mCompID, byte mBranchID)
        {
            IEnumerable<SMSTemplate> obj = this.context.SMSTemplates.SqlQuery("SELECT * FROM dbo.SMSTemplate where SMSTypeID!=1  AND CompID="+mCompID+" AND BranchID="+mBranchID+" ").ToList();
            return obj;
        }

        public IEnumerable<SMSTemplate> GetSMSTemplateListForWishesStudent(byte mCompID, byte mBranchID)
        {
            IEnumerable<SMSTemplate> obj = this.context.SMSTemplates.SqlQuery("SELECT * FROM dbo.SMSTemplate where SMSTypeID in (2,3) AND CompID="+mCompID+" AND BranchID="+mBranchID+" ").ToList();
            return obj;
        }

        public IEnumerable<SMSTemplate> GetSMSTemplateListForLoginStudent(byte mCompID, byte mBranchID)
        {
            IEnumerable<SMSTemplate> obj = this.context.SMSTemplates.SqlQuery("SELECT * FROM dbo.SMSTemplate where SMSTypeID in (8) AND CompID=" + mCompID + " AND BranchID=" + mBranchID + " ").ToList();
            return obj;
        }

        public IEnumerable<SMSTemplate> GetSMSTemplateListForTeacher(byte mCompID, byte mBranchID)
        {
            IEnumerable<SMSTemplate> obj = this.context.SMSTemplates.Where(x => x.SMSTypeID == 6 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public IEnumerable<SMSTemplate> GetSMSTemplateListForGeneral(byte mCompID, byte mBranchID)
        {
            IEnumerable<SMSTemplate> obj = this.context.SMSTemplates.Where(x=>x.SMSTypeID==5 && x.CompID==mCompID && x.BranchID==mBranchID).ToList();
            return obj;
        }

        public IEnumerable<SMSTemplate> GetSMSTemplateListForLoginTeacher(byte mCompID, byte mBranchID)
        {
            IEnumerable<SMSTemplate> obj = this.context.SMSTemplates.SqlQuery("SELECT * FROM dbo.SMSTemplate where SMSTypeID in (9) AND CompID=" + mCompID + " AND BranchID=" + mBranchID + " ").ToList();
            return obj;
        }


        public modelSendSms GetSendSMSModele ()
        {
            modelSendSms obj = new modelSendSms() ;

            obj.PrefixEnglish = ".";
            obj.PrefixHindi = ".";
            obj.smsTextEnglish = ".";
            obj.smsTextHindi = ".";
            obj.smsMobileNo = string.Empty;
            obj.smsStudentID = string.Empty;
            obj.OrderedBy = 0;
            obj.smsCopy = false;
            obj.smsAdminCopy = false;
            obj.includeName = true;
            obj.SMSLanguage = 0;
            obj.TemplateType = 0;

            return obj;
        }

        

    }


    #region SMS Template Metadata
    [MetadataType(typeof(SMSTemplateMetadata))]
    public partial class SMSTemplate
    {
    }

    public class SMSTemplateMetadata
    {
        [Key]
        public int TemplateID { get; set; } // Has to have the same type and name as your model
         [Required(ErrorMessage = "TemplateName is required")]
        public string TemplateName { get; set; } // Has to have the same type and name as your model
        public int LanguageID { get; set; } // Has to have the same type and name as your model
        public int SMSTypeID { get; set; } // Has to have the same type and name as your model
      
        [Required(ErrorMessage = "TemplateMessage is required")]
        public string TemplateMessage { get; set; } // Has to have the same type and name as your model

    }
    #endregion
  


}
