using AutoMapper;
using RiceShop.Api.ViewModel;
using RiceShop.Clb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiceShop.Api.Mapping
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<Supplier, VmSupplier>().ReverseMap();
            CreateMap<Category, VmCategory>().ReverseMap();
            CreateMap<List<Category>,List< VmCategory>>().ReverseMap();
        }
    }
}
