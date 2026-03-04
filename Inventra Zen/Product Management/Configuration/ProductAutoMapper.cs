using AutoMapper;
using Product_Management.DTOs;
using Product_Management.Entity_Models;

namespace Product_Management.Configuration
{
    public class ProductAutoMapper : Profile
    {
        public ProductAutoMapper()
        {
            //General response mapping
            CreateMap<ProductEntityModel, ProductDTO>();


            //Create body to entity model mapping
            CreateMap<CreateProductDTO, ProductEntityModel>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore());


            //Update body to entity model mapping
            CreateMap<UpdateProductDTO, ProductEntityModel>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
