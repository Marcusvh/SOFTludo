﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoftLudoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiceController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public int RollDice()
        {
            return 0;
        }
    }
}
