$('#customersTable').DataTable();

var table = $('#customersTable').DataTable();
function editCustomer(id,elem) {
    $('#bigModalLabel').html('Input Customer Information');
    $.get('/Customer/Edit/'+id, function (data, status) {
        $('#saveBig').unbind();
        if ('success' == status) {
            $('#bigModalBody').html(data);
            $('#saveBig').html('Save');
            $('#saveBig').on('click', function () {
                saveCustomer(id,elem);
            });
            $('#saveBig').show();
            $('#cancelBig').show();
        } else {
            $('#bigModalBody').html("<p class='alert alert-danger'>Something's wrong.</p>");
        }
    });
}


function getCreate() {
    $('#bigModalLabel').html('Input Customer Information');
    $.get('/Customer/Create', function (data,status) {
        $('#saveBig').unbind();
        if ('success' == status) {
            $('#bigModalBody').html(data);
            $('#saveBig').html('Save');
            $('#saveBig').on('click', function () {
                saveCustomer(null,null);
            });
            $('#saveBig').show();
            $('#cancelBig').show();
        } else {
            $('#bigModalBody').html("<p class='alert alert-danger'>Something's wrong.</p>");
        }
    });
}

function saveCustomer(id,elem) {
    var customer = getData();
    var url = '/Customer/Create';
    var updateTable = function (customer) {
        
        table.row.add(fillRowData(customer)).draw();
    }
    var updateRow = function (customer) {
        
        table.row($(elem).parents('tr')).data(fillRowData(customer)).invalidate().draw();
    }

    if (null != id) {
        url = '/Customer/Edit';
        customer.id = id;
        updateTable = updateRow;
    }
    
    if (checkEmptyFields()) {
        swal("Fill out the missing fields", {
            icon: "error"
        });
    } else {
        $.post(url, customer, function (result) {
            if ("success" == result.status) {
                swal(result.message, {
                    icon: "success"
                });
                updateTable(result.customer);
                $('#cancelBig').click();
            } else {
                swal(result.message, {
                    icon: "error"
                });
            }
        });
    }
    
}
function deleteCustomer(id, elem) {
    swal({
        title: "Are you sure?",
        text: "Customer information will be deleted!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
    .then((willDelete) => {
        if (willDelete) {
            $.post('/Customer/Delete', { id: id }, function (result) {
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
function viewCustomer(id) {
    $('#bigModalLabel').html('Customer Information');
    $.get('/Customer/Details/'+id, function (data, status) {
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

function getData() {
    var data = {
        name: toTitleCase($('#name').val()),
        business_type: $('#business_type').val(),
        industry: $('#industry').val(),
        email: $('#email').val(),
        phone: $('#phone').val(),
        street: toTitleCase($('#street').val()),
        city: toTitleCase($('#city').val()),
        zip: $('#zip').val(),
        customer_type: $('#customer_type').val()
    }
    return data;
}

function toTitleCase(str) {
    return str.replace(/\w\S*/g, function (txt) {
        return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
    });
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

function fillRowData(customer) {
    
    var row = [
         customer.name,
         customer.customer_type,
         customer.business_type,
         customer.industry,
        '<a href="#" onclick="viewCustomer(' + customer.ID + ')" class="btn btn-sm btn-outline-dark" id="editUser" data-toggle="modal" data-target="#bigModal"><i class= "fa fa-eye" ></i></a > &nbsp; | &nbsp; <a href="#" onclick="editCustomer(' + customer.ID + ',this)" class="btn btn-sm btn-outline-info" id="editUser" data-toggle="modal" data-target="#bigModal"><i class="fa fa-edit"></i></a> &nbsp; | &nbsp; <a href="#" onclick="deleteCustomer(' + customer.ID + ',this)" class="btn btn-sm btn-outline-danger" id="deleteUser"><i class="fa fa-trash"></i></a>',
    ];
    
    return row;
}




function checkEmail() {

}