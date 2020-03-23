using AutoMapper;
using Infrastructure.Entities;

namespace Web.Profiles
{
    public class EmployeesProfile : Profile
    {
        public EmployeesProfile()
        {
            CreateMap<Employees, Employees>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName));
        }
    }
}