using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab2_Construction.Models
{
    public class Book
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource_en1),
          ErrorMessageResourceName = "IDRequired")]
        [Display(Name = "ID", ResourceType = typeof(Resources.Resource_en1))]
        public int ID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource_en1),
          ErrorMessageResourceName = "TitleRequired")]
        [Display(Name = "Title", ResourceType = typeof(Resources.Resource_en1))]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource_en1),
          ErrorMessageResourceName = "AuthorRequired")]
        [Display(Name = "Author", ResourceType = typeof(Resources.Resource_en1))]
        public string Author { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource_en1),
          ErrorMessageResourceName = "AvailableRequired")]
        [Display(Name = "Available", ResourceType = typeof(Resources.Resource_en1))]
        public bool Available { get; set; }
    }
}