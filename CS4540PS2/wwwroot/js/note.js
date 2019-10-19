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