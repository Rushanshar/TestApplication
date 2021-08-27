
using AutoMapper;
using BL.Models;

namespace BL.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Client, DL.Entities.Client>().ReverseMap();
        }
    }
}
