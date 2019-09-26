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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
                //UserManager<IdentityUser> manageRoles = provider.GetService<UserManager<IdentityUser>>();
                IdentityUser user0 = new IdentityUser() {
                    Email = "professor_jim@cs.utah.edu",
                    NormalizedEmail = "PROFESSOR_JIM@CS.UTAH.EDU",
                    UserName = "professor_jim@cs.utah.edu",
                    NormalizedUserName = "PROFESSOR_JIM@CS.UTAH.EDU",
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                    SecurityStamp = "Stamp"
                };
                //await manageRoles.CreateAsync(user0, "Password0?");
                IdentityUser user1 = new IdentityUser() {
                    Email = "admin_erin@cs.utah.edu",
                    NormalizedEmail = "ADMIN_ERIN@CS.UTAH.EDU",
                    UserName = "admin_erin@cs.utah.edu",
                    NormalizedUserName = "ADMIN_ERIN@CS.UTAH.EDU",
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                    SecurityStamp = "Stamp"
                };
                //await manageRoles.CreateAsync(user1, "Password0?");
                IdentityUser user2 = new IdentityUser() {
                    Email = "chair_whitaker@cs.utah.edu",
                    NormalizedEmail = "CHAIR_WHITAKER@CS.UTAH.EDU",
                    UserName = "chair_whitaker@cs.utah.edu",
                    NormalizedUserName = "CHAIR_WHITAKER@CS.UTAH.EDU",
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                    SecurityStamp = "Stamp"
                };
                IdentityUser user3 = new IdentityUser() {
                    Email = "professor_mary@cs.utah.edu",
                    NormalizedEmail = "PROFESSOR_MARY@CS.UTAH.EDU",
                    UserName = "professor_mary@cs.utah.edu",
                    NormalizedUserName = "PROFESSOR_MARY@CS.UTAH.EDU",
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                    SecurityStamp = "Stamp"
                };
                IdentityUser user4 = new IdentityUser() {
                    Email = "professor_danny@cs.utah.edu",
                    NormalizedEmail = "PROFESSOR_DANNY@CS.UTAH.EDU",
                    UserName = "professor_danny@cs.utah.edu",
                    NormalizedUserName = "PROFESSOR_DANNY@CS.UTAH.EDU",
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                    SecurityStamp = "Stamp"
                };
                //await manageRoles.CreateAsync(user2, "Password0?");
                (IdentityUser, string)[] users = {
                    (user0, "Instructor"),
                    (user1, "Admin"),
                    (user2, "Chair"),
                    (user3, "Instructor"),
                    (user4, "Instructor")
                };
                foreach ((IdentityUser, string) user in users) {
                    if (!context.Users.Any(u => u.UserName == user.Item1.UserName)) {
                        var pass = new PasswordHasher<IdentityUser>().HashPassword(user.Item1, "123ABC!@#def");
                        user.Item1.PasswordHash = pass;
                        var userStor = new UserStore<IdentityUser>(context);
                        await userStor.CreateAsync(user.Item1);
                        await userStor.AddToRoleAsync(user.Item1, user.Item2);
                        //context.Users.Add(user);
                    }
                }
                await context.SaveChangesAsync();
                //Give users roles
                //await manageRoles.AddToRoleAsync(user0, "Instructor");
                //await manageRoles.AddToRoleAsync(user1, "Admin");
                //await manageRoles.AddToRoleAsync(user2, "Chair");
                //await context.SaveChangesAsync();
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
            var ci0 = new CourseInstance { Name = "Web Software Architecture",
                Description = "Software architectures, programming models, and programming environments pertinent to developing web " +
                "applications.  Topics include client-server model, multi-tier software architecture, client-side scripting (JavaScript), " +
                "server-side programming (Servlets and JavaServer Pages), component reuse (JavaBeans), database connectivity (JDBC), and " +
                "web servers.",
                Department = "CS", Number = 4540, Semester = "Fall", Year = 2019 };
            var ci1 = new CourseInstance { Name = "Introduction To Algorithms and Data Structures",
                Description = "This course provides an introduction to the problem of engineering computational efficiency into programs. " +
                "Students will learn about classical algorithms (including sorting, searching, and graph traversal), data structures " +
                "(including stacks, queues, linked lists, trees, hash tables, and graphs), and analysis of program space and time " +
                "requirements. Students will complete extensive programming exercises that require the application of elementary techniques " +
                "from software engineering.",
                Department = "CS", Number = 2420, Semester = "Fall", Year = 2019 };
            var ci2 = new CourseInstance { Name = "Software Practice",
                Description = "Practical exposure to the process of creating large software systems, including requirements specifications, " +
                "design, implementation, testing, and maintenance. Emphasis on software process, software tools (debuggers, profilers, source " +
                "code repositories, test harnesses), software engineering techniques (time management, code, and documentation standards, " +
                "source code management, object-oriented analysis and design), and team development practice. Much of the work will be in " +
                "groups and will involve modifying preexisting software systems.",
                Department = "CS", Number = 3500, Semester = "Fall", Year = 2019 };
            var ci3 = new CourseInstance { Name = "Discrete Structures",
                Description = "Introduction to propositional logic, predicate logic, formal logical arguments, finite sets, functions, relations," +
                " inductive proofs, recurrence relations, graphs, probability, and their applications to Computer Science.",
                Department = "CS", Number = 2100, Semester = "Fall", Year = 2019 };
            var ci4 = new CourseInstance { Name = "Computer Systems",
                Description = "Introduction to computer systems from a programmer's point of view.  Machine level representations of programs, " +
                "optimizing program performance, memory hierarchy, linking, exceptional control flow, measuring program performance, virtual memory, " +
                "concurrent programming with threads, network programming.",
                Department = "CS", Number = 4400, Semester = "Spring", Year = 2019 };
            var ci5 = new CourseInstance {
                Name = "Software Practice",
                Description = "Practical exposure to the process of creating large software systems, including requirements specifications, " +
                "design, implementation, testing, and maintenance. Emphasis on software process, software tools (debuggers, profilers, source " +
                "code repositories, test harnesses), software engineering techniques (time management, code, and documentation standards, " +
                "source code management, object-oriented analysis and design), and team development practice. Much of the work will be in " +
                "groups and will involve modifying preexisting software systems.",
                Department = "CS", Number = 3500, Semester = "Spring", Year = 2019
            };
            var courses = new CourseInstance[] { ci0, ci1, ci2, ci3, ci4, ci5 };
            foreach(CourseInstance co in courses) {
                context.CourseInstance.Add(co);
            }
            context.SaveChanges();
            #region Learning Outcomes
            //CS4540
            var lo0 = new LearningOutcomes { CourseInstance = ci0, Name = "HTML and CSS",
                Description = "Construct web pages using modern HTML and CSS practices, including modern frameworks."
            };
            var lo1 = new LearningOutcomes { CourseInstance = ci0, Name = "Accessibility",
                Description = "Define accessibility and utilize techniques to create accessible web pages." };
            var lo2 = new LearningOutcomes { CourseInstance = ci0, Name = "MVC",
                Description = "Outline and utilize MVC technologies across the “full-stack” of web design (including front-end, " +
                "back-end, and databases) to create interesting web applications. Deploy an application to a “Cloud” provider."
            };
            var lo3 = new LearningOutcomes { CourseInstance = ci0, Name = "Security",
                Description = "Describe the browser security model and HTTP; utilize techniques for data validation, secure session " +
                "communication, cookies, single sign-on, and separate roles."
            };
            var lo4 = new LearningOutcomes { CourseInstance = ci0, Name = "JavaScript and AJAX",
                Description = "Utilize JavaScript for simple page manipulations and AJAX for more complex/complete “single-page” applications."
            };
            var lo5 = new LearningOutcomes { CourseInstance = ci0, Name = "Responsive Webpages",
                Description = "Demonstrate how responsive techniques can be used to construct applications that are usable at a variety of page " +
                "sizes.  Define and discuss responsive, reactive, and adaptive."
            };
            var lo6 = new LearningOutcomes { CourseInstance = ci0, Name = "Web-Crawler",
                Description = "Construct a simple web-crawler for validation of page functionality and data scraping."
            };
            //CS2420
            var lo7 = new LearningOutcomes {
                CourseInstance = ci1, Name = "Implement and Analyze Data Structures",
                Description = "Implement, and analyze for efficiency, fundamental data structures (including lists, graphs, and trees) and " +
                "algorithms (including searching, sorting, and hashing)."
            };
            var lo8 = new LearningOutcomes {
                CourseInstance = ci1, Name = "Complexity",
                Description = "Employ Big-O notation to describe and compare the asymptotic complexity of algorithms, as well as perform empirical " +
                "studies to validate hypotheses about running time."
            };
            var lo9 = new LearningOutcomes {
                CourseInstance = ci1, Name = "Recognize Applications of Data Types",
                Description = "Recognize and describe common applications of abstract data types (including stacks, queues, priority queues, sets, " +
                "and maps)."
            };
            var lo10 = new LearningOutcomes {
                CourseInstance = ci1, Name = "Apply Algorithms to Real-World Data",
                Description = "Apply algorithmic solutions to real-world data."
            };
            var lo11 = new LearningOutcomes {
                CourseInstance = ci1, Name = "Generics",
                Description = "Use generics to abstract over functions that differ only in their types."
            };
            var lo12 = new LearningOutcomes {
                CourseInstance = ci1, Name = "Pair Programming",
                Description = "Appreciate the collaborative nature of computer science by discussing the benefits of pair programming."
            };
            //CS 3500
            var lo13 = new LearningOutcomes {
                CourseInstance = ci2, Name = "Large, Complex Software Systems with Process Models, Libraries and Software Development Tools",
                Description = "Design and implement large and complex software systems (including concurrent software) through the use " +
                "of process models (such as waterfall and agile), libraries (both standard and custom), and modern software development " +
                "tools (such as debuggers, profilers, and revision control systems)."
            };
            var lo14 = new LearningOutcomes {
                CourseInstance = ci2, Name = "Validation, Error Handling and Testing",
                Description = "Perform input validation and error handling, as well as employ advanced testing principles and tools to " +
                "systematically evaluate software."
            };
            var lo15 = new LearningOutcomes {
                CourseInstance = ci2, Name = "Model View Controller with GUI",
                Description = "Apply the model-view-controller pattern and event handling fundamentals to create a graphical user interface."
            };
            var lo16 = new LearningOutcomes {
                CourseInstance = ci2, Name = "Client-Server Model and APIs",
                Description = "Exercise the client-server model and high-level networking APIs to build a web-based software system."
            };
            var lo17 = new LearningOutcomes {
                CourseInstance = ci2, Name = "Relational Databases",
                Description = "Operate a modern relational database to define relations, as well as store and retrieve data."
            };
            var lo18 = new LearningOutcomes {
                CourseInstance = ci2, Name = "Peer Code Reviews",
                Description = "Appreciate the collaborative nature of software development by discussing the benefits of peer code reviews."
            };
            //CS 2100
            var lo19 = new LearningOutcomes {
                CourseInstance = ci3, Name = "Symbol Logic",
                Description = "Use symbolic logic to model real-world situations by converting informal language statements to propositional " +
                "and predicate logic expressions, as well as apply formal methods to propositions and predicates (such as computing normal " +
                "forms and calculating validity)."
            };
            var lo20 = new LearningOutcomes {
                CourseInstance = ci3, Name = "Relations",
                Description = "Analyze problems to determine underlying recurrence relations, as well as solve such relations by rephrasing " +
                "as closed formulas."
            };
            var lo21 = new LearningOutcomes {
                CourseInstance = ci3, Name = "Real-World Application",
                Description = "Assign practical examples to the appropriate set, function, or relation model, while employing the associated " +
                "terminology and operations."
            };
            var lo22 = new LearningOutcomes {
                CourseInstance = ci3, Name = "Counting, Permutations, Combinations and Combinatorics",
                Description = "Map real-world applications to appropriate counting formalisms, including permutations and combinations of sets, " +
                "as well as exercise the rules of combinatorics (such as sums, products, and inclusion-exclusion)."
            };
            var lo23 = new LearningOutcomes {
                CourseInstance = ci3, Name = "Probability",
                Description = "Calculate probabilities of independent and dependent events, in addition to expectations of random variables."
            };
            var lo24 = new LearningOutcomes {
                CourseInstance = ci3, Name = "Graph Theory",
                Description = "Illustrate by example the basic terminology of graph theory, as wells as properties and special cases (such as " +
                "Eulerian graphs, spanning trees, isomorphism, and planarity)."
            };
            var lo25 = new LearningOutcomes {
                CourseInstance = ci3, Name = "Proofs",
                Description = "Employ formal proof techniques (such as direct proof, proof by contradiction, induction, and the pigeonhole " +
                "principle) to construct sound arguments about properties of numbers, sets, functions, relations, and graphs."
            };
            //CS4400
            var lo26 = new LearningOutcomes {
                CourseInstance = ci4, Name = "Abstraction",
                Description = "Explain the objectives and functions of abstraction layers in modern computing systems, including operating " +
                "systems, programming languages, compilers, and applications."
            };
            var lo27 = new LearningOutcomes {
                CourseInstance = ci4, Name = "Communications Between Abstraction Layers",
                Description = "Understand cross-layer communications and how each layer of abstraction is implemented in the next layer of " +
                "abstraction (such as how C programs are translated into assembly code and how C library allocators are implemented in terms " +
                "of operating system memory management)."
            };
            var lo28 = new LearningOutcomes {
                CourseInstance = ci4, Name = "Performance",
                Description = "Analyze how the performance characteristics of one layer of abstraction affect the layers above it (such as how " +
                "caching and services of the operating system affect the performance of C programs)."
            };
            var lo29 = new LearningOutcomes {
                CourseInstance = ci4, Name = "Operating Systems Concepts",
                Description = "Construct applications using operating-system concepts (such as processes, threads, signals, virtual memory, I/O)."
            };
            var lo30 = new LearningOutcomes {
                CourseInstance = ci4, Name = "Operating System and Networking for Concurrency",
                Description = "Synthesize operating-system and networking facilities to build concurrent, communicating applications."
            };
            var lo31 = new LearningOutcomes {
                CourseInstance = ci4, Name = "Concurrent and Parallel Programs",
                Description = "Implement reliable concurrent and parallel programs using appropriate synchronization constructs."
            };
            //CS3500
            //CS 3500
            var lo32 = new LearningOutcomes {
                CourseInstance = ci5, Name = "Large, Complex Software Systems with Process Models, Libraries and Software Development Tools",
                Description = "Design and implement large and complex software systems (including concurrent software) through the use " +
                "of process models (such as waterfall and agile), libraries (both standard and custom), and modern software development " +
                "tools (such as debuggers, profilers, and revision control systems)."
            };
            var lo33 = new LearningOutcomes {
                CourseInstance = ci5, Name = "Validation, Error Handling and Testing",
                Description = "Perform input validation and error handling, as well as employ advanced testing principles and tools to " +
                "systematically evaluate software."
            };
            var lo34 = new LearningOutcomes {
                CourseInstance = ci5, Name = "Model View Controller with GUI",
                Description = "Apply the model-view-controller pattern and event handling fundamentals to create a graphical user interface."
            };
            var lo35 = new LearningOutcomes {
                CourseInstance = ci5, Name = "Client-Server Model and APIs",
                Description = "Exercise the client-server model and high-level networking APIs to build a web-based software system."
            };
            var lo36 = new LearningOutcomes {
                CourseInstance = ci5, Name = "Relational Databases",
                Description = "Operate a modern relational database to define relations, as well as store and retrieve data."
            };
            var lo37 = new LearningOutcomes {
                CourseInstance = ci5, Name = "Peer Code Reviews",
                Description = "Appreciate the collaborative nature of software development by discussing the benefits of peer code reviews."
            };

            #endregion Learning outcomes
            var learningo = new LearningOutcomes[] {
                lo0, lo1, lo2, lo3, lo4, lo5, lo6,
                lo7, lo8, lo9, lo10, lo11, lo12,
                lo13, lo14, lo15, lo16, lo17, lo18,
                lo19, lo20, lo21, lo22, lo23, lo24, lo25,
                lo26, lo27, lo28, lo29, lo30, lo31,
                lo32, lo33, lo34, lo35, lo36, lo37
            };
            foreach(LearningOutcomes lo in learningo) {
                context.LearningOutcomes.Add(lo);
            }
            context.SaveChanges();
            //CS4540
            var em0 = new EvaluationMetrics { Lo=lo0, Name = "PS1", Description = "Students will write static HTML web pages." };
            var em1 = new EvaluationMetrics { Lo = lo0, Name = "Exam 1", Description = "The exam covers HTML concepts such as __ and __." };
            var em2 = new EvaluationMetrics { Lo = lo1, Name = "PS2", Description = "Students will write HTML pages with accessible ____." };
            var em3 = new EvaluationMetrics { Lo = lo2, Name = "PS2", Description = "Students will write a web application with a model-view-controller " +
                "architecture, utilizing C# .NET Core." };
            var em4 = new EvaluationMetrics { Lo = lo3, Name = "PS3", Description = "Write a secure web server that protects against database" +
                " injection attacks, spoofing, and utilizes authorization." };
            var em5 = new EvaluationMetrics { Lo = lo4, Name = "PS3", Description = "Web server that uses JavaScript to make interactive" +
                " web pages." };
            var em6 = new EvaluationMetrics { Lo = lo5, Name = "PS3", Description = "Web page that adjusts size based on user's browser and" +
                " screen." };
            var em7 = new EvaluationMetrics { Lo = lo6, Name = "PS4", Description = "Students will implement a web-crawler that will take " +
                "data from _____ and display that data." };
            //CS2420
            var em8 = new EvaluationMetrics { Lo = lo7, Name = "Homework 1", Description = "Students will write a linked list and an array " +
                "list, and implement searching algorithms." };
            var em9 = new EvaluationMetrics { Lo = lo8, Name = "Homework 2", Description = "Students will identify the complexity of " +
                "various algorithms." };
            //CS3500
            var em10 = new EvaluationMetrics { Lo = lo13, Name = "PS1", Description = "Students will write a C# program." };
            //CS2100
            var em11 = new EvaluationMetrics { Lo = lo19, Name = "PS1", Description = "Students will use symbolic logic to model real-world " +
                "situations by converting informal language statements to propositional and predicate logic expressions, as well as apply " +
                "formal methods to propositions and predicates (such as computing normal forms and calculating validity)..." };
            //CS4400
            //CS3500
            var em12 = new EvaluationMetrics { Lo = lo32, Name = "PS1", Description = "Students will write a C# program." };
            var em13 = new EvaluationMetrics { Lo = lo33, Name = "PS1", Description = "Students will write a C# program and implement tests" +
                " where the code coverage is 100%." };
            var em14 = new EvaluationMetrics { Lo = lo34, Name = "PS2", Description = "Students will write a C# program using the model" +
                " view controller structure." };

            var evals = new EvaluationMetrics[] { em0, em1, em2, em3, em4, em5, em6, em7,
                em8, em9,
                em10,
                em11,
                em12, em13, em14
            };
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
                new SampleFiles { Em=em5, FileName="Example_Student_Work.pdf", Score=100 },

                new SampleFiles { Em=em8, FileName="Example_Student_Work.pdf", Score=80 },
                new SampleFiles { Em=em9, FileName="Example_Student_Work.pdf", Score=90 },

                new SampleFiles { Em=em10, FileName="Example_Student_Work.pdf", Score=90 },

                new SampleFiles { Em=em11, FileName="Example_Student_Work.pdf", Score=90 },
                new SampleFiles { Em=em12, FileName="Example_Student_Work.pdf", Score=90 },
                new SampleFiles { Em=em12, FileName="Example_Student_Work.pdf", Score=55 },
                new SampleFiles { Em=em12, FileName="Example_Student_Work.pdf", Score=75 }
            };
            foreach(SampleFiles s in samples) {
                context.SampleFiles.Add(s);
            }
            context.SaveChanges();
            var instructorAssignments = new Instructors[] {
                new Instructors { CourseInstance=ci0, InstructorLoginEmail="professor_jim@cs.utah.edu", InstructorTitle="Professor Jim de St. Germain" },
                new Instructors { CourseInstance=ci1, InstructorLoginEmail="professor_jim@cs.utah.edu", InstructorTitle="Professor Jim de St. Germain" },
                new Instructors { CourseInstance=ci2, InstructorLoginEmail="professor_jim@cs.utah.edu", InstructorTitle="Professor Jim de St. Germain" },
                new Instructors { CourseInstance=ci3, InstructorLoginEmail="professor_mary@cs.utah.edu", InstructorTitle="Professor Mary Hall" },
                new Instructors { CourseInstance=ci4, InstructorLoginEmail="professor_mary@cs.utah.edu", InstructorTitle="Professor Mary Hall" },
                new Instructors { CourseInstance=ci4, InstructorLoginEmail="professor_danny@cs.utah.edu", InstructorTitle="Professor Danny Kopta" }
            };
            foreach(Instructors inst in instructorAssignments) {
                context.Instructors.Add(inst);
            }
            context.SaveChanges();
        }
    }
}
