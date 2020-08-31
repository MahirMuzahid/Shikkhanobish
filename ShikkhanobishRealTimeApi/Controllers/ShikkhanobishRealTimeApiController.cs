using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ShikkhanobishRealTimeApi.Controllers
{
    [Route ( "api/[controller]" )]
    [ApiController]
    public class ShikkhanobishRealTimeApiController : ControllerBase
    {
        private readonly IHubContext<ShikkhanobishHub> _hubContext;
        public ShikkhanobishRealTimeApiController ( IHubContext<ShikkhanobishHub> hubContext )
        {
            _hubContext = hubContext;
        }

        [HttpPost ( "CallTeacher" )]
        public async Task<IActionResult> Post ( int ApiKey , string SessionId , string UserToken , int studentID, int teacherID, string Cls, string subject )
        {
            await _hubContext.Clients.All.SendAsync ( "CallInfo" , ApiKey , SessionId , UserToken, studentID, teacherID, Cls, subject );

            return Ok ( "Call Done!" );
        }

    }
}
