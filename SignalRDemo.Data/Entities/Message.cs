using System.ComponentModel.DataAnnotations;

namespace SignalRDemo.Data.Entities
{
    public class Message
    {
        [MaxLength(255)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(255)]
        public string Content { get; set; }

        [MaxLength(255)]
        public string UserId { get; set; }   
        
        public AppUser User { get; set; }
    }
}
