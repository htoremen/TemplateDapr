namespace Infrastructure.Models;

public class CreateUserModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<UserRegisterStageModel> UserRegisterStages { get; set; }
}

public class UserRegisterStageModel
{
    public string ParameterId { get; set; }
    public string ParameterTypeId { get; set; }
    public string ParameterStageId { get; set; }
}