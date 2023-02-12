using FadiBackEndApI.Models;

namespace FadiBackEndApI.Services.Contract
{
    public interface IEmployeeSerivce
    {
        Task<List<Employee>> GetEmployees();

        Task<Employee> GetEmployee(Guid id);

        Task<Employee> CreateEmployee(Employee employee);

        Task<Employee> UpdateEmployee(Employee employee);

        Task<bool> DeleteEmployee(Employee employee);
    }
}
