using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class TimeScheduleMasterRepository : GenericRepository<TimeSchedule>
    {
        public TimeScheduleMasterRepository() : base(new dbSchoolAppEntities()) { }
        public TimeScheduleMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<TimeSchedule> GetTimeScheduleMasterList(byte mCompID, byte mBranchID)
        {
            List<TimeSchedule> obj = new List<TimeSchedule>();
            obj = this.context.TimeSchedules.Where(x => x.ScheduleId > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public void UpdateTimeScheduleMaster(TimeSchedule obj)
        {
            TimeSchedule newObj = this.GetByID(obj.ScheduleId);
            newObj.ScheduleName = obj.ScheduleName;
            newObj.StartTime = obj.StartTime;
            newObj.EndTime = obj.EndTime;
            newObj.IsBreak = obj.IsBreak;
            newObj.ModDate = obj.ModDate;
            newObj.UIDMod = obj.UIDMod;
            this.Update(newObj);
        }

        //public int CheckDelete(int mID)
        //{
        //    int ID = 0;
        //    ID = this.context.FeesStructureDetails.Where(x => x.FeeTermID == mID).Count();
        //    return ID;
        //}


    }




    #region METADATA
    [MetadataType(typeof(TimeScheduleMetadata))]
    public partial class TimeScheduleMaster
    {
    }

    public class TimeScheduleMetadata
    {
        [Key]
        public short ScheduleId { get; set; } // Has to have the same type and name as your model
        [Required(ErrorMessage = "ScheduleName is required")]
        public string ScheduleName { get; set; } // Has to have the same type and name as your model
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        

    }
    #endregion
}