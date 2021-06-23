function viewRecord(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Enable();
    $("#Code").val(object.Code);
    $("#RegNo").val(object.RegNo);
    $("#BodyType").val(object.BodyType);
    $("#ChasisNo").val(object.ChasisNo);
    $("#DimensionUnitType").val(object.DimensionUnitType);
    $("#Documents").show();
    $("#Documents").html(object.Documents + " | <i class='fa fa-download'></i>");
    $("#EngineNo").val(object.EngineNo);
    if (object.IsOwnVehicle) {
        $("#IsOwnVehicle").prop("checked", true);
    }
    else {
        $("#IsOwnVehicle").prop("checked", false);
    }
    $("#LoadingLimitNHA").val(object.LoadingLimitNHA);
    $("#Make").val(object.Make);
    $("#ManufacturerName").val(object.ManufacturerName);
    $("#ManufacturingYear").val(object.ManufacturingYear);
    $("#MaximumLoadingCapacity").val(object.MaximumLoadingCapacity);
    $("#OwnerContact").val(object.OwnerContact);
    $("#OwnerName").val(object.OwnerName);
    $("#OwnerNIC").val(object.OwnerNIC);
    $("#PurchaseFromDetail").val(object.PurchaseFromDetail);
    $("#PurchaseFromName").val(object.PurchaseFromName);
    $("#PurchasingAmount").val(object.PurchasingAmount);
    $("#PurchasingDate").val(object.PurchasingDate);
    $("#VehicleColor").val(object.VehicleColor);
    $("#VehicleModel").val(object.VehicleModel);
    $("#VehicleTypeID").val(object.VehicleTypeID);
    $("#StandardVehicleID").val(object.StandardVehicleID);
    $("#Width").val(object.Width);
    $("#Length").val(object.Length);
    $("#Weight").val(object.Weight);
    $("#Description").val(object.Description);
    $("#PrincipleCompanyId").val(object.PrincipleCompanyId);
    $("#Comission").val(object.Comission);
    $("#Amount").val(object.Amount);
    $("#Amount2").val(object.Amount2);
    let OrderID = object.VehicleID;
    $("#hfEditID").val(OrderID);

}

function viewRecordRead(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Disable();
    $("#Code").val(object.Code);
    $("#RegNo").val(object.RegNo);
    $("#BodyType").val(object.BodyType);
    $("#ChasisNo").val(object.ChasisNo);
    $("#DimensionUnitType").val(object.DimensionUnitType);
    $("#Documents").show();
    $("#Documents").html(object.Documents + " | <i class='fa fa-download'></i>");
    $("#EngineNo").val(object.EngineNo);
    if (object.IsOwnVehicle) {
        $("#IsOwnVehicle").prop("checked", true);
    }
    else {
        $("#IsOwnVehicle").prop("checked", false);
    }
    $("#LoadingLimitNHA").val(object.LoadingLimitNHA);
    $("#Make").val(object.Make);
    $("#ManufacturerName").val(object.ManufacturerName);
    $("#ManufacturingYear").val(object.ManufacturingYear);
    $("#MaximumLoadingCapacity").val(object.MaximumLoadingCapacity);
    $("#OwnerContact").val(object.OwnerContact);
    $("#OwnerName").val(object.OwnerName);
    $("#OwnerNIC").val(object.OwnerNIC);
    $("#PurchaseFromDetail").val(object.PurchaseFromDetail);
    $("#PurchaseFromName").val(object.PurchaseFromName);
    $("#PurchasingAmount").val(object.PurchasingAmount);
    $("#PurchasingDate").val(object.PurchasingDate);
    $("#VehicleColor").val(object.VehicleColor);
    $("#VehicleModel").val(object.VehicleModel);
    $("#VehicleTypeID").val(object.VehicleTypeID);
    $("#StandardVehicleID").val(object.StandardVehicleID);
    $("#Width").val(object.Width);
    $("#Length").val(object.Length);
    $("#Weight").val(object.Weight);
    $("#Description").val(object.Description);
    $("#PrincipleCompanyId").val(object.PrincipleCompanyId);
    $("#Comission").val(object.Comission);
    $("#Amount").val(object.Amount);
    $("#Amount2").val(object.Amount2);
    let OrderID = object.VehicleID;
    $("#hfEditID").val(OrderID);

}

