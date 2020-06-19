﻿using nguyenthikimtuyen_lab04.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nguyenthikimtuyen_lab04.ViewModels;
using System.Data.Entity;

namespace nguyenthikimtuyen_lab04.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;


        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();

        }
        // GET: Courses
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList()

            };
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place

            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Attendances
            .Where(a => a.AttendeeId == userId)
            .Select(a => a.Course)
            .Include(l => l.Lecturer)
            .Include(l => l.Category)
            .ToList();

            var viewModel = new CourseViewModel
            {
                UpcommingCourse = courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }

    }

}
