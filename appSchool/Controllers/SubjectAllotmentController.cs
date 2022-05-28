using appSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;


namespace appSchool.Controllers
{
    [NoCache]
    public class SubjectAllotmentController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appSubjectAllotment == 0)
            {
                return Redirect("~/");
            }


            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 11, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            if (objuser != null)
            {
                PermissionFlag._AddFlag = objuser.AddP;
                PermissionFlag._ModFlag = objuser.ModP;
                PermissionFlag._DelFlag = objuser.DelP;
            }
            else
            {
                PermissionFlag._AddFlag = false;
                PermissionFlag._ModFlag = false;
                PermissionFlag._DelFlag = false;
            }



            return PartialView("Index");
        }


        public ActionResult PartialGridSubjectLevelOne(int PClassID, int mSubjectlevel)
        {
            ViewData["ClassID"] = PClassID;
            ViewData["SubjectLevel"] = mSubjectlevel;

            return PartialView("ListForSubjectlevelOne", new UnitOfWork().SubjectAllotmentService.GetSubjectlevelOneList( byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult PartialGridSubjectLeveTwo(int PClassID, int mSubjectlevel)
        {
            ViewData["ClassID"] = PClassID;
            ViewData["SubjectLevel"] = mSubjectlevel;
            return PartialView("ListForSubjectLevelTwo", new UnitOfWork().SubjectAllotmentService.GetSubjectlevelTwoList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult PartialGridSubjectLevelThree(int PClassID, int mSubjectlevel)
        {
            ViewData["ClassID"] = PClassID;
            ViewData["SubjectLevel"] = mSubjectlevel;
            return PartialView("ListForSubjectLevelThree", new UnitOfWork().SubjectAllotmentService.GetSubjectlevelThreeList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult PartialGridSubjectAllotment(int PClassID, int PSubjectLevel)
        {
            ViewData["ClassID"] = PClassID;
            ViewData["SubjectLevel"] = PSubjectLevel;
            return PartialView("ListForSubjectAllotment", new UnitOfWork().SubjectAllotmentService.GetSubjectAllotmentList(PClassID,PSubjectLevel));
        }

        public JsonResult GetSubjectlevelbyClassID(string MClassID)
        {
            byte mSubjectLevel = 0;
            string mSubjectLevelName = string.Empty;
           
            ViewData["ClassIDForSubjectAllotment"] = int.Parse(MClassID);

            Class objclass = new Class();
            objclass = unitOfWork.SubjectAllotmentService.GetSubjectLevelByClassID(int.Parse(MClassID));

            if (objclass != null)
            {
                mSubjectLevel = byte.Parse(objclass.SubjectLevel.ToString());
                if (mSubjectLevel == 1)
                {
                    mSubjectLevelName = "Subject level One";
                }
                else
                {
                    if (mSubjectLevel == 2)
                    {
                        mSubjectLevelName = "Subject level Two";

                    }

                    else
                    {
                        mSubjectLevelName = "Subject Level Three";
                    }
                }
               
            }


            return Json(new { ResultSubLevel = mSubjectLevel, ResultSubLevelName = mSubjectLevelName }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowSubjectForClass(int PClassID, int PSubjectlevel)
        {
            string Errormsg = string.Empty;
            int  ResultSubjectLevel = 1;
            ViewData["ClassID"] = PClassID;
            ViewData["SubjectLevel"] = PSubjectlevel;



            if (PSubjectlevel == 1)
            {
                //ViewData["SubjectLevel"] = PSubjectlevel;
                ResultSubjectLevel = 1;
                List<SubjectLevelOne> lst = unitOfWork.SubjectAllotmentService.GetSubjectlevelOneListByClassID(PClassID, PSubjectlevel, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                List<SubjectAllotment> lstSubAllot = unitOfWork.SubjectAllotmentService.GetSubjectAllotmentList(PClassID, PSubjectlevel);
                
                return Json(new { DisplaySubjectLevel = ResultSubjectLevel, ListData = cCommon.RenderRazorViewToString("ListForSubjectlevelOne", lst, ControllerContext, ViewData, TempData),
                                  ListDataSuballot = cCommon.RenderRazorViewToString("ListForSubjectAllotment", lstSubAllot, ControllerContext, ViewData, TempData)
                                   }, JsonRequestBehavior.AllowGet);
    
            }

         

             else
            {
                if (PSubjectlevel == 2)
                {
                  
                    ResultSubjectLevel = 2;
                    List<vSubjectAllotmentwithIDLTwo> lst2 = unitOfWork.SubjectAllotmentService.GetSubjectlevelTwoListByClassID(PClassID, PSubjectlevel, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                    List<SubjectAllotment> lstSubAllot = unitOfWork.SubjectAllotmentService.GetSubjectAllotmentList(PClassID, PSubjectlevel);

                
                    return Json(new { DisplaySubjectLevel = ResultSubjectLevel, ListData = cCommon.RenderRazorViewToString("ListForSubjectLevelTwo", lst2, ControllerContext, ViewData, TempData),
                                      ListDataSuballot = cCommon.RenderRazorViewToString("ListForSubjectAllotment", lstSubAllot, ControllerContext, ViewData, TempData)
                                        }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                  

                    ResultSubjectLevel = 3;
                    List<vSubjectAllotmentwithIDLThree> lst3 = unitOfWork.SubjectAllotmentService.GetSubjectlevelThreeListByClassID(PClassID, PSubjectlevel, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                    List<SubjectAllotment> lstSubAllot = unitOfWork.SubjectAllotmentService.GetSubjectAllotmentList(PClassID, PSubjectlevel);
                    
                    return Json(new { DisplaySubjectLevel = ResultSubjectLevel, ListData = cCommon.RenderRazorViewToString("ListForSubjectLevelThree", lst3, ControllerContext, ViewData, TempData),
                                      ListDataSuballot = cCommon.RenderRazorViewToString("ListForSubjectAllotment", lstSubAllot, ControllerContext, ViewData, TempData)
                    }, JsonRequestBehavior.AllowGet);
    
                }

            }

          
        
        
        }

        public ActionResult AllotmentSubjectForClass(int ClassID, int pSubjectlevel, string PSubjectlevelID)
        {
            string Errormsg = string.Empty;
            int NewID = 0;

            try
            {
                string[] StudentIDList = PSubjectlevelID.Split(',');
                 foreach (string SubjectlevelID in StudentIDList)
                 {
                     int MSubjectLevelID = int.Parse(SubjectlevelID);

                     if (pSubjectlevel == 1)
                     {


                         int Subjectlevel2 = -1;
                         int Subjectlevel3 = -1;
                         int i = int.Parse(SubjectlevelID);

                         NewID = AddSubjectAllotment(ClassID, i, Subjectlevel2, Subjectlevel3);

                         if (NewID > 0)
                         {
                             Errormsg = "Subject Alloted";
                         }
                         else
                         {
                             Errormsg = "Already Subject Alloted For this Class";
                         }
                     }



                     else
                     {
                         if (pSubjectlevel == 2)
                         {
                             int mSubjectLevel1 = 0;
                             int mSubjectLevel3 = -1;
                             SubjectLevelTwo objsubl1 = new SubjectLevelTwo();
                             objsubl1 = unitOfWork.SubjectAllotmentService.GetSubjectlevelOneByLevelTwo(MSubjectLevelID);
                             if (objsubl1 != null)
                             {
                                 mSubjectLevel1 = objsubl1.IdL1;
                             }

                             NewID = AddSubjectAllotment(ClassID, mSubjectLevel1, MSubjectLevelID, mSubjectLevel3);

                             if (NewID > 0)
                             {
                                 Errormsg = "Subject Alloted";
                             }
                             else
                             {
                                 Errormsg = "Already Subject Alloted For this Class";
                             }

                         }

                         else
                         {

                             int mSubjectLevel1 = 0;
                             int mSubjectLevel2 = 0;
                             SubjectLevelThree objsub3 = new SubjectLevelThree();
                             objsub3 = unitOfWork.SubjectAllotmentService.GetSubjectlevelOneAndTwoByLevelThree(MSubjectLevelID);
                             if (objsub3 != null)
                             {

                                 mSubjectLevel2 = objsub3.IdL2;
                                 SubjectLevelTwo objsubl2 = new SubjectLevelTwo();

                                 objsubl2 = unitOfWork.SubjectAllotmentService.GetSubjectlevelOneByLevelTwo(mSubjectLevel2);
                                 if (objsubl2 != null)
                                 {
                                     mSubjectLevel1 = objsubl2.IdL1;
                                 }
                             }

                             NewID = AddSubjectAllotment(ClassID, mSubjectLevel1, mSubjectLevel2, MSubjectLevelID);

                             if (NewID > 0)
                             {
                                 Errormsg = "Subject Alloted";
                             }
                             else
                             {
                                 Errormsg = "Already Subject Alloted For this Class";
                             }
                         }

                     }


                 }   
               



            }

            catch (Exception e)
            {
                // updateValues.SetErrorText(product, e.Message);
            }

            ViewData["ClassID"]  = ClassID;
            ViewData["SubjectLevel"] = pSubjectlevel;

            List<SubjectAllotment> lst = unitOfWork.SubjectAllotmentService.GetSubjectAllotmentList(ClassID, pSubjectlevel);
            return Json(new { Displaymsg = Errormsg, ListData = cCommon.RenderRazorViewToString("ListForSubjectAllotment", lst, ControllerContext, ViewData, TempData) }, JsonRequestBehavior.AllowGet);
    
        }


        public int AddSubjectAllotment(int mClassID, int SubjLOne, int SubjLTwo, int SubjLThree)
        {
            int Res = 0;

            SqlCommand cmd = new SqlCommand("Add_SubjectAllotment", DB.GetActiveConnection());
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@ClassID", mClassID);
            cmd.Parameters.AddWithValue("@IDL1", SubjLOne);
            cmd.Parameters.AddWithValue("@IDL2", SubjLTwo);
            cmd.Parameters.AddWithValue("@IDL3", SubjLThree);
            cmd.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmd.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            Res = cmd.ExecuteNonQuery();
            return Res;
        }
    
    
    
    
    
    
    
    
    
    
    }


}
