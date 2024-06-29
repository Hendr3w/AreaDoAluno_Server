namespace AreaDoAluno.Models
{
    public class Professor : Person
    {
        public float HourlyRate { get; set; }
        public float HoursWorked { get; set; }

        public Professor() {}
    }
}