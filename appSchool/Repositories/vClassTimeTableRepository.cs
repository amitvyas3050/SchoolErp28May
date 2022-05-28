using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using System.Data.SqlClient;

namespace appSchool.Repositories
{
    public class vClassTimeTableRepository : GenericRepository<TimeTable> 
    {
        public vClassTimeTableRepository() : base(new dbSchoolAppEntities()) { }
        public vClassTimeTableRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<vClassTimeTable> GetClassTimeTableList(int mClassID, byte mCompID, byte mBranchID)
        {
            List<vClassTimeTable> obj = new List<vClassTimeTable>();
            obj = this.context.vClassTimeTables.Where(x => x.ClassId == mClassID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public List<modelTeacherRegistration> GetTeachereList(byte mCompID, byte mBranchID)
        {
            List<modelTeacherRegistration> lst =
                (from xx in this.context.Teachers
                 select new modelTeacherRegistration()
                 {
                     TeacherID = xx.TeacherID,

                     FirstName = xx.FirstName,
                     LastName = xx.LastName,
                     FullName = xx.FirstName + " " + xx.LastName,


                 }
                 ).ToList<modelTeacherRegistration>();
            modelTeacherRegistration obj = new modelTeacherRegistration();
            obj.TeacherID = -1;
            obj.FullName = "Break";

            lst.Add(obj);

            return lst;

        }

        public List<vSubjectAllotement> GetSubjectlListForTimeTable(int mClassID, byte mCompID, byte mBranchID)
        {


            List<vSubjectAllotement> list = new List<vSubjectAllotement>();
            var param = new[] { 
                new SqlParameter("@ClassID", mClassID),           
                new SqlParameter("@CompID",mCompID),
                new SqlParameter("@BranchID", mBranchID),
                
                };
            list = this.context.Database.SqlQuery<vSubjectAllotement>(
                                     "GetSubjectList @ClassID,@CompID,@BranchID",
                                      param
                             ).ToList();

            vSubjectAllotement obj = new vSubjectAllotement();
            obj.IDL1 = -1;
            obj.SubjectNameL1 = "Break";

            list.Add(obj);

            return list;
         }


        

        public List<TimeTable> GetTimeTableList(byte mCompID, byte mBranchID)
        {
            List<TimeTable> listobj = new  List<TimeTable>();
            listobj = this.context.TimeTables.Where(x => x.ID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return listobj;
        }



        public void UpdateClassTimeTable(TimeTable obj)
        {


            this.Update(obj);
            


        }



        public void UpdateClassTimeTable(vClassTimeTable obj)
        {
            TimeTable edittimetable = this.GetByID(obj.ID);
           
            if (edittimetable != null)
            {

                edittimetable.MonFID = obj.MonFID;
                edittimetable.TueFID = obj.TueFID;
                edittimetable.WedFID = obj.WedFID;
                edittimetable.ThuFID = obj.ThuFID;
                edittimetable.FriFID = obj.FriFID;
                edittimetable.SatFID = obj.SatFID;

                edittimetable.MonSID = obj.MonSID;
                edittimetable.TueSID = obj.TueSID;
                edittimetable.WedSID = obj.WedSID;
                edittimetable.ThuSID = obj.ThuSID;
                edittimetable.FriSID = obj.FriSID;
                edittimetable.SatSID = obj.SatSID;

                this.Update(edittimetable);
            }



        }


    }








}
