namespace Domain.Models
{
    public class ClienteDto
    {
        public string Dni { get; set; } = null!;
        public string name { get; set; } = null!;
        public string lastname { get; set; } = null!;
        public string address { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
    }
}
