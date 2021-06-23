function viewRecord(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Enable();
    $("#OrderNo").val(object.OrderNo);
    $("#PackageTypeCode").val(object.PackageTypeCode);
    $("#PackageTypeName").val(object.PackageTypeName);
    $("#Length").val(object.Length);
    $("#Width").val(object.Width);
    $("#Height").val(object.Height);
    $("#DimensionUnit").val(object.DimensionUnit);
    $("#Weight").val(object.Weight);
    $("#WeightUnit").val(object.WeightUnit);
    $("#Description").val(object.Description);
    let OrderID = object.PackageTypeID;
    $("#hfEditID").val(OrderID);

}
function viewRecordRead(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Disable();
    $("#OrderNo").val(object.OrderNo);
    $("#PackageTypeCode").val(object.PackageTypeCode);
    $("#PackageTypeName").val(object.PackageTypeName);
    $("#Length").val(object.Length);
    $("#Width").val(object.Width);
    $("#Height").val(object.Height);
    $("#DimensionUnit").val(object.DimensionUnit);
    $("#Weight").val(object.Weight);
    $("#WeightUnit").val(object.WeightUnit);
    $("#Description").val(object.Description);
    let OrderID = object.PackageTypeID;
    $("#hfEditID").val(OrderID);

}

function Disable() {
    $("#OrderNo").attr("readonly", true);
    $("#PackageTypeCode").attr("readonly", true);
    $("#PackageTypeName").attr("readonly", true);
    $("#Length").attr("readonly", true);
    $("#Width").attr("readonly", true);
    $("#Height").attr("readonly", true);
    $("#DimensionUnit").attr("readonly", true);
    $("#Weight").attr("readonly", true);
    $("#WeightUnit").attr("readonly", true);
    $("#Description").attr("readonly", true);
}

function Enable() {
    $("#OrderNo").attr("readonly", false);
    $("#PackageTypeCode").attr("readonly", false);
    $("#PackageTypeName").attr("readonly", false);
    $("#Length").attr("readonly", false);
    $("#Width").attr("readonly", false);
    $("#Height").attr("readonly", false);
    $("#DimensionUnit").attr("readonly", false);
    $("#Weight").attr("readonly", false);
    $("#WeightUnit").attr("readonly", false);
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
                url: `../Admin/DeletePackageType`,
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
        url: `../Admin/ChangePackageTypeStatus`,
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