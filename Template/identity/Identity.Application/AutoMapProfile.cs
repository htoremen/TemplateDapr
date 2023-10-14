using AutoMapper;
using Identity.Application.Accounts;
using Identity.Application.Authenticates;
using Infrastructure.Models.Account;

namespace Identity.Application;

public class AutoMapProfile : Profile
{
    public AutoMapProfile()
    {
        // Account
        CreateMap<ForgotUpdatePasswordModel, UpdatePasswordCommand>(); 
        CreateMap<UpdateUserNameModel, UpdateUserNameCommand>();

        // Users
        CreateMap<LoginViewModel, LoginCommand>();
        CreateMap<ForgotPasswordModel, ForgotPasswordCommand>();
        CreateMap<ForgotUpdatePasswordModel, ForgotUpdatePasswordCommand>();
        CreateMap<CreateUserModel, CreateUserCommand>();
        CreateMap<ExternalCreateUserModel, ExternalCreateUserCommand>();
    }
}