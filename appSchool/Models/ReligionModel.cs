using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appSchool.Model
{
    public class ReligionModel
    {

        public int ReligionId { get; set; }
        public string ReligionName { get; set; }

        public static List<ReligionModel> GetReligionList()
        {
            List<ReligionModel> listreligion = new List<ReligionModel>();

            listreligion.Add(new ReligionModel { ReligionId = 1, ReligionName = "Hindu" });
            listreligion.Add(new ReligionModel { ReligionId = 2, ReligionName = "Muslim" });
            listreligion.Add(new ReligionModel { ReligionId = 3, ReligionName = "Muslim" });
            listreligion.Add(new ReligionModel { ReligionId = 4, ReligionName = "Muslim" });

            return listreligion;
        
        }

    }


}