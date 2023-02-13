using FadiBackEndApI.Models;
using FadiBackEndApI.Services.Contract;
using Microsoft.EntityFrameworkCore;

namespace FadiBackEndApI.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly FadiDbContext _fadiDbContext;

        public DepartmentService(FadiDbContext fadiDbContext)
        {
            this._fadiDbContext = fadiDbContext;
        }

        public async Task<List<Department>> GetDepartments()
        {
            try
            {
                List<Department> department = new List<Department>();
                department = await _fadiDbContext.Departments.ToListAsync();
                return department;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Department> GetDepartment(int id)
        {
            try
            {
                Department? department = new Department();
                department = await _fadiDbContext.Departments.FirstOrDefaultAsync(dept => dept.DepartmentId == id);
                return department;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Department> CreateDepartment(Department department)
        {
            try
            {
                _fadiDbContext.Departments.Add(department);
                await _fadiDbContext.SaveChangesAsync();
                return department;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Department> UpdateDepartment(Department department)
        {
            try
            {
                _fadiDbContext.Departments.Update(department);
                await _fadiDbContext.SaveChangesAsync();
                return department;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteDepartment(Department department)
        {
            try
            {
                _fadiDbContext.Departments.Remove(department);
                await _fadiDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
