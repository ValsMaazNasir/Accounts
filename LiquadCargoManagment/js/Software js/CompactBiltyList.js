function onSearchSuccess(response) {

    $('#modal-search').modal('hide');
    $("#grid").html(response.html);
    ClearItem();
    GetOrders();
}

function Message(title, text, type) {
    Swal.fire({
        title: title + '!',
        text: text,
        icon: type,
        confirmButtonText: 'OK'
    })
}
//function GetOrders() {
//    $("#example").DataTable({
//        "lengthMenu": [[50,100,150,200 -1], [50,100,150,200, "All"]]
//    }).order([1, 'des'])
//        .draw();
//}

function GetOrders() {
    $('#example').DataTable({
        "lengthMenu": [[100, 200, 300, 400 - 1], [100, 200, 300, 400, "All"]],
            dom: 'Bfrtip',
            buttons: [
                //'copyHtml5',
                {
                    extend: 'copyHtml5',
                    text: '<i class="fa fa-copy"></i> | COPY',
                    titleAttr: 'COPY'
                },
                //'excelHtml5',
                {
                    extend: 'excelHtml5',
                    text: '<i class="fa fa-file-o"></i> | EXCEL',
                    titleAttr: 'Excel'
                },
                //'csvHtml5',
                {
                    extend: 'csvHtml5',
                    text: '<i class="fa fa-file-o"></i> | CSV',
                    titleAttr: 'CSV'
                },
                {
                    extend:'pdfHtml5',
                    text: '<i class="fa fa-file-pdf-o"></i> | PDF',
                    titleAttr: 'PDF'
                },
                'pageLength'
            ]
    }).order([1, 'des'])
        .draw();
}
function ClearItem() {
    $("input[type='text']").val('');
    $("input[type='hidden']").val('');
    $("input[type='date']").val('');
}

function DeleteModel(Id) {
    debugger;
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, cancel it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: `../Admin/DeleteBilty`,
                type: 'POST',
                data: { ID: Id },
                success: function (res) {
                    if (res.Status != "OK") {
                        Message("Error", res.Message, "error");
                    }
                    else {
                        $("#grid").html(res.html);
                        GetOrders();
                        Message("Success", "Record Deleted Success", "success");
                        window.location.reload();
                    }
                },
                error: function () {
                    Message("Error", "An Error Occured", "error")
                    window.location.reload();
                }
            });

        }
    })

}

function Message(title, text, type) {
    Swal.fire({
        title: title + '!',
        text: text,
        icon: type,
        confirmButtonText: 'OK'
    })
}


function onSearchSuccess(response) {
    $('#modal-search').modal('hide');
    $("#grid").html(response.html);
    ClearItem();
    GetOrders();
}