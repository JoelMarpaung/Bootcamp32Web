$(document).ready(function () {

    loadData();
    //$('#dataGrid').DataTable({
    //    "data": loadData(),
    //    "columnDefs": [
    //        { "orderable": false, "targets": 4 }
    //    ]
    //}); 
});

$('#dataGrid').DataTable({
    "ajax": loadData(),
    "columnDefs": [
        { "orderable": false, "targets": 4 }
    ]
});

function loadData() {
    $.ajax({
        url: "/Suppliers/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            //const obj = JSON.parse(result);
            $.each(result, function (index, item) {
                html += '<tr>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Email + '</td>';
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

function Create() {
    var supplier = new Object();
    supplier.Name = $('#name').val();
    supplier.Email = $('#email').val();
    $.ajax({
        url: "/Suppliers/InsertOrUpdate/",
        //data: JSON.stringify(itemObj),
        type: "POST",
        //contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: supplier,
        success: function (result) {
            //debugger;
            console.log(result.StatusCode);
            $('#supplierModal').modal('hide');
            //var table = $('#dataGrid').dataTable({
            //    ajax: result
            //});
            loadData();
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
    var supplier = new Object();
    supplier.Id = $('#id').val();
    supplier.Name = $('#name').val();
    supplier.Email = $('#email').val();
    $.ajax({
        url: "/Suppliers/InsertOrUpdate/",
        //data: JSON.stringify(itemObj),
        type: "PUT",
        //contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: supplier,
        success: function (result) {
            console.log(result);
            $('#supplierModal').modal('hide');
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
        url: "/Suppliers/GetById/",
        type: "GET",
        data: { id: Id },
        dataType: "json",
        success: function (result) {
            $('#id').val(result.Id);
            $('#name').val(result.Name);
            $('#email').val(result.Email);
            $('#supplierModal').modal('show');
            $('#UpdateSupplier').show();
            $('#CreateSupplier').hide();
        }
    })
}

function ClearScreen() {
    $('#id').val('');
    $('#name').val('');
    $('#email').val('');
    $('#UpdateSupplier').hide();
    $('#CreateSupplier').show();
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
                url: "/Suppliers/Delete/",
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