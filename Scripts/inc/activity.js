$('#actTable').DataTable();

function viewActivity(id) {
    $('#bigModalLabel').html('Activity Information');
    $.get('/Activity/Details/' + id, function (data, status) {
        $('#saveBig').unbind();
        if ('success' == status) {
            $('#bigModalBody').html(data);
            $('#saveBig').hide();
            $('#cancelBig').hide();
        } else {
            $('#bigModalBody').html("<p class='alert alert-danger'>Something's wrong.</p>");
        }
    });
}
