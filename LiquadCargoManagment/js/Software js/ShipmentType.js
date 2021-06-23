function viewRecord(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Enable();
    debugger;
    $("#SCode").val(object.Code);
    $("#SName").val(object.Name);
    $("#DDescription").val(object.Description);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);

}

function viewRecordRead(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Disable();
    $("#SCode").val(object.Code);
    $("#SName").val(object.Name);
    $("#SDescription").val(object.Description);
    let OrderID = object.DepartID;
    $("#hfEditID").val(OrderID);
}

function Disable() {
    $("#SCode").attr("readonly", true);
    $("#SName").attr("readonly", true);
    $("#SDescription").attr("readonly", true);
}
function Enable() {
    $("#SCode").attr("readonly", false);
    $("#SName").attr("readonly", false);
    $("#SDescription").attr("readonly", false);
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
                url: `../Admin/DeleteShipmentType`,
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
        url: `../Admin/ChangeShipmentTypeStatus`,
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