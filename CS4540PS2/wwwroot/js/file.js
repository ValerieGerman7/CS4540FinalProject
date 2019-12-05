//Initialize Sample Files modal to take evaluation metric ID
$('#sModal').on('show.bs.modal', function (event) {
    var evaluationMetricId = $(event.relatedTarget).data('em-id');
    $('#emIdInput').val(evaluationMetricId);
})

$('#emModal').on('show.bs.modal', function (event) {
    var loid = $(event.relatedTarget).data('loid');
    $('#loidInput').val(loid);
})

function SubmitForm(id) {
    $('#' + id).submit();
}