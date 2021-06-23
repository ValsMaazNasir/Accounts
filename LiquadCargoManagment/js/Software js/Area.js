﻿//Area Function
function viewRecord(object, e) {
    console.log(object);
    Enable();
    $("#modal-add").modal('show');
    $("#Code").val(object.Code);
    $("#AreaName").val(object.Name);
    $("#CityID").val(object.CityID);
    $("#Description").val(object.Description);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);

}
function viewRecordRead(object, e) {
    console.log(object);
    Disable();
    $("#modal-add").modal('show');
    $("#Code").val(object.Code);
    $("#AreaName").val(object.Name);
    $("#CityID").val(object.CityID);
    $("#Description").val(object.Description);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);

}

function Disable() {
    $("#Code").attr("readonly", true);
    $("#AreaName").attr("readonly", true);
    $("#CityID").attr("disabled", true);
    $("#Description").attr("readonly", true);
}

function Enable() {
    $("#Code").attr("readonly", false);
    $("#AreaName").attr("readonly", false);
    $("#CityID").attr("disabled", false);
    $("#Description").attr("readonly", false);
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
                url: `../Admin/DeleteArea`,
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
                    Message("Error","Use another forms please check now !","error");
                }
            });

        }
    })

}

function ChangeStatus(Id, status) {
    $.ajax({
        url: `../Admin/ChangeAreaStatus`,
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
//Area Function