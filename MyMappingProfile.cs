using AutoMapper;
using PawsomeAdoptBackEnd.DTOs;
using PawsomeAdoptBackEnd.Entitites;

namespace PawsomeAdoptBackEnd
{
    public class MyMappingProfile : Profile
    {
        public MyMappingProfile()
        {
            CreateMap<Pet, PetDTO>().ReverseMap();
            CreateMap<Application, ApplicationDTO>().ReverseMap();
        }

    }
}
