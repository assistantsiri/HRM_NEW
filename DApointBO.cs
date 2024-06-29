using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class DApointBO
    {
        public int DAP_SINo { get; set; }
        public DateTime DAP_FROM { get; set; }
        public DateTime DAP_TO { get; set; }
        public decimal DAP_Point { get; set; }
        public decimal DAP_PER { get; set; }
        public DateTime DAP_CrDate { get; set; }
        public string Action { get; set; }
    }
}
