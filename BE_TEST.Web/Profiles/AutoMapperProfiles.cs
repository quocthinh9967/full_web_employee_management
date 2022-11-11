using AutoMapper;
using BE_TEST.Domain.Entities;
using BE_TEST.Web.Models;

namespace BE_TEST.Web.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<Employee, Employee>();
        }
    }
}
