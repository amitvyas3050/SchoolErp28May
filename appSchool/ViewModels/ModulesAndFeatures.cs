using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appSchool.ViewModels
{
    public class ModulesAndFeatures
    {
        public int Id { get; set; }

        public int ModuleID { get; set; }
        public string MenuName { get; set; }
        public string BgColor1 { get; set; }
        public string BgColor2 { get; set; }
        public string IconName { get; set; }
        public string NavUrl { get; set; }


    }
}