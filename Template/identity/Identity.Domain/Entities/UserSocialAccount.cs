namespace Identity.Domain.Entities;

public class UserSocialAccount
{
    [Key]
    [JsonIgnore]
    public string UserId { get; set; }
    public string Web { get; set; }
    public string Facebook { get; set; }
    public string Twitter { get; set; }
    public string Instagram { get; set; }
    public string YouTube { get; set; }
    public virtual User User { get; set; }
}
