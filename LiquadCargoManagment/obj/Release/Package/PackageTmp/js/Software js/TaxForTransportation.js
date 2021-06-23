function viewRecord(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Enable();
    debugger;
    $("#Name").val(object.Name);
    $("#OriginProvince").val(object.OriginProvince);
    $("#TaxRatePercentOrigin").val(object.TaxRatePercentOrigin);
    $("#DestinationProvince").val(object.DestinationProvince);
    $("#TaxRatePercentDestination").val(object.TaxRatePercentDestination);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);

}

function viewRecordRead(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Disable();
    $("#Name").val(object.Name);
    $("#OriginProvince").val(object.OriginProvince);
    $("#TaxRatePercentOrigin").val(object.TaxRatePercentOrigin);
    $("#DestinationProvince").val(object.DestinationProvince);
    $("#TaxRatePercentDestination").val(object.TaxRatePercentDestination);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);
}

function Disable() {
    $("#Name").attr("readonly", true);
    $("#OriginProvince").attr("disabled", true);
    $("#DestinationProvince").attr("disabled", true);
    $("#TaxRatePercentOrigin").attr("disabled", true);
    $("#TaxRatePercentDestination").attr("disabled", true);
}
function Enable() {
    $("#Name").attr("readonly", false);
    $("#OriginProvince").attr("disabled", false);
    $("#DestinationProvince").attr("disabled", false);
    $("#TaxRatePercentOrigin").attr("disabled", false);
    $("#TaxRatePercentDestination").attr("disabled", false);
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
                url: `../Admin/DeleteTaxForTransportation`,
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
        url: `../Admin/ChangeTaxForTransportStatus`,
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