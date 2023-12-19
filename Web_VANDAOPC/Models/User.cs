using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web_VANDAOPC.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Họ tên người dùng không được bỏ trống!")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Email không được bỏ trống!")]
        [EmailAddress(ErrorMessage ="Email phải có dạng exemple@email.com")]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Role { get; set; }
    }
}