namespace Identity.Domain.Entities;

public class UserRegisterStage
{
    [Key]
    public string UserRegisterStageId { get; set; }
    public string UserId { get; set; }
    public string ParameterId { get; set; }
    public string ParameterTypeId { get; set; }
    public string ParameterStageId { get; set; }
    public DateTime? Created { get; set; }

    public virtual User User { get; set; }
    public virtual Parameter Parameter { get; set; }
    public virtual ParameterType ParameterType { get; set; }
    public virtual ParameterStage ParameterStage { get; set; }
}
