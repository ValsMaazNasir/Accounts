//Container Type Function
function viewRecord(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Enable();
    $("#Code").val(object.Code);
    $("#ContainerTypeName").val(object.ContainerTypeName);
    $("#Description").val(object.Description);
    let OrderID = object.ContainerTypeID;
    $("#hfEditID").val(OrderID);

}
function viewRecordRead(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Disable();
    $("#Code").val(object.Code);
    $("#ContainerTypeName").val(object.ContainerTypeName);
    $("#Description").val(object.Description);
    let OrderID = object.ContainerTypeID;
    $("#hfEditID").val(OrderID);
}

function Disable() {
    $("#Code").attr("readonly", true);
    $("#ContainerTypeName").attr("readonly", true);
    $("#Description").attr("disabled", true);
}

function Enable() {
    $("#Code").attr("readonly", false);
    $("#ContainerTypeName").attr("readonly", false);
    $("#Description").attr("disabled", false);
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
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: `../Admin/DeleteContainerType`,
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
                    }
                },
                error: function () {
                    alert('error');
                }
            });

        }
    })

}

function ChangeStatus(Id, status) {
    $.ajax({
        url: `../Admin/ChangeContainerTypeStatus`,
        type: 'POST',
        data: { ID: Id, status: status },
        success: function (res) {
            if (res.Status != "OK") {
                Message("Error", res.Message, "error");
            }
            else {
                if (status == true) {
                    $("#grid").html(res.html);
                    GetOrders();
                    Message("Success", "Activate Success", "success");
                }
                else {
                    $("#grid").html(res.html);
                    GetOrders();
                    Message("Success", "Deactivate Success", "success");
                }

            }
        },
        error: function () {
            window.location.reload();
        }
    })
}
//Container Type Function