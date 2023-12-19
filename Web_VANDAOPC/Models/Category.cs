using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Web_VANDAOPC.Models
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}