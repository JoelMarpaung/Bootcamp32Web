$(document).ready(function () {
    loadSupplier();
    loadData();
});

$('#dataGrid').DataTable({
    "ajax": loadData(),
    "columnDefs": [
        { "orderable": false, "targets": 4 }
    ]
});

function loadData() {
    $.ajax({
        url: "/Items/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            var html = '';
            //const obj = JSON.parse(result);
            $.each(result, function (index, item) {
                html += '<tr>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Price + '</td>';
                html += '<td>' + item.Stock + '</td>';
                html += '<td>' + item.Supplier.Name + '</td>';
                //html += '<td>' + item.createDate + '</td>';
                //html += '<td>' + "test" + '</td>';
                html += '<td><a href="#" onclick="return getbyID(' + item.Id + ')">Edit</a> | <a href="#" onclick="Delete(' + item.Id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!'
            })
        }
    });
}

function loadSupplier() {
    $.ajax({
        url: "/Suppliers/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            //const obj = JSON.parse(result);
            $.each(result, function (index, item) {
                html += '<option value=' + item.Id;
                if ($('#Supp_Id').val() == item.Id) {
                    html += ' selected ';
                }
                html += '>' + item.Name + '</option>';
            });
            $('#Supplier_Id').html(html);
        },
        error: function (errormessage) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!'
            })
        }
    });
}

function Create() {
    var item = new Object();
    item.name = $('#name').val();
    item.price = $('#price').val();
    item.stock = $('#stock').val();
    item.Supplier_Id = $('#Supplier_Id').val();
    $.ajax({
        url: "/Items/InsertOrUpdate/",
        //data: JSON.stringify(itemObj),
        type: "POST",
        //contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: item,
        success: function (result) {
            //debugger;
            console.log(result.StatusCode);
            $('#itemModal').modal('hide');
            //var table = $('#dataGrid').dataTable({
            //    ajax: result
            //});
            //loadData();
            ResetTable();
            ClearScreen();
            if (result.StatusCode == 200) {
                Swal.fire(
                    'Success',
                    'Data has been created',
                    'success'
                );
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong!'
                });
            }

        },
        error: function (errormessage) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!'
            })
        }

    });
}

function Update() {
    var item = new Object();
    item.id = $('#id').val();
    item.name = $('#name').val();
    item.price = $('#price').val();
    item.stock = $('#stock').val();
    item.Supplier_Id = $('#Supplier_Id').val();
    $.ajax({
        url: "/Items/InsertOrUpdate/",
        //data: JSON.stringify(itemObj),
        type: "PUT",
        //contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: item,
        success: function (result) {
            console.log(result);
            $('#itemModal').modal('hide');
            //var table = $('#dataGrid').dataTable({
            //    ajax: result
            //});
            loadData();
            ClearScreen();
            Swal.fire(
                'Success',
                'Data has been updated',
                'success'
            );
        },
        error: function (errormessage) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Something went wrong!'
            })
        }

    });
}

function getbyID(Id) {
    $.ajax({
        url: "/Items/GetById/",
        type: "GET",
        data: { id: Id },
        dataType: "json",
        success: function (result) {
            $('#id').val(result.Id);
            $('#name').val(result.Name);
            $('#price').val(result.Price);
            $('#stock').val(result.Stock);
            $('#Supp_Id').val(result.Supplier.Id);
            loadSupplier();
            $('#itemModal').modal('show');
            $('#UpdateItem').show();
            $('#CreateItem').hide();
        }
    })
}

function ClearScreen() {
    $('#id').val('');
    $('#name').val('');
    $('#price').val('');
    $('#stock').val('');
    $('#Supp_Id').val('');
    $('#UpdateItem').hide();
    $('#CreateItem').show();
    loadSupplier();
}

function Delete(Id) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Items/Delete/",
                type: "DELETE",
                data: { id: Id },
                success: function (result) {
                    swalWithBootstrapButtons.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    );
                    loadData();
                    ClearScreen();
                },
                error: function (errormessage) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong!'
                    })
                }
            });


        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Your data is safe :)',
                'error'
            )
        }
    })

}

function ResetTable() {
    //$('#dataGrid').DataTable().destroy();
    $('#dataGrid').DataTable({
        "ajax": loadData(),
        "columnDefs": [
            { "orderable": false, "targets": 4 }
        ]
    });
}