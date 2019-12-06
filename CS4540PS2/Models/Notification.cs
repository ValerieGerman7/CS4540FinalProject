using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Author: Valerie German
/// Date: 5 Dec 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: Class for timed notifications.
/// </summary>
namespace CS4540PS2.Models {
    public class Notification {
        private static Notification self;
        private Timer time;

        //private static LOTDBContext _context;
        private static IServiceProvider services;
        public Notification(IServiceProvider service) {
            services = service;
            //_context = context;
            Debug.WriteLine("Notification initialized");
            DateTime now = DateTime.Now;
            DateTime next = DateTime.Now.AddSeconds(1); //Next check is in 1 day
            TimeSpan timeRemaining = next - now;
            if (timeRemaining <= TimeSpan.Zero) {
                timeRemaining = TimeSpan.Zero;
            }
            Action not = delegate () { Notify(); };
            time = new Timer(t => { not.Invoke(); }, null, timeRemaining, TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// Send out notifications to instructors when courses are due within a week.
        /// </summary>
        private void Notify() {
            Debug.WriteLine("Notify called: " + DateTime.Now.ToString());
            //LOTDBContext _context = new LOTDBContext()
            //Debug.WriteLine("Read DB: " + _context.Notifications.First().Text);
        }
    }

}
