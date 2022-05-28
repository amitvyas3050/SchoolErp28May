using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace appSchool.Repositories
{
    public class SchoolMasterRepository : GenericRepository<SchoolMaster>
    {
        public SchoolMasterRepository() : base(new dbSchoolAppEntities()) { }
        public SchoolMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        public SchoolMaster GetSchoolSetup()
        {
            SchoolMaster obj1 = this.context.SchoolMasters.FirstOrDefault();
            if (obj1 == null)
            {
                SchoolMaster objnew = new SchoolMaster();
                obj1 = objnew;
            }
            return obj1;
        }

        public SchoolMaster SchoolLogo()
        {
            SchoolMaster obj1 = this.context.SchoolMasters.FirstOrDefault();

            if (obj1 != null)
            {
                if (obj1.Logo == null)
                {

                    string startupPath = AppDomain.CurrentDomain.BaseDirectory;
                    string targetPath = startupPath + "\\Images\\blank.jpg";
                    string imageLocation = targetPath;
                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(imageLocation);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);
                    obj1.Logo = imageData;

                }
            }
            else
            {
                SchoolMaster objNew = new SchoolMaster();

                string startupPath = AppDomain.CurrentDomain.BaseDirectory;
                string targetPath = startupPath + "\\Images\\blank.jpg";
                string imageLocation = targetPath;
                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(imageLocation);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                objNew.Logo = imageData;

                obj1 = objNew;
            }

            return obj1;
        }


        public byte[] GetSchoolLogo()
        {
            byte[] mLogo = null;


            return mLogo;
        }


        public void UpdateSchoolProfile(SchoolMaster obj, int UserID)
        {
            SchoolMaster newObj = this.GetByID(obj.SchoolID);
            newObj.Address1 = obj.Address1;
            newObj.Address2 = obj.Address2;
            newObj.BusFacility = obj.BusFacility;
            newObj.City = obj.City;
            newObj.EmailID = obj.EmailID;
            newObj.HostelFacility = obj.HostelFacility;
            newObj.MessFacility = obj.MessFacility;
            newObj.PhoneNo1 = obj.PhoneNo1;
            newObj.PhoneNo2 = obj.PhoneNo2;
            newObj.RegDate = obj.RegDate;
            newObj.RegistrationNo = obj.RegistrationNo;
            newObj.Remark = obj.Remark;
            newObj.SchollerNo = obj.SchollerNo;
            newObj.SchoolName = obj.SchoolName;
            newObj.State = obj.State;
            newObj.HostelFacility = obj.HostelFacility;
            
         
            this.Update(newObj);
        }

        public void UpdateLogo(SchoolMaster obj)
        {
            try
            {
                SchoolMaster newInfo = this.GetByID(obj.SchoolID);
                //newInfo.EnrollmentNo = obj.EnrollmentNo;
                //newInfo.EnrollmentDate = obj.EnrollmentDate;
                newInfo.Logo = obj.Logo;
                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
  
}