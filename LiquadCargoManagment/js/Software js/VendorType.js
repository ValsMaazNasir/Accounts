function viewRecord(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Enable();
    $("#Code").val(object.Code);
    $("#Name").val(object.Name);
    $("#Description").val(object.Description);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);
}

function viewRecordRead(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Disable();
    $("#Code").val(object.Code);
    $("#Name").val(object.Name);
    $("#Description").val(object.Description);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);
}

function Disable() {
    $("#Code").attr("readonly", true);
    $("#Name").attr("readonly", true);
    $("#Description").attr("readonly", true);
}

function Enable() {
    $("#Code").attr("readonly", false);
    $("#Name").attr("readonly", false);
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
                url: `../Admin/DeleteVendorType`,
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