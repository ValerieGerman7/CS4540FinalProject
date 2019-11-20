using CS4540PS2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Author: Valerie German
/// Date: 19 Nov 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: This file contains controller the instructor's actions on sample files. This continues the InstructorController class.
/// </summary>
namespace CS4540PS2.Controllers {
   [Authorize(Roles = "Instructor")]
    public partial class InstructorController : Controller {


        [HttpPost]
        public JsonResult CreateSampleFile(int courseId, int emId, int score){//, IFormFile sample) {
            //Verify professor
            /*using (MemoryStream memStream = new MemoryStream()) {
                sample.CopyToAsync(memStream).Wait();
                //File must be < 2MB
                if (memStream.Length < 2097152) {
                    var files = new { Content = memStream.ToArray() };
                    SampleFiles sf = new SampleFiles() {
                        Score = score,
                        FileName = Encoding.UTF8.GetString(memStream.ToArray())
                    };
                    _context.SampleFiles.Add(sf);
                    _context.SaveChanges();
                    return Json(new { success = true });
                } else {
                    return Json(new { success = false, reason = "File too large. Must be less than 2MB." });
                }
            }*/
            return Json(new { success = true });

        }



    }
}
