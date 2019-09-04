# CS4540PS2
CS 4540 Web Software Architecture PS2 Assignment

Author: Valerie German
Date: 3 Sept 2019
Course: CS 4540, University of Utah
Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.

Comments to Evaluators: 
	The Course.cshtml and Overview.cshtml are the webpages that should be evaluated, sample webpages exist to show some links within the website.

  The navbar was changed to use a bootstrap navbar, including a dropdown and button. In both of the course and overview pages, cards were used to 
  organize the information. Buttons were designed with bootstrap css. Modals were used to demonstrate pop-ups for adding evaluation metrics or
  sample files. Bootstrap styling was used in the forms on those modals. Bootrap collapses replaced the original collapses.

Design Notes:
	Course.cshtml: I considered using the accordion structure for the course webpage, but it seemed more convienent for the user to be able to look at several learning outcomes
	if desired. The original collapses were replaced with bootstrap collapses. 
		Each evaluation metric was separated into a card for easy visual separation, as well as with the whole learning outcome's description. The cards provided
	an easy way to separate data and highlight titles (this is also true in the Department view) that was more effective than the previous structure. 
		Adding Evaluation Metrics or Sample Files uses a modal, displaying a simple form - this functionality wasn't in PS1.
		The layout of each evaluation metric was also improved, and the button style was updated.
	Overview.cshtml: Cards were used to separate each of the classes, and progress bars were used to visualize their progress. The default bootstrap background colors
	weren't used since they make the webpage too vibrant. Each card links to a course view, the CS 4540 card links to the sample Course.cshtml.
  
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