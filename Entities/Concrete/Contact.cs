using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
   public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        [Required(ErrorMessage = "Please enter a name.")]
        [StringLength(50, ErrorMessage = "Max length = 50")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Please enter a surname.")]
        [StringLength(50, ErrorMessage = "Max length = 50")]
        public string ContactSurname { get; set; }
        [Required(ErrorMessage = "Please enter a phone number.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid phone number.")]
        public string ContactTelNumber { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter an e-mail adress.")]
        [StringLength(100, ErrorMessage = "Max length = 100")]

        public string ContactEmail { get; set; }
        public int UserId { get; set; }
    }
}
