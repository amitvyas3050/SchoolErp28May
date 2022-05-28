using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class Lib_BookDemandRepository : GenericRepository<Lib_BookDemand> 
    {
        public Lib_BookDemandRepository () : base(new dbSchoolAppEntities()) { }
        public Lib_BookDemandRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


    }


   


}
