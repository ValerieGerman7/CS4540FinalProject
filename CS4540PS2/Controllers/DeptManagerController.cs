using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS4540PS2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Author: Valerie German
/// Date: 21 Nov 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains controller for department managing web pages. Administrators may create and modify existing departments.
/// </summary>
namespace CS4540PS2.Controllers {
    [Authorize(Roles="Admin")]
    public class DeptManagerController : Controller {
        private readonly LOTDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DeptManagerController(LOTDBContext context, RoleManager<IdentityRole> role) {
            _context = context;
            _roleManager = role;
        }

        /// <summary>
        /// View all existing departments
        /// </summary>
        /// <returns></returns>
        public IActionResult Index() {
            return View(_context.Departments.OrderByDescending(d => d.Code));
        }

        /// <summary>
        /// GET for department create page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create() {
            return View();
        }


        /// <summary>
        /// POST for creating a new department.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(string code, string name) {
            if(name != null && name.Length > 0 && code != null && code.Length > 0 && code.Length <= 5) {
                if(_context.Departments.Where(d => d.Code == code).Any()) {
                    return RedirectToAction("Index");
                }
                _context.Departments.Add(new Departments() { Name = name, Code = code });
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get for department editing page.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string code) {
            if (code == null) {
                return NotFound();
            }
            var dept = await _context.Departments.FindAsync(code);
            if (dept == null) {
                return NotFound();
            }
            return View(dept);
        }

        /// <summary>
        /// POST for editing a department.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string code, string name) {
            if(code == null) {
                return NotFound();
            }
            var dept = _context.Departments.Where(d => d.Code == code).FirstOrDefault();
            if(dept == null) {
                return NotFound();
            }
            dept.Name = name;
            _context.Update(dept);
            _context.SaveChanges();
            return RedirectToAction("Edit", new { code = code });
        }

        /// <summary>
        /// GET for deleting a department
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string code) {
            if (code == null) {
                return NotFound();
            }
            var dept = await _context.Departments.FirstOrDefaultAsync(d => d.Code == code);
            if (dept == null) {
                return NotFound();
            }
            if (dept.CourseInstance.Any()) {
                return Json(new { success = false });
            }
            _context.Departments.Remove(dept);
            _context.SaveChanges();
            return Json(new { success = true });
        }


    }
}