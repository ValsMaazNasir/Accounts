//function fnSuccess(response) {
//    if (response.Status != "OK") {
//        Message("Required", response.Message, "error");
//    }
//    else {
//        $('#modal-add').modal('hide');
//        Message("Success", "Record Saved Success", "success");
//        $("#grid").html(response.html);
//        ClearItem();
//        GetOrders();
//    }
//}
//function fnFailed(error) {
//    window.location.reload();
//}

function Space() {
    $("#Code,#CityCode,#LocationCode,#DCode,#SCode,#ExpensesTypeCode,#EmployeeCode,#PackageTypeCode").on("keydown", function (e) {
        return e.which !== 32;
    });
}


