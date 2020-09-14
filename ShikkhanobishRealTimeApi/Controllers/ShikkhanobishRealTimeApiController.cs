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
        public async Task<IActionResult> ClTeacher ( string SessionId , string UserToken , int studentID, int teacherID, string Cls, string subject, double cost, string studentName )
        {
            await _hubContext.Clients.All.SendAsync ( "CallInfo" , SessionId , UserToken, studentID, teacherID, Cls, subject,cost, studentName );

            return Ok ( "ok" );
        }

        [HttpPost ( "SendStudentThatCallRecivedOrIgnored" )]
        public async Task<IActionResult> CallConfirmation ( int studentID, int teacherID, bool recivedOrNot )
        {
            await _hubContext.Clients.All.SendAsync ( "SendStudentThatCallRecivedOrIgnored" , studentID , teacherID , recivedOrNot );

            return Ok ( "ok" );
        }
        [HttpPost ( "sendTime" )]
        public async Task<IActionResult> sendTime ( int sec, int teacherID )
        {
            await _hubContext.Clients.All.SendAsync ( "sendTime" , sec , teacherID );

            return Ok ( "ok" );
        }
        [HttpPost ( "cutCall" )]
        public async Task<IActionResult> cutCall ( int stop , int teacherID, int studentID, bool isStudent )
        {
            await _hubContext.Clients.All.SendAsync ( "cutCall" , stop , teacherID, studentID, isStudent );

            return Ok ( "Sent End CAll event" );
        }

    }
}
