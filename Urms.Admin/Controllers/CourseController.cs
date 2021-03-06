﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Urms.Model;
using Urms.Service;

namespace Urms.Admin.Controllers
{
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;
        private readonly IDepartmentService departmentService;
        private readonly ISemesterService semesterService;
       

        public CourseController(ICourseService courseService, IDepartmentService departmentService, ISemesterService semesterService, ApplicationUserManager userManager) : base(userManager)
        {
            this.courseService = courseService;
            this.departmentService = departmentService;
            this.semesterService = semesterService;
          
        }
        // GET: Course
        public ActionResult Index()
        {
            var courses = courseService.GetAll();
            return View(courses);
        }
        public ActionResult Create()
        {
            
            var course = new Course();
            ViewBag.DeptId = new SelectList(departmentService.GetAll(), "Id", "DeptName");
            ViewBag.SemesterId = new SelectList(semesterService.GetAll(), "Id", "SemesterName");
           
            return View(course);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                
                courseService.Insert(course);
                return RedirectToAction("Index");
            }
            ViewBag.DeptId = new SelectList(departmentService.GetAll(), "Id", "DeptName", course.DeptId);
            ViewBag.SemesterId = new SelectList(semesterService.GetAll(), "Id", "SemesterName", course.SemesterId);
          
            return View();
        }
        public ActionResult Edit(Guid id)
        {
            var course = courseService.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeptId = new SelectList(departmentService.GetAll(), "Id", "DeptName", course.DeptId);
            ViewBag.SemesterId = new SelectList(semesterService.GetAll(), "Id", "SemesterName", course.SemesterId);
           
            return View(course);


        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                var model = courseService.Find(course.Id);
                model.CourseCode = course.CourseCode;
                model.CourseName = course.CourseName;
                model.Credit = course.Credit;
                model.Description = course.Description;
                model.IsActive = course.IsActive;
                courseService.Update(model);
                return RedirectToAction("Index");

            }
            ViewBag.DeptId = new SelectList(departmentService.GetAll(), "Id", "DeptName", course.DeptId);
            ViewBag.SemesterId = new SelectList(semesterService.GetAll(), "Id", "SemesterName", course.SemesterId);
         
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            courseService.Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Details(Guid id)
        {
            return View(courseService.Find(id));
        }
    }
}