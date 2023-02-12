using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FadiBackEndApI.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [DisplayName("First name")]
        [MinLength(2, ErrorMessage = "First name must be 2 characters at least!")]
        [MaxLength(15, ErrorMessage = "First name can't be more than 15 characters!")]
        public string FirstName { get; set; }
        
        [DisplayName("Last name")]
        [MinLength(2, ErrorMessage = "Last name must be 2 characters at least!")]
        [MaxLength(20, ErrorMessage = "Last name can't be more than 20 characters!")]
        public string LastName { get; set; }

        public string Address { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Phone number must be 10 numbers!")]
        [MaxLength(10, ErrorMessage = "Phone number must be 10 numbers!")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public int Salary { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
