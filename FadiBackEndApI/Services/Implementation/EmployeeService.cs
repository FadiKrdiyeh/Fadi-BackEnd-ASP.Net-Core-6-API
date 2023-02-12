using FadiBackEndApI.Models;
using FadiBackEndApI.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace FadiBackEndApI.Services.Implementation
{

    public class EmployeeService : IEmployeeSerivce
    {
        private readonly FadiDbContext _fadiDbContext;

        public EmployeeService(FadiDbContext fadiDbContext)
        {
            this._fadiDbContext = fadiDbContext;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            try 
            {
                List<Employee> employees = new List<Employee>();
                employees = await _fadiDbContext.Employees.Include(department => department.Department).ToListAsync();
                return employees;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> GetEmployee(Guid id)
        {
            try
            {
                Employee? employee = new Employee();
                employee = await _fadiDbContext.Employees.Include(department => department.Department).Where(emp => emp.Id == id).FirstOrDefaultAsync();
                return employee;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            try
            {
                employee.Id = Guid.NewGuid();
                _fadiDbContext.Employees.Add(employee);
                await _fadiDbContext.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            try
            {
                _fadiDbContext.Employees.Update(employee);
                await _fadiDbContext.SaveChangesAsync();
                return employee;
            }
            catch(Exception ex) 
            { 
                throw ex;
            }
        }

        public async Task<bool> DeleteEmployee(Employee employee)
        {
            try
            {
                _fadiDbContext.Employees.Remove(employee);
                await _fadiDbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
