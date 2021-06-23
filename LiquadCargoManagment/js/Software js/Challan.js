
function GetOrders() {
    $("#example").DataTable({
        "lengthMenu": [[50, 100, 150, 200 - 1], [50, 100, 150, 200, "All"]]
    }).order([1, 'des'])
    $('#example').DataTable({
        "lengthMenu": [[100, 200, 300, 400 - 1], [100, 200, 300, 400, "All"]],
        dom: 'Bfrtip',
        buttons: [
            //'copyHtml5',
            {
                extend: 'copyHtml5',
                text: '<i class="fa fa-copy"></i> | COPY',
                titleAttr: 'COPY'
            },
            //'excelHtml5',
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-o"></i> | EXCEL',
                titleAttr: 'Excel'
            },
            //'csvHtml5',
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-o"></i> | CSV',
                titleAttr: 'CSV'
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o"></i> | PDF',
                titleAttr: 'PDF'
            },
            'pageLength'
        ]
    }).order([2, 'des'])
        .draw();
}

function GetPickLocation(e) {
    let row = $(e).parent("td").parent("tr");
    $.post(`../Admin/GetPickLocationChallan?id=${$(e).val()}`).done(function (response) {
        row.find("td:eq(1) input").val(response.City);
        row.find("td:eq(2) input").val(response.Area);
        row.find("td:eq(3) input").val(response.Address);
    });
}
function GetDropLocation(e) {
    let row = $(e).parent("td").parent("tr");
    $.post(`../Admin/GetDropLocationChallan?id=${$(e).val()}`).done(function (response) {
        row.find("td:eq(1) input").val(response.City);
        row.find("td:eq(2) input").val(response.Area);
        row.find("td:eq(3) input").val(response.Address);
    });
}
//function GetDropLocation(e) {
//    let row = $(e).parent("td").parent("tr");
//    $.post(`../Admin/GetPickLocationChallan?id=${$(e).val()}`).done(function (response) {
//        row.find("td:eq(1) input").val(response.City);
//        row.find("td:eq(2) input").val(response.Area);
//        row.find("td:eq(3) input").val(response.Address);
//    });
//}
function GetVehicle(e) {
    let row = $(e).parent("td").parent("tr");
    $.post(`../Admin/GetVehicleChallan?id=${$(e).val()}`).done(function (response) {
        row.find("td:eq(1) input").val(response.Reg);
        row.find("td:eq(2) input").val(response.Type);
    });
}
function GetVendor(e) {
    let row = $(e).parent("td").parent("tr");
    $.post(`../Admin/GetVendorChallan?id=${$(e).val()}`).done(function (response) {
        row.find("td:eq(1) input").val(response.Contact);
    });
}
function GetDriver(e) {
    let row = $(e).parent("td").parent("tr");
    $.post(`../Admin/GetDriverChallan?id=${$(e).val()}`).done(function (response) {
        row.find("td:eq(4) input").val(response.Contact);
    });
}

