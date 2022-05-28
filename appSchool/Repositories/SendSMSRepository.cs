using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using DevExpress.Web;

namespace appSchool.Repositories
{
    public class SendSMSRepository : GenericRepository<ClassSetup> 
    {
        public SendSMSRepository() : base(new dbSchoolAppEntities()) { }
        public SendSMSRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }
       
        public void AddNewClassSetup(ClassSetup obj)
        {
            obj.ClassDescription = SetDescription(obj);
            Insert(obj);
            return;
        }
        public void UpdateClassSetup(ClassSetup obj)
        {
            obj.ClassDescription = SetDescription(obj);
            Update(obj);
            return;
        }
        private string SetDescription(ClassSetup obj)
        {
            string strDescription = string.Empty;
            strDescription += this.context.Classes.Where(x => x.ClassID == obj.ClassID).FirstOrDefault().ClassName+" ";
            strDescription += this.context.Sections.Where(x => x.SectionID == obj.SectionID).FirstOrDefault().SectionName + "  ";
            if(obj.ClassCategoryID!=null)
                strDescription += this.context.ClassCategories.Where(x => x.ClassCategoryID == obj.ClassCategoryID).FirstOrDefault().ClassCategoryName;
            return strDescription;
        }

        public MVCxTreeListNode GetSMSTreeItems()
        {
            MVCxTreeListNode childNode;
            MVCxTreeListNode rootNode = (new MVCxTreeListNodeCollection()).Add(-1, new Dictionary<string, object> { { "Name", "Choose Template" },{ "IconName", "TemplateIcon" }});
            List<listItem> lstTypes =(from xx in this.context.SMSTypes select new listItem(){ Value= xx.SMSTypeID, Description= xx.SMSTypeName}).ToList();
            foreach (listItem mItem in  lstTypes)
            {
                int mParentID= mItem.Value;
                childNode= rootNode.ChildNodes.Add(mItem.Value, new Dictionary<string, object> { { "Name", mItem.Description},{ "IconName", "TemplateIcon" }});
                List<listItem> lstTemplates = (new SMSTemplateRepository()).GetSMSTemplateItemsforSMSType(mItem.Value).ToList();
                foreach(listItem itm in lstTemplates)
                {
                    childNode.ChildNodes.Add((mParentID * 1000) + itm.Value, new Dictionary<string, object> { { "Name", itm.Description }, { "IconName", "TemplateIcon" }});
                }
                childNode.Expanded = true;
            }
            rootNode.Expanded = true;
            return rootNode;
        }

         
    }








}
