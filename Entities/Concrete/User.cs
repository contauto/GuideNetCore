﻿using System;
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
        [Required]
        public string UserName { get; set; }
        [StringLength(50)]
        [Required]
        public string UserSurname { get; set; }
        [StringLength(250)]
        [Required]
        public string UserMail { get; set; }
        [StringLength(200)]
        [Required]
        public string UserPassword { get; set; }

        public int RoleId { get; set; }
    }
}