function ChangeStatus(Id, status) {
    $.ajax({
        url: `../Admin/ChangeChallanStatus`,
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
                url: `../Admin/DeleteCompactChallan`,
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

function SaveData() {
    debugger;
    let Order = {}
    let ID = $("#hfEditID").val();
    let ChallanDate = $("#ChallanDate").val();
    let ChallanManualNo = $("#ChallanManualNo").val();
    let PickLocationID = $("#PickLocationID").val();
    let DropLocationID = $("#DropLocationID").val();
    let VehicleID = $("#VehicleID").val();
    let DriverID = $("#DriverID").val();
    let VendorID = $("#VendorID").val();
    let ExternalVehicleNo = $("#ExternalVehicleNo").val();


    if (ChallanManualNo == "" || ChallanDate == "" || PickLocationID == "" || DropLocationID == "" || VehicleID == "") {
        Message("Required", "Enter all required fields", "error");
        return;
    }

    Order.ID = ID;
    Order.ChallanDate = ChallanDate;
    Order.ChallanManualNo = ChallanManualNo;
    Order.PickLocationID = PickLocationID;
    Order.DropLocationID = DropLocationID;
    Order.VehicleID = VehicleID;
    Order.DriverID = DriverID;
    Order.VendorID = VendorID;
    Order.ExternalVehicleNo = ExternalVehicleNo;

    //var Customers = new Array();
    //$("#example tbody tr").each(function () {
    //    var row = $(this);
    //    var customer = {};
    //    if (row.find("td:eq(0) input").is(":checked")) {
    //        customer.CompactBiltyID = row.find("td:eq(0) input[type='checkbox']").val();
    //        Customers.push(customer);
    //    }
    //});

    var Customers = new Array();
    $("#ChallanModal tbody tr").each(function () {
        var row = $(this);
        var customer = {};
        customer.CompactBiltyID = row.find("td:eq(0) input").val();
        Customers.push(customer);
        //if (customer != null) {
        //    if (row.find("td:eq(0) input").is(":checked")) {
        //        customer.CompactBiltyID = row.find("td:eq(0) input[type='checkbox']").val();
        //        Customers.push(customer);
        //    }
        //}
        //else {

        //}

    });

    if (Customers != "") {
        $.ajax({
            url: "../Admin/SaveChallanCompact",
            type: "POST",
            data: { Order: Order, check: Customers },
            success: function (r) {
                debugger;
                if (status != "OK") {
                    Message("Error", "An Error Occured", "error");
                }
                Message("Success", "Compact Challan has been created", "success");
                window.location = "../Admin/ChallanList";
            },
            error: function () {
                Message("Error", "An Error Occured", "error");
            }
        });
    }
    else {
        Message("Required", "Please select bilty !", "info");
    }
}

function UpdateData() {
    debugger;
    let Order = {}
    let ID = $("#hfEditID").val();
    //let ChallanNo = $("#ChallanNo").val();
    let ChallanDate = $("#ChallanDate").val();
    let ChallanManualNo = $("#ChallanManualNo").val();
    let PickLocationID = $("#PickLocationID").val();
    let DropLocationID = $("#DropLocationID").val();
    let VehicleID = $("#VehicleID").val();
    let DriverID = $("#DriverID").val();
    let VendorID = $("#VendorID").val();
    let ExternalVehicleNo = $("#ExternalVehicleNo").val();


    if (ChallanManualNo == "" || ChallanDate == "" || PickLocationID == "" || DropLocationID == "" || VehicleID == "") {
        Message("Required", "Enter all required fields", "error");
        return;
    }

    Order.ID = ID;
    Order.ChallanDate = ChallanDate;
    Order.ChallanManualNo = ChallanManualNo;
    Order.PickLocationID = PickLocationID;
    Order.DropLocationID = DropLocationID;
    Order.VehicleID = VehicleID;
    Order.DriverID = DriverID;
    Order.VendorID = VendorID;
    Order.ExternalVehicleNo = ExternalVehicleNo;

    var Customers = new Array();
    $("#ChallanModal tbody tr.CheckBilty").each(function () {
        var row = $(this);
        var customer = {};
        //customer.ID = row.find("td:eq(0) input").val();
        customer.CompactBiltyID = row.find("td:eq(0) input").val();
        Customers.push(customer);
        //if (row.find("td:eq(0) input").is(":checked")) {
        //    customer.ID = row.find("td:eq(0) input").val();
        //    customer.CompactBiltyID = row.find("td:eq(0) input[type='checkbox']").val();
        //    Customers.push(customer);
        //}
    });

    $.ajax({
        url: "../Admin/SaveChallanCompact",
        type: "POST",
        data: { Order: Order, check: Customers },
        success: function (r) {
            debugger;
            Message("Success", "Challan has been created", "success");
            window.location = "../Admin/ChallanList";
        }
    });
}

function EditChallan(Id) {
    debugger;
    $.ajax({
        url: `../Admin/EditChallan`,
        type: 'GET',
        data: { ID: Id },
        success: function (res) {
            console.log(res);
            $("#hfEditID").val(Id);
            //$("#ChallanNo").val(res.challan.ChallanNo);
            $("#ChallanDate").val(res.challan.ChallanDate);
            $("#ChallanManualNo").val(res.challan.ChallanManualNo);
            $("#PickLocationID").chosen('destroy');
            $("#DropLocationID").chosen('destroy');
            $("#VehicleID").chosen('destroy');
            $("#DriverID").chosen('destroy');
            $("#VendorID").chosen('destroy');

            $("#PickLocationID").val(res.challan.PickLocationID);
            $("#PickCity").val(res.challan.City);
            $("#PickArea").val(res.challan.Area);
            $("#PickAddress").val(res.challan.Address);
            $("#DropLocationID").val(res.challan.DropLocationID);
            $("#DropCity").val(res.challan.DropCity);
            $("#DropArea").val(res.challan.DropArea);
            $("#DropAddress").val(res.challan.DropAddress);
            $("#VehicleID").val(res.challan.VehicleID);
            $("#DriverID").val(res.challan.DriverID);
            $("#VendorID").val(res.challan.VendorID);
            $("#BrokerContact").val(res.challan.BrokerContact);
            $("#RegNo").val(res.challan.Reg);
            $("#VehicleType").val(res.challan.VehicleType);
            $("#DriverContact").val(res.challan.DriverContact);

            $("#ExternalVehicleNo").val(res.challan.ExternalVehicleNo);
            console.log(res.bilty);
            for (var i = 0; i < res.bilty.length; i++) {
                console.log(res.bilty[i]);
                var row = `<tr class='CheckBilty'><td hidden><input type='hidden' value='${res.bilty[i].ID}' name='ID'></td><td>${res.bilty[i].OrderNo}</td>
    <td>${res.bilty[i].OrderDate}</td><td>${res.bilty[i].BillTo}</td>
    <td>${res.bilty[i].CustomerType}</td><td>${res.bilty[i].Quantity}</td>
    <td>${res.bilty[i].Weight}</td><td>${res.bilty[i].TotalFreight}</td>
    <td><button type='button' class='btn btn-danger btn-xs' onclick="remove(this)"><i class='fa fa-trash'></i></button></td>
    <tr>`;
                $("#ChallanModal tbody").append(row);
            }
            $('#modal-add').modal('show');
        }
    });
    $("#ChallanModal tbody").empty();

}