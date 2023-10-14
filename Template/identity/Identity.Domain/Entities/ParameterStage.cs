namespace Identity.Domain.Entities;

public class ParameterStage
{
    [Key]
    public string ParameterStageId {  get; set; }
    public string ParameterId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int OrderId { get; set; }
    public bool IsActive { get; set; }

    public Parameter Parameter {  get; set; }
}
