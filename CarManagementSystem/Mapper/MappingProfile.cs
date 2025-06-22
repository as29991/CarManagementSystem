using AutoMapper;
using CarManagementSystem.DTOs;
using CarManagementSystem.Models;

namespace CarManagementSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Vehicle, VehicleDTO>()
                            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())); 
            CreateMap<CreateVehicleDTO, Vehicle>();


            CreateMap<Transaction, TransactionDTO>();
            CreateMap<CreateTransactionDTO, Transaction>();
            CreateMap<TransactionDTO, Transaction>();
        }
    }
}
