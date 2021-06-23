//Designation Function
function viewRecord(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Enable();
    debugger;
    $("#DCode").val(object.DesignationCode);
    $("#DName").val(object.DesignationName);
    $("#DDescription").val(object.Description);
    let OrderID = object.DesignationID;
    $("#hfEditID").val(OrderID);

}

function viewRecordRead(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Disable();
    $("#DCode").val(object.DesignationCode);
    $("#DName").val(object.DesignationName);
    $("#DDescription").val(object.Description);
    let OrderID = object.DesignationID;
    $("#hfEditID").val(OrderID);
}

function Disable() {
    $("#DCode").attr("readonly", true);
    $("#DName").attr("readonly", true);
    $("#DDescription").attr("readonly", true);
}
function Enable() {
    $("#DCode").attr("readonly", false);
    $("#DName").attr("readonly", false);
    $("#DDescription").attr("readonly", false);
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
                url: `../Admin/DeleteOwnGroupDesignation`,
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
        url: `../Admin/ChangeDesignationStatus`,
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
//Designation Function