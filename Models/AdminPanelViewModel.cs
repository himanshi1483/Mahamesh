using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class AdminPanelViewModel
    {
        public TenderModel Tenders { get; set; }
        public NewsModel News { get; set; }
        public PressInformationModel PressInfo { get; set; }
        public FeedbackModel Feedbacks { get; set; }
        public List<TenderModel> TenderList { get; set; }
        public List<NewsModel> NewsList { get; set; }
        public List<PressInformationModel> PressList { get; set; }
        public List<MediaFolders> FolderList { get; set; }
        public List<FeedbackModel> FeedbackList { get; set; }

    }
}