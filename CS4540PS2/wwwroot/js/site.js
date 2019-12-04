//Author: Valerie German
//Date: 2 Dec 2019
//Course: CS 4540, University of Utah
//Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
//I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
//File Contents: This file contains the JavaScript for the PS1 webpages, modified for PS2-5 wepages.

//--Initializing--
//Set Sample file button colors and text
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var sample = document.getElementsByClassName("sampleButton");
for (index = 0; index < sample.length; index++) {
    SetColor(sample[index], sample[index].value);
}

//SignalR

connection.on("ReceiveMessage", function (sender, receiver, message) {
    var el = document.getElementById("userID");
    var userID = el.innerText;
    if (receiver == userID) {
        let div = document.createElement("div");
        div.className = "single_message";
        let i = document.createElement("i");
        i.className = "fas fa-circle";
        let p = document.createElement("p");
        p.textContent = message;
        div.appendChild(i);
        div.appendChild(p);
        let messageBoxID = sender + "addMessage";
        let box = document.getElementById(messageBoxID);
        box.appendChild(div);
        box.scrollTop = box.scrollHeight;
    }
   
    
    console.log(`Hey baby ;) sender: ${sender}, receiver: ${receiver} message: ${message}`);
});

connection.start().then(function () {
    console.log("Connection Established Successfully");
}).catch(function (err) {
    console.log("Connection Failed");
    return console.error(err.toString());
});


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

//functions for sliding down contact list and making messagebox appear or disappear

$(".chat_header").click(function () {
    $(".user_list").slideToggle();
});

$(".message_header").click(function () {
    var id = $(this).attr('id');
    id = "#" + id + "ox";
    $(id).hide();
});

$(".user").click(function () {
    var id = $(this).attr('id');
    id = '#' + id + 'box';
    $(id).show();
})



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
    //("here");
    e.preventDefault();
    $.ajax({
        url: "/User/AddRole",
        method: "POST",
        data: {
            username: username,
            role : "Admin"
        }
    }).done(function (data) {
        //window.alert("Always");
        //window.alert(data.success);
    });
}

