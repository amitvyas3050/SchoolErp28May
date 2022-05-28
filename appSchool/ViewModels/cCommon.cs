using appSchool.Repositories;
using DevExpress.Web.ASPxThemes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
//
namespace appSchool.ViewModels
{
    public enum BirthdayType
    {
        Student = 1,
        Father = 2,
        Mother = 3,
        Anniverssary = 4

    }
    public class listItem
    {
        public int Value { get; set; }
        public string Description { get; set; }
    }

    public class listItemMonth
    {
        public int MonthID { get; set; }
        public string MonthName { get; set; }
    }

   public class myIcons
   {
       
       public myIcons()
       {
           
       }
       IconID mIDs = new IconID();

       
       public IconID ICON { get; set; }
       public string IconName { get; set; }
       
   }

    public class cCommon
    {
        public cCommon() { }
        public static string EncryptPwdkey = "eeei-3hn8-solu19";
        public static Dictionary<string, string> GetBloodGroupList()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("A+ve", "A+ve");
            lst.Add("A-ve", "A-ve");
            lst.Add("B+ve", "B+ve");
            lst.Add("B-ve", "B-ve");
            lst.Add("AB+ve", "AB+ve");
            lst.Add("AB-ve", "AB-ve");
            lst.Add("O+ve", "O+ve");
            lst.Add("O-ve", "O-ve");
            return lst;
        }
        public static Dictionary<string, string> GetExportList()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("Pdf", "Pdf");
            lst.Add("Excel", "Excel");
            lst.Add("Mht", "Mht");
            lst.Add("Rtf", "Rtf");
            lst.Add("Text", "Text");
            lst.Add("Html", "Html");
          
