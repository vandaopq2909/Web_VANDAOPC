using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_VANDAOPC.Models
{
    public class Cart
    { 
        [Key]
        public int CartId { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }
}