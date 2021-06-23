//Customer Group Function
function viewRecord(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Enable();
    $("#Code").val(object.Code);
    $("#Name").val(object.Name);
    $("#Contact").val(object.Contact);
    $("#ContactOther").val(object.ContactOther);
    $("#EmailAdd").val(object.EmailAdd);
    $("#WebAdd").val(object.WebAdd);
    $("#Address").val(object.Address);
    $("#Description").val(object.Description);
    let OrderID = object.GroupID;
    $("#hfEditID").val(OrderID);

}

function viewRecordRead(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Disable();
    $("#Code").val(object.Code);
    $("#Name").val(object.Name);
    $("#Contact").val(object.Contact);
    $("#ContactOther").val(object.ContactOther);
    $("#EmailAdd").val(object.EmailAdd);
    $("#WebAdd").val(object.WebAdd);
    $("#Address").val(object.Address);
    $("#Description").val(object.Description);
    let OrderID = object.GroupID;
    $("#hfEditID").val(OrderID);

}

function Disable() {
    $("#Code").attr("readonly", true);
    $("#Name").attr("readonly", true);
    $("#Contact").attr("readonly", true);
    $("#ContactOther").attr("readonly", true);
    $("#EmailAdd").attr("readonly", true);
    $("#WebAdd").attr("readonly", true);
    $("#Address").attr("readonly", true);
    $("#Description").attr("readonly", true);
}
function Enable() {
    $("#Code").attr("readonly", false);
    $("#Name").attr("readonly", false);
    $("#Contact").attr("readonly", false);
    $("#ContactOther").attr("readonly", false);
    $("#EmailAdd").attr("readonly", false);
    $("#WebAdd").attr("readonly", false);
    $("#Address").attr("readonly", false);
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
                url: `../Admin/DeleteCustomerGroup`,
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
        url: `../Admin/ChangeCustomerGroupStatus`,
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
//Customer Group Function
