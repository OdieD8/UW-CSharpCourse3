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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<User> UsersList { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Classes { get; set; }

        [NotMapped]
        public List<string> SelectedClass { get; set; }
    }
}