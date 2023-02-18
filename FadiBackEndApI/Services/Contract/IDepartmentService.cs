using FadiBackEndApI.Models;

namespace FadiBackEndApI.Services.Contract
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetDepartments();

        Task<Department> GetDepartment(int id);

        Task<Department> CreateDepartment(Department department);

        Task<Department> UpdateDepartment(Department department);

        Task<bool> DeleteDepartment(Department department);

        Task<int> CountEmployees(int departmentId);
    }
}
