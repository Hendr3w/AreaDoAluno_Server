namespace AreaDoAluno.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Cep { get; set; }

        public Address() {}

    }
}
