﻿//Author: Valerie German
//Date: 28 Aug 2019
//Course: CS 4540, University of Utah
//Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
//I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
//File Contents: This file contains the JavaScript for the PS1 webpages, modified for PS2 wepages.

//--Initializing--
//Set Sample file button colors and text.
var sample = document.getElementsByClassName("sampleButton");
for (index = 0; index < sample.length; index++) {
    SetColor(sample[index], sample[index].value);
}

//--Functions--
//Set the color and description for sample file buttons
function SetColor(button, scale) {
    var red = 255 * (1 - (scale / 100));
    var green = 255 * (scale / 100);
    button.style.backgroundColor = "rgb(" + red + ", " + green + ", 0)";
    var color;
    var descript;
    if (scale < 70) {
        descript = "Poor";
        color = "rgb(168, 50, 50)";
    } else if (scale < 80) {
        descript = "Mediocre";
        color = "rgb(168, 125, 50)";
    } else if (scale < 90) {
        descript = "Good";
        color = "rgb(145, 168, 50)";
    } else {
        descript = "Superior";
        color = "rgb(50, 168, 82)";
    }
    button.innerHTML = descript + " (" + scale + "%)";
    button.style.backgroundColor = color;
}
//Redirect to a webpage
function RedirectToAction(action) {
    window.location.href = "/Home/" + action;
}
