using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
   public class User
    {
        [Key]
        public int UserId { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Please enter an username.")]
        public string UserName { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Please enter a surname.")]
        public string UserSurname { get; set; }
        [StringLength(250)]
        [Required(ErrorMessage = "Please enter an e-mail.")]
        public string UserMail { get; set; }
        [StringLength(200)]
        [Required(ErrorMessage = "Please enter a password.")]
        public string UserPassword { get; set; }

        public int RoleId { get; set; }
    }
}
