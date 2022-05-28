using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class RouteMasterRepository : GenericRepository<RouteMaster>
    {
        public RouteMasterRepository() : base(new dbSchoolAppEntities()) { }
        public RouteMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<RouteMaster> GetRouteMasterList(byte mCompID, byte mBranchID)
        {
            List<RouteMaster> obj = new List<RouteMaster>();
            obj = this.context.RouteMasters.Where(x => x.RouteID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        public  void UpdateProduct(RouteMaster product)
        {
            RouteMaster editProduct = this.GetByID(product.RouteID);
            if (editProduct != null)
            {
                editProduct.RouteName = product.RouteName;
                editProduct.Description = product.Description;
                editProduct.ModDate = product.ModDate;
                editProduct.UIDMod = product.UIDMod;
                this.Update(editProduct);
            }

        }
        public void DeleteProduct(RouteMaster product)
        {
            RouteMaster editProduct = this.GetByID(product.RouteID);
            if (editProduct != null)
            {
                this.Delete(editProduct);
            }

        }
        public void InsertProduct(RouteMaster product)
        {
            RouteMaster editProduct = new RouteMaster();
            if (editProduct != null)
            {
                editProduct.RouteName = product.RouteName;
                editProduct.Description = product.Description;
                this.Insert(editProduct);
            }

        }
       
    }
    #region METADATA
    [MetadataType(typeof(RouteMasterMetadata))]
    public partial class RouteMaster
    {
    }

    public class RouteMasterMetadata
    {
        [Key]
        public short RouteID { get; set; } // Has to have the same type and name as your model
        [Required(ErrorMessage = "Route Name is required")]
        public string RouteName { get; set; } // Has to have the same type and name as your model
        public string Description { get; set; }
    }
    #endregion
}