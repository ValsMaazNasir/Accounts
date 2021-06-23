//Own Group Department Function
function viewRecord(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Enable();
    debugger;
    $("#DCode").val(object.DepartCode);
    $("#DName").val(object.DepartName);
    $("#DContact").val(object.Contact);
    $("#CompanyID").val(object.OwnCompanyID);
    $("#DContactOther").val(object.ContactOther);
    $("#DEmailAdd").val(object.EmailAdd);
    $("#DWebAdd").val(object.WebAdd);
    $("#DAddress").val(object.Address);
    $("#DDescription").val(object.Description);
    let OrderID = object.DepartID;
    $("#hfEditID").val(OrderID);

}

function viewRecordRead(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Disable();
    $("#DCode").val(object.DepartCode);
    $("#DName").val(object.DepartName);
    $("#DContact").val(object.Contact);
    $("#CompanyID").val(object.OwnCompanyID);
    $("#DContactOther").val(object.ContactOther);
    $("#DEmailAdd").val(object.EmailAdd);
    $("#DWebAdd").val(object.WebAdd);
    $("#DAddress").val(object.Address);
    $("#DDescription").val(object.Description);
    let OrderID = object.DepartID;
    $("#hfEditID").val(OrderID);
}

function Disable() {
    $("#DCode").attr("readonly", true);
    $("#DName").attr("readonly", true);
    $("#DContact").attr("readonly", true);
    $("#DContactOther").attr("readonly", true);
    $("#CompanyID").attr("disabled", true);
    $("#DEmailAdd").attr("readonly", true);
    $("#DWebAdd").attr("readonly", true);
    $("#DAddress").attr("readonly", true);
    $("#DDescription").attr("readonly", true);
}
function Enable() {
    $("#DCode").attr("readonly", false);
    $("#DName").attr("readonly", false);
    $("#DContact").attr("readonly", false);
    $("#DContactOther").attr("readonly", false);
    $("#CompanyID").attr("disabled", false);
    $("#DEmailAdd").attr("readonly", false);
    $("#DWebAdd").attr("readonly", false);
    $("#DAddress").attr("readonly", false);
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
                url: `../Admin/DeleteOwnGroupDepartment`,
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
        url: `../Admin/ChangeDepartmentStatus`,
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
//Own Group Department Function