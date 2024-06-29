namespace AreaDoAluno.Models
{
    public class Class
    {
        public int Id { get; set;}
        public Professor? Professor { get; set; }
        public int ProfessorId { get; set; }
        public Discipline? Discipline { get; set; }
        public int DisciplineId { get; set; }
        public string? Room { get; set; }

        public Class() {}
    }
}