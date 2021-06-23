function viewRecord(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Enable();
    $("#OrderNo").val(object.OrderNo);
    $("#Code").val(object.Code);
    $("#ProductName").val(object.Name);
    $("#Category").val(object.Category);
    $("#PackageTypeID").val(object.PackageTypeID);
    $("#ProdcutNatureID").val(object.ProdcutNatureID);
    $("#Unit").val(object.Unit);
    $("#Volume").val(object.Volume);
    $("#Height").val(object.Height);
    $("#Width").val(object.Width);
    $("#Length").val(object.Length);
    $("#Weight").val(object.Weight);
    $("#Description").val(object.Description);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);

}

function viewRecordRead(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Disable();
    $("#OrderNo").val(object.OrderNo);
    $("#Code").val(object.Code);
    $("#ProductName").val(object.Name);
    $("#Category").val(object.Category);
    $("#PackageTypeID").val(object.PackageTypeID);
    $("#ProductNatureID").val(object.ProductNatureID);
    $("#Unit").val(object.Unit);
    $("#Volume").val(object.Volume);
    $("#Height").val(object.Height);
    $("#Width").val(object.Width);
    $("#Length").val(object.Length);
    $("#Weight").val(object.Weight);
    $("#Description").val(object.Description);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);

}

function Disable() {
    $("#OrderNo").attr("readonly", true);
    $("#Code").attr("readonly", true);
    $("#ProductName").attr("readonly", true);
    $("#Category").attr("readonly", true);
    $("#PackageTypeID").attr("readonly", true);
    $("#ProductNatureID").attr("readonly", true);
    $("#Unit").attr("readonly", true);
    $("#Volume").attr("readonly", true);
    $("#Height").attr("readonly", true);
    $("#Width").attr("readonly", true);
    $("#Length").attr("readonly", true);
    $("#Weight").attr("readonly", true);
    $("#Description").attr("readonly", true);
}
function Enable() {
    $("#OrderNo").attr("readonly", false);
    $("#Code").attr("readonly", false);
    $("#ProductName").attr("readonly", false);
    $("#Category").attr("readonly", false);
    $("#PackageTypeID").attr("readonly", false);
    $("#ProductNatureID").attr("readonly", false);
    $("#Unit").attr("readonly", false);
    $("#Volume").attr("readonly", false);
    $("#Height").attr("readonly", false);
    $("#Width").attr("readonly", false);
    $("#Length").attr("readonly", false);
    $("#Weight").attr("readonly", false);
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
                url: `../Admin/DeleteProduct`,
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
        url: `../Admin/ChangeProductStatus`,
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