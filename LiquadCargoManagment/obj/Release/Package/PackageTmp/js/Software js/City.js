//City Function
function viewRecord(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Enable();
    $("#CityCode").val(object.CityCode);
    $("#CityName").val(object.CityName);
    $("#ProvinceID").val(object.ProvinceID);
    $("#Description").val(object.Description);
    let OrderID = object.CityID;
    $("#hfEditID").val(OrderID);

}
function viewRecordRead(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Disable();
    $("#CityCode").val(object.CityCode);
    $("#CityName").val(object.CityName);
    $("#ProvinceID").val(object.ProvinceID);
    $("#Description").val(object.Description);
    let OrderID = object.CityID;
    $("#hfEditID").val(OrderID);
}
function Disable() {
    $("#CityCode").attr("readonly", true);
    $("#CityName").attr("readonly", true);
    $("#ProvinceID").attr("readonly", true);
    $("#Description").attr("readonly", true);
}

function Enable() {
    $("#CityCode").attr("readonly", false);
    $("#CityName").attr("readonly", false);
    $("#ProvinceID").attr("readonly", false);
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
                url: `../Admin/DeleteCity`,
                type: 'POST',
                data: { ID: Id },
                success: function (res) {
                    if (res.Status != "OK") {
                        Message("Error", res.Message, "error");
                    }
                    else {
                        $("#grid").html(res.html);
                        GetOrders();
                        Message("Success", "Deleted Successfully", "success");
                    }
                },
                error: function () {
                    Message("Error", "Use another forms please check now !", "error");
                }
            });
        }
    })

}

function ChangeStatus(Id, status) {
    $.ajax({
        url: `../Admin/ChangeCityStatus`,
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
//City Function