//Send a request to create a message in the MessagesController
function SendMessage(e, text, sender, receiver) {
    e.preventDefault();
    if (e.keyCode == 13) {


        var message = text;
        connection.invoke("SendMessage", sender, receiver, message).catch(function (err) {
            return console.error(err.toString());
        });


        $.ajax(
            {
                url: "/Messages/SendMessage",
                method: "POST",
                data: { text: text, sender: sender, receiver: receiver }

            })
            .done(function (result) { //if post request succeeds
                console.log("action taken: " + result);

                let div = document.createElement("div");
                div.className = "single_message2";
                let i = document.createElement("i");
                i.className = "fas fa-circle";
                let p = document.createElement("p");
                p.textContent = message;
                div.appendChild(p);
                div.appendChild(i);
                let messageBoxID = receiver + "addMessage";
                let box = document.getElementById(messageBoxID);
                box.appendChild(div);
                box.scrollTop = box.scrollHeight;

                let textAreaID = receiver + "text";
                document.getElementById(textAreaID).value = "";
                //$('#' + element).hide();
                console.log(e);
            }).fail(function (jqXHR, textStatus, errorThrown) {
                console.log("failed: ");
                console.log(jqXHR); console.log(textStatus); console.log(errorThrown);
            }).always(function () {
                console.log("but I will always do this")

            });

    } else {
        let textAreaID = receiver + "text";
        document.getElementById(textAreaID).value += e.key;
    }

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
//Sends a request to delete the specified user.
function DeleteUser(e, username) {
    e.preventDefault();
    Swal.fire({
        title: "Are you sure?",
        text: "You are about to delete " + username + "'s account.",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: "Delete User"
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/User/RemoveUser",
                method: "POST",
                data: {
                    username: username
                }
            }).fail(function () {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!'
                })
            }).done(function (data) {
                if (data.success) {
                    var row = document.getElementById(username + " Row");
                    row.parentNode.removeChild(row);
                    Swal.fire({
                        position: 'top-end',
                        type: 'success',
                        title: 'The user was deleted.',
                        showConfirmButton: false,
                        timer: 1500
                    })
                } else {
                    Swal.fire({
                        position: 'top-end',
                        type: 'error',
                        title: 'User delete failed.',
                        showConfirmButton: false,
                        timer: 1000
                    })
                }
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
                if (data.success) {
                    window.location.href = '/DeptManager/Index';
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
//Submits SampleFile form after appending sid
function SubmitSFForm(sid) {
    var form = $("<form>", {
        action: '/Department/GetSampleFile',
        method: 'GET',
        target: '_blank',
        hidden: 'hidden'
    });
    form.append($("<input>", {
        type: 'number',
        name: 'sfId', value: sid
    }));
    $('body').append(form);
    form.submit();
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
//Sends a request to change a course's due date
function ChangeDueDate(e, cid) {
    //window.alert("here");
    e.preventDefault();
    Swal.fire({
        title: "Are you sure?",
        text: "You are about to change this course's due date.",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: "Change Due Date"
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Department/UpdateCourseDueDate",
                method: "POST",
                data: {
                    courseId: cid,
                    newDueDate: $("#dateInput").val()
                }
            }).fail(function () {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!'
                })
            }).done(function (data) {
                if (data.success) {
                    Swal.fire({
                        position: 'top-end',
                        type: 'success',
                        title: 'The due date was updated.',
                        showConfirmButton: false,
                        timer: 1000
                    })
                } else {
                    Swal.fire({
                        position: 'top-end',
                        type: 'error',
                        title: 'The date could not be updated.',
                        showConfirmButton: false,
                        timer: 1000
                    })
                }
            });
        }
    })
}
//Sends request to approve a course
function ApproveCourse(e, cid) {
    //window.alert("here");
    e.preventDefault();
    Swal.fire({
        title: "Are you sure?",
        text: "You are about to change this course's status to approved.",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: "Approve"
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Department/ApproveCourse",
                method: "POST",
                data: {
                    courseId: cid
                }
            }).fail(function () {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!'
                })
            }).done(function (data) {
                if (data.success) {
                    $("#statusI").text("Complete");
                    Swal.fire({
                        position: 'top-end',
                        type: 'success',
                        title: 'The course was approved.',
                        showConfirmButton: false,
                        timer: 1000
                    })
                } else {
                    Swal.fire({
                        position: 'top-end',
                        type: 'error',
                        title: 'The course could not be approved.',
                        showConfirmButton: false,
                        timer: 1000
                    })
                }
            });
        }
    })
}
//Sends request to set a course to in-review
function ReviewCourse(e, cid) {
    //window.alert("here");
    e.preventDefault();
    Swal.fire({
        title: "Are you sure?",
        text: "You are about to change this course's status to in-review.",
        input: 'text',
        inputPlaceholder: "Message for Instructors",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: "In-Review"
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Department/SetReviewCourse",
                method: "POST",
                data: {
                    courseId: cid,
                    message: result.value
                }
            }).fail(function () {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!'
                })
            }).done(function (data) {
                if (data.success) {
                    $("#statusI").text("In-Review");
                    Swal.fire({
                        position: 'top-end',
                        type: 'success',
                        title: 'The course was set to in-review.',
                        showConfirmButton: false,
                        timer: 1000
                    })
                } else {
                    Swal.fire({
                        position: 'top-end',
                        type: 'error',
                        title: 'The course could not be set to in-review.',
                        showConfirmButton: false,
                        timer: 1000
                    })
                }
            });
        }
    })
}
//Sends request to get course approval
function RequestApproveCourse(e, cid) {
    //window.alert("here");
    e.preventDefault();
    Swal.fire({
        title: "Are you sure?",
        text: "You are about to request approval for this course.",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: "Request"
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Instructor/RequestApproval",
                method: "POST",
                data: {
                    courseId: cid
                }
            }).fail(function () {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!'
                })
            }).done(function (data) {
                if (data.success) {
                    $("#statusI").text("Awaiting Approval");
                    //window.alert($("#statusI").value);
                    Swal.fire({
                        position: 'top-end',
                        type: 'success',
                        title: 'The course was submitted for approval.',
                        showConfirmButton: false,
                        timer: 1000
                    })
                } else {
                    Swal.fire({
                        position: 'top-end',
                        type: 'error',
                        title: 'The course could not be submitted for approval.',
                        showConfirmButton: false,
                        timer: 1000
                    })
                }
            });
        }
    })
}
//Marks a notification as read
function ReadNotify(e, nid, row, col) {
    e.preventDefault();
    $.ajax({
        url: "/Home/ReadNotification",
        method: "POST",
        data: {
            notificationId: nid
        }
    }).fail(function () {
        Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'Something went wrong!'
        })
    }).done(function (data) {
        if (data.success) {
            $('#' + col).empty();
            $('#' + row).removeClass();
            $('#' + row).addClass('bg-light text-dark');

        } 
    });
}
//Delete a notification
function DeleteNotify(e, nid, row) {
    e.preventDefault();
    Swal.fire({
        title: "Are you sure?",
        text: "You are about to delete this notification.",
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: "Delete"
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Home/DeleteNotification",
                method: "POST",
                data: {
                    notificationId: nid
                }
            }).fail(function () {
                Swal.fire({
                    type: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!'
                })
            }).done(function (data) {
                if (data.success) {
                    if (data.success) {
                        $('#' + row).remove();
                    }
                } else {
                    Swal.fire({
                        position: 'top-end',
                        type: 'error',
                        title: 'The notification could not be deleted.',
                        showConfirmButton: false,
                        timer: 1000
                    })
                }
            });
        }
    })
}