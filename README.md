# CS4540PS3
CS 4540 Web Software Architecture PS3 Assignment

Author: Valerie German
Date: 11 Sept 2019
Course: CS 4540, University of Utah
Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.

Comments to Evaluators: 
	The connection string for the database is located in appsettings.json. If the database is empty, it is populated with test data. DbInitialize
initializes the database schema and data (if empty). 
	Department: The homepage redirects to the Computer Science Department view. This lists all CS courses, and each course links to a department view of the
course.
	Course: The department course view allows learning outcomes to be added, deleted and modified. The evaluation metrics and sample files aren't currently
displayed - this webpage is currently intended to show functionality for editing learning outcomes. For assignment purposes, on the top right, it will
indicate that it is the department view, and the professor view can be seen by clicking 'Go To Professor View'. The professor view displays learning
outcomes, evaluation metrics and samples files. Evalutation metric and sample file editing aren't currently enabled, but the forms for creating are
displayed in modals.
	

Design Notes:
	
  
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