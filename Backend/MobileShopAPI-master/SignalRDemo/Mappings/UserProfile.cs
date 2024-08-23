using AutoMapper;
using SignalRDemo.Models;
using SignalRDemo.ViewModels;

namespace SignalRDemo.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserChatViewModel>()
                .ForMember(dst => dst.UserName, opt => opt.MapFrom(x => x.UserName));

            CreateMap<UserChatViewModel, ApplicationUser>();
        }
    }
}
