using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class MediaGalleryModel
    {
        [Key]
        public int MediaId { get; set; }
        public MediaType MediaType { get; set; }
        public string MediaFolder { get; set; }
        [NotMapped]
        public string MediaFolderNew { get; set; }
        [NotMapped]
        public List<MediaFolders> FolderList { get; set; }
        public string MediaName { get; set; }
        public string MediaLocation { get; set; }
        public string Caption { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [NotMapped]
        public List<MediaGalleryModel> MediaList { get; set; }
    }

    public enum MediaType
    {
        Picture,
        Video
    }
}