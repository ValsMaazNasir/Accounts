function SaveDiesel() {
    let Order = {}
    let ID = $("#ID").val();
    let SerialNo = $("#SerialNo").val();
    let DieselDate = $("#DieselDate").val();
    let DieselRate = $("#DieselRate").val();
    let OilRate = $("#OilRate").val();
    let PetrolPump = $("#PetrolPump").val();
    if (DieselDate == "" || DieselRate == "") {
        Message("Required", "Enter all requried fields", "error");
        return;
    }
    Order.ID = ID;
    Order.SerialNo = SerialNo;
    Order.DieselDate = DieselDate;
    Order.DieselRate = DieselRate;
    Order.OilRate = OilRate;
    Order.PetrolPump = PetrolPump;
    Order.DieselTotalLiter = $("#Add").find("tfoot tr.grand_total td:eq(1)").text();
    Order.DieselTotalRate = $("#Add").find("tfoot tr.grand_total td:eq(2)").text();
    Order.DieselTotalAmount = $("#Add").find("tfoot tr.grand_total td:eq(3)").text();
    Order.OilTotalLiter = $("#Add").find("tfoot tr.grand_total td:eq(4)").text();
    Order.OilTotalRate = $("#Add").find("tfoot tr.grand_total td:eq(5)").text();
    Order.OilTotalAmount = $("#Add").find("tfoot tr.grand_total td:eq(6)").text();
    Order.TotalOtherAmount = $("#Add").find("tfoot tr.grand_total td:eq(7)").text();
    Order.TotalNetAmount = $("#Add").find("tfoot tr.grand_total td:eq(8)").text();
    Order.TotalCashReceived = $("#Add").find("tfoot tr.grand_total td:eq(9)").text();
    Order.TotalCashPaid = $("#Add").find("tfoot tr.grand_total td:eq(10)").text();

    var customers = new Array();
    $("#Add tbody tr").each(function () {
        var row = $(this);
        var customer = {};
        //customer.ID = row.find("td:eq(0) input").val();
        customer.SerialNo = row.find("td:eq(0) input").val();
        customer.RegNo = row.find("td:eq(1) select option:selected").val();
        customer.DieselLiter = row.find("td:eq(2) input").val();
        customer.DieselRate = row.find("td:eq(3) input").val();
        customer.DieselAmount = row.find("td:eq(4) input").val();
        customer.OilLiter = row.find("td:eq(5) input").val();
        customer.OilRate = row.find("td:eq(6) input").val();
        customer.OilAmount = row.find("td:eq(7) input").val();
        customer.OtherAmount = row.find("td:eq(8) input").val();
        customer.NetAmount = row.find("td:eq(9) input").val();
        customer.CashReceived = row.find("td:eq(10) input").val();
        customer.CashPaid = row.find("td:eq(11) input").val();
        customer.Remarks = row.find("td:eq(12) input").val();
        customers.push(customer);
    });


    $.ajax({
        url: "../Admin/addDieselManagement",
        type: "POST",
        data: { Order: Order, Details: customers },
        success: function (r) {
            debugger;
            Message("Success", r.Message, "success");
            window.location = "../Admin/DieselManagementList";
        }
    });
}

//Add Row
function AddRow(A, q, totalColumnIndex,) {
    $(".chosen").chosen('destroy');
    var parent = $(A).parent("td").parent("tr").html();
    parent = parent.replace("btn-primary", "btn-danger").replace("fa-plus", "fa-trash").replace("AddRow", "remove");
    $("#Add").find("tbody").append(`<tr>${parent}</tr>`);
    $(".chosen").chosen();
    GenerateSerial("Add");
    TotalThis();
    calculate();

    $(q).find("tfoot tr.grand_total").before(`<tr>${parent}</tr>`);
    GrandtotalDiesel(q, totalColumnIndex);
    Grandtotal(q, totalColumnIndex);
    calculate();
}

function remove(e) {
    $(e).parent("td").parent("tr").fadeOut(100, function () { $(this).remove(); });
}

function Remove(button) {
    var row = $(button).closest("tr");
    var name = $("tr", row).eq(0).html();
    if (confirm("Do you want to delete: " + name)) {
        var table = $("#Add")[0];
        table.deleteRow(row[0].rowIndex);
    }
};

function TotalThis() {
    let Rate = $("#DieselRate").val();
    $(".DieselRate").val(Rate);

    let ORate = $("#OilRate").val();
    $(".OilRate").val(ORate);
}

function GenerateSerial(tableId) {
    debugger;
    let rows = $(`#${tableId} tbody tr`);
    for (var i = 0; i < rows.length; i++) {
        rows.eq(i).find("td:eq(0)").html(i + 1);
    }
}

function GrandtotalDiesel(q, totalColumnIndex, type) {
    let grTotal = 0;
    for (var i = 0; i < $(q).find("tbody tr").length; i++) {

        grTotal += parseFloat($(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim() == "" ? 0 : $(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim());
    }
    if (type == "DieselLiter") {
        $(q).find("tr.grand_total td:eq(1)").text(grTotal);
    }
    //else if (type == "DieselRate") {
    //    $(q).find("tr.grand_total td:eq(2)").text(grTotal);
    //}
    else if (type == "TotalAmount") {
        $(q).find("tr.grand_total td:eq(3)").text(grTotal);
    }
    else if (type == "OilLiter") {
        $(q).find("tr.grand_total td:eq(4)").text(grTotal);
    }
    //else if (type == "OilRate") {
    //    $(q).find("tr.grand_total td:eq(5)").text(grTotal);
    //}
    else if (type == "TotalOilAmount") {
        $(q).find("tr.grand_total td:eq(6)").text(grTotal);
    }
    else if (type == "OtherAmount") {
        $(q).find("tr.grand_total td:eq(7)").text(grTotal);
    }
    else if (type == "NetAmount") {
        $(q).find("tr.grand_total td:eq(8)").text(grTotal);
    }
    else if (type == "CashReceived") {
        $(q).find("tr.grand_total td:eq(9)").text(grTotal);
    }
    else if (type == "CashPaid") {
        $(q).find("tr.grand_total td:eq(10)").text(grTotal);
    }
}