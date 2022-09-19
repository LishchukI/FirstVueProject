using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
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
            mess.DateTime = DateTime.Now;
            messageDBContext.Messages.Add(mess);
            messageDBContext.SaveChanges();
            return Ok(mess);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Message message = messageDBContext.Messages.Where(x => x.Id == id).First();
            messageDBContext.Messages.Remove(message);
            messageDBContext.SaveChanges();
            return Ok(id);
        }
        [HttpPut()]
        public IActionResult Update([FromBody] Message mess)
        {
            messageDBContext.Messages.Update(mess);
            messageDBContext.SaveChanges();
            return Ok(mess);
        }
    }
}
