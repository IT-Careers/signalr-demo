namespace SignalRDemo.Data.Entities
{
    public class Message
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Content { get; set; }

        public string UserId { get; set; }   
        
        public AppUser User { get; set; }
    }
}