            return lst;
        }

        public static Dictionary<string, string> GetTopperstudentType()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("Toppers", "Toppers");
            lst.Add("ClassToppers", "ClassToppers");
            return lst;
        }


        public static Dictionary<string, string> GetSessionName()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("2015-2016", "2015-2016");
            lst.Add("2016-2017", "2016-2017");
            lst.Add("2017-2018", "2017-2018");

            return lst;
        }

       
        public static Dictionary<string, string> GetMarkType()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("Number", "Number");
            lst.Add("Grade", "Grade");
            lst.Add("Remark", "Remark");

            return lst;
        }
        public static Dictionary<string, string> GetNewsCategoryList()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("News", "News");
            lst.Add("Event", "Event");
            return lst;
        }

        public static Dictionary<string, string> GetDesignationType()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("Teaching", "Teaching");
            lst.Add("NonTeaching", "NonTeaching");

            return lst;
        }
        public static Dictionary<string, string> GetDocumentTypeList()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("Application Form", "Application Form");
            lst.Add("10th Marksheet", "10th Marksheet");
            lst.Add("12th Marksheet", "12th Marksheet");
            lst.Add("Graduation Marksheet", "Graduation Marksheet");
            lst.Add("IncomeCertificate", "IncomeCertificate");
            lst.Add(" Samagra ID", " Samagra ID");
            lst.Add("AAdhar Card", "AAdhar Card");
            lst.Add("Cast Certificate", "Cast Certificate");
            lst.Add("Domicile Certificate ", "Domicile Certificate ");
            lst.Add("Bank Passbook", "Bank Passbook");

            return lst;
        }

        public static Dictionary<string, string> GetEmpTypeForDriver()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("Driver", "Driver");
            lst.Add("Cleaner", "Cleaner");

            return lst;
        }


        public static Dictionary<string, string> GetMediumTypeList()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("English","English");
            lst.Add("Hindi", "Hindi");
            return lst;
        }

        public static Dictionary<string, string> GetGenderList()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("M", "Male");
            lst.Add("F", "Female");
            return lst;
        }
        public static Dictionary<string, string> GetAttendanceTypeList()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("P", "Present");
            lst.Add("A", "Absent");
            lst.Add("L","Leave");
            return lst;
        }

        public static Dictionary<int, int> GetClassSubjectLevelList()
        {
            Dictionary<int, int> lst = new Dictionary<int, int>();
            lst.Add(1,1);
            lst.Add(2, 2);
            lst.Add(3, 3);
            return lst;
        }

       


        public static Dictionary<int, string> GetStudentSessionSelectList()
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();
            lst.Add(1, "House");
            lst.Add(2, "Class Description");
            lst.Add(3, "SMS Setting");
            return lst;
        }


        public static Dictionary<int, string> GetFeesCategoryIDList()
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();
            lst.Add(1, "Admission Fees");
            lst.Add(2, "Library Fees");
            lst.Add(3, "Tution Fees");
            lst.Add(4, "Exam Fees");
            lst.Add(5, "Sports Fees");
            lst.Add(6, "School Maintenance Fees");
            lst.Add(7, "Transport Fees");
            return lst;
        }


        public static Dictionary<string, string> GetNoticeBoard()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("All", "All");
            lst.Add("Teacher", "Teacher");
            lst.Add("Student", "Student");
            lst.Add("Parents", "Parents");

            return lst;
        }




        public static Dictionary<int, string> GetLanguageList()
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();
            lst.Add(1, "English");
            lst.Add(2, "Hindi");
           
            return lst;
        }

   
        public static Dictionary<string, string> GetReligionList()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("Hindu", "Hindu");
            lst.Add("Muslim", "Muslim");
            lst.Add("Sikh", "Sikh");
            lst.Add("Christians", "Christians");

            return lst;
        }
        public static Dictionary<string, string> GetPayModeListForFee()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("Cash", "Cash");
            lst.Add("Cheque", "Cheque");

            return lst;
        }

        public static Dictionary<string, string> GetDiscountTypeList()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("F", "Fix");
            lst.Add("P", "Percentage");
            return lst;
        }

        public static Dictionary<string, string> GetMaritalStatus()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("Married", "Married");
            lst.Add("UnMarried", "UnMarried");
            return lst;
        }
        public static Dictionary<int, string> GetWishesTypeList()
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();
            lst.Add(1, "Student Birthday");
            lst.Add(2, "Father Birthday");
            lst.Add(3, "Mother Birthday");
            lst.Add(4, "Parent's anniversary");

            return lst;
        }

        public static Dictionary<int, string> GetWishesTypeListForTeacher()
        {
            Dictionary<int, string> lst = new Dictionary<int, string>();
            lst.Add(3, "Teacher All");
            lst.Add(1, "Teacher Birthday");
            lst.Add(2, "Teacher anniversary");
           
          
            return lst;
        }
        public static Dictionary<string, string> GetFeesHeadType()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("OneTime", "OneTime");
            lst.Add("Optional", "Optional");
            lst.Add("Compulsory", "Compulsory");
            return lst;
        }
        public static Dictionary<string, string> GetSMSModelType()
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            lst.Add("All", "All Model");
            lst.Add("GeneralSMS", "General SMS");
            lst.Add("StudentSMS", "Student SMS");
            lst.Add("AbsenteesSMS", "Absentees SMS");
            lst.Add("WishesSMS", "Wishes SMS");
            lst.Add("FeesDefaulterSMS", "FeesDefaulter SMS");
            lst.Add("GeneralScheduledSMS", "GeneralScheduledSMS");
            lst.Add("SchedulerWishesSMS", "SchedulerWishesSMS");
            
            return lst;
        }



        public static List<string> GetIconNames(Enum nameEnum)
        {
            List<string> strNames=new List<string>();
            //Array itemValues = System.Enum.GetValues(typeof(IconID));
            Array itemNames = System.Enum.GetNames(nameEnum.GetType());
            foreach( string str in itemNames)
            strNames.Add(str);
            return strNames;
        }


        public static IEnumerable<listItemMonth> GetMonthNameList()
        {
            List<listItemMonth> lst = new List<listItemMonth>();
            lst.Add(new listItemMonth() { MonthID = 0, MonthName = "All" });
            lst.Add(new listItemMonth() { MonthID = 1, MonthName = "January" });
            lst.Add(new listItemMonth() { MonthID = 2, MonthName = "February" });
            lst.Add(new listItemMonth() { MonthID = 3, MonthName = "March" });
            lst.Add(new listItemMonth() { MonthID = 4, MonthName = "April" });
            lst.Add(new listItemMonth() { MonthID = 5, MonthName = "May" });
            lst.Add(new listItemMonth() { MonthID = 6, MonthName = "june" });
            lst.Add(new listItemMonth() { MonthID = 7, MonthName = "July" });
            lst.Add(new listItemMonth() { MonthID = 8, MonthName = "August" });
            lst.Add(new listItemMonth() { MonthID = 9, MonthName = "September" });
            lst.Add(new listItemMonth() { MonthID = 10, MonthName = "October" });
            lst.Add(new listItemMonth() { MonthID = 11, MonthName = "November" });
            lst.Add(new listItemMonth() { MonthID = 12, MonthName = "December" });
           
           
            return lst;
        }

        public static string RenderRazorViewToString(string viewName, object model, ControllerContext controllerContext, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            viewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var viewContext = new ViewContext(controllerContext, viewResult.View, viewData, tempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }


        public static void GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath)
        {
            using (var image = Image.FromStream(sourcePath))
            {
                var newWidth = (int)(image.Width * scaleFactor);
                var newHeight = (int)(image.Height * scaleFactor);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
            }
        }

        public static string Encrypt(string input, string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string input, string key)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }  


    }

 

    public static class SchoolSetupStaticClass
    {

    
        public static string _RegistrationNo=string.Empty;
        public static string _SchoolName = string.Empty;
        public static string _Address1 = string.Empty;
        public static string _Address2 = string.Empty;
        public static string _City = string.Empty;
        public static string _State = string.Empty;
        public static string _PhoneNo1 = string.Empty;
        public static string _PhoneNo2 = string.Empty;
        public static string _EmailID = string.Empty;
        public static string _Remark = string.Empty;
        public static bool _BusFacility =false;
        public static bool _HostelFacility = false;
        public static bool _MessFacility = false;
        public static string _ThemesName =string.Empty;
        public static int _HouseColor =-1;

    }

    public static class SettingMasterStaticClass
    {
        public static bool _IncludeNameInSMS = false;
        public static bool _SaveSMSLog = false;
        public static bool _ManageHistory = false;


    }




    public static class StudentDataExportSC
    {
      
        public static bool _PersonalInfo = false;
        public static bool _ParentsInfo = false;
        public static bool _GardianInfo = false;


    }

    public static class PermissionFlag
    {
        public static bool _AddFlag = false;
        public static bool _ModFlag = false;
        public static bool _DelFlag = false;

    }


}