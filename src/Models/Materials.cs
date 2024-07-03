using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AreaDoAluno.Models
{
    public class Materials
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;}
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Class? Class { get; set; }
        public int ClassId { get; set; }

        public Materials() {}
    }
}