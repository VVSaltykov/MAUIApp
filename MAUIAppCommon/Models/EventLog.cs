using System.Reflection.Metadata;

namespace MAUIAppCommon.Models
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
