namespace Domain.Entities
{
    public class Parameter
    {
        [Key]
        public string ParameterId { get; set; }
        public string ParameterTypeId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ParameterType ParameterType { get; set; }
    }
}