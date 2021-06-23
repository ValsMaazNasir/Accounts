function viewRecord(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Enable();
    debugger;
    $("#VehicleRegNo").val(object.VehicleRegNo);
    $("#VehicleType").val(object.VehicleType);
    $("#Ownership").val(object.OwnerShip);
    $("#PrincipleCompany").val(object.PrincipleCompany);
    $("#MaintenanceTypeID").val(object.MaintenanceTypeID);
    $("#txtStartFrom").val(object.StartFrom);
    $("#txtExpiryDate").val(object.ExpiryDate);
    $("#txtAlertType").val(object.AlertType);
    $("#txtSmsNo").val(object.SMSNo);
    $("#txtEmailAdd").val(object.EmailAdd);
    $("#txtAlertFrequency").val(object.AlertFrequency);
    $("#txtAlertBefore").val(object.AlertBefore);
    $("#GracePeriod").val(object.GracePeriod);
    $("#CurrentOdoMeter").val(object.CurrentOdoMeter);
    $("#DueKMs").val(object.DueKMs);
    $("#GraceDue").val(object.GracedueKMs);

    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);

}

function viewRecordRead(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Disable();
    $("#VehicleRegNo").val(object.VehicleRegNo);
    $("#VehicleType").val(object.VehicleType);
    $("#Ownership").val(object.OwnerShip);
    $("#PrincipleCompany").val(object.PrincipleCompany);
    $("#MaintenanceTypeID").val(object.MaintenanceTypeID);
    $("#txtStartFrom").val(object.StartFrom);
    $("#txtExpiryDate").val(object.ExpiryDate);
    $("#txtAlertType").val(object.AlertType);
    $("#txtSmsNo").val(object.SMSNo);
    $("#txtEmailAdd").val(object.EmailAdd);
    $("#txtAlertFrequency").val(object.AlertFrequency);
    $("#txtAlertBefore").val(object.AlertBefore);
    $("#GracePeriod").val(object.GracePeriod);
    $("#CurrentOdoMeter").val(object.CurrentOdoMeter);
    $("#DueKMs").val(object.DueKMs);
    $("#GraceDue").val(object.GracedueKMs);

    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);

}

function Disable() {
    $("#VehicleRegNo").attr("disabled", true);
    $("#VehicleType").attr("disabled", true);
    $("#Ownership").attr("disabled", true);
    $("#PrincipleCompany").attr("disabled", true);
    $("#MaintenanceTypeID").attr("disabled", true);
    $("#txtStartFrom").attr("disabled", true);
    $("#txtExpiryDate").attr("disabled", true);
    $("#txtAlertType").attr("disabled", true);
    $("#txtSmsNo").attr("disabled", true);
    $("#txtEmailAdd").attr("disabled", true);
    $("#txtAlertFrequency").attr("disabled", true);
    $("#txtAlertBefore").attr("disabled", true);
    $("#GracePeriod").attr("disabled", true);
    $("#CurrentOdoMeter").attr("disabled", true);
    $("#DueKMs").attr("disabled", true);
    $("#GraceDue").attr("disabled", true);

}
function Enable() {
    $("#VehicleRegNo").attr("disabled", false);
    $("#VehicleType").attr("disabled", false);
    $("#Ownership").attr("disabled", false);
    $("#PrincipleCompany").attr("disabled", false);
    $("#MaintenanceTypeID").attr("disabled", false);
    $("#txtStartFrom").attr("disabled", false);
    $("#txtExpiryDate").attr("disabled", false);
    $("#txtAlertType").attr("disabled", false);
    $("#txtSmsNo").attr("disabled", false);
    $("#txtEmailAdd").attr("disabled", false);
    $("#txtAlertFrequency").attr("disabled", false);
    $("#txtAlertBefore").attr("disabled", false);
    $("#GracePeriod").attr("disabled", false);
    $("#CurrentOdoMeter").attr("disabled", false);
    $("#DueKMs").attr("disabled", false);
    $("#GraceDue").attr("disabled", false);
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
                url: `../Admin/DeleteVehicleMaintenance`,
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
        url: `../Admin/ChangeVehicleMaintenanceStatus`,
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

function GetVehicleReg(e) {
    $.get("../Admin/GetVehicleRegistration", { ID: $(e).val() }, function (data) {
        let currIndex = $(e).parent("td").parent("tr").closest("tr").index();
        let ddl = $(`#Add tbody tr:eq(${currIndex}) td:eq(1) select`);
        ddl.empty();
        $.each(data, function (index, row) {
            ddl.append("<option value='" + row.Value + "'>" + row.Text + "</option>");
        });
    });
}