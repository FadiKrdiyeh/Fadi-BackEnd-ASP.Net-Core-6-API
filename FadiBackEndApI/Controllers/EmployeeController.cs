using AutoMapper;
using FadiBackEndApI.DTOs;
using FadiBackEndApI.Models;
using FadiBackEndApI.Services.Contract;
using FadiBackEndApI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FadiBackEndApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeSerivce _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeSerivce employeeSerivce, IMapper mapper)
        {
            this._employeeService = employeeSerivce;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            ResponseApi<List<EmployeeDTO>> response = new ResponseApi<List<EmployeeDTO>>();

            try
            {
                List<Employee> employees = await _employeeService.GetEmployees();

                if (employees.Count > 0)
                {
                    List<EmployeeDTO> employeeDTOs = _mapper.Map<List<EmployeeDTO>>(employees);
                    response = new ResponseApi<List<EmployeeDTO>>() { Status = true, Message = "Ok", Value = employeeDTOs };
                }
                else
                {
                    response = new ResponseApi<List<EmployeeDTO>>() { Status = false, Message = "No employees found" };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<List<EmployeeDTO>>() { Status = false, Message = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid id)
        {
            ResponseApi<EmployeeDTO> response = new ResponseApi<EmployeeDTO>();

            try
            {
                Employee employee = await _employeeService.GetEmployee(id);

                if (employee == null)
                {
                    response = new ResponseApi<EmployeeDTO>() { Status = false, Message = "Employee not found" };
                }
                else
                {
                    EmployeeDTO employeeDTO = _mapper.Map<EmployeeDTO>(employee);
                    response = new ResponseApi<EmployeeDTO>() { Status = true, Message = "Ok", Value = employeeDTO };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<EmployeeDTO>() { Status = false, Message = ex.Message };
                //return StatusCode(StatusCodes.Status500InternalServerError, response);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody]EmployeeDTO employeeDTO)
        {
            ResponseApi<EmployeeDTO> response = new ResponseApi<EmployeeDTO>();

            try
            {
                Employee employee = _mapper.Map<Employee>(employeeDTO);
                Employee createdEmployee = await _employeeService.CreateEmployee(employee);

                if (createdEmployee.EmployeeId == Guid.Empty)
                {
                    response = new ResponseApi<EmployeeDTO>() { Status = false, Message = "Could not create employee" };
                }
                else
                {
                    response = new ResponseApi<EmployeeDTO>() { Status = true, Message = "Ok", Value = _mapper.Map<EmployeeDTO>(createdEmployee) };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<EmployeeDTO>() { Status = false, Message = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody]EmployeeDTO employeeDTO)
        {
            ResponseApi<EmployeeDTO> response = new ResponseApi<EmployeeDTO>();

            try
            {
                Employee employee = _mapper.Map<Employee>(employeeDTO);
                Employee updatedEmployee = await _employeeService.UpdateEmployee(employee);

                if (updatedEmployee.EmployeeId == Guid.Empty)
                {
                    response = new ResponseApi<EmployeeDTO>() { Status = false, Message = "Could not update employee" };
                }
                else
                {
                    response = new ResponseApi<EmployeeDTO>() { Status = true, Message = "Ok", Value = _mapper.Map<EmployeeDTO>(updatedEmployee) };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<EmployeeDTO>() { Status = false, Message = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute]Guid id)
        {
            ResponseApi<bool> response = new ResponseApi<bool>();

            try
            {
                Employee employee = await _employeeService.GetEmployee(id);

                bool deletedEmployee = await _employeeService.DeleteEmployee(employee);

                if (deletedEmployee)
                {
                    response = new ResponseApi<bool>() { Status = true, Message = "Employee deleted successfully" };
                }
                else
                {
                    response = new ResponseApi<bool>() { Status = false, Message = "Could not delete employee" };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<bool>() { Status = false, Message = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
