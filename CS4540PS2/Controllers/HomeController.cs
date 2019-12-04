using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CS4540PS2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Author: Valerie German
/// Date: 25 Sept 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains controller for home webpages.
/// </summary>
namespace CS4540PS2.Controllers {
    public class HomeController : Controller {
        private readonly LOTDBContext _context;
        /// <summary>
        /// Creates home controller with given database context.
        /// </summary>
        /// <param name="context"></param>
        public HomeController(LOTDBContext context) {
            _context = context;
        }
        /// <summary>
        /// Default home page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index() {
            return View();
        }

        /// <summary>
        /// Display the user's notifications
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Notifications() {
            UserLocator userLoc = _context.UserLocator.Where(u => u.UserLoginEmail == User.Identity.Name)
                .Include(u => u.Notifications)
                .ThenInclude(n => n.CourseInstance)
                .FirstOrDefault();
            if (userLoc == null) return NotFound();
            return View(userLoc);
        }

        /// <summary>
        /// Marks the given notification as read if the notification belongs to the current user.
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        [Authorize]
        public IActionResult ReadNotification(int? notificationId) {
            if (notificationId == null) return new JsonResult(new { success = false });
            Notifications notify = _context.Notifications.Include(n => n.User)
                .Where(n => n.NotificationId == notificationId && n.User.UserLoginEmail == User.Identity.Name).FirstOrDefault();
            if (notify == null) return new JsonResult(new { success = false });
            notify.Read = true;
            _context.SaveChanges();
            return new JsonResult(new { success = true });
        }

        /// <summary>
        /// Removes the given notification entry.
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        [Authorize]
        public IActionResult DeleteNotification(int? notificationId) {
            if (notificationId == null) return new JsonResult(new { success = false });
            Notifications notify = _context.Notifications.Include(n => n.User)
                .Where(n => n.NotificationId == notificationId && n.User.UserLoginEmail == User.Identity.Name).FirstOrDefault();
            if (notify == null) return new JsonResult(new { success = false });
            _context.Notifications.Remove(notify);
            _context.SaveChanges();
            return new JsonResult(new { success = true });
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
