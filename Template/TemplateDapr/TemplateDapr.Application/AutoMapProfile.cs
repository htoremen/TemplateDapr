namespace Application;

public class AutoMapProfile : Profile
{
    public AutoMapProfile()
    {
        CreateMap<Parameter, ParameterModel>();
    }
}