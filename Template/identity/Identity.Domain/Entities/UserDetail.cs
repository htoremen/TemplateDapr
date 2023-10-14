using Identity.Domain.Enums;

namespace Identity.Domain.Entities;

public class UserDetail
{
    [Key]
    [JsonIgnore]
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTime? Birthdate { get; set; }
    public string Photo { get; set; }

    public int Country { get; set; }
    public int City { get; set; }
    public int Region { get; set; }
    public int PostalCode { get; set; }

    public string Email { get; set; }
    public int EmailVerified { get; set; }

    public string PhoneNumber { get; set; }
    public bool PhoneVerified { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; } = DateTime.Now;
    public DateTime LastLogin { get; set; }

    public bool IsActive { get; set; }
    public UserType UserTypeId { get; set; }

    public virtual User User { get; set; }
}
