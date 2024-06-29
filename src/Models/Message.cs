namespace AreaDoAluno.Models
{
    public class Message
    {
        public int Id { get; set;}
        public string? Body { get; set; }
        public Class? Class { get; set; }
        public int ClassId { get; set; }

        public Message() {}
    }
}