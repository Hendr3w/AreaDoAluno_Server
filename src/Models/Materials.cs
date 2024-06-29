namespace AreaDoAluno.Models
{
    public class Materials
    {
        public int Id { get; set;}
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Class? Class { get; set; }
        public int ClassId { get; set; }

        public Materials() {}
    }
}