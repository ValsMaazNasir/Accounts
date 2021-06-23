    function viewRecord(object, e) {
        $("#modal-add").modal('show');
        Enable();
        $("#ProvinceName").val(object.ProvinceName);
        let OrderID = object.ProvinceID;
        $("#hfEditID").val(OrderID);

    }
    function viewRecordRead(object, e) {
        $("#modal-add").modal('show');
        Disable();
        $("#ProvinceName").val(object.ProvinceName);
        let OrderID = object.ProvinceID;
        $("#hfEditID").val(OrderID);

    }
    function Disable() {
        $("#ProvinceName").attr("readonly",true);
    }
    function Enable() {
        $("#ProvinceName").attr("readonly",false);
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
                    url: `../Admin/DeleteProvince`,
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