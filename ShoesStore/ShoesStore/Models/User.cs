using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesStore.Models
{
    public class User
    {
        [Key]
        [EmailAddress]
        [Required(ErrorMessage = "Email must not be emptied!!!")]
        public string Email { get; set; }
        [RegularExpression("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$", ErrorMessage = "Valid Characters include (A-Z) (a-z) (' space -)")]
        [Required(ErrorMessage = "Name must not be emptied!!!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address must not be emptied!!!")]

        public string Address { get; set; }
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})", ErrorMessage = "Password must be suitable!!!")]
        [Required(ErrorMessage = "Password must not be emptied!!!")]
        
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Registration Date")]
        public DateTime RegistrationDate { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Phone must not be emptied!!!")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Phone must have 10 digits and contains no characters!!")]
        public string Phone { get; set; }
        public Role Role { get; set; }

        public bool Status { get; set; }
    }
}
