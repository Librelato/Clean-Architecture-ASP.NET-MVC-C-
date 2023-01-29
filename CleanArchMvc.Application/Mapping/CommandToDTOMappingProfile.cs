using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Products.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Mapping
{
    public class CommandToDTOMappingProfile : Profile
    {
        public CommandToDTOMappingProfile()
        {
            CreateMap<ProductCreateCommand, ProductDTO>().ReverseMap();
            CreateMap<ProductUpdateCommand, ProductDTO>().ReverseMap();
        }
    }
}
