using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class SelectedListViewModel
    {
        public List<SelectedHandicapped> SelectedHandicappedList { get; set; }
        public SelectedHandicapped SelectedHandicappedModel { get; set; }
        public List<SelectedFemale> SelectedFemaleList { get; set; }
        public SelectedFemale SelectedFemaleModel { get; set; }
        public List<SelectedGeneral> SelectedGeneralList { get; set; }
        public SelectedGeneral SelectedGeneralModel { get; set; }

        public List<SelectedGeneral> WaitingList { get; set; }
    }
}