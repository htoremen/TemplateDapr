namespace Identity.Application.Authenticates;

public class UserRegisterStageQueryResponse
{
    public string ParameterId { get; set; }
    public string ParameterName { get; set; }
    public string Description { get; set; }

    public IEnumerable<GetRegisterParameterStage> ParameterSections { get; set; }
}

public class GetRegisterParameterStage
{
    public string ParameterStageId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}