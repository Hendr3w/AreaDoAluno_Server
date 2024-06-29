namespace AreaDoAluno.Models
{
    public class Tuition
    {
        public int Id { get; set;}
        public Enrollment? Enrollment { get; set; }
        public int EnrollmentId { get; set; }
        public string? SelfStatus { get; set; }
        public string? MonthRef { get; set; }
        public float Amount { get; set; }

        public Tuition() {}
    }
}