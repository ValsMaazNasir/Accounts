function viewRecord(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Enable();
    $("#VehicleRegNo").val(object.VehicleRegNo);
    $("#VehicleType").val(object.VehicleType);
    $("#VehicleOwnership").val(object.VehicleOwnership);
    $("#PrincipleCompany").val(object.PrincipleCompany);
    $("#SMSAlert").val(object.SMSAlert);
    $("#EmailAlert").val(object.EmailAlert);
    $("#VehicleDocumentType").val(object.VehicleDocumentType);
    $("#VehicleDocuments").val(object.VehicleDocuments);
    $("#RenewalDate").val(object.RenewalDate);
    $("#ExpiryDate").val(object.ExpiryDate);
    $("#AlertBefore").val(object.AlertBefore);
    $("#AlertFrequency").val(object.AlertFrequency);
    $("#Remarks").val(object.Remarks);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);
}

function viewRecordRead(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Disable();
    $("#VehicleRegNo").val(object.VehicleRegNo);
    $("#VehicleType").val(object.VehicleType);
    $("#VehicleOwnership").val(object.VehicleOwnership);
    $("#PrincipleCompany").val(object.PrincipleCompany);
    $("#SMSAlert").val(object.SMSAlert);
    $("#EmailAlert").val(object.EmailAlert);
    $("#VehicleDocumentType").val(object.VehicleDocumentType);
    $("#VehicleDocuments").val(object.VehicleDocuments);
    $("#RenewalDate").val(object.RenewalDate);
    $("#ExpiryDate").val(object.ExpiryDate);
    $("#AlertBefore").val(object.AlertBefore);
    $("#AlertFrequency").val(object.AlertFrequency);
    $("#Remarks").val(object.Remarks);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);
}

function Disable() {
    $("#VehicleRegNo").attr("disabled", true);
    $("#VehicleType").attr("disabled", true);
    $("#VehicleOwnership").attr("disabled", true);
    $("#PrincipleCompany").attr("disabled", true);
    $("#SMSAlert").attr("disabled", true);
    $("#EmailAlert").attr("disabled", true);
    $("#VehicleDocumentType").attr("disabled", true);
    $("#VehicleDocuments").attr("disabled", true);
    $("#RenewalDate").attr("disabled", true);
    $("#ExpiryDate").attr("disabled", true);
    $("#AlertType").attr("disabled", true);
    $("#AlertBefore").attr("disabled", true);
    $("#AlertFrequency").attr("disabled", true);
    $("#Remarks").attr("disabled", true);
    $("#file").attr("disabled", true);
}

function Enable() {
    $("#VehicleRegNo").attr("disabled", false);
    $("#VehicleType").attr("disabled", false);
    $("#VehicleOwnership").attr("disabled", false);
    $("#PrincipleCompany").attr("disabled", false);
    $("#SMSAlert").attr("disabled", false);
    $("#EmailAlert").attr("disabled", false);
    $("#VehicleDocumentType").attr("disabled", false);
    $("#VehicleDocuments").attr("disabled", false);
    $("#RenewalDate").attr("disabled", false);
    $("#ExpiryDate").attr("disabled", false);
    $("#AlertType").attr("disabled", false);
    $("#AlertBefore").attr("disabled", false);
    $("#AlertFrequency").attr("disabled", false);
    $("#Remarks").attr("disabled", false);
    $("#file").attr("disabled", false);
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
                url: `../Admin/DeleteVehicleDocuments`,
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
        url: `../Admin/ChangeVehicleDocumentsStatus`,
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
    $.get("../Admin/GetVehicleReg", { ID: $(e).val() }, function (data) {
        let currIndex = $(e).parent("td").parent("tr").closest("tr").index();
        let ddl = $(`#Add tbody tr:eq(${currIndex}) td:eq(1) select`);
        ddl.empty();
        $.each(data, function (index, row) {
            ddl.append("<option value='" + row.Value + "'>" + row.Text + "</option>");
        });
    });
}