using System.ComponentModel.DataAnnotations;

namespace FadiBackEndApI.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required!")]
        [MinLength(2, ErrorMessage = "Department name can't be less than 2 characters!")]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
