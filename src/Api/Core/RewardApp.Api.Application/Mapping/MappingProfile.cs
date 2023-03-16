using AutoMapper;
using RewardApp.Api.Domain.Models;
using RewardApp.Common.Models.Queries.User;
using RewardApp.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserCommand, User>();
        
        CreateMap<UpdateUserCommand, User>();

        CreateMap<User, LoginUserViewModel>()
           .ReverseMap();

        CreateMap<UserDetailViewModel, User>()
            .ReverseMap();

        CreateMap<CreateAssignmentCommand, Assignment>();
    }
}
