namespace SignalRDemo.Data.Entities
{
    public class AppUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
