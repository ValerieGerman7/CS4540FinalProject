# CS4540PS4
CS 4540 Web Software Architecture PS4 Assignment

Author: Valerie German
Date: 27 Sept 2019
Course: CS 4540, University of Utah
Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.

Note:
		The home page contains links to pages, listed with the associated user type authorized to access them. The instructor
	list contains links to two different classes. The home page may be accessed by anyone, any other page requires the user
	to be logged in.
		Admins can view a list of all learning outcomes, as well as create, modify and delete learning outcomes. They can view
	a list of all available courses (note the link on each course is invalid - no current course overview page for admins.)
	Courses may be added, modified and deleted. Admins may also view a list of all users and their roles.
		Professors can view a list of their courses, and a course overview page containing the course's learning outcomes,
	evaluation metrics and sample files.
		Chairs may view a department overview - listing all classes in the department and the evaluation metric and sample file
	progress. They also have access to a page that links to all the departments (note this only shows the department code 
	currently).

Authentication and Authorization:
		To authorize pages, I split the different access levels to use controllers, so only one authorize statement would
	cover all the web pages associated. The InstructorController was the only one to check the actual user, since it has
	specific courses for specific instructors. This verified the IdentityUser with a table in the database which associates
	course instances with users. Ideally, the database will have a forien key directly linked to users, however linking
	with the default user tables was a little more difficult than I thought with scaffolding so it currently uses an inserted
	user email in the seed file.
		Overall, the support for users/identity is very convienient in .NET and after a little setup was very simple to use.

Extra:
		Creating LearningOutcomes uses a drop down containing the course's department, number, name and semester/year.
		The list of learning outcomes is displayed in pages (currently 5 at a time), with some table design changes to make it 
	more visually appealing. An Admin may go to the course to see all learning outcomes in a course, and add learning outcomes
	to that course directly by the + button on the right (labeled). General additional styling was applied to the learning 
	outcome pages (links to buttons, cleaner table).
	
  
References:
(PS1)
-Navigation bar: https://www.w3schools.com/howto/howto_js_topnav.asp
-Collapses: https://www.w3schools.com/howto/howto_js_collapsible.asp
-Progress bar: https://stackoverflow.com/questions/45507970/how-can-i-change-the-color-of-a-progress-bar-value-in-html
(PS2)
-Dropdown menu: https://getbootstrap.com/docs/4.0/components/dropdowns/
-Alignment: https://stackoverflow.com/questions/42388989/bootstrap-4-center-vertical-and-horizontal-alignment
-File Open: https://stackoverflow.com/questions/11620698/how-to-trigger-a-file-download-when-clicking-an-html-button-or-javascript
-Progress bar: https://getbootstrap.com/docs/4.1/components/progress/
-Forms: https://getbootstrap.com/docs/4.1/components/forms/
-Modals: https://getbootstrap.com/docs/4.1/components/modal/
-Cards: https://getbootstrap.com/docs/4.1/components/card/
-Collapses: https://getbootstrap.com/docs/4.0/components/collapse/
-Navbar: https://getbootstrap.com/docs/4.0/components/navbar/
(PS3)
-Database Setup: https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-2.2
(PS4)
-Identity: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-2.2&tabs=visual-studio
-Roles: https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-2.2
-Seeding: https://romansimuta.com/blogs/blog/authorization-with-roles-in-asp.net-core-mvc-web-application,
https://stackoverflow.com/questions/34343599/how-to-seed-users-and-roles-with-code-first-migration-using-identity-asp-net-cor
-Tabs: https://www.w3schools.com/bootstrap4/tryit.asp?filename=trybs_ref_js_tab-content&stacked=h
-Pagination: https://getbootstrap.com/docs/4.0/components/pagination/