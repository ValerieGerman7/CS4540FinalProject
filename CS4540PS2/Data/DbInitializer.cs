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
using System.Text;

/// <summary>
/// Author: Valerie German
/// Date: 5 Dec 2019
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
        public static void Initialize(LOTDBContext context) {
            if (context.Database.EnsureCreated()) {
                //context.Database.Migrate();
            }
            //
            if (context.CourseInstance.Any()) {
                return;
            }
            var user0 = new UserLocator() { UserLoginEmail = "professor_jim@cs.utah.edu", UserTitle = "Jim de St. Germain" };
            var user1 = new UserLocator() { UserLoginEmail = "admin_erin@cs.utah.edu", UserTitle = "Erin Parker" };
            var user2 = new UserLocator() { UserLoginEmail = "chair_whitaker@cs.utah.edu", UserTitle = "Ross Whitaker" };
            var user3 = new UserLocator() { UserLoginEmail = "professor_mary@cs.utah.edu", UserTitle = "Mary Hall" };
            var user4 = new UserLocator() { UserLoginEmail = "professor_danny@cs.utah.edu", UserTitle = "Danny Kopta" };
            var users = new UserLocator[] { user0, user1, user2, user3, user4 };
            foreach(UserLocator user in users) {
                context.UserLocator.Add(user);
            }
            context.SaveChanges();
            //CourseStatus
            var completeStatus = new CourseStatus() { Status = CourseStatusNames.Complete }; //"Complete"
            context.CourseStatus.Add(completeStatus);
            var inProg = new CourseStatus() { Status = CourseStatusNames.InProgress }; //"In-progress"
            context.CourseStatus.Add(inProg);
            var waitingApp = new CourseStatus() { Status = CourseStatusNames.AwaitingApproval }; //"Awaiting Approval"
            context.CourseStatus.Add(waitingApp);
            var arch = new CourseStatus() { Status = CourseStatusNames.Archived }; //"Archieved"
            context.CourseStatus.Add(arch);
            var inRev = new CourseStatus() { Status = CourseStatusNames.InReview }; //"In-Review"
            context.CourseStatus.Add(inRev);
            context.SaveChanges();
            //Departments
            var csDept = new Departments() { Name = "Computer Science", Code = "CS" };
            context.Departments.Add(csDept);
            var mathdept = new Departments() { Name = "Mathematics", Code = "MATH" };
            context.Departments.Add(mathdept);
            context.SaveChanges();
            //Course Notes
            var cnote = new CourseNotes() {
                Note = "Sample note on course",
                NoteModified = DateTime.Now
            };
            //Course Instances
            var ci0 = new CourseInstance { Name = "Web Software Architecture",
                Description = "Software architectures, programming models, and programming environments pertinent to developing web " +
                "applications.  Topics include client-server model, multi-tier software architecture, client-side scripting (JavaScript), " +
                "server-side programming (Servlets and JavaServer Pages), component reuse (JavaBeans), database connectivity (JDBC), and " +
                "web servers.",
                DepartmentNavigation=csDept, Number = 4540, Semester = "Fall", Year = 2019,
                Status= inRev, DueDate=new DateTime(2019, 12, 10),
                CourseNotes = new List<CourseNotes>() { cnote }
            };
            cnote.CourseInstance = ci0;
            var ci1 = new CourseInstance { Name = "Introduction To Algorithms and Data Structures",
                Description = "This course provides an introduction to the problem of engineering computational efficiency into programs. " +
                "Students will learn about classical algorithms (including sorting, searching, and graph traversal), data structures " +
                "(including stacks, queues, linked lists, trees, hash tables, and graphs), and analysis of program space and time " +
                "requirements. Students will complete extensive programming exercises that require the application of elementary techniques " +
                "from software engineering.",
                DepartmentNavigation = csDept, Number = 2420, Semester = "Fall", Year = 2019,
                Status = inProg, DueDate = new DateTime(2019, 12, 10)
            };
            var ci2 = new CourseInstance { Name = "Software Practice",
                Description = "Practical exposure to the process of creating large software systems, including requirements specifications, " +
                "design, implementation, testing, and maintenance. Emphasis on software process, software tools (debuggers, profilers, source " +
                "code repositories, test harnesses), software engineering techniques (time management, code, and documentation standards, " +
                "source code management, object-oriented analysis and design), and team development practice. Much of the work will be in " +
                "groups and will involve modifying preexisting software systems.",
                DepartmentNavigation = csDept, Number = 3500, Semester = "Fall", Year = 2019,
                Status = inProg, DueDate = new DateTime(2019, 12, 10)
            };
            CourseNotes cnote1 = new CourseNotes() {
                Note = "Sample note on course",
                NoteModified = DateTime.Now
            };
            var ci3 = new CourseInstance { Name = "Discrete Structures",
                Description = "Introduction to propositional logic, predicate logic, formal logical arguments, finite sets, functions, relations," +
                " inductive proofs, recurrence relations, graphs, probability, and their applications to Computer Science.",
                DepartmentNavigation = csDept, Number = 2100, Semester = "Fall", Year = 2019,
                Status = waitingApp, DueDate = new DateTime(2019, 12, 10),
                CourseNotes = new List<CourseNotes>() {
                    cnote1      
                }
            };
            cnote1.CourseInstance = ci3;
            var ci4 = new CourseInstance { Name = "Computer Systems",
                Description = "Introduction to computer systems from a programmer's point of view.  Machine level representations of programs, " +
                "optimizing program performance, memory hierarchy, linking, exceptional control flow, measuring program performance, virtual memory, " +
                "concurrent programming with threads, network programming.",
                DepartmentNavigation = csDept, Number = 4400, Semester = "Spring", Year = 2019,
                Status = inProg, DueDate = new DateTime(2019, 12, 10)
            };
            var ci5 = new CourseInstance {
                Name = "Software Practice",
                Description = "Practical exposure to the process of creating large software systems, including requirements specifications, " +
                "design, implementation, testing, and maintenance. Emphasis on software process, software tools (debuggers, profilers, source " +
                "code repositories, test harnesses), software engineering techniques (time management, code, and documentation standards, " +
                "source code management, object-oriented analysis and design), and team development practice. Much of the work will be in " +
                "groups and will involve modifying preexisting software systems.",
                DepartmentNavigation = csDept, Number = 3500, Semester = "Spring", Year = 2019,
                Status = inRev, DueDate = new DateTime(2019, 12, 10)
            };
            var ci6 = new CourseInstance {
                Name = "Foundations of Analysis",
                Description = "Advanced multivariable calculus. Topics include  continuity, compactness, differentiation and affine approximation, " +
                "chain rule, Taylor series, extremization, error estimation, inverse and implicit function theorems, Riemann integration, Fubini's " +
                "Theorem, change of variables formula. The emphasis is on further developing the student's ability to understand more abstract concepts " +
                "and to write an effective and rigorous mathematical argument.",
                DepartmentNavigation = mathdept, Number = 3220, Semester = "Fall", Year = 2018,
                Status = arch, DueDate = new DateTime(2018, 12, 10)
            };
            var ci7 = new CourseInstance {
                Name = "Software Practice",
                Description = "Practical exposure to the process of creating large software systems, including requirements specifications, " +
                "design, implementation, testing, and maintenance. Emphasis on software process, software tools (debuggers, profilers, source " +
                "code repositories, test harnesses), software engineering techniques (time management, code, and documentation standards, " +
                "source code management, object-oriented analysis and design), and team development practice. Much of the work will be in " +
                "groups and will involve modifying preexisting software systems.",
                DepartmentNavigation = csDept, Number = 3500, Semester = "Fall", Year = 2018,
                Status = completeStatus, DueDate = new DateTime(2018, 12, 10)
            };
            var ci8 = new CourseInstance {
                Name = "Introduction To Algorithms and Data Structures",
                Description = "This course provides an introduction to the problem of engineering computational efficiency into programs. " +
                "Students will learn about classical algorithms (including sorting, searching, and graph traversal), data structures " +
                "(including stacks, queues, linked lists, trees, hash tables, and graphs), and analysis of program space and time " +
                "requirements. Students will complete extensive programming exercises that require the application of elementary techniques " +
                "from software engineering.",
                DepartmentNavigation = csDept, Number = 2420, Semester = "Fall", Year = 2018,
                Status = arch, DueDate = new DateTime(2018, 12, 10)
            };
            var courses = new CourseInstance[] { ci0, ci1, ci2, ci3, ci4, ci5, ci6, ci7, ci8 };
            foreach(CourseInstance co in courses) {
                context.CourseInstance.Add(co);
            }
            //context.SaveChanges();
            context.CourseNotes.Add(cnote);
            context.CourseNotes.Add(cnote1);
            context.SaveChanges();
            #region Learning Outcomes
            //CS4540
            var lonote = new LONotes() {
                Note = "Sample note on HTML learning outcome",
                NoteModified = DateTime.Now,
                NoteUserModified = "professor_jim@cs.utah.edu"
            };
            var lo0 = new LearningOutcomes { CourseInstance = ci0, Name = "HTML and CSS",
                Description = "Construct web pages using modern HTML and CSS practices, including modern frameworks.",
                LONotes = new List<LONotes>() {
                    lonote
                }
            };
            lonote.Lo = lo0;
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
            var lonote1 = new LONotes() {
                Note = "Sample note on real world application learning outcome",
                NoteModified = DateTime.Now,
                NoteUserModified = "chair_whitaker@cs.utah.edu"
            };
            var lo21 = new LearningOutcomes {
                CourseInstance = ci3, Name = "Real-World Application",
                Description = "Assign practical examples to the appropriate set, function, or relation model, while employing the associated " +
                "terminology and operations.",
                LONotes = new List<LONotes>() {
                    lonote1
                }
            };
            lonote1.Lo = lo21;
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
            //CS3500 Complete
            var lo38 = new LearningOutcomes {
                CourseInstance = ci7, Name = "Large, Complex Software Systems with Process Models, Libraries and Software Development Tools",
                Description = "Design and implement large and complex software systems (including concurrent software) through the use " +
                "of process models (such as waterfall and agile), libraries (both standard and custom), and modern software development " +
                "tools (such as debuggers, profilers, and revision control systems)."
            };
            var lo39 = new LearningOutcomes {
                CourseInstance = ci7, Name = "Validation, Error Handling and Testing",
                Description = "Perform input validation and error handling, as well as employ advanced testing principles and tools to " +
                "systematically evaluate software."
            };
            //CS2420 Archive
            var lo40 = new LearningOutcomes {
                CourseInstance = ci8, Name = "Implement and Analyze Data Structures",
                Description = "Implement, and analyze for efficiency, fundamental data structures (including lists, graphs, and trees) and " +
                "algorithms (including searching, sorting, and hashing)."
            };
            var lo41 = new LearningOutcomes {
                CourseInstance = ci8, Name = "Complexity",
                Description = "Employ Big-O notation to describe and compare the asymptotic complexity of algorithms, as well as perform empirical " +
                "studies to validate hypotheses about running time."
            };
            #endregion Learning outcomes
            var learningo = new LearningOutcomes[] {
                lo0, lo1, lo2, lo3, lo4, lo5, lo6,
                lo7, lo8, lo9, lo10, lo11, lo12,
                lo13, lo14, lo15, lo16, lo17, lo18,
                lo19, lo20, lo21, lo22, lo23, lo24, lo25,
                lo26, lo27, lo28, lo29, lo30, lo31,
                lo32, lo33, lo34, lo35, lo36, lo37,
                lo38, lo39, lo40, lo41
            };
            foreach(LearningOutcomes lo in learningo) {
                context.LearningOutcomes.Add(lo);
            }
            context.LONotes.Add(lonote);
            context.LONotes.Add(lonote1);
            context.SaveChanges();
            //CS4540
            var em0 = new EvaluationMetrics { Lo=lo0, Name = "PS1", Description = "Students will write static HTML web pages.",
                ContentType=sampleFileType, FileContent= sampleAssignmentContent, FileName="Sample_Assignment.txt" 
            };
            var em1 = new EvaluationMetrics { Lo = lo0, Name = "Exam 1", Description = "The exam covers HTML concepts such as __ and __.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            var em2 = new EvaluationMetrics { Lo = lo1, Name = "PS2", Description = "Students will write HTML pages with accessible ____.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            var em3 = new EvaluationMetrics { Lo = lo2, Name = "PS2", Description = "Students will write a web application with a model-view-controller " +
                "architecture, utilizing C# .NET Core.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            var em4 = new EvaluationMetrics { Lo = lo3, Name = "PS3", Description = "Write a secure web server that protects against database" +
                " injection attacks, spoofing, and utilizes authorization.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            var em5 = new EvaluationMetrics { Lo = lo4, Name = "PS3", Description = "Web server that uses JavaScript to make interactive" +
                " web pages.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            var em6 = new EvaluationMetrics { Lo = lo5, Name = "PS3", Description = "Web page that adjusts size based on user's browser and" +
                " screen.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            var em7 = new EvaluationMetrics { Lo = lo6, Name = "PS4", Description = "Students will implement a web-crawler that will take " +
                "data from _____ and display that data.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            //CS2420
            var em8 = new EvaluationMetrics { Lo = lo7, Name = "Homework 1", Description = "Students will write a linked list and an array " +
                "list, and implement searching algorithms.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            var em9 = new EvaluationMetrics { Lo = lo8, Name = "Homework 2", Description = "Students will identify the complexity of " +
                "various algorithms.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            //CS3500
            var em10 = new EvaluationMetrics { Lo = lo13, Name = "PS1", Description = "Students will write a C# program.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            //CS2100
            var em11 = new EvaluationMetrics { Lo = lo19, Name = "PS1", Description = "Students will use symbolic logic to model real-world " +
                "situations by converting informal language statements to propositional and predicate logic expressions, as well as apply " +
                "formal methods to propositions and predicates (such as computing normal forms and calculating validity)...",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            //CS4400
            //CS3500
            var em12 = new EvaluationMetrics { Lo = lo32, Name = "PS1", Description = "Students will write a C# program.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            var em13 = new EvaluationMetrics { Lo = lo33, Name = "PS1", Description = "Students will write a C# program and implement tests" +
                " where the code coverage is 100%.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            var em14 = new EvaluationMetrics { Lo = lo34, Name = "PS2", Description = "Students will write a C# program using the model" +
                " view controller structure.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            //CS3500 Complete
            var em15 = new EvaluationMetrics { Lo = lo38, Name = "PS1", Description = "Students will write a C# program.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            var em16 = new EvaluationMetrics {
                Lo = lo39, Name = "PS1", Description = "Students will write a C# program and implement tests" +
                " where the code coverage is 100%.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            //CS2420 Archive
            var em17 = new EvaluationMetrics {
                Lo = lo40, Name = "Homework 1", Description = "Students will write a linked list and an array " +
                "list, and implement searching algorithms.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            var em18 = new EvaluationMetrics {
                Lo = lo41, Name = "Homework 2", Description = "Students will identify the complexity of " +
                "various algorithms.",
                ContentType = sampleFileType, FileContent = sampleAssignmentContent, FileName = "Sample_Assignment.txt"
            };
            var evals = new EvaluationMetrics[] { em0, em1, em2, em3, em4, em5, em6, em7,
                em8, em9,
                em10,
                em11,
                em12, em13, em14,
                em15, em16,
                em17, em18
            };
            foreach(EvaluationMetrics em in evals) {
                context.EvaluationMetrics.Add(em);
            }
            context.SaveChanges();
            var samples = new SampleFiles[] {
                new SampleFiles { Em=em0, FileName="Example_Student_Work.txt", Score=60, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em0, FileName="Example_Student_Work.txt", Score=70, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em0, FileName="Example_Student_Work.txt", Score=80, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em0, FileName="Example_Student_Work.txt", Score=90, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em1, FileName="Example_Student_Work.txt", Score=45, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em1, FileName="Example_Student_Work.txt", Score=70, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em1, FileName="Example_Student_Work.txt", Score=95, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em3, FileName="Example_Student_Work.txt", Score=0, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em3, FileName="Example_Student_Work.txt", Score=50, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em3, FileName="Example_Student_Work.txt", Score=100, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em5, FileName="Example_Student_Work.txt", Score=100, FileContent=sampleFileContent, ContentType=sampleFileType },

                new SampleFiles { Em=em8, FileName="Example_Student_Work.txt", Score=80, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em9, FileName="Example_Student_Work.txt", Score=90, FileContent=sampleFileContent, ContentType=sampleFileType },

                new SampleFiles { Em=em10, FileName="Example_Student_Work.txt", Score=90, FileContent=sampleFileContent, ContentType=sampleFileType },

                new SampleFiles { Em=em11, FileName="Example_Student_Work.txt", Score=90, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em12, FileName="Example_Student_Work.txt", Score=90, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em12, FileName="Example_Student_Work.txt", Score=55, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em12, FileName="Example_Student_Work.txt", Score=75, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em15, FileName="Example_Student_Work.txt", Score=90, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em16, FileName="Example_Student_Work.txt", Score=85, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em17, FileName="Example_Student_Work.txt", Score=90, FileContent=sampleFileContent, ContentType=sampleFileType },
                new SampleFiles { Em=em18, FileName="Example_Student_Work.txt", Score=88, FileContent=sampleFileContent, ContentType=sampleFileType }
            };
            foreach(SampleFiles s in samples) {
                context.SampleFiles.Add(s);
            }
            context.SaveChanges();
            var instructorAssignments = new Instructors[] {
                new Instructors { CourseInstance=ci0, User=user0 },
                new Instructors { CourseInstance=ci1, User=user0 },
                new Instructors { CourseInstance=ci2, User=user0 },
                new Instructors { CourseInstance=ci3, User=user3 },
                new Instructors { CourseInstance=ci4, User=user3 },
                new Instructors { CourseInstance=ci4, User=user4 },
                new Instructors { CourseInstance=ci6, User=user4 }
            };
            foreach(Instructors inst in instructorAssignments) {
                context.Instructors.Add(inst);
            }
            context.SaveChanges();

            var messages = new Messages[]
            {
                new Messages { Text="This is a message from Jim.", Date=DateTime.Now, Receiver=user1.Id, Sender=user0.Id }
            };
            foreach(Messages m in messages)
            {
                context.Messages.Add(m);
            }
            context.SaveChanges();
            var notifications = new Notifications[] {
                new Notifications { Text="This course has been changed to in-review. Message from chair: 'Add samples for some learning outcomes.'", DateNotified=new DateTime(2019, 12, 5), 
                    Read=false, CourseInstance=ci0, User=user0 },
                new Notifications { Text="This course is awaiting approval.", DateNotified=new DateTime(2019, 12, 4), Read=true, CourseInstance=ci0, User=user2 },
                new Notifications { Text="This course is awaiting approval.", DateNotified=new DateTime(2019, 12, 5), Read=false, CourseInstance=ci3, User=user2 },
                new Notifications { Text="This course has been archived.", DateNotified=new DateTime(2019, 12, 5), Read=false, CourseInstance=ci8, User=user4 }
            };
            foreach(Notifications notif in notifications) {
                context.Notifications.Add(notif);
            }
            context.SaveChanges();
        }

        private static byte[] sampleAssignmentContent = Encoding.ASCII.GetBytes("Sample Assignment File.");
        private static byte[] sampleFileContent = Encoding.ASCII.GetBytes("Sample Student Work.");
        private static string sampleFileType = "text/plain";
    }
}
