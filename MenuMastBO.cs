using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class MenuMastBO
    {
        public string HMM_OFFICE_TYP { get; set; }
        public Byte HMM_MENU_LEVEL1 { get; set; }
        public Byte HMM_MENU_LEVEL2 { get; set; }
        public Byte HMM_MENU_LEVEL3 { get; set; }

        public Byte HMM_MENU_LEVEL4 { get; set; }
        public Byte HMM_MENU_LEVEL5 { get; set; }
        public string HMM_MENU_TITLE { get; set; }
        public string HMM_MENU_URL { get; set; }
        public string Action { get; set; }
    }
}
