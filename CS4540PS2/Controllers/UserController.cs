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
/// Date: 18 Oct 2019
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
        /// <summary>
        /// View containing all users, where user roles can be modified.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index() {
            return View();
        }

        /// <summary>
        /// Changes the given user's status of the given role. If the user has that role, the user is removed from that
        /// role, if the user is not in that role the user is given that role.
        /// If the user is the last administrator, a warning is returned.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public JsonResult ChangeRole(string username, string role) {
            int x = 0;
            IdentityUser user = _userContext.Users.Where(u => u.UserName == username).FirstOrDefault();
            if(user == null) {
                return Json(new { success = false, reason = "The user could not be found." });
            }
            bool isInRole = _userManager.IsInRoleAsync(user, role).Result;
            if (isInRole) {
                if(role.Equals("Admin") && _userManager.GetUsersInRoleAsync("Admin").Result.Count() == 1) {
                    return Json(new { success = false, reason = "This is the last administrator." });
                }
                _userManager.RemoveFromRoleAsync(user, role).Wait();
            } else {
                _userManager.AddToRoleAsync(user, role).Wait();
            }
            return Json(new { success = true, isRole = !isInRole });
        }

    }
}