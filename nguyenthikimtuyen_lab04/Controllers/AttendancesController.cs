using Microsoft.AspNet.Identity;
using nguyenthikimtuyen_lab04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using nguyenthikimtuyen_lab04.DTOs;

namespace nguyenthikimtuyen_lab04.Controllers
{
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId))
                return BadRequest("The Attdance Already Exits!");
            var attendance = new Attendance
            {
                CourseId = attendanceDto.CourseId,
                AttendeeId = userId
            };
            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
