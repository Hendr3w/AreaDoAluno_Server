namespace AreaDoAluno.Models
{
    public class StudentClass
    {
        public int Id { get; set;}
        public Class? Class { get; set; }
        public int ClassId { get; set; }
        public Student? Student { get; set; }
        public int StudentId { get; set; }
        public decimal Grade { get; set; }
        public decimal AttendanceRate { get; set; }

        public StudentClass() {}
    }
}