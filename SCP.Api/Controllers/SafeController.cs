﻿using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SCP.Api.Controllers.Base;
using SCP.Api.DTO;
using SCP.Application.Core.Safe;

namespace SCP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SafeController : CustomController
    {
        private readonly SafeCore safeCore;

        public SafeController(SafeCore safeCore)
        {
            this.safeCore = safeCore;
        }

        /// <summary>
        /// Создание собственного сейфа
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost(nameof(CreateMy))]
        public async Task<ActionResult> CreateMy(CreateSafeDTO dto)
        {
            var command = dto.Adapt<CreateSafeCommand>();
            command.UserId = ContextUserId;
            var res = await safeCore.CreateUserSafe(command);
            return res.IsSuccess ? Ok(res.Data) : BadRequest(res.ErrorList);
        }

        /// <summary>
        /// Получение всех связанных сейфов с текущим пользователем
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetLinked))]
        public async Task<ActionResult> GetLinked()
        {
            var query = new GetLinkedSafesQuery() { UserId = ContextUserId };
            var res = await safeCore.GetLinked(query);
            return res.IsSuccess ? Ok(res.Data) : BadRequest(res.ErrorList);
        }


        /// <summary>
        /// Получение публичного ключа сейфа для отправки зашифрованной записи
        /// </summary>
        /// <param name="recId"></param>
        /// <param name="clientPublicKey"></param>
        /// <returns></returns>
        [HttpGet("Pubk/{safeId}")]
        public async Task<IActionResult> GetPubKForSafe(string safeId)
        {
            var res = safeCore.GetPubKForSafe(safeId);
            return res.IsSuccess ? Ok(res.Data) : BadRequest(res.ErrorList);
        }


        /// <summary>
        /// Получение статистики для сейфа
        /// </summary>
        /// <param name="sId"></param>
        /// <returns></returns>
        [HttpGet(nameof(GetStat))]
        public async Task<ActionResult> GetStat(string sId)
        {
            var res = await safeCore.GetStat(Guid.Parse(sId));
            return res.IsSuccess ? Ok(res.Data) : BadRequest(res.ErrorList);
        }
    }
}
