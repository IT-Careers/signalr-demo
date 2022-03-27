using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRDemo.Data;

namespace SignalRDemo.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public MessagesController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(this.appDbContext.Messages
                .Include(message => message.User)
                .Select(message => new
                {
                    Content = message.Content,
                    Username = message.User.Username,
                })
                .ToList());
        }
    }
}
