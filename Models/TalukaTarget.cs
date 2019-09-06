using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class TalukaTarget
    {
      
            [Key]
            public int SrNo { get; set; }
            public string Name_of_District { get; set; }
            public string Name_Of_Taluka { get; set; }
            public int Component_No_1 { get; set; }
            public int Component_No_2 { get; set; }
            public int Component_No_3_7 { get; set; }
            public int Component_No_8 { get; set; }
            public int Component_No_9 { get; set; }
            public int Component_No_10 { get; set; }
            public int Component_No_11 { get; set; }
            public int Component_No_12 { get; set; }
            public int Component_No_13 { get; set; }
        
    }
}