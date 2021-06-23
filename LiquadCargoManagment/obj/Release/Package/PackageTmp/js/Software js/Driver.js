function viewRecord(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Enable();
    $("#Code").val(object.Code);
    $("#Name").val(object.Name);
    $("#FatherName").val(object.FatherName);
    $("#Type").val(object.Type);
    $("#BloodGroup").val(object.BloodGroup);
    $("#DateOfBirth").val(object.DateOfBirth);
    $("#Gender").val(object.Gender);
    $("#CellNo").val(object.CellNo);
    $("#OtherContact").val(object.OtherContact);
    $("#HomeNo").val(object.HomeNo);
    $("#Address").val(object.Address);
    $("#NIC").val(object.NIC);
    $("#IdentityMark").val(object.IdentityMark);
    $("#NICIssueDate").val(object.NICIssueDate);
    $("#NICExpiryDate").val(object.NICExpiryDate);
    $("#LicenseNo").val(object.LicenseNo);
    $("#LicenseCategory").val(object.LicenseCategory);
    $("#LicenseIssueDate").val(object.LicenseIssueDate);
    $("#LicenseExpiryDate").val(object.LicenseExpiryDate);
    $("#LicenseIssuingAuthority").val(object.LicenseIssuingAuthority);
    $("#LicenseStatus").val(object.LicenseStatus);
    $("#EmergencyContactName").val(object.EmergencyContactName);
    $("#EmergencyContactNo").val(object.EmergencyContactNo);
    $("#ContactRelation").val(object.ContactRelation);
    $("#HomeNo").val(object.HomeNo);
    $("#Description").val(object.Description);
    $("#Documents").show();
    $("#Documents").html(object.Documents + " | <i class='fa fa-download'></i>");
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);
}

function viewRecordRead(object, e) {
    console.log(object);
    $("#modal-add").modal('show');
    Disable();
    $("#Code").val(object.Code);
    $("#Name").val(object.Name);
    $("#FatherName").val(object.FatherName);
    $("#Type").val(object.Type);
    $("#BloodGroup").val(object.BloodGroup);
    $("#DateOfBirth").val(object.DateOfBirth);
    $("#Gender").val(object.Gender);
    $("#CellNo").val(object.CellNo);
    $("#OtherContact").val(object.OtherContact);
    $("#HomeNo").val(object.HomeNo);
    $("#Address").val(object.Address);
    $("#NIC").val(object.NIC);
    $("#IdentityMark").val(object.IdentityMark);
    $("#NICIssueDate").val(object.NICIssueDate);
    $("#NICExpiryDate").val(object.NICExpiryDate);
    $("#LicenseNo").val(object.LicenseNo);
    $("#LicenseCategory").val(object.LicenseCategory);
    $("#LicenseIssueDate").val(object.LicenseIssueDate);
    $("#LicenseExpiryDate").val(object.LicenseExpiryDate);
    $("#LicenseIssuingAuthority").val(object.LicenseIssuingAuthority);
    $("#LicenseStatus").val(object.LicenseStatus);
    $("#EmergencyContactName").val(object.EmergencyContactName);
    $("#EmergencyContactNo").val(object.EmergencyContactNo);
    $("#ContactRelation").val(object.ContactRelation);
    $("#Description").val(object.Description);
    $("#HomeNo").val(object.HomeNo);
    $("#Documents").show();
    $("#Documents").html(object.Documents + " | <i class='fa fa-download'></i>");
    let OrderID = object.ID;
    $("#hfEditID").val(OrderID);
}

function Disable() {
    $("#Code").attr("readonly", true);
    $("#Name").attr("readonly", true);
    $("#FatherName").attr("readonly", true);
    $("#Type").attr("readonly", true);
    $("#BloodGroup").attr("readonly", true);
    $("#DateOfBirth").attr("readonly", true);
    $("#Gender").attr("readonly", true);
    $("#CellNo").attr("readonly", true);
    $("#OtherContact").attr("readonly", true);
    $("#HomeNo").attr("readonly", true);
    $("#Address").attr("readonly", true);
    $("#NIC").attr("readonly", true);
    $("#IdentityMark").attr("readonly", true);
    $("#NICIssueDate").attr("readonly", true);
    $("#NICExpiryDate").attr("readonly", true);
    $("#LicenseNo").attr("readonly", true);
    $("#LicenseCategory").attr("readonly", true);
    $("#LicenseIssueDate").attr("readonly", true);
    $("#LicenseExpiryDate").attr("readonly", true);
    $("#LicenseIssuingAuthority").attr("readonly", true);
    $("#LicenseStatus").attr("readonly", true);
    $("#EmergencyContactName").attr("readonly", true);
    $("#EmergencyContactNo").attr("readonly", true);
    $("#ContactRelation").attr("readonly", true);
    $("#Description").attr("readonly", true);
    $("#Documents").attr("readonly", true);
    $("#Documents").attr("readonly", true);
    $("#Address").attr("readonly", true);
}

function Enable() {
    $("#Code").attr("readonly", false);
    $("#Name").attr("readonly", false);
    $("#FatherName").attr("readonly", false);
    $("#Type").attr("readonly", false);
    $("#BloodGroup").attr("readonly", false);
    $("#DateOfBirth").attr("readonly", false);
    $("#Gender").attr("readonly", false);
    $("#CellNo").attr("readonly", false);
    $("#OtherContact").attr("readonly", false);
    $("#HomeNo").attr("readonly", false);
    $("#Address").attr("readonly", false);
    $("#NIC").attr("readonly", false);
    $("#IdentityMark").attr("readonly", false);
    $("#NICIssueDate").attr("readonly", false);
    $("#NICExpiryDate").attr("readonly", false);
    $("#LicenseNo").attr("readonly", false);
    $("#LicenseCategory").attr("readonly", false);
    $("#LicenseIssueDate").attr("readonly", false);
    $("#LicenseExpiryDate").attr("readonly", false);
    $("#LicenseIssuingAuthority").attr("readonly", false);
    $("#LicenseStatus").attr("readonly", false);
    $("#EmergencyContactName").attr("readonly", false);
    $("#EmergencyContactNo").attr("readonly", false);
    $("#ContactRelation").attr("readonly", false);
    $("#Description").attr("readonly", false);
    $("#Documents").attr("readonly", false);
    $("#Documents").attr("readonly", false);
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
                url: `../Admin/DeleteDriver`,
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
        url: `../Admin/ChangeDriverStatus`,
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