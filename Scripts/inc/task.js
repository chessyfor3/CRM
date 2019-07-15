$('#tasksTable').DataTable(
    {
        "columns": [
            { "data": "date" },
            { "data": "title" },
            { "data": "view" },
        ]
    }
);
var table = $('#tasksTable').DataTable();
function confirm(id) {
    $.post('/Task/Confirm', { id: id }, function (data, status) {

        if ('success' == status) {
            swal(data.message, {
                icon: "success"
            });
            $('#cancelBig').click();
        } else {
            swal(data.message, {
                icon: "error"
            });
        }
    });
}


function markDone(id) {
    $.post('/Task/Mark', {id:id}, function (data, status) {
        
        if ('success' == status) {
            swal(data.message, {
                icon: "success"
            });
            $('#cancelBig').click();
        } else {
            swal(data.message, {
                icon: "error"
            });
        }
    });
}

function getFinished() {
    $('#bigModalLabel').html('Finished Tasks');
    $.get('/Task/Finished', function (data, status) {
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


function getToday() {
    $('#bigModalLabel').html('Today Schedule');
    $.get('/Task/Today', function (data, status) {
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


function editTask(id, elem) {
    $('#bigModalLabel').html('Edit Task Information');
    $.get('/Task/Edit/' + id, function (data, status) {
        $('#saveBig').unbind();
        if ('success' == status) {
            $('#bigModalBody').html(data);
            $('#saveBig').html('Save');
            $('#saveBig').on('click', function () {
                if (null == elem) {
                    elem = findElem(id);
                }
                saveTask(id, elem);
            });
            $('#saveBig').show();
            $('#cancelBig').show();
        } else {
            $('#bigModalBody').html("<p class='alert alert-danger'>Something's wrong.</p>");
        }
    });
}


function viewTask(id) {
    $('#bigModalLabel').html('Task Information');
    $.get('/Task/Details/' + id, function (data, status) {
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


function deleteTask(id, elem) {
    swal({
        title: "Are you sure?",
        text: "This task will be deleted!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.post('/Task/Delete', { id: id }, function (result) {
                    if ("success" == result.status) {
                        swal(result.message, {
                            icon: "success"
                        });
                        table.row($(elem).parents('tr')).remove().draw();
                    } else {
                        swal(result.message, {
                            icon: "error"
                        });
                    }
                });
            }
        });
}


function getCreate() {
    $('#bigModalLabel').html('Input Task Information');
    $.get('/Task/Create', function (data, status) {
        $('#saveBig').unbind();
        if ('success' == status) {
            $('#bigModalBody').html(data);
            $('#saveBig').html('Save');
            $('#saveBig').on('click', function () {
                saveTask(null, null);
            });
            $('#saveBig').show();
            $('#cancelBig').show();
        } else {
            $('#bigModalBody').html("<p class='alert alert-danger'>Something's wrong.</p>");
        }
    });
}

function saveTask(id, elem) {
    var task = getData();

    var url = '/Task/Create';
    var updateTable = function (task,date) {
        table.row.add(fillRowData(task,date)).draw();
    }
    var updateRow = function (ok,date) {
        table.row($(elem).parents('tr')).data(fillRowData(task,date)).invalidate().draw();
    }

    if (null != id) {
        url = '/Task/Edit';
        task.id = id;
        updateTable = updateRow;
    }

    if (checkEmptyFields()) {
        swal("Fill out the missing fields", {
            icon: "error"
        });
    } else {
        $.post(url, task, function (result) {
            if ("success" == result.status) {
                swal(result.message, {
                    icon: "success"
                });
                updateTable(result.task, result.date);
                $('#cancelBig').click();
            } else {
                swal(result.message, {
                    icon: "error"
                });
            }
        });
    }
}

function getData() {
    var data = {
        title: $('#title').val(),
        description: $('#description').val(),
        date: $('#date').val(),
        user_id: $('#user_id').val(),
    }
    return data;
}

function checkEmptyFields() {
    var stat = false;
    $(".input").each(function () {
        var val = $(this).val();
        if ("" == val) {
            $(this).addClass("is-invalid");
            stat = true;
        }

    });
    return stat;
}
function fillRowData(task, date) {
    var row = {
        "DT_RowId" : task.ID,
        "date": date,
        "title": task.title,
        'view':'<a href="#" onclick="viewTask(' + task.ID + ',this)" class="btn btn-sm btn-outline-dark" id="editUser" data-toggle="modal" data-target="#bigModal"><i class= "fa fa-eye" ></i></a > &nbsp; | &nbsp; <a href="#" onclick="editTask(' + task.ID + ',this)" class="btn btn-sm btn-outline-info" id="editUser" data-toggle="modal" data-target="#bigModal"><i class="fa fa-edit"></i></a> &nbsp; | &nbsp; <a href="#" onclick="deleteTask(' + task.ID + ',this)" class="btn btn-sm btn-outline-danger" id="deleteUser"><i class="fa fa-trash"></i></a>',
    };
    return row;
}

function findElem(id) {
    var row;
    table.rows().eq(0).each(function (index) {
        if ($(table.row(index).node()).attr('id') == id) {
            row = table.row(index).node();
        }
    });
    return $(row).children('td').get(0);
}   