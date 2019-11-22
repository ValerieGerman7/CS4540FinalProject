//Author: Valerie German
//Date: 18 Oct 2019
//Course: CS 4540, University of Utah
//Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
//I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
//File Contents: This file contains the JavaScript for the PS1 webpages, modified for PS2-5 wepages.

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

//Redirects to the Course overview page (obsolete)
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
//Redirects to the instructor's view of a course.
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
//Redirects to the chair's view of a course.
function RedirectToCourseDept(CID) {
    var form = document.createElement("form");
    var course = document.createElement("input");
    course.name = "CourseId"; course.value = CID;
    form.appendChild(course);
    form.action = "/Department/Course";
    form.hidden = 'hidden';
    document.body.appendChild(form);
    form.submit();
}
//Redirects to the chair's department overview page.
function RedirectToDept(Dept) {
    var form = document.createElement("form");
    var dept = document.createElement("input");
    dept.name = "DeptCode"; dept.value = Dept;
    form.appendChild(dept);
    form.action = "/Department/Department";
    form.hidden = 'hidden';
    document.body.appendChild(form);
    form.submit();
}
//Redirects to the given controller with the given action.
function Redirect(Controller, Action) {
    var form = document.createElement("form");
    form.action = "/" + Controller + "/" + Action;
    form.hidden = 'hidden';
    document.body.appendChild(form);
    form.submit();
}
//Requests to add a user to a role in the User controller
//(obsolete)
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
//Sends a request to change the given user's current status for the
//given role.
function ChangeUserRole(e, username, role) {
    //window.alert("here");
    e.preventDefault();
    Swal.fire({
        title: "Are you sure?",
        text: "You are about to change " + username + "'s " + role + " status?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: "Change Role Status"
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/User/ChangeRole",
                method: "POST",
                data: {
                    username: username,
                    role: role
                }
            }).fail(function () {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!'
                })
            }).done(function (data) {
                var box = e.target;
                if (data.success) {
                    box.checked = !box.checked;
                    Swal.fire({
                        position: 'top-end',
                        type: 'success',
                        title: 'The role was changed',
                        showConfirmButton: false,
                        timer: 1500
                    })
                } else {
                    Swal.fire({
                        position: 'top-end',
                        type: 'error',
                        title: 'Role change failed',
                        showConfirmButton: false,
                        timer: 1000
                    })
                }
                //var js = $.parseJSON(data);
                //window.alert(js);
                //box.checked = data.isRole;
            });
        }
    })
    
}
//Sends a request to delete a department
function DeleteDept(e, deptCode) {
    //window.alert("here");
    e.preventDefault();
    Swal.fire({
        title: "Are you sure?",
        text: "You are about to delete the " + deptCode + " department.",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: "Delete Department"
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/DeptManager/Delete",
                method: "POST",
                data: {
                    code: deptCode
                }
            }).fail(function () {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!'
                })
            }).done(function (data) {
                var box = e.target;
                if (data.success) {
                    window.location.href = '/DeptManager/Index';
                    /*box.checked = !box.checked;
                    Swal.fire({
                        position: 'top-end',
                        type: 'success',
                        title: 'The department was deleted.',
                        showConfirmButton: false,
                        timer: 1500
                    })*/
                } else {
                    Swal.fire({
                        position: 'top-end',
                        type: 'error',
                        title: 'The department could not be deleted. There may be courses for this department.',
                        showConfirmButton: false,
                        timer: 1000
                    })
                }
            });
        }
    })
}
//Submit the Sample Files form
function GotoSF(sid) {
    var form = $("<form>", {
        action: '/Instructor/SampleFile',
        method: 'GET'
    });
    form.append($("<input>", {
        type: 'number',
        name: 'sfId', value: sid
    }));
    $('body').append(form);
    form.submit();
}
//Sends a request to delete a sample file
function DeleteSample(e, sid, ret) {
    //window.alert("here");
    e.preventDefault();
    Swal.fire({
        title: "Are you sure?",
        text: "You are about to delete this sample.",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: "Delete Sample"
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Instructor/DeleteSampleFile",
                method: "POST",
                data: {
                    sfId: sid
                }
            }).fail(function () {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!'
                })
            }).done(function (data) {
                var box = e.target;
                if (data.success) {
                    ret.click();
                    /*box.checked = !box.checked;
                    Swal.fire({
                        position: 'top-end',
                        type: 'success',
                        title: 'The department was deleted.',
                        showConfirmButton: false,
                        timer: 1500
                    })*/
                } else {
                    Swal.fire({
                        position: 'top-end',
                        type: 'error',
                        title: 'The sample could not be deleted.',
                        showConfirmButton: false,
                        timer: 1000
                    })
                }
            });
        }
    })
}