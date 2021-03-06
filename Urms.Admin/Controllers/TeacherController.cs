﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Urms.Model;
using Urms.Service;

namespace Urms.Admin.Controllers
{
    [Authorize(Roles ="Admin,Teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly IDesignationService designationService;
        private readonly IDepartmentService departmentService;
        private readonly ITeacherService teacherService;

        public TeacherController(IDesignationService designationService, IDepartmentService departmentService, ITeacherService teacherService, ApplicationUserManager userManager):base(userManager)
        {
            this.designationService = designationService;
            this.departmentService = departmentService;
            this.teacherService = teacherService;
        }
        // GET: Teacher
        public ActionResult Index()
        {
            var teachers = teacherService.GetAll();
            return View(teachers);
        }
        public ActionResult Create()
        {
            var teachers = new Teacher();
            ViewBag.DesignationId = new SelectList(designationService.GetAll(), "Id", "DesignationName");
            ViewBag.DeptId = new SelectList(departmentService.GetAll(), "Id", "DeptName");
            return View(teachers);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacherService.Insert(teacher);
                return RedirectToAction("Index");
            }
            ViewBag.DesignationId = new SelectList(designationService.GetAll(), "Id", "DesignationName",teacher.DesignationId);
            ViewBag.DeptId = new SelectList(departmentService.GetAll(), "Id", "DeptName",teacher.DeptId);
            return View();
        }
        public ActionResult Edit(Guid id)
        {
            var teacher = teacherService.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.DesignationId = new SelectList(designationService.GetAll(), "Id", "DesignationName", teacher.DesignationId);
            ViewBag.DeptId = new SelectList(departmentService.GetAll(), "Id", "DeptName", teacher.DeptId);
            return View(teacher);


        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                var model = teacherService.Find(teacher.Id);
                model.TeacherName = teacher.TeacherName;
                model.Address = teacher.Address;
                model.Email = teacher.Email;
                model.DesignationId = teacher.DesignationId;
                model.DeptId = teacher.DeptId;
                model.CreaditToBeTaken = teacher.CreaditToBeTaken;
                model.RemaingCreadit = teacher.RemaingCreadit;
                teacherService.Update(model);
                return RedirectToAction("Index");

            }
            ViewBag.DesignationId = new SelectList(designationService.GetAll(), "Id", "DesignationName", teacher.DesignationId);
            ViewBag.DeptId = new SelectList(departmentService.GetAll(), "Id", "DeptName", teacher.DeptId);
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            teacherService.Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Details(Guid id)
        {
            return View(teacherService.Find(id));
        }
    }
}