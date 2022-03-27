using System.ComponentModel.DataAnnotations;

namespace SignalRDemo.Data.Entities
{
    public class AppUser
    {
        [MaxLength(255)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(255)]
        public string Username { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }
    }
}
