using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mahamesh.Models
{
    public class MediaFolders
    {
        [Key]
        public int FolderId { get; set; }
        public string FolderName { get; set; }
        public string MediaType { get; set; }
    }
}