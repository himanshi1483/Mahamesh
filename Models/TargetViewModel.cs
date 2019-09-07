using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class TargetViewModel
    {
        public List<TargetViewModel> TargetList { get; set; }
        public string Name_of_District { get; set; }
        public int ApplicationCount_dist { get; set; }
        public List<TalukaViewModel> TalukaTarget { get; set; }
        public int Component_No_1 { get; set; }
        public int Component_No_2 { get; set; }
        public int Component_No_3_7 { get; set; }
        public int Component_No_8 { get; set; }
        public int Component_No_9 { get; set; }
        public int Component_No_10 { get; set; }
        public int Component_No_11 { get; set; }
        public int Component_No_12 { get; set; }
        public int Component_No_13 { get; set; }

        public decimal HandicapTarget_Component_No_1 { get; set; }
        public decimal HandicapTarget_Component_No_2 { get; set; }
        public decimal HandicapTarget_Component_No_3_7 { get; set; }
        public decimal HandicapTarget_Component_No_8 { get; set; }
        public decimal HandicapTarget_Component_No_9 { get; set; }
        public decimal HandicapTarget_Component_No_10 { get; set; }
        public decimal HandicapTarget_Component_No_11 { get; set; }
        public decimal HandicapTarget_Component_No_12 { get; set; }
        public decimal HandicapTarget_Component_No_13 { get; set; }

        public decimal FemaleTarget_Component_No_1 { get; set; }
        public decimal FemaleTarget_Component_No_2 { get; set; }
        public decimal FemaleTarget_Component_No_3_7 { get; set; }
        public decimal FemaleTarget_Component_No_8 { get; set; }
        public decimal FemaleTarget_Component_No_9 { get; set; }
        public decimal FemaleTarget_Component_No_10 { get; set; }
        public decimal FemaleTarget_Component_No_11 { get; set; }
        public decimal FemaleTarget_Component_No_12 { get; set; }
        public decimal FemaleTarget_Component_No_13 { get; set; }
    }

    public class TalukaViewModel
    {
        public string Name_Of_Taluka { get; set; }
        public int ApplicationCount_Tal { get; set; }
        public int Component_No_1 { get; set; }
        public int Component_No_2 { get; set; }
        public int Component_No_3_7 { get; set; }
        public int Component_No_8 { get; set; }
        public int Component_No_9 { get; set; }
        public int Component_No_10 { get; set; }
        public int Component_No_11 { get; set; }
        public int Component_No_12 { get; set; }
        public int Component_No_13 { get; set; }

        public decimal GeneralTarget_Component_No_1 { get; set; }
        public decimal GeneralTarget_Component_No_2 { get; set; }
        public decimal GeneralTarget_Component_No_3_7 { get; set; }
        public decimal GeneralTarget_Component_No_8 { get; set; }
        public decimal GeneralTarget_Component_No_9 { get; set; }
        public decimal GeneralTarget_Component_No_10 { get; set; }
        public decimal GeneralTarget_Component_No_11 { get; set; }
        public decimal GeneralTarget_Component_No_12 { get; set; }
        public decimal GeneralTarget_Component_No_13 { get; set; }

        public decimal Application_Component_No_1 { get; set; }
        public decimal Application_Component_No_2 { get; set; }
        public decimal Application_Component_No_3_7 { get; set; }
        public decimal Application_Component_No_8 { get; set; }
        public decimal Application_Component_No_9 { get; set; }
        public decimal Application_Component_No_10 { get; set; }
        public decimal Application_Component_No_11 { get; set; }
        public decimal Application_Component_No_12 { get; set; }
        public decimal Application_Component_No_13 { get; set; }
    }

}