function viewRecord(object, e) {
    console.log(object)
    Enable();
    $("#modal-add").modal('show');
    $("#EmployeeCode").val(object.EmployeeCode);
    $("#EmployeeName").val(object.EmployeeName);
    $("#FatherName").val(object.FatherName);
    $("#DepartmentID").val(object.DepartmentID);
    $("#CNIC").val(object.CNIC);
    $("#Description").val(object.Description);
    $("#CompanyID").val(object.CompanyID);
    $("#OwnGroupID").val(object.OwnGroupID);
    $("#BloodGroup").val(object.BloodGroup);
    $("#Gender").val(object.Gender);
    $("#Address").val(object.Address);
    $("#IdentityMark").val(object.IdentityMark);
    $("#DateOfBirth").val(object.DateOfBirth);
    $("#NICIssueDate").val(object.NICIssueDate);
    $("#NICExpiryDate").val(object.NICExpiryDate);
    $("#CNIC").val(object.CNIC);
    $("#ContactNo").val(object.ContactNo);
    $("#OtherContactNo").val(object.OtherContactNo);
    $("#HomeContactNo").val(object.HomeContactNo);
    $("#EmergencyContactNo").val(object.EmergencyContactNo);
    $("#ContactRelation").val(object.ContactRelation);
    $("#EmergencyContactName").val(object.EmergencyContactName);
    let OrderID = object.EmployeeID;
    $("#hfEditID").val(OrderID);
}
function viewRecordRead(object, e) {
    console.log(object)
    $("#modal-add").modal('show');
    Disable();
    $("#modal-add").modal('show');
    $("#EmployeeCode").val(object.EmployeeCode);
    $("#EmployeeName").val(object.EmployeeName);
    $("#FatherName").val(object.FatherName);
    $("#DepartmentID").val(object.DepartmentID);
    $("#CNIC").val(object.CNIC);
    $("#Description").val(object.Description);
    $("#CompanyID").val(object.CompanyID);
    $("#OwnGroupID").val(object.OwnGroupID);
    $("#BloodGroup").val(object.BloodGroup);
    $("#Gender").val(object.Gender);
    $("#Address").val(object.Address);
    $("#IdentityMark").val(object.IdentityMark);
    $("#DateOfBirth").val(object.DateOfBirth);
    $("#NICIssueDate").val(object.NICIssueDate);
    $("#NICExpiryDate").val(object.NICExpiryDate);
    $("#CNIC").val(object.CNIC);
    $("#ContactNo").val(object.ContactNo);
    $("#OtherContactNo").val(object.OtherContactNo);
    $("#HomeContactNo").val(object.HomeContactNo);
    $("#EmergencyContactNo").val(object.EmergencyContactNo);
    $("#ContactRelation").val(object.ContactRelation);
    $("#EmergencyContactName").val(object.EmergencyContactName);
    let OrderID = object.EmployeeID;
    $("#hfEditID").val(OrderID);
}

function Disable() {
    $("#EmployeeCode").attr("disabled", true);
    $("#EmployeeName").attr("disabled", true);
    $("#FatherName").attr("disabled", true);
    $("#CNIC").attr("disabled", true);
    $("#Description").attr("disabled", true);
    $("#CompanyID").attr("disabled", true);
    //$("#DesignationID").attr("disabled", true);
    $("#DepartmentID").attr("disabled", true);
    $("#OwnGroupID").attr("disabled", true);
    $("#BloodGroup").attr("disabled", true);
    $("#Gender").attr("disabled", true);
    $("#Address").attr("disabled", true);
    $("#IdentityMark").attr("disabled", true);
    $("#DateOfBirth").attr("disabled", true);
    $("#NICIssueDate").attr("disabled", true);
    $("#NICExpiryDate").attr("disabled", true);
    $("#ContactNo").attr("disabled", true);
    $("#OtherContactNo").attr("disabled", true);
    $("#HomeContactNo").attr("disabled", true);
    $("#EmergencyContactNo").attr("disabled", true);
    $("#ContactRelation").attr("disabled", true);
    $("#EmergencyContactName").attr("disabled", true);
    $("#UploadDocument").attr("disabled", true);
    $("#CNICImagePath").attr("disabled", true);
}

function Enable() {
    $("#EmployeeCode").attr("disabled", false);
    $("#EmployeeName").attr("disabled", false);
    $("#FatherName").attr("disabled", false);
    $("#CNIC").attr("disabled", false);
    $("#Description").attr("disabled", false);
    $("#CompanyID").attr("disabled", false);
    //$("#DesignationID").attr("disabled", true);
    $("#DepartmentID").attr("disabled", false);
    $("#OwnGroupID").attr("disabled", false);
    $("#BloodGroup").attr("disabled", false);
    $("#Gender").attr("disabled", false);
    $("#Address").attr("disabled", false);
    $("#IdentityMark").attr("disabled", false);
    $("#DateOfBirth").attr("disabled", false);
    $("#NICIssueDate").attr("disabled", false);
    $("#NICExpiryDate").attr("disabled", false);
    $("#ContactNo").attr("disabled", false);
    $("#OtherContactNo").attr("disabled", false);
    $("#HomeContactNo").attr("disabled", false);
    $("#EmergencyContactNo").attr("disabled", false);
    $("#ContactRelation").attr("disabled", false);
    $("#EmergencyContactName").attr("disabled", false);
    $("#UploadDocument").attr("disabled", false);
    $("#CNICImagePath").attr("disabled", false);
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
                url: `../Admin/DeleteEmployee`,
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

function GetDepartment(e) {
    $.get("../Admin/GetDepartment", { ID: $(e).val() }, function (data) {
        let currIndex = $(e).parent("td").parent("tr").closest("tr").index();
        let ddl = $(`#Add tbody tr:eq(${currIndex}) td:eq(1) select`);
        ddl.empty();
        $.each(data, function (index, row) {
            ddl.append("<option value='" + row.Value + "'>" + row.Text + "</option>");
        });
    });
}

//function GetOwnCompany(e) {
//    $.get("../Admin/GetOwnCompany", { ID: $(e).val() }, function (data) {
//        let currIndex = $(e).parent("td").parent("tr").closest("tr").index();
//        let ddl = $(`#Add tbody tr:eq(${currIndex}) td:eq(1) select`);
//        ddl.empty();
//        $.each(data, function (index, row) {
//            ddl.append("<option value='" + row.Value + "'>" + row.Text + "</option>");
//        });
//    });
//}

function GetGroup(e) {
    let row = $(e).parent("td").parent("tr");
    $.post(`../Admin/GetOwnGroup?id=${$(e).val()}`).done(function (response) {
        row.find("td:eq(1) input").val(response.Group);
    });
}

function ChangeStatus(Id, status) {
    $.ajax({
        url: `../Admin/ChangeEmployeeStatus`,
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