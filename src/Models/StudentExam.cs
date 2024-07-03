using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDoAluno.Models
{
    public class StudentExam
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}
        public Student? Student { get; set; }
        public int StudentId { get; set; }
        public Exam? Exam { get; set; }
        public int ExamId { get; set; }
        public float Grade { get; set; }

        public StudentExam() {}
    }
}