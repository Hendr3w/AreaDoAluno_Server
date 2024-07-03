using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDoAluno.Models
{
    public class Enrollment
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Student? Student { get; set; }
        public int StudentId { get; set; }
        public Course? Course { get; set; }
        public int CourseId { get; set; }
        public string? EnrollmentStatus { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public float DiscountRate { get; set; }
        public int Period { get; set; }
        public int ComplementaryHours { get; set; }

        public Enrollment() { }
    }
}