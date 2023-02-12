using AutoMapper;
using FadiBackEndApI.DTOs;
using FadiBackEndApI.Models;
using System.Globalization;

namespace FadiBackEndApI.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapping
            #region Department
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            #endregion
            
            #region Employee
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(destiny => 
                    destiny.DepartmentName,
                    opt => opt.MapFrom(origin => origin.Department.Name)
            )
                .ForMember(destiny =>
                    destiny.Salary,
                    opt => opt.MapFrom(origin => origin.Salary.ToString())
            );

            CreateMap<EmployeeDTO, Employee>()
                .ForMember(destiny =>
                    destiny.Department,
                    opt => opt.Ignore()
                )
                .ForMember(destiny =>
                    destiny.Salary,
                    opt => opt.MapFrom(origin => int.Parse(origin.Salary))
                );
            #endregion
        }
    }
}
