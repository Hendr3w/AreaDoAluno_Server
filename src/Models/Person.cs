namespace AreaDoAluno.Models
{
    public class Person 
    {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Cpf { get; set; }
    public string? Rg { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Rgm { get; set; }
    public char Sex { get; set; }
    public Adress? Adress { get; set; }
    public int AdressId { get; set; }
    public DateOnly Birthdate { get; set; }
    public Person(){}
    }  
}

