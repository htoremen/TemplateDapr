using TemplateDapr.Application;

namespace Application;

public class AutoMapProfile : Profile
{
    public AutoMapProfile()
    {
        CreateMap<Parameter, CreateParameterModel>();
        CreateMap<CreateParameterModel, CreateParameterCommand>();
    }
}