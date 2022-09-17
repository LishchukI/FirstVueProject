using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using vusSQL.Models;
using Microsoft.AspNetCore.Http;

namespace vusSQL.Controllers
{
    [Route("/api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private readonly MessageDBContext messageDBContext;

        public MessagesController(IConfiguration configuration, IWebHostEnvironment env, MessageDBContext messageDBContext)
        {
            _configuration = configuration;
            _env = env;
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
