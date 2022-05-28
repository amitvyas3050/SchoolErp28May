using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class SessionRepository : GenericRepository<Achievement> 
    {
        public SessionRepository() : base(new dbSchoolAppEntities()) { }
        public SessionRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


       
        public List<Session> GetSessionListForToppers()
        {
            List<Session> obj = new List<Session>();
            obj = this.context.Sessions.Where(x => x.SessionId > 0 ).ToList();
            return obj;
        }



    }



   

}
