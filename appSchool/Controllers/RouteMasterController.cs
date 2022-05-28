using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.Repositories;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Web.SessionState;
using appSchool.ViewModels;

namespace appSchool.Controllers
{
    //[NoCache]
    public class RouteMasterController : Controller
    {
         private UnitOfWork unitOfWork = new UnitOfWork();

         public ActionResult RouteView()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appRouteMaster == 0)
            {
                return Redirect("~/");
            }
            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 42, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            ViewData["RouteID"] = 0;
            return PartialView("RouteView");
        }
         public ActionResult PartialRouteView()
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListRouteView", unitOfWork.routeMasterService.GetRouteMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

         public ActionResult PartialRouteDetailView(int pRouteID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             ViewData["RouteID"] = pRouteID;
             return PartialView("ListRouteDetailView", unitOfWork.routeDetailService.GetRouteDetailListByRouteID(pRouteID,byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }

         public ActionResult GetRouteDetailView(string pRouteID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             int mRouteID = int.Parse(pRouteID);

             List<RouteDetail> objlist = new List<RouteDetail>();

             objlist = unitOfWork.routeDetailService.GetRouteDetailListByRouteID(mRouteID,byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString()));
             ViewData["RouteID"] = pRouteID;
             return PartialView("ListRouteDetailView",objlist);
         }


         [HttpPost, ValidateInput(false)]
         public ActionResult AddNewRoute(RouteMaster obj)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {

                    obj.CompID = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    obj.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    obj.AddDate = DateTime.Now;
                    
                    unitOfWork.routeMasterService.Insert(obj);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = obj;
            return PartialView("ListRouteView", unitOfWork.routeMasterService.GetRouteMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
         public ActionResult UpdateRoute(RouteMaster obj)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    obj.UIDMod = byte.Parse(Session["UserID"].ToString());
                    obj.ModDate = DateTime.Now;

                    unitOfWork.routeMasterService.UpdateProduct(obj);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = obj;
            return PartialView("ListRouteView", unitOfWork.routeMasterService.GetRouteMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [ValidateInput(false)]
        public ActionResult UpdateRouteDetailAll(MVCxGridViewBatchUpdateValues<RouteDetail, int> updateValues, int pRouteID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            foreach (var product in updateValues.Insert)
            {
                if (updateValues.IsValid(product))
                    InsertProduct(product, updateValues,pRouteID);
            }
            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                    UpdateProduct(product, updateValues);
            }
            foreach (var productID in updateValues.DeleteKeys)
            {
                DeleteProduct(productID, updateValues);
            }
          
            return PartialView("ListRouteDetailView", unitOfWork.routeDetailService.GetRouteDetailListByRouteID(pRouteID,byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }
        protected void UpdateProduct(RouteDetail product, MVCxGridViewBatchUpdateValues<RouteDetail, int> updateValues)
        {
            try
            {
                unitOfWork.routeDetailService.UpdateProduct(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void DeleteProduct(int product, MVCxGridViewBatchUpdateValues<RouteDetail, int> updateValues)
        {
            try
            {
                unitOfWork.routeDetailService.DeleteProduct(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void InsertProduct(RouteDetail product, MVCxGridViewBatchUpdateValues<RouteDetail, int> updateValues, int mRouteID)
        {
            try
            {
                product.RouteID = mRouteID;
                product.BranchID = byte.Parse(Session["BranchID"].ToString());
                product.CompID = byte.Parse(Session["CompID"].ToString());

                unitOfWork.routeDetailService.InsertProduct(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteRoute(RouteMaster obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            try
            {
                unitOfWork.routeMasterService.Delete(obj);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("ListRouteView", unitOfWork.routeMasterService.GetRouteMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        public ActionResult RouteMasterGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            RouteMaster obj = new RouteMaster();

            obj = unitOfWork.routeMasterService.GetByID(ID);

            return PartialView("RouteDisplayView", obj);
        }
    }



    public class GridViewEditingDemosHelper
    {
        const string
            EditingModeSessionKey = "AA054892-1B4C-4158-96F7-894E1545C05C",
            BatchEditModeSessionKey = "E509E30E-99E3-4CB3-A07B-A08B04784A46",
            BatchStartEditActionSessionKey = "F2014F18-18A5-42F2-B713-B1538D1D1720";

        public static GridViewEditingMode EditMode
        {
            get
            {
                if (Session[EditingModeSessionKey] == null)
                    Session[EditingModeSessionKey] = GridViewEditingMode.EditFormAndDisplayRow;
                return (GridViewEditingMode)Session[EditingModeSessionKey];
            }
            set { HttpContext.Current.Session[EditingModeSessionKey] = value; }
        }

        static List<GridViewEditingMode> availableEditModesList;
        public static List<GridViewEditingMode> AvailableEditModesList
        {
            get
            {
                if (availableEditModesList == null)
                    availableEditModesList = new List<GridViewEditingMode> {
                        GridViewEditingMode.Inline,
                        GridViewEditingMode.EditForm,
                        GridViewEditingMode.EditFormAndDisplayRow,
                        GridViewEditingMode.PopupEditForm
                    };
                return availableEditModesList;
            }
        }

        public static GridViewBatchEditMode BatchEditMode
        {
            get
            {
                if (Session[BatchEditModeSessionKey] == null)
                    Session[BatchEditModeSessionKey] = GridViewBatchEditMode.Cell;
                return (GridViewBatchEditMode)Session[BatchEditModeSessionKey];
            }
            set { Session[BatchEditModeSessionKey] = value; }
        }
        public static GridViewBatchStartEditAction BatchStartEditAction
        {
            get
            {
                if (Session[BatchStartEditActionSessionKey] == null)
                    Session[BatchStartEditActionSessionKey] = GridViewBatchStartEditAction.Click;
                return (GridViewBatchStartEditAction)Session[BatchStartEditActionSessionKey];
            }
            set { Session[BatchStartEditActionSessionKey] = value; }
        }
        protected static HttpSessionState Session { get { return HttpContext.Current.Session; } }

    }

  


}



    