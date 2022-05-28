using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class vStudentDataExportRepository : GenericRepository<vStudentDataExport> 
    {
        public vStudentDataExportRepository() : base(new dbSchoolAppEntities()) { }
        public vStudentDataExportRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<vStudentDataExport> GetStudentListByClassSetupIDs(string mClassSetupID, int mSessionID, byte mCompID, byte mBranchID)
        {
            // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();

            //string sql = "SELECT        dbo.StudentRegistration.StudentID, dbo.StudentRegistration.EnrollmentNo, dbo.StudentRegistration.EnrollmentDate, dbo.StudentRegistration.FirstName, " +
            //             " dbo.StudentRegistration.MiddleName, dbo.StudentRegistration.LastName, dbo.StudentRegistration.FirstName + '  ' + ISNULL(dbo.StudentRegistration.LastName, 0)  " +
            //             " AS FullName, dbo.StudentRegistration.ICardNo, (CASE dbo.StudentRegistration.Gender WHEN 'F' THEN 'Female' WHEN 'M' THEN 'Male' END) AS Gender,  " +
            //             " dbo.StudentRegistration.DateOfBirth, dbo.StudentRegistration.City, dbo.StudentRegistration.Nationality, dbo.StudentRegistration.MotherTounge,  " +
            //             " dbo.StudentRegistration.ClassID, dbo.StudentRegistration.LocalAddress, dbo.StudentRegistration.ParmanentAddress, dbo.StudentRegistration.Caste,  " +
            //             " dbo.StudentRegistration.BloodGroup, dbo.StudentRegistration.Religion, dbo.StudentRegistration.TransportRequired, dbo.StudentRegistration.HostelRequired,  " +
            //             " dbo.StudentRegistration.EnableSMSFeature, dbo.StudentRegistration.TCGiven, dbo.StudentRegistration.TCNumber, dbo.StudentRegistration.TCDate,  " +
            //            "  dbo.StudentRegistration.CasteCategory, dbo.StudentRegistration.StudentPhoto, dbo.StudentRegistration.FatherName, dbo.StudentRegistration.MotherName,  " +
            //            "  dbo.StudentRegistration.FQualification, dbo.StudentRegistration.MQualification, dbo.StudentRegistration.FatherMobileNo, dbo.StudentRegistration.MotherMobileNo,  " +
            //            "  dbo.StudentRegistration.FOccupation, dbo.StudentRegistration.MOccupation, dbo.StudentRegistration.FatherDOB, dbo.StudentRegistration.MotherDOB,  " +
            //             " dbo.StudentRegistration.FIncome, dbo.StudentRegistration.MIncome, dbo.StudentRegistration.FOfficeAddress, dbo.StudentRegistration.MOfficeAddress,  " +
            //             " dbo.StudentRegistration.FatherEmailID, dbo.StudentRegistration.MotherEmailID, dbo.StudentRegistration.FOfficePhoneNo, dbo.StudentRegistration.MOfficePhoneNo,  " +
            //             " dbo.StudentRegistration.HomePhoneNo, dbo.StudentRegistration.GuardianName, dbo.StudentRegistration.GOccupation, dbo.StudentRegistration.Relationship,  " +
            //             " dbo.StudentRegistration.GAddress, dbo.StudentRegistration.GEmailID, dbo.StudentRegistration.GMobileNo, dbo.StudentRegistration.GPhoneNo,  " +
            //             " dbo.StudentRegistration.SMSMobileNo, dbo.StudentRegistration.AnniversaryDate, dbo.StudentSession.ClassSetupID, dbo.StudentSession.SessionID,  " +
            //             " dbo.StudentSession.HouseID, dbo.StudentSession.HostelFacility, dbo.StudentSession.BusFacility, dbo.StudentSession.BusID, dbo.StudentSession.SMSInHindi,   " +
            //             " dbo.ClassSetup.ClassDescription, dbo.House.HouseName, dbo.StudentRegistration.CompID, dbo.StudentRegistration.BranchID, dbo.StudentSession.RollNo,  " +
            //             " dbo.StudentRegistration.AppImageName " +
            //   " FROM            dbo.StudentRegistration INNER JOIN " +
            //           "   dbo.StudentSession ON dbo.StudentSession.StudentID = dbo.StudentRegistration.StudentID AND dbo.StudentRegistration.CompID = dbo.StudentSession.CompID AND " +
            //             "  dbo.StudentRegistration.BranchID = dbo.StudentSession.BranchID LEFT OUTER JOIN " +
            //             " dbo.House ON dbo.StudentSession.BranchID = dbo.House.BranchID AND dbo.StudentSession.CompID = dbo.House.CompID AND  " +
            //             " dbo.StudentSession.HouseID = dbo.House.HouseID LEFT OUTER JOIN " +
            //             " dbo.ClassSetup ON dbo.StudentSession.BranchID = dbo.ClassSetup.BranchID AND dbo.StudentSession.CompID = dbo.ClassSetup.CompID AND  " +
            //             " dbo.StudentSession.ClassSetupID = dbo.ClassSetup.ClassSetupID " + 
            //             " Where  dbo.StudentSession.ClassSetupID in (" + mClassSetupID + ") and dbo.StudentRegistration.TCGiven=0 AND dbo.StudentSession.SessionID=" + mSessionID + "  and dbo.StudentSession.CompID=" + mCompID + " AND dbo.StudentSession.BranchID=" + mBranchID;


            string sql = "SELECT vStudentDataExport.* FROM dbo.vStudentDataExport " +
                         " Where  ClassSetupID in (" + mClassSetupID + ") and TCGiven=0 AND SessionID=" + mSessionID + "  and CompID=" + mCompID + " AND BranchID=" + mBranchID;

            List<vStudentDataExport> obj1 = this.context.vStudentDataExports.SqlQuery(sql).ToList();

            return obj1;
        }



        //public List<vStudentDataExport> GetStudentListByClassSetupIDs(string mClassSetupID, int mSessionID, byte mCompID, byte mBranchID)
        //{

        //    List<vStudentDataExport> objlst = new List<vStudentDataExport>();
        //    objlst = this.context.vStudentDataExports.Where(x => x.ClassSetupID == mClassSetupID && x.SessionID == mSessionID && x.CompID == mCompID && x.BranchID == mBranchID && x.TCGiven == false).ToList();
            

        //    return obj;
        //}


        public List<vStudentDataExport> GetStudentListForPhotoReport(int mClassID, int mSessionID, byte mCompID, byte mBranchID)
        {
            List<vStudentDataExport> objlst = new List<vStudentDataExport>();
            objlst = this.context.vStudentDataExports.Where(x => x.ClassID == mClassID && x.SessionID == mSessionID && x.CompID == mCompID && x.BranchID == mBranchID && x.TCGiven==false).ToList();
            return objlst;
        }







       
    }

  
}
