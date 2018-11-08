using AutoMapper;
using Core.Models;
using Core.Models.User;
using Web.Models.Car;
using Web.Models.Showroom;
using Web.Models.User;

namespace Web.AutoMapper
{
	public class MappingEntity : Profile
	{
		public MappingEntity()
		{
			CreateMap<UserRegisterViewModel, User>()
				.ReverseMap();

			CreateMap<ShowroomEditViewModel, Showroom>()
				.ReverseMap();
			CreateMap<ShowroomDetailsViewModel, Showroom>()
				.ReverseMap();

			CreateMap<CarDetailsViewModel, Car>()
				.ReverseMap();
			CreateMap<CarEditViewModel, Car>()
				.ReverseMap();
		}
	}
}
