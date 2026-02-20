using AutoMapper;
using Tango.Employee.DTOs;
using Tango.Employee.Entities;

namespace Tango.Employee.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<EmployeeEntityModel, EmployeeDTO>()
                .ForMember(n => n.Location, opt => opt.MapFrom(x => x.CityCode))
                .ReverseMap()
                .ForMember(n => n.CityCode, opt => opt.MapFrom(x => x.Location));

            CreateMap<CreateEmployeeDTO, EmployeeEntityModel>()
                .ForMember(n => n.EmployeeID, opt => opt.Ignore())
                .ForMember(n => n.CityCode, opt => opt.MapFrom(x => x.Location));

            CreateMap<UpdateEmployeeDTO, EmployeeEntityModel>()
                .ForMember(n => n.CityCode, opt => opt.MapFrom(x => x.Location));

            CreateMap<EmployeeEntityModel, PartialUpdateDTO>()
                .ForMember(n => n.Location, opt => opt.MapFrom(x => x.CityCode));

            CreateMap<PartialUpdateDTO, EmployeeEntityModel>()
                .ForMember(n => n.CityCode, opt => opt.MapFrom(x => x.Location))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
