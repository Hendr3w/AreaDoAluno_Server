namespace AreaDoAluno.Models
{
    public class Professor : User
    {
        public decimal HourlyRate { get; set; }
        public decimal HoursWorked { get; set; }

        public Professor() {}
    }
}