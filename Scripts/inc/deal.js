$('#dealsTable').DataTable();
var table = $('#dealsTable').DataTable();

function dealUpgrade(id) {
    $.post('/Deal/Upgrade', {id:id},function(result){
        if ('success' == result.status) {
            $('#next-stage').html(result.next);
            $('#stage').html(result.stage);
            if ("Closed" == result.stage) {
                swal("Win or Lost?", {
                    buttons: {
                        win: {
                            text: "Win!",
                            value: "win",
                        },
                        lost: {
                            text: "Lost",
                            value: "lost",
                        },
                    },
                })
                    .then((value) => {
                        switch (value) {

                            case "win":
                                wind(id, 1);
                                break;

                            case "lost":
                                wind(id, 0);
                                break;

                            default:
                                swal("Got away safely!");
                        }
                    });
            } else {
                swal(result.message, {
                    icon: "success"
                });
            }

        } else {
            swal("Something went wrong.", {
                icon: "error"
            });
        }
    });
}
function wind(id,win) {
    $.post('/Deal/Win', {id:id, win:win}, function (result) {
        if ('success' == result.status) {
            swal(result.message, {
                icon: "success"
            });
        } else {
            swal(result.message, {
                icon: "error"
            });
        }
    });
}

function editDeal(id, elem) {
    $('#bigModalLabel').html('Edit Deal Information');
    $.get('/Deal/Edit/' + id, function (data, status) {
        $('#saveBig').unbind();
        if ('success' == status) {
            $('#bigModalBody').html(data);
            $('#saveBig').html('Save');
            $('#saveBig').on('click', function () {
                saveDeal(id, elem);
            });
            $('#saveBig').show();
            $('#cancelBig').show();
        } else {
            $('#bigModalBody').html("<p class='alert alert-danger'>Something's wrong.</p>");
        }
    });
}


function viewDeal(id) {
    $('#bigModalLabel').html('Deal Information');
    $.get('/Deal/Details/' + id, function (data, status) {
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


function deleteDeal(id, elem) {
    swal({
        title: "Are you sure?",
        text: "This promo will be deleted!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
    .then((willDelete) => {
        if (willDelete) {
            $.post('/Deal/Delete', { id: id }, function (result) {
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
    $('#bigModalLabel').html('Input Deal Information');
    $.get('/Deal/Create', function (data, status) {
        $('#saveBig').unbind();
        if ('success' == status) {
            $('#bigModalBody').html(data);
            $('#saveBig').html('Save');
            $('#saveBig').on('click', function () {
                saveDeal(null, null);
            });
            $('#saveBig').show();
            $('#cancelBig').show();
        } else {
            $('#bigModalBody').html("<p class='alert alert-danger'>Something's wrong.</p>");
        }
    });
}

function saveDeal(id, elem) {
    var deal = getData();

    var url = '/Deal/Create';
    var updateTable = function (deal,customer) {
        table.row.add(fillRowData(deal,customer)).draw();
    }
    var updateRow = function (ok, customer) {
        table.row($(elem).parents('tr')).data(fillRowData(deal, customer)).invalidate().draw();
    }

    if (null != id) {
        url = '/Deal/Edit';
        deal.id = id;
        updateTable = updateRow;
    }

    if (checkEmptyFields()) {
        swal("Fill out the missing fields", {
            icon: "error"
        });
    } else {
        $.post(url, deal, function (result) {
            if ("success" == result.status) {
                swal(result.message, {
                    icon: "success"
                });
                updateTable(result.deal, result.customer);
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
        value: $('#value').val(),
        stage: $('#stage').val(),
        closing_probability: $('#closing_probability').val(),
        expected_closing: $('#expected_closing').val(),
        promo_code: $('#promo_code').val(),
        customer_id: $('#customer_id').val(),
        agent_id: $('#agent_id').val(),
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
function fillRowData(deal,customer) {
    var row = [
        deal.title,
        customer.name,
        deal.value,
        '<a href="#" onclick="viewDeal(' + deal.ID + ',this)" class="btn btn-sm btn-outline-dark" id="editUser" data-toggle="modal" data-target="#bigModal"><i class= "fa fa-eye" ></i></a > &nbsp; | &nbsp; <a href="#" onclick="editDeal(' + deal.ID + ',this)" class="btn btn-sm btn-outline-info" id="editUser" data-toggle="modal" data-target="#bigModal"><i class="fa fa-edit"></i></a> &nbsp; | &nbsp; <a href="#" onclick="deleteDeal(' + deal.ID + ',this)" class="btn btn-sm btn-outline-danger" id="deleteUser"><i class="fa fa-trash"></i></a>',
    ];
    return row;
}