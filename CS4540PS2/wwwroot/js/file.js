//Initialize Sample Files modal to take evaluation metric ID
$('#sModal').on('show.bs.modal', function (event) {
    var evaluationMetricId = $(event.relatedTarget).data('em-id');
    $('#emIdInput').val(evaluationMetricId);
})

function SubmitForm(id) {
    $('#' + id).submit();
}