using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class AcademicyearRepository : GenericRepository<Session> 
    {
        public AcademicyearRepository() : base(new dbSchoolAppEntities()) { }
        public AcademicyearRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        public IEnumerable<listItem> GetAcademicyearforSelect()
        {


            List<listItem> lst = new List<listItem>();
            lst.Add(new listItem() { Value = -1, Description = "(None)" });
            lst.AddRange(
                from xx in this.context.Sessions
                select new listItem() { Value = xx.SessionId, Description = xx.SessionName }
                );
            return lst;
        }

        
        public void AddNewAcademicyear(Session obj) 
        {

            this.Insert(obj);
            return;
        }
        public void UpdateAcademicyear(Session obj)
        {
            Session c = this.GetByID(obj.SessionId);
            c.SessionName = obj.SessionName;
            this.Update(c);
            return;
        }
        public void DeleteAcademicyear(Session obj)
        {
            this.Delete(obj);
            return;
        }
        public Session GetCurrentSession() // old metyhod
        {
            //DateTime currDT=DateTime.Now;
            //Session s= this.Get(). .Where(x => x.StartDate.Value >= currDT && x.EndDate.Value <= currDT).FirstOrDefault();
            return this.GetByID(2);
        }

        public IEnumerable<Session> GetSessionListDescending()
        {
            List<Session> listsession = new List<Session>();
             listsession = this.context.Sessions.Where(x => x.SessionId > 1).OrderByDescending(y => y.SessionId).ToList();
            return listsession;
        }
        public Session GetCurrentSessionByFlag()
        {
            
            Session session= this.context.Sessions.Where(x => x.SessionId > 1).OrderByDescending(y=> y.SessionId).FirstOrDefault();
            return session;
        }
    }

    #region Academicyear
    [MetadataType(typeof(SessionMetadata))]
    public partial class Session
    {
    }
    public class SessionMetadata
    {
        [Key]
        public int SessionId { get; set; } // Has to have the same type and name as your model
        [Required(ErrorMessage = "Cat Name needed")]
        [StringLength(50, ErrorMessage = "Can't be more than 50 chars.")]
        public string SessionName { get; set; } // Has to have the same type and name as your model

    }
    #endregion


}
