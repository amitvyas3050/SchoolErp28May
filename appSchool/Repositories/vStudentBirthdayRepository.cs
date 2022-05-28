using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;


namespace appSchool.Repositories
{
    public class vStudentBirthdayRepository : GenericRepository<vStudentBirthday> 
    {
        public vStudentBirthdayRepository() : base(new dbSchoolAppEntities()) { }
        public vStudentBirthdayRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<vStudentBirthday> GetTodayStudentBirthday(int mSessionID,byte mCompID, byte mBranchID)
        {
            List<vStudentBirthday> objStudentBirthday = new List<vStudentBirthday>();
            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                             new SqlParameter("@BirthdayType", BirthdayType.Student),
                             new SqlParameter("@CompID", mCompID),
                             new SqlParameter("@BranchID", mBranchID),

                            };
            objStudentBirthday = this.context.Database.SqlQuery<vStudentBirthday>(
                                     "GetStudentBirthDay @SessionID, @BirthdayType, @CompID, @BranchID ",
                                      param
                             ).ToList();

            return objStudentBirthday;
        }

        public List<vStudentBirthday> GetTodayBirthday(int mSessionID,byte mCompID, byte mBranchID)
        {

            List<vStudentBirthday> objFinal = new List<vStudentBirthday>();

           


            List<vStudentBirthday> objFatherBirthday = new List<vStudentBirthday>();
            var paramFather = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                             new SqlParameter("@BirthdayType", BirthdayType.Father),
                             new SqlParameter("@CompID", mCompID),
                             new SqlParameter("@BranchID", mBranchID),

                            };
            objFatherBirthday = this.context.Database.SqlQuery<vStudentBirthday>(
                                     "GetStudentBirthDay @SessionID, @BirthdayType, @CompID, @BranchID",
                                      paramFather
                             ).ToList();


            //objFinal = objFatherBirthday;
            objFinal.AddRange(objFatherBirthday);

            List<vStudentBirthday> objMotherBirthday = new List<vStudentBirthday>();
            var paramMother = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                             new SqlParameter("@BirthdayType", BirthdayType.Mother),
                             new SqlParameter("@CompID", mCompID),
                             new SqlParameter("@BranchID", mBranchID),

                            };
            objMotherBirthday = this.context.Database.SqlQuery<vStudentBirthday>(
                                     "GetStudentBirthDay @SessionID, @BirthdayType,@CompID,@BranchID",
                                      paramMother
                             ).ToList();


           // objFinal = objMotherBirthday;
            objFinal.AddRange(objMotherBirthday);
            List<vStudentBirthday> objAnniversary = new List<vStudentBirthday>();
            var paramAnniversary = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                             new SqlParameter("@BirthdayType", BirthdayType.Anniverssary),
                              new SqlParameter("@CompID", mCompID),
                               new SqlParameter("@BranchID", mBranchID),

                            };
            objAnniversary = this.context.Database.SqlQuery<vStudentBirthday>(
                                     "GetStudentBirthDay @SessionID, @BirthdayType, @CompID, @BranchID",
                                      paramAnniversary
                             ).ToList();

           // objFinal = objAnniversary;
            objFinal.AddRange(objAnniversary);


            return objFinal;
        }


     








       
    }

  
}
