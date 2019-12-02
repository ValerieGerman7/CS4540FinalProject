function select_page(e, Model) {
    console.log("in select page function");
    console.log(e);
    e.preventDefault();

    $.ajax({
        url: "/Course/ChangePage",
        method: "POST",
        data: {
            instances: Model,
            pageNumber: $("#CoursePageNumInput").val()
        }

    }).done(function (data) {
        console.log("action taken: " + JSON.stringify(data));
        $("#CoursePageNumInput").val(42);

    }).fail(function (jqXHR, textStatus, errorThrown) {
        console.log("failed: ");
        console.log(jqXHR);
        console.log(textStatus);
        console.log(errorThrown);

    }).always(function () {
        console.log("User selected page.");
    });
}