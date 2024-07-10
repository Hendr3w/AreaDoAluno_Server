namespace AreaDoAluno.Models
{
    public class Course 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Field { get; set; }
        public bool Active { get; set; }

        public Course() {}

    }
}