﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCP.Api.Controllers.Base;
using SCP.Application.Core.UserInf;
using SCP.Application.Services;

namespace SCP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : CustomController
    {
        private readonly UserCore userCore;
        private readonly RabbitMqService rabbitMqService;

        public UserController(UserCore userCore, RabbitMqService rabbitMqService)
        {
            this.userCore = userCore;
            this.rabbitMqService = rabbitMqService;
        }

        [AllowAnonymous]
        [HttpGet("Info")]
        public async Task<ActionResult> Info([FromQuery] string? uId)
        {
            var res = await userCore.GetUserInfo(uId ?? ContextUserId.ToString());
            return res.IsSuccess ? Ok(res.Data) : BadRequest(res.ErrorList);
        }


        [Route("[action]/{data}")]
        [HttpPost]
        public async Task<ActionResult> SendMessageRb(string? data)
        {
            data = data ?? DateTime.UtcNow.ToString();

            rabbitMqService.SendMessage(data);
            return Ok("OK");
        }
    }
}
