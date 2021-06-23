    function viewRecord(object, e) {
        console.log(object)
        $("#modal-add").modal('show');
        Enable();
        debugger;
        $("#TaxRateName").val(object.TaxRateName);
        $("#ProvinceID").val(object.ProvinceID);
        $("#TaxRatePercent").val(object.TaxRatePercent);
        let OrderID = object.ID;
        $("#hfEditID").val(OrderID);

    }

    function viewRecordRead(object, e) {
        console.log(object)
        $("#modal-add").modal('show');
        Disable();
        $("#TaxRateName").val(object.TaxRateName);
        $("#ProvinceID").val(object.ProvinceID);
        $("#TaxRatePercent").val(object.TaxRatePercent);
        let OrderID = object.DepartID;
        $("#hfEditID").val(OrderID);
    }

    function Disable() {
        $("#TaxRateName").attr("disabled", true);
        $("#ProvinceID").attr("disabled", true);
        $("#TaxRatePercent").attr("disabled", true);
    }
    function Enable() {
        $("#TaxRateName").attr("disabled", false);
        $("#ProvinceID").attr("disabled", false);
        $("#TaxRatePercent").attr("disabled", false);
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
                    url: `../Admin/DeleteGeneralTaxes`,
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

    function ChangeStatus(Id,status) {
        $.ajax({
            url: `../Admin/ChangeGeneralTaxStatus`,
            type: 'POST',
            data: { ID: Id , status : status },
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