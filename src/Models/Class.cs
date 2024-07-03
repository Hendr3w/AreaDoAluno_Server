using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDoAluno.Models
{
    public class Class
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}
        public Professor? Professor { get; set; }
        public int ProfessorId { get; set; }
        public Discipline? Discipline { get; set; }
        public int DisciplineId { get; set; }
        public string? Room { get; set; }

        public Class() {}
    }
}