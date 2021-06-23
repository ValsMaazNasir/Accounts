function viewRecord(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Enable();
    $("#CityID").val(object.CityID);
    $("#ShipmentTypeID").val(object.ShipmentTypeID);
    $("#AreaID").val(object.AreaID);
    $("#LocationCode").val(object.LocationCode);
    $("#LocationName").val(object.LocationName);
    $("#LocationType").val(object.LocationType);
    $("#Address").val(object.Address);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);

}
function viewRecordRead(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Disable();
    $("#CityID").val(object.CityID);
    $("#ShipmentTypeID").val(object.ShipmentTypeID);
    $("#AreaID").val(object.AreaID);
    $("#LocationCode").val(object.LocationCode);
    $("#LocationName").val(object.LocationName);
    $("#LocationType").val(object.LocationType);
    $("#Address").val(object.Address);
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);
}
function Disable() {
    $("#CityID").attr("readonly", true);
    $("#AreaID").attr("readonly", true);
    $("#ShipmentTypeID").attr("readonly", true);
    $("#LocationCode").attr("readonly", true);
    $("#LocationName").attr("readonly", true);
    $("#LocationType").attr("readonly", true);
    $("#Address").attr("readonly", true);
}

function Enable() {
    $("#CityID").attr("readonly", false);
    $("#AreaID").attr("readonly", false);
    $("#ShipmentTypeID").attr("readonly", false);
    $("#LocationCode").attr("readonly", false);
    $("#LocationName").attr("readonly", false);
    $("#LocationType").attr("readonly", false);
    $("#Address").attr("readonly", false);
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
                url: `../Admin/DeletePickLocation`,
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

function GetLocation(e) {
    $.get("../Admin/GetLocation", { ID: $(e).val() }, function (data) {
        let currIndex = $(e).parent("td").parent("tr").closest("tr").index();
        let ddl = $(`#Add tbody tr:eq(${currIndex}) td:eq(1) select`);
        ddl.empty();
        $.each(data, function (index, row) {
            ddl.append("<option value='" + row.Value + "'>" + row.Text + "</option>");
        });
    });
}

function ChangeStatus(Id, status) {
    $.ajax({
        url: `../Admin/ChangePickLocationStatus`,
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