﻿using System;
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
            await _hubContext.Clients.All.SendAsync ( "CallInfo" , SessionId , UserToken, studentID, teacherID, Cls, subject,cost, studentName);

            return Ok ( "ok" );
        }

        [HttpPost ( "SendStudentThatCallRecivedOrIgnored" )]
        public async Task<IActionResult> CallConfirmation ( int studentID, int teacherID, bool recivedOrNot )
        {
            await _hubContext.Clients.All.SendAsync ( "SendStudentThatCallRecivedOrIgnored" , studentID , teacherID , recivedOrNot );

            return Ok ( "ok" );
        }
        [HttpPost ( "sendCost" )]
        public async Task<IActionResult> sendCost ( float cost, int teacherID, int studentID )
        {
            await _hubContext.Clients.All.SendAsync ( "sendCost" , cost , teacherID , studentID );

            return Ok ( "ok" );
        }
        [HttpPost ( "sendTime" )]
        public async Task<IActionResult> sendTime ( int teacherID  )
        {
            await _hubContext.Clients.All.SendAsync ( "sendTime" ,teacherID );

            return Ok ( "ok" );
        }
        [HttpPost ( "cutCall" )]
        public async Task<IActionResult> cutCall ( int stop , int teacherID, int studentID, bool isStudent )
        {
            await _hubContext.Clients.All.SendAsync ( "cutCall" , stop , teacherID, studentID, isStudent );

            return Ok ( "Sent End CAll event" );
        }
        [HttpPost ( "TurnOffActiveStatus" )]
        public async Task<IActionResult> TurnOffActiveStatus ( int TeacherID, string isOnline )
        {
            await _hubContext.Clients.All.SendAsync ( "TurnOffActiveStatus" , TeacherID ,isOnline);

            return Ok ( "Turn off active status" );
        }
        [HttpPost("SendShortNote")]
        public async Task<IActionResult> SendShortNote(int teacherID ,string shortNote)
        {
            await _hubContext.Clients.All.SendAsync("SendShortNote", teacherID, shortNote);

            return Ok("Short Note Sent");
        }

    }
}
