using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using vueSQL.Models;

namespace vueSQL.Controllers
{
    [Route("/api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageDBContext messageDBContext;

        public MessagesController(IConfiguration configuration, IWebHostEnvironment env, MessageDBContext messageDBContext)
        {
            this.messageDBContext = messageDBContext;
        }


        [HttpGet("getfirst")]
        public IActionResult Get()
        {
            return Ok(messageDBContext.Messages.ToList());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Message mess)
        {
            messageDBContext.Messages.Add(mess);
            messageDBContext.SaveChanges();
            return Ok(mess);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromBody] Message mess)
        {
            messageDBContext.Messages.Remove(mess);
            messageDBContext.SaveChanges();
            return Ok(mess);
        }
    }
}
