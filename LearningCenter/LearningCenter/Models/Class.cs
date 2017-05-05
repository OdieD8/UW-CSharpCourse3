using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace LearningCenter.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }

        public virtual ICollection<User> Users { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Classes { get; set; }

        [NotMapped]
        public string SelectedClass { get; set; }
    }
}