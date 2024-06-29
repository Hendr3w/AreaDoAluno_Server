namespace AreaDoAluno.Models
{
    public class Exam
    {
        public int Id { get; set;}
        public DateOnly? Deadline { get; set; }
        public float MaxGrade { get; set; }
        public Class? Class { get; set; }
        public int ClassId { get; set; }

        public Exam() {}
    }
}