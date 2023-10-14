namespace Identity.Domain.Entities;

public class UserForgotPassword
{
    [Key]
    [JsonIgnore]
    public string UserId { get; set; }
    public string Code { get; set; }
    public DateTime Created { get; set; }
    public DateTime ValidPeriod { get; set; }
    public bool IsCompleted { get;set; }

    public virtual User User { get; set; }
}
