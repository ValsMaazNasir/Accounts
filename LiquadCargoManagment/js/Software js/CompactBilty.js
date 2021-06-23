function GetUnitAuto(e) {
    let row = $(e).parent("td").parent("tr");
    $.post(`../Admin/GetUnit?id=${$(e).val()}`).done(function (response) {
        row.find("td:eq(4) input").val(response.unit);
    });
}

//Pick Location
function fnSuccessLocate(response) {
    if (response.Status != "OK") {
        Message("Required", response.Message, "error");
    }
    else {
        $('#modal-add').modal('hide');
        Message("Success", "Record Saved Success", "success");
        ClearItem();
        GetOrders();
        debugger;
        $("#ddlReciever").html('');
        for (var i = 0; i < response.html.length; i++) {
            $("#ddlReciever").append(`<option value="${response.html[i].ID}">${response.html[i].LocationName}</option>`);
        }
    }
}

function GetLocation(e) {
    $.get("../Admin/GetLocation", { ID: $(e).val() }, function (data) {
        let currIndex = $(e).parent("td").parent("tr").closest("tr").index();
        let ddl = $(`#Locate tbody tr:eq(${currIndex}) td:eq(1) select`);
        ddl.empty();
        $.each(data, function (index, row) {
            ddl.append("<option value='" + row.Value + "'>" + row.Text + "</option>");
        });
    });
}

function fnFailedLocate(error) {
    window.location.reload();
}

//Product Broker
function ProductBrokerSuccess(response) {
    if (response.Status != "OK") {
        Message("Required", response.Message, "error");
    }
    else {
        $('#modal-addProBroker').modal('hide');
        Message("Success", "Record Saved Success", "success");
        $("#ddlProductBroker").html('');
        for (var i = 0; i < response.html.length; i++) {
            $("#ddlProductBroker").append(`<option value="${response.html[i].Id}">${response.html[i].Name}</option>`);
            $('#modal-add').modal('hide');
        }
        ClearItem();
        GetOrders();
    }
}

function ProductBrokerFailed(error) {
    window.location.reload();
}

function SuccessProduct(response) {
    if (response.Status != "OK") {
        Message("Required", response.Message, "error");
    }
    else {
        $('#modal-addPro').modal('hide');
        Message("Success", "Record Saved Success", "success");
        $("#Product").html('');
        for (var i = 0; i < response.html.length; i++) {
            $("#Product").append(`<option value="${response.html[i].ID}">${response.html[i].Name}</option>`);
            $('#modal-addPro').modal('hide');
        }
        ClearItem();
        GetOrders();
    }
}

function ProductBrokerFailed(error) {
    window.location.reload();
}



//Sweet Alert
function Message(title, text, type) {
    Swal.fire({
        title: title + '!',
        text: text,
        icon: type,
        confirmButtonText: 'OK'
    })
}

function GetOrders() {
    $('#example').DataTable().column(1)
        .data()
        .sort();;
}

function ClearItem() {
    $("input[type='text']").val('');
    $("input[type='hidden']").val('');
}

//Add Row
function AddRow(A) {
    debugger;
    $(".ddlchosen").chosen('destroy');
    $(".select-chosen").chosen('destroy');
    var parent = $(A).parent("td").parent("tr").html();
    parent = parent.replace("btn-primary", "btn-danger").replace("fa-plus", "fa-trash").replace("AddRow", "remove");
    $("#Add").find("tbody").append(`<tr>${parent}</tr>`);
    $(".ddlchosen").chosen();
    $(".select-chosen").chosen();
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

//Cascade Dropdown
function getProducts(e) {
    $.get("../Admin/getProductList", { ID: $(e).val() }, function (data) {
        let currIndex = $(e).parent("td").parent("tr").closest("tr").index();
        let ddl = $(`#Add tbody tr:eq(${currIndex}) td:eq(2) select`);
        ddl.empty();
        ddl.chosen('destroy');
        $.each(data, function (index, row) {
            ddl.append("<option value='" + row.Value + "'>" + row.Text + "</option>");
        });
        ddl.chosen();
    });
}




function Grandtotal(q, totalColumnIndex, type) {
    let grTotal = 0;
    for (var i = 0; i < $(q).find("tbody tr").length; i++) {

        grTotal += parseFloat($(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim() == "" ? 0 : $(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim());
    }
    if (type == "weight") {
        $(q).find("tr.grand_total td:eq(1)").text(grTotal);
    }
    else if (type == "qty") {
        $(q).find("tr.grand_total td:eq(3)").text(grTotal);
    }
    else if (type == "totalweight") {
        $(q).find("tr.grand_total td:eq(4)").text(grTotal);
    }
    else if (type == "Additional") {
        $(q).find("tr.grand_total td:eq(5)").text(grTotal);
    }
    else if (type == "Weighbridge") {
        $(q).find("tr.grand_total td:eq(6)").text(grTotal);
    }
    else if (type == "Labour") {
        $(q).find("tr.grand_total td:eq(7)").text(grTotal);
    }
    else if (type == "totalfreight") {
        $(q).find("tr.grand_total td:eq(8)").text(grTotal);
    }
}

