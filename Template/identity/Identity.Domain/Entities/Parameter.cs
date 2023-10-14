namespace Identity.Domain.Entities;

public class Parameter
{
    [Key]
    public string ParameterId { get; set; }
    public string ParameterTypeId { get; set; }
    public string ParameterName { get; set; }
    public string Description { get; set; }
    public int OrderId { get; set; }
    public bool IsActive { get; set; }

    public virtual ParameterType ParameterType { get; set; }

    public virtual ICollection<UserNotification> UserNotifications { get; set; }
    public virtual ICollection<ParameterStage> ParameterStages { get; set; }
}
