using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS4540PS2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Author: Valerie German
/// Date: 25 Sept 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains controller for displaying (and in the future editing) users and roles.
/// </summary>
namespace CS4540PS2.Controllers {
    [Authorize(Roles="Admin")]
    public class UserController : Controller {
        private readonly LearningOutcomeDBContext _context;
        private readonly UserContext _userContext;
        private UserManager<IdentityUser> _userManager;
        /// <summary>
        /// Construct a course controller with a database context.
        /// </summary>
        /// <param name="context"></param>
        public UserController(LearningOutcomeDBContext context, UserContext userContext, UserManager<IdentityUser> userManager) {
            _context = context;
            _userContext = userContext;
            _userManager = userManager;
        }
        public IActionResult Index() {
            return View();
        }

        public JsonResult AddRole(string username, string role) {
            _userManager.AddToRoleAsync(_userContext.Users.Where(u => u.UserName == username).FirstOrDefault(), role).Wait();
            return Json(new { success = true });
        }

    }
}