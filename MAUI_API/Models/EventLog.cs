using System.Reflection.Metadata;

namespace MAUI_API.Models
{
    public class EventLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Information { get; set; }
        public string IPAddress { get; set; }
        public User? User { get; set; }
    }
}
