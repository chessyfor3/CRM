$('#actTable').DataTable({
    "processing": true,
    "ajax": {
        "url": "/api/act",
        dataSrc: ''
    },
    "columns": [{
        "data": "date_created"
    }, {
        "data": "deal_title"
    }, {
        "data": "stage"
    }, {
            "render": function (ID, type, full) {
                return '<a href="#" onclick="viewActivity(' + full["ID"]+ ')" class="btn btn-sm btn-outline-dark" id="viewPromo" data-toggle="modal" data-target="#bigModal"><i class="fa fa-eye" ></i ></a >';
            }
    }]
});

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
