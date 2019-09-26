# CS4540PS4
CS 4540 Web Software Architecture PS4 Assignment

Author: Valerie German
Date: 25 Sept 2019
Course: CS 4540, University of Utah
Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.

Authentication and Authorization:
		To authorize pages, I split the different access levels to use controllers, so only one authorize statement would
	cover all the web pages associated. The InstructorController was the only one to check the actual user, since it has
	specific courses for specific instructors. This verified the IdentityUser with a table in the database which associates
	course instances with users. Ideally, the database will have a forien key directly linked to users, however linking
	with the default user tables was a little more difficult than I thought with scaffolding so it currently uses an inserted
	user email in the seed file.
		Overall, the support for users/identity is very convienient in .NET and after a little setup was very simple to use.

Extra:
	xx
	
  
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