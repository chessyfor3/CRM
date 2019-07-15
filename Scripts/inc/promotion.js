$('#promotionsTable').DataTable();
var table = $('#promotionsTable').DataTable();

function editPromo(id, elem) {
    $('#bigModalLabel').html('Edit Promo Information');
    $.get('/Promotion/Edit/' + id, function (data, status) {
        $('#saveBig').unbind();
        if ('success' == status) {
            $('#bigModalBody').html(data);
            $('#saveBig').html('Save');
            $('#saveBig').on('click', function () {
                savePromotion(id, elem);
            });
            $('#saveBig').show();
            $('#cancelBig').show();
        } else {
            $('#bigModalBody').html("<p class='alert alert-danger'>Something's wrong.</p>");
        }
    });
}


function viewPromo(id) {
    $('#bigModalLabel').html('Promo Information');
    $.get('/Promotion/Details/' + id, function (data, status) {
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


function deletePromo(id, elem) {
    swal({
        title: "Are you sure?",
        text: "This promo will be deleted!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
    .then((willDelete) => {
        if (willDelete) {
            $.post('/Promotion/Delete', { id: id }, function (result) {
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
    $('#bigModalLabel').html('Input Promo Information');
    $.get('/Promotion/Create', function (data,status) {
        $('#saveBig').unbind();
        if ('success' == status) {
            $('#bigModalBody').html(data);
            $('#saveBig').html('Save');
            $('#saveBig').on('click', function () {
                savePromotion(null, null);
            });
            $('#saveBig').show();
            $('#cancelBig').show();
        } else {
            $('#bigModalBody').html("<p class='alert alert-danger'>Something's wrong.</p>");
        }
    });
}

function savePromotion(id, elem) {
    var promo = getData();

    var url = '/Promotion/Create';
    var updateTable = function (promo,validity) {
        table.row.add(fillRowData(promo, validity)).draw();
    }
    var updateRow = function (promo, validity) {
        table.row($(elem).parents('tr')).data(fillRowData(promo, validity)).invalidate().draw();
    }

    if (null != id) {
        url = '/Promotion/Edit';
        promo.id = id;
        updateTable = updateRow;
    }

    if (checkEmptyFields()) {
        swal("Fill out the missing fields", {
            icon: "error"
        });
    } else {
        $.post(url, promo, function (result) {
            if ("success" == result.status) {
                swal(result.message, {
                    icon: "success"
                });
                
                updateTable(result.promo, result.validity);
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
        name: $('#name').val(),
        code: $('#code').val(),
        description: $('#description').val(),
        ext_desc: $('#ext_desc').val(),
        start: $('#start').val(),
        end: $('#end').val()
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
function fillRowData(promo,validity) {
    var row = [
        promo.code,
        promo.name,
        validity.start + ' - ' + validity.end,
        '<a href="#" onclick="viewPromo(' + promo.ID + ',this)" class="btn btn-sm btn-outline-dark" id="editUser" data-toggle="modal" data-target="#bigModal"><i class= "fa fa-eye" ></i></a > &nbsp; | &nbsp; <a href="#" onclick="editPromo(' + promo.ID + ',this)" class="btn btn-sm btn-outline-info" id="editUser" data-toggle="modal" data-target="#bigModal"><i class="fa fa-edit"></i></a> &nbsp; | &nbsp; <a href="#" onclick="deletePromo(' + promo.ID + ',this)" class="btn btn-sm btn-outline-danger" id="deleteUser"><i class="fa fa-trash"></i></a>',
    ];
    return row;
}