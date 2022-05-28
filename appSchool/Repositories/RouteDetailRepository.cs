using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class RouteDetailRepository : GenericRepository<RouteDetail>
    {
        public RouteDetailRepository() : base(new dbSchoolAppEntities()) { }
        public RouteDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }





        public List<RouteDetail> GetRouteDetailListByRouteID(int mRouteID,byte mCompID, byte mBranchID)
        {
            List<RouteDetail> obj = new List<RouteDetail>();
            obj = this.context.RouteDetails.Where(x => x.RouteID ==mRouteID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        public void UpdateProduct(RouteDetail product)
        {
            RouteDetail editProduct = this.GetByID(product.RouteDetailID);
            if (editProduct != null)
            {
                editProduct.OrderNo = product.OrderNo;
                editProduct.StopId = product.StopId;
                editProduct.StopName = this.context.BusStopMasters.Where(x => x.StopID == product.StopId).SingleOrDefault().StopName;
               
                this.Update(editProduct);
            }

        }

        public void DeleteProduct(int product)
        {
                this.Delete(product);
        }

        public void InsertProduct(RouteDetail product)
        {
            product.StopName = this.context.BusStopMasters.Where(x => x.StopID == product.StopId).SingleOrDefault().StopName;
            this.Insert(product);
        }
       
    }
  
}