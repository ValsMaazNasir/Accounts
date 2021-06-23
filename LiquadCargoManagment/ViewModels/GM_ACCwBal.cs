using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquadCargoManagment.ViewModels
{
    public class GM_ACCwBal
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int parent_id { get; set; }
        public int isActive { get; set; }
        public System.DateTime created { get; set; }
        public System.DateTime modified { get; set; }
        public string createdBy { get; set; }
        public string modifiedBy { get; set; }
        public string CODE { get; set; }
        public Nullable<int> LEVEL { get; set; }
        public Nullable<decimal> balance { get; set; }
    }
}