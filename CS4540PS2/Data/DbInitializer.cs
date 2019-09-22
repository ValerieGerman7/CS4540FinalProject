using CS4540PS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IO;

/// <summary>
/// Author: Valerie German
/// Date: 10 Sept 2019
/// Course: CS 4540, University of Utah
/// Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
/// I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
/// File Contents: Initializes the database with testing data if empty.
/// </summary>
namespace CS4540PS2.Data {
    public class DbInitializer {
        public static async Task InitializeUser(UserContext context, IServiceProvider provider) {
            //context.Database.EnsureCreated();
            context.Database.Migrate();

            //Referencesd https://romansimuta.com/blogs/blog/authorization-with-roles-in-asp.net-core-mvc-web-application in
            //initializing users database.
            //Create Roles
            var manager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] availableRoles = { "Admin", "Instructor", "Chair" };
            IdentityResult identRes;
            foreach(string role in availableRoles) {
                var existing = await manager.RoleExistsAsync(role);
                if (!existing) {
                    identRes = await manager.CreateAsync(new IdentityRole(role));
                }
            }
            if (!context.Users.Any()) { //Only add if database is empty
                IdentityUser user0 = new IdentityUser() {
                    Email = "testInstructor@gmail.com",
                    UserName = "TestInstructor",
                    PasswordHash = new PasswordHasher<UserContext>().HashPassword(context, "Password0?")
                };
                IdentityUser user1 = new IdentityUser() {
                    Email = "testAdmin@gmail.com",
                    UserName = "TestAdmin",
                    PasswordHash = new PasswordHasher<UserContext>().HashPassword(context, "Password0?")
                };
                IdentityUser user2 = new IdentityUser() {
                    Email = "testChair@gmail.com",
                    UserName = "TestChair",
                    PasswordHash = new PasswordHasher<UserContext>().HashPassword(context, "Password0?")
                };
                IdentityUser[] users = { user0, user1, user2 };
                foreach (IdentityUser user in users) {
                    if (!context.Users.Any(u => u.UserName == user.UserName)) {
                        context.Users.Add(user);
                    }
                }
                context.SaveChanges();
                //Give users roles
                UserManager<IdentityUser> manageRoles = provider.GetService<UserManager<IdentityUser>>();
                await manageRoles.AddToRoleAsync(user0, "Instructor");
                await manageRoles.AddToRoleAsync(user1, "Admin");
                await manageRoles.AddToRoleAsync(user2, "Chair");
                await context.SaveChangesAsync();
            }
        }
        public static void Initialize(LearningOutcomeDBContext context) {
            if (context.Database.EnsureCreated()) {
                //context.Database.Migrate();
            }
            //
            if (context.CourseInstance.Any()) {
                return;
            }
            var ci0 = new CourseInstance { Name = "Web Software Architecture", Description = "Web software, HTML, JavaScript.", Department = "CS", Number = 4540, Semester = "Fall", Year = 2019 };
            var ci1 = new CourseInstance { Name = "Discrete Structures", Description = "Proofs, statistics, booleans...", Department = "CS", Number = 2100, Semester = "Fall", Year = 2019 };
            var ci2 = new CourseInstance { Name = "Software Practice I", Description = "Students will learn how to program in C#.", Department = "CS", Number = 3500, Semester = "Fall", Year = 2019 };
            var ci3 = new CourseInstance { Name = "Software Practice II", Description = "Students will learn how to program in C++.", Department = "CS", Number = 3505, Semester = "Fall", Year = 2019 };
            var courses = new CourseInstance[] { ci0, ci1, ci2, ci3 };
            foreach(CourseInstance co in courses) {
                context.CourseInstance.Add(co);
            }
            context.SaveChanges();
            var lo0 = new LearningOutcomes { CourseInstance = ci0, Name = "Learn HTML", Description = "Students will learn how to use HTML to create static and dynamic web pages" };
            var lo1 = new LearningOutcomes { CourseInstance = ci0, Name = "JavaScript", Description = "Students will learn how to use JavaScript in web pages" };
            var lo2 = new LearningOutcomes { CourseInstance = ci0, Name = "Publish a Web Server", Description = "Students will learn how to publish a web server" };
            var lo3 = new LearningOutcomes { CourseInstance = ci0, Name = "Example Learning Outcome", Description = "Example learning outcome description. This is where the deparment's description of the desired learning outcome will be displayed." };
            var lo4 = new LearningOutcomes { CourseInstance = ci1, Name = "Write proofs", Description = "Students are able to write proofs for common problems" };
            var lo5 = new LearningOutcomes { CourseInstance = ci1, Name = "Boolean Logic", Description = "Students are able to solve boolean logic." };
            var lo6 = new LearningOutcomes { CourseInstance = ci2, Name = "Write C# Program", Description = "Students are able to write code in C#" };
            var learningo = new LearningOutcomes[] { lo0, lo1, lo2, lo3, lo4, lo5, lo6 };
            foreach(LearningOutcomes lo in learningo) {
                context.LearningOutcomes.Add(lo);
            }
            context.SaveChanges();
            var em0 = new EvaluationMetrics { Lo=lo0, Name = "PS1", Description = "Students will write static HTML web pages." };
            var em1 = new EvaluationMetrics { Lo = lo0, Name = "Exam 1", Description = "The exam covers HTML concepts such as __ and __." };
            var em2 = new EvaluationMetrics { Lo = lo1, Name = "Assignment 1", Description = "Assignment description ... this is where the professor describes how the assignment/exam fulfills the learning outcome. " +
                    "The file for this assignment can be downloaded/viewed from here, as well as the sample files." };
            var em3 = new EvaluationMetrics { Lo = lo3, Name = "Example Assignment/Exam", Description = "Assignment description ... this is where the professor describes how the assignment/exam fulfills the learning outcome. " +
                    "The file for this assignment can be downloaded/viewed from here, as well as the sample files." };
            var em4 = new EvaluationMetrics { Lo = lo4, Name = "Homework 1", Description = "Students will write proofs." };
            var em5 = new EvaluationMetrics { Lo = lo6, Name = "Programming Assignment 1", Description = "Students write C# program." };
            var evals = new EvaluationMetrics[] { em0, em1, em2, em3, em4, em5 };
            foreach(EvaluationMetrics em in evals) {
                context.EvaluationMetrics.Add(em);
            }
            context.SaveChanges();
            var samples = new SampleFiles[] {
                new SampleFiles { Em=em0, FileName="Example_Student_Work.pdf", Score=60 },
                new SampleFiles { Em=em0, FileName="Example_Student_Work.pdf", Score=70 },
                new SampleFiles { Em=em0, FileName="Example_Student_Work.pdf", Score=80 },
                new SampleFiles { Em=em0, FileName="Example_Student_Work.pdf", Score=90 },
                new SampleFiles { Em=em1, FileName="Example_Student_Work.pdf", Score=45 },
                new SampleFiles { Em=em1, FileName="Example_Student_Work.pdf", Score=70 },
                new SampleFiles { Em=em1, FileName="Example_Student_Work.pdf", Score=95 },
                new SampleFiles { Em=em3, FileName="Example_Student_Work.pdf", Score=0 },
                new SampleFiles { Em=em3, FileName="Example_Student_Work.pdf", Score=50 },
                new SampleFiles { Em=em3, FileName="Example_Student_Work.pdf", Score=100 },
                new SampleFiles { Em=em5, FileName="Example_Student_Work.pdf", Score=100 }
            };
            foreach(SampleFiles s in samples) {
                context.SampleFiles.Add(s);
            }
            context.SaveChanges();

        }
    }
}
