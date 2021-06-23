function viewRecord(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Enable();
    $("#Code").val(object.Code);
    $("#Name").val(object.Name);
    $("#AccountTitle").val(object.AccountTitle);
    $("#AccountNo").val(object.AccountNo);
    $("#OwnerContactName").val(object.OwName);
    $("#OwnerContactNo").val(object.OwnerContactNo);
    $("#PrimaryContactPerson").val(object.PrimaryContactPerson);
    $("#PrimaryContactNo").val(object.PrimaryContactNo);
    $("#SecContactPerson").val(object.SecContactPerson);
    $("#SecContactNo").val(object.SecContactNo);
    $("#PTCLNo").val(object.PTCLNo);
    $("#Website").val(object.Website);
    $("#Email").val(object.Email);
    $("#Address").val(object.Address);
    $("#Description").val(object.Description);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);

}

function viewRecordRead(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Disable();
    $("#Code").val(object.Code);
    $("#Name").val(object.Name);
    $("#AccountTitle").val(object.AccountTitle);
    $("#AccountNo").val(object.AccountNo);
    $("#OwnerContactName").val(object.OwName);
    $("#OwnerContactNo").val(object.OwnerContactNo);
    $("#PrimaryContactPerson").val(object.PrimaryContactPerson);
    $("#PrimaryContactNo").val(object.PrimaryContactNo);
    $("#SecContactPerson").val(object.SecContactPerson);
    $("#SecContactNo").val(object.SecContactNo);
    $("#PTCLNo").val(object.PTCLNo);
    $("#Website").val(object.Website);
    $("#Email").val(object.Email);
    $("#Address").val(object.Address);
    $("#Description").val(object.Description);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);
}
function Disable() {
    $("#Code").attr("readonly", true);
    $("#Name").attr("readonly", true);
    $("#AccountTitle").attr("readonly", true);
    $("#AccountNo").attr("readonly", true);
    $("#OwnerContactName").attr("readonly", true);
    $("#OwnerContactNo").attr("readonly", true);
    $("#PrimaryContactPerson").attr("readonly", true);
    $("#PrimaryContactNo").attr("readonly", true);
    $("#SecContactPerson").attr("readonly", true);
    $("#SecContactNo").attr("readonly", true);
    $("#PTCLNo").attr("readonly", true);
    $("#Website").attr("readonly", true);
    $("#Email").attr("readonly", true);
    $("#Address").attr("readonly", true);
    $("#Description").attr("readonly", true);
}
function Enable() {
    $("#Code").attr("readonly", false);
    $("#Name").attr("readonly", false);
    $("#AccountTitle").attr("readonly", false);
    $("#AccountNo").attr("readonly", false);
    $("#OwnerContactName").attr("readonly", false);
    $("#OwnerContactNo").attr("readonly", false);
    $("#PrimaryContactPerson").attr("readonly", false);
    $("#PrimaryContactNo").attr("readonly", false);
    $("#SecContactPerson").attr("readonly", false);
    $("#SecContactNo").attr("readonly", false);
    $("#PTCLNo").attr("readonly", false);
    $("#Website").attr("readonly", false);
    $("#Email").attr("readonly", false);
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
                url: `../Admin/DeleteFillingStation`,
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