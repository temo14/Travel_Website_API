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
            CreateMap<UserUpdateDto, User>();
            CreateMap<ReturnProfileDto,User>();
            CreateMap<ApartmentCreationDto,Apartments>();
            CreateMap<BookingGuestAddDto,BookingGuests>();
        }
        
    }
}