function Disable() {
    $("#LoadingLimitNHA").attr("readonly", true);
    $("#Make").attr("readonly", true);
    $("#ManufacturerName").attr("readonly", true);
    $("#ManufacturingYear").attr("readonly", true);
    $("#MaximumLoadingCapacity").attr("readonly", true);
    $("#OwnerContact").attr("readonly", true);
    $("#OwnerName").attr("readonly", true);
    $("#OwnerNIC").attr("readonly", true);
    $("#PurchaseFromDetail").attr("readonly", true);
    $("#PurchaseFromName").attr("readonly", true);
    $("#PurchasingAmount").attr("readonly", true);
    $("#PurchasingDate").attr("readonly", true);
    $("#VehicleColor").attr("readonly", true);
    $("#VehicleModel").attr("readonly", true);
    $("#VehicleTypeID").attr("readonly", true);
    $("#StandardVehicleID").attr("readonly", true);
    $("#Width").attr("readonly", true);
    $("#Length").attr("readonly", true);
    $("#Weight").attr("readonly", true);
    $("#Description").attr("readonly", true);
    $("#IsOwnVehicle").attr("readonly", true);
    $("#IsOwnVehicle").attr("readonly", true);
    $("#Code").attr("readonly", true);
    $("#RegNo").attr("readonly", true);
    $("#BodyType").attr("readonly", true);
    $("#ChasisNo").attr("readonly", true);
    $("#DimensionUnitType").attr("readonly", true);
    $("#Documents").attr("readonly", true);
    $("#Documents").attr("readonly", true);
    $("#EngineNo").attr("readonly", true);
    $("#Comission").attr("readonly", true);
    $("#Amount").attr("readonly", true);
    $("#Amount2").attr("readonly", true);
    $("#PrincipleCompanyId").attr("disabled", true);
}

function Enable() {
    $("#LoadingLimitNHA").attr("readonly", false);
    $("#Make").attr("readonly", false);
    $("#ManufacturerName").attr("readonly", false);
    $("#ManufacturingYear").attr("readonly", false);
    $("#MaximumLoadingCapacity").attr("readonly", false);
    $("#OwnerContact").attr("readonly", false);
    $("#OwnerName").attr("readonly", false);
    $("#OwnerNIC").attr("readonly", false);
    $("#PurchaseFromDetail").attr("readonly", false);
    $("#PurchaseFromName").attr("readonly", false);
    $("#PurchasingAmount").attr("readonly", false);
    $("#PurchasingDate").attr("readonly", false);
    $("#VehicleColor").attr("readonly", false);
    $("#VehicleModel").attr("readonly", false);
    $("#VehicleTypeID").attr("readonly", false);
    $("#StandardVehicleID").attr("readonly", false);
    $("#Width").attr("readonly", false);
    $("#Length").attr("readonly", false);
    $("#Weight").attr("readonly", false);
    $("#Description").attr("readonly", false);
    $("#IsOwnVehicle").attr("readonly", false);
    $("#IsOwnVehicle").attr("readonly", false);
    $("#Code").attr("readonly", false);
    $("#RegNo").attr("readonly", false);
    $("#BodyType").attr("readonly", false);
    $("#ChasisNo").attr("readonly", false);
    $("#DimensionUnitType").attr("readonly", false);
    $("#Documents").attr("readonly", false);
    $("#Documents").attr("readonly", false);
    $("#EngineNo").attr("readonly", false);
    $("#Comission").attr("readonly", false);
    $("#Amount").attr("readonly", false);
    $("#Amount2").attr("readonly", false);
    $("#PrincipleCompanyId").attr("disabled", false);
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
                url: `../Admin/DeleteVehicle`,
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
        url: `../Admin/ChangeVehicleStatus`,
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