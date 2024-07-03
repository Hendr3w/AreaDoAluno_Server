using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDoAluno.Models
{
    public class Tuition
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}
        public Enrollment? Enrollment { get; set; }
        public int EnrollmentId { get; set; }
        public string? SelfStatus { get; set; }
        public string? MonthRef { get; set; }
        public float Amount { get; set; }

        public Tuition() {}
    }
}