using AutoMapper;
using FadiBackEndApI.DTOs;
using FadiBackEndApI.Models;
using FadiBackEndApI.Services.Contract;
using FadiBackEndApI.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace FadiBackEndApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            this._departmentService = departmentService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            ResponseApi<List<DepartmentDTO>> response = new ResponseApi<List<DepartmentDTO>>();

            try
            {
                List<Department> departments = await _departmentService.GetDepartments();

                if (departments.Count > 0)
                {
                    List<DepartmentDTO> departmentDTOs = _mapper.Map<List<DepartmentDTO>>(departments);
                    response = new ResponseApi<List<DepartmentDTO>>() { Status = true, Message = "Ok", Value = departmentDTOs };
                } 
                else
                {
                    response = new ResponseApi<List<DepartmentDTO>>() { Status = false, Message = "No departments found" };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch(Exception ex)
            {
                response = new ResponseApi<List<DepartmentDTO>>() { Status = false, Message = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment([FromRoute]int id)
        {
            ResponseApi<DepartmentDTO> response = new ResponseApi<DepartmentDTO>();
        
            try
            {
                Department department = await _departmentService.GetDepartment(id);

                if (department == null)
                {
                    response = new ResponseApi<DepartmentDTO>() { Status = false, Message = "Department not found" };
                }
                else
                {
                    DepartmentDTO departmentDTO = _mapper.Map<DepartmentDTO>(department);
                    response = new ResponseApi<DepartmentDTO>() { Status = true, Message = "Ok", Value = departmentDTO };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<DepartmentDTO>() { Status = false, Message = ex.Message};
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody]DepartmentDTO departmentDTO)
        {
            ResponseApi<DepartmentDTO> response = new ResponseApi<DepartmentDTO>();

            try
            {
                Department department = _mapper.Map<Department>(departmentDTO);

                Department createDepartment = await _departmentService.CreateDepartment(department);

                if (createDepartment.Id == 0)
                {
                    response = new ResponseApi<DepartmentDTO>() { Status = false, Message = "Could not create department" };
                }
                else
                {
                    response = new ResponseApi<DepartmentDTO>() { Status = true, Message = "Department created successfully", Value = _mapper.Map<DepartmentDTO>(createDepartment) };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<DepartmentDTO>() { Status = false, Message = ex.Message};
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut]
        //[Route("{id}")]
        public async Task<IActionResult> UpdateDepartment([FromBody]DepartmentDTO departmentDTO)
        {
            ResponseApi<DepartmentDTO> response = new ResponseApi<DepartmentDTO>();

            try
            {
                Department department = _mapper.Map<Department>(departmentDTO);

                Department updateDepartment = await _departmentService.UpdateDepartment(department);

                if (department.Id == 0)
                {
                    response = new ResponseApi<DepartmentDTO>() { Status = false, Message = "Could not update department" };
                }
                else
                {
                    response = new ResponseApi<DepartmentDTO>() { Status = true, Message = "Department updated successfully", Value = _mapper.Map<DepartmentDTO>(updateDepartment) };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<DepartmentDTO>() { Status = false, Message = ex.Message};
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete("{id}")]
        //[Route("{id}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute]int id)
        {
            ResponseApi<bool> response = new ResponseApi<bool>();

            try
            {
                Department department = await _departmentService.GetDepartment(id);

                bool deletedDepartment = await _departmentService.DeleteDepartment(department);

                if (deletedDepartment)
                {
                    response = new ResponseApi<bool>() { Status = true, Message = "Department deleted successfully" };
                }
                else
                {
                    response = new ResponseApi<bool>() { Status = false, Message = "Could not delete department" };
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseApi<bool>() { Status = false, Message = ex.Message};
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
