using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Web_VANDAOPC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Tên Sản Phẩm Không Được Bỏ Trống!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Giá Sản Phẩm Không Được Bỏ Trống!")]
        public double Price { get; set; }
        public string Brand { get; set; }
        public string Details { get; set; }
        public string Img { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Loại Sản Phẩm Không Được Bỏ Trống!")]
        public int CatId { get; set; }
        public virtual Category Category { get; set; }
    }
}