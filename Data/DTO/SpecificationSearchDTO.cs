namespace backend_se.Data.DTO
{
    public class SpecificationSearchDTO
    {
        public int SpecificationId { get; set; }
        public required string Name { get; set; }
        public List<string> Values { get; set; } = new List<string>();
        public bool IsBool { get; set; }
    }
}
