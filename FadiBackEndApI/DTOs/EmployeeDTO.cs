using FadiBackEndApI.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FadiBackEndApI.DTOs
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Salary { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
