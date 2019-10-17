//Author: Valerie German
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

function RedirectToCourse(Dept, Num, Sem, Year) {
    var form = document.createElement("form");
    var dept = document.createElement("input");
    dept.name = "Dept"; dept.value = Dept;
    form.appendChild(dept);
    var num = document.createElement("input");
    num.name = "Num"; num.value = Num;
    form.appendChild(num);
    var sem = document.createElement("input");
    sem.name = "Sem"; sem.value = Sem;
    form.appendChild(sem);
    var year = document.createElement("input");
    year.name = "Year"; year.value = Year;
    form.appendChild(year);
    form.action = "/Course/Course";
    form.hidden = 'hidden';
    document.body.appendChild(form);
    form.submit();
}
function RedirectToInstCourse(Dept, Num, Sem, Year) {
    var form = document.createElement("form");
    var dept = document.createElement("input");
    dept.name = "Dept"; dept.value = Dept;
    form.appendChild(dept);
    var num = document.createElement("input");
    num.name = "Num"; num.value = Num;
    form.appendChild(num);
    var sem = document.createElement("input");
    sem.name = "Sem"; sem.value = Sem;
    form.appendChild(sem);
    var year = document.createElement("input");
    year.name = "Year"; year.value = Year;
    form.appendChild(year);
    form.action = "/Instructor/Course";
    form.hidden = 'hidden';
    document.body.appendChild(form);
    form.submit();
}
function RedirectToCourseDept(Dept, Num, Sem, Year) {
    var form = document.createElement("form");
    var dept = document.createElement("input");
    dept.name = "Dept"; dept.value = Dept;
    form.appendChild(dept);
    var num = document.createElement("input");
    num.name = "Num"; num.value = Num;
    form.appendChild(num);
    var sem = document.createElement("input");
    sem.name = "Sem"; sem.value = Sem;
    form.appendChild(sem);
    var year = document.createElement("input");
    year.name = "Year"; year.value = Year;
    form.appendChild(year);
    form.action = "/LearningOutcomes/Course";
    form.hidden = 'hidden';
    document.body.appendChild(form);
    form.submit();
}
function RedirectToDept(Dept) {
    var form = document.createElement("form");
    var dept = document.createElement("input");
    dept.name = "Dept"; dept.value = Dept;
    form.appendChild(dept);
    form.action = "/Department/Department";
    form.hidden = 'hidden';
    document.body.appendChild(form);
    form.submit();
}
function Redirect(Controller, Action) {
    var form = document.createElement("form");
    form.action = "/" + Controller + "/" + Action;
    form.hidden = 'hidden';
    document.body.appendChild(form);
    form.submit();
}

function AddUserToRole(e, username) {
    window.alert("here");
    e.preventDefault();
    $.ajax({
        url: "/User/AddRole",
        method: "POST",
        data: {
            username: username,
            role : "Admin"
        }
    }).done(function (data) {
        window.alert("Always");
        window.alert(data.success);
    });
}
function test() {
    window.alert("here");
}
function ChangeUserRole(e, username, role) {
    //window.alert("here");
    e.preventDefault();
    $.ajax({
        url: "/User/ChangeRole",
        method: "POST",
        data: {
            username: username,
            role: role
        }
    }).fail(function () {
        window.alert('Fail');
    }).done(function (data) {
        window.alert("Done" + data.success);
        var box = e.target;
        if (data.success) {
            box.checked = !box.checked;
        } else {

        }
        //var js = $.parseJSON(data);
        //window.alert(js);
        //box.checked = data.isRole;
    });
}