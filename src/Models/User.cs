namespace AreaDoAluno.Models
{
    public class User 
    {
        public User () {

        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Cpf { get; set; }
        public string? Rg { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Rgm { get; set; }
        public char Gender { get; set; }
        public Address? Address { get; set; }
        public int AddressId { get; set; }
        public DateOnly Birthdate { get; set; }
    }  
}

