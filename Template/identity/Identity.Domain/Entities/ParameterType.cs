using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Entities;

public class ParameterType
{
    public ParameterType()
    {
        Parameters = new HashSet<Parameter>();
    }

    [Key]
    //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

    public string ParameterTypeId { get; set; } 
    public string Name { get; set; }

    public virtual ICollection<Parameter> Parameters { get; set;}
}
