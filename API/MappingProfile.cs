using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreationDto, User>();
            CreateMap<UserBase, User>();
            CreateMap<ReturnProfileDto,User>();
            CreateMap<ApartmentBase,Apartments>();
            CreateMap<BookingGuestAddDto,BookingGuests>();
        }
    }
}
