//Author: Valerie German
//Date: 18 Oct 2019
//Course: CS 4540, University of Utah
//Copyright: CS 4540 and Valerie German - This work may not be copied for use in Academic Coursework.
//I, Valerie German, certify that I wrote this code from scratch and did not copy it in part or whole from another source. Any references used in the completion of this assignment are cited in my README file.
//File Contents: JavaScript functions for course and learning outcome notes.

//Updates the Instructor's course note given the course id. The course note should be contained
//in a text area with the id 'CourseNote', and have a label with the id 'NoteLabel'.
function UpdateNote(e, courseId) {
    e.preventDefault();
    $.ajax({
        url: "/Instructor/ChangeNote",
        method: "POST",
        data: {
            CourseInstanceId: courseId,
            NewNote: $("#CourseNote").val()
        }
    }).done(function (data) {
        if (data.success) {
            Swal.fire({
                position: 'top-end',
                type: 'success',
                title: 'The note was updated.',
                showConfirmButton: false,
                timer: 1500
            })
        } else {
            Swal.fire({
                position: 'top-end',
                type: 'error',
                title: 'The note was not updated.',
                showConfirmButton: false,
                timer: 1000
            })
        }
    }).then(function (data) {
        //window.alert("then" + data.success);
        //window.alert(data.noteContent);
        $("#CourseNote").val(data.noteContent);
        var date = new Date(data.modified);
        $("#NoteLabel").text("Last modified: " + date);
    });
}
//Updates the learning outcome note for an instructor.
function UpdateLONoteInst(e, loId, noteId, noteLabelId) {
    e.preventDefault();
    $.ajax({
        url: "/Instructor/ChangeLONote",
        method: "POST",
        data: {
            LearningOutcomeId: loId,
            NewNote: $('#' + noteId).val()
        }
    }).done(function (data) {
        if (data.success) {
            Swal.fire({
                position: 'top-end',
                type: 'success',
                title: 'The note was updated.',
                showConfirmButton: false,
                timer: 1500
            })
        } else {
            Swal.fire({
                position: 'top-end',
                type: 'error',
                title: 'The note was not updated.',
                showConfirmButton: false,
                timer: 1000
            })
        }
    }).then(function (data) {
        $('#' + noteId).val(data.noteContent);
        var date = new Date(data.modified);
        $('#' + noteLabelId).text("Last modified: " + date + " by " + data.user);
    });
}
//Updates teh learning outcome note for a chair.
function UpdateLONoteDept(e, loId, noteId, noteLabelId) {
    e.preventDefault();
    $.ajax({
        url: "/Department/ChangeNote",
        method: "POST",
        data: {
            LearningOutcomeId: loId,
            NewNote: $('#' + noteId).val()
        }
    }).done(function (data) {
        if (data.success) {
            Swal.fire({
                position: 'top-end',
                type: 'success',
                title: 'The note was updated.',
                showConfirmButton: false,
                timer: 1500
            })
        } else {
            Swal.fire({
                position: 'top-end',
                type: 'error',
                title: 'The note was not updated.',
                showConfirmButton: false,
                timer: 1000
            })
        }
    }).then(function (data) {
        $('#' + noteId).val(data.noteContent);
        var date = new Date(data.modified);
        $('#' + noteLabelId).text("Last modified: " + date + " by " + data.user);
    });
}