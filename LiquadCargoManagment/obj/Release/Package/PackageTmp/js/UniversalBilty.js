function GetArea(e) {
    let row = $(e).parent("td").parent("tr.LocationCode");
    let check = $("#LocationCode").val();
    if (check == 0) {
        row.find("td:eq(1) input").val('');
        row.find("td:eq(2) input").val('');
        row.find("td:eq(3) input").val('');
    }
    else {
        $.post(`../Admin/GetUniversalLocation?id=${$(e).val()}`).done(function (response) {
            row.find("td:eq(1) input").val(response.City);
            //row.find("td:eq(2) input").val(response.Area);
            row.find("td:eq(3) input").val(response.Address);
        });
    }
}
function GetAreas(e) {
    debugger;
    let row = $(e).parent("td").parent("tr.LocationCode");
    $.post(`../Admin/GetAreas?id=${$(e).val()}`).done(function (response) {
        row.find("td:eq(2) input").val(response.Area);
    });
}

function GetCustomerName(e) {
    let row = $(e).parent("td").parent("tr");
    $.post(`../Admin/GetCustomer?id=${$(e).val()}`).done(function (response) {
        row.find("td:eq(1) input").val(response.CompanyName);
        row.find("td:eq(2) input").val(response.Department);
    });
}

function GetCustomerClient(e) {
    let row = $(e).parent("td").parent("tr.CustomerCompany");
    $.post(`../Admin/GetCustomerClient?id=${$(e).val()}`).done(function (response) {
        //row.find("td:eq(0) input").val(response.BillTo);
        row.find("td:eq(1) input").val(response.CustomerName);
        row.find("td:eq(2) input").val(response.DepartName);
    });
}

//function GetVehicleType(e) {
//    let row = $(e).parent("td").parent("tr");
//    $.post(`../Admin/GetVehicleType?id=${$(e).val()}`).done(function (response) {
//        row.find("td:eq(1) input").val(response.VehicleType);
//    });
//}

function GetContainerType(e) {
    let row = $(e).parent("td").parent("tr");
    $.post(`../Admin/GetContainerType?id=${$(e).val()}`).done(function (response) {
        row.find("td:eq(1) input").val(response.ContainerType);
    });
}

function GetProduct(e) {
    let row = $(e).parent("td").parent("tr");
    if (row.find("td:eq(0) select").val() == 0) {
        row.find("td:eq(1) input").val('');
        row.find("td:eq(2) input").val('');
    }
    else {
        $.post(`../Admin/GetProductCascade?id=${$(e).val()}`).done(function (response) {
            //row.find("td:eq(1) input").val(response.ProductName);
            row.find("td:eq(1) input").val(response.PackageType);
            row.find("td:eq(2) input").val(response.ProductNature);
        });
    }
}

function GetDocument(e) {
    let row = $(e).parent("td").parent("tr");
    $.post(`../Admin/GetDispatchDocument?id=${$(e).val()}`).done(function (response) {
        row.find("td:eq(1) input").val(response.DocumentType);
    });
}

function AddVehicleRow(A) {
    debugger;
    $(".select-chosen").chosen('destroy');
    //$("#P").hide();
    //$("#D").show();
    var parent = $(A).parent("td").parent("tr").html();
    parent = parent.replace("btn-danger", "btn-danger").replace("disabled", "enabled").replace("btn-success", "").replace("fa-info", "").replace(" | Vehicle Details", "").replace("btn-success", "").replace("fa-info", "").replace(" | Update Vehicle Details","");
    //var lastrow = $("#Vehicle tbody tr").length - 1;
    //$("#Vehicle").find("tbody tr:eq(" + lastrow + ")").before(`<tr>${parent}</tr>`);
    $("#Vehicle tbody").append(`<tr>${parent}</tr>`)
    $(".select-chosen").chosen();
}
function AddProductRow(A) {
    debugger;
    $(".select-chosen").chosen('destroy');
    var parent = $(A).parent("td").parent("tr").html();
    parent = parent.replace("btn-danger", "btn-danger").replace("disabled", "enabled");
    $("#Product tbody").append(`<tr>${parent}</tr>`)
    //var lastrow = $("#Product tbody tr").length - 1;
    //$("#Product").find("tbody tr:eq(" + lastrow + ")").before(`<tr>${parent}</tr>`);
    $(".select-chosen").chosen();
}

function AddDispatchRow(A) {
    debugger;
    $(".select-chosen").chosen('destroy');
    var parent = $(A).parent("td").parent("tr").html();
    parent = parent.replace("btn-danger", "btn-danger").replace("disabled", "enabled");
    $(".dispatch tbody").append(`<tr>${parent}</tr>`)
    //var lastrow = $(".dispatch tbody tr").length - 1;
    //$(".dispatch").find("tbody tr:eq(" + lastrow + ")").before(`<tr>${parent}</tr>`);
    $(".select-chosen").chosen();
}

function AddReceivingInformationRow(A) {
    debugger;
    $(".select-chosen").chosen('destroy');
    var parent = $(A).parent("td").parent("tr").html();
    parent = parent.replace("btn-danger", "btn-danger").replace("disabled", "enabled");
    $("#ReceivingInformation tbody").append(`<tr>${parent}</tr>`)
    //var lastrow = $("#ReceivingInformation tbody tr").length - 1;
    //$("#ReceivingInformation").find("tbody tr:eq(" + lastrow + ")").before(`<tr>${parent}</tr>`);
    $(".select-chosen").chosen();
}

function AddReceivingDocumentRow(A) {
    debugger;
    $(".select-chosen").chosen('destroy');
    var parent = $(A).parent("td").parent("tr").html();
    parent = parent.replace("btn-danger", "btn-danger").replace("disabled", "enabled");
    $("#ReceivingDocument tbody").append(`<tr>${parent}</tr>`)
    //var lastrow = $("#ReceivingDocument tbody tr").length - 1;
    //$("#ReceivingDocument").find("tbody tr:eq(" + lastrow + ")").before(`<tr>${parent}</tr>`);
    $(".select-chosen").chosen();
}

function AddDamageRow(A) {
    debugger;
    $(".select-chosen").chosen('destroy');
    var parent = $(A).parent("td").parent("tr").html();
    parent = parent.replace("btn-danger", "btn-danger").replace("disabled", "enabled").replace("btn-success", "").replace("fa-info", "").replace(" | Damage Detail", "");
    $("#DamageDetail tbody").append(`<tr>${parent}</tr>`)
    //var lastrow = $("#DamageDetail tbody tr").length - 1;
    //$("#DamageDetail").find("tbody tr:eq(" + lastrow + ")").before(`<tr>${parent}</tr>`);
    $(".select-chosen").chosen();
    DamageCostTotal();
}

function AddContainerRow(A) {
    debugger;
    $(".select-chosen").chosen('destroy');
    var parent = $(A).parent("td").parent("tr").html();
    //parent = parent.replace("btn-primary", "btn-danger").replace("fa-plus", "fa-trash").replace("btn-success", "").replace("fa-info", "").replace(" | Container Details", "").replace("btn-success", "").replace("fa-info", "").replace(" | Update Container Details", "").replace("AddContainerRow", "remove");
    parent = parent.replace("btn-danger", "btn-danger").replace("disabled", "enabled").replace("btn-success", "").replace("fa-info", "").replace(" | Container Details", "").replace("btn-success", "").replace("fa-info", "").replace(" | Update Container Details", "");
    //parent = parent.replace("btn-primary", "btn-danger").replace("fa-plus", "fa-trash").replace("AddContainerRow", "remove");
    //var lastrow = $("#Container tbody tr").length - 1;
    //$("#Container").find("tbody tr:eq(" + lastrow + ")").before(`<tr>${parent}</tr>`);
    $("#Container tbody").append(`<tr>${parent}</tr>`)
    $(".select-chosen").chosen();
}

function AddExpensesRow(A) {
    debugger;
    $(".select-chosen").chosen('destroy');
    var parent = $(A).parent("td").parent("tr").html();
    parent = parent.replace("btn-danger", "btn-danger").replace("disabled", "enabled");
    $("#Expenses tbody").append(`<tr>${parent}</tr>`)
    //var lastrow = $("#Expenses tbody tr").length - 1;
    //$("#Expenses").find("tbody tr:eq(" + lastrow + ")").before(`<tr>${parent}</tr>`);
    $(".select-chosen").chosen();
}

function AddFrieghtDetial(A) {
    debugger;
    var parent = $(A).parent("td").parent("tr").html();
    parent = parent.replace("btn-danger", "btn-danger").replace("disabled", "enabled");
    $("#FreightCharges tbody").append(`<tr>${parent}</tr>`)
}

function remove(e) {
    $(e).parent("td").parent("tr").fadeOut(100, function () { $(this).remove(); });
}

function ShowAllForms() {
    var Check = $("#ManualBiltyNo").val();
    if (Check != "") {
        $("#DamageInformation, #DispatchInformation,#ExpensesInformation,#ProductInformation,#ReceivingInformation1,#ReceivingDocument1,#VehicleInformation,#ContainerInformation,#PackageTypeInformation,#LoadingUnloadingDetail").show("slow");
        $("#LocationFoot,#VehicleFoot,#ContainerFoot,#ProductFoot,#DispatchFoot,#ReceivingFoot,#DamageFoot,#ExpenseFoot").hide("slow");
    }
    else {
        Message("Required","Manual Bilty No is required","info");
    }
}

function HideAllForms() {
    $("#DamageInformation, #DispatchInformation,#ExpensesInformation,#ProductInformation,#ReceivingInformation1,#ReceivingDocument1,#VehicleInformation,#PackageTypeInformation,#ContainerInformation,#LoadingUnloadingDetail").hide("slow");
}

function ShowVehicle() {
    $("#VehicleInformation").show("slow");
    $("#LocationFoot").hide("slow");
}

function ShowContainer() {
    $("#ContainerCode").chosen('destroy');
    $("#ContainerInformation").show("slow");
    $("#VehicleFoot").hide("slow");
}

function ShowProduct() {
    $("#ProductCode").chosen('destroy');
    $("#ProductInformation").show("slow");
    $("#PackageTypeInformation").show("slow");
    $("#ContainerFoot").hide("slow");
}

function ShowDispatchDocument() {
    $("#DispatchDocumentCode").chosen('destroy');
    $("#DispatchInformation").show("slow");
    $("#ProductFoot").hide("slow");
}
function ShowLoadingUnLoading() {
    $("#LoadingUnloadingDetail").show("slow");
    $("#DispatchFoot").hide("slow");
}
function ShowReceivingDetail() {
    $("#EmployeeID").chosen('destroy');
    $("#ReceivingDocumentCode").chosen('destroy');
    $("#ReceivingInformation1").show("slow");
    $("#ReceivingDocument1").show("slow");
    $("#LoadingUnloadingFoot").hide("slow");
}

function ShowDamage() {
    $("#ProductItemID").chosen('destroy');
    $("#DamageTypeID").chosen('destroy');
    $("#DocumentTypeID").chosen('destroy');
    $("#DamageInformation").show("slow");
    $("#ReceivingFoot").hide("slow");
}

function ShowExpenses() {
    $("#ExpenseID").chosen('destroy');
    $("#ExpensesInformation").show("slow");
    $("#DamageFoot").hide("slow");
}

function SaveData() {
    debugger;
    $("#SaveBtn").attr("disabled",true);
    disable();
    let Universal = {}
    let ID = $("#hfBiltyID").val();
    let AutoBiltyNo = $("#AutoBiltyNo").val();
    let ManualBiltyNo = $("#ManualBiltyNo").val();
    let BiltyDate = $("#BiltyDate").val();
    let DeliveryDate = $("#DeliveryDate").val();
    let CompanyID = $("#OwnCompanyID").val();
    let Remarks = $("#Remarks").val();

    let LocationCode = $("#LocationCode").val();
    let DropLocationCode = $("#DropLocationCode").val();
    let LocalPickCity = $("#LocalPickCity").val();
    let LocalPickArea = $("#LocalPickArea").val();
    let LocalPickAddress = $("#LocalPickAddress").val();
    let LocalDropCity = $("#LocalDropCity").val();
    let LocalDropArea = $("#LocalDropArea").val();
    let LocalDropAddress = $("#LocalDropAddress").val();
    let LocalPickStatus = false;
    if ($("#Pick").is(":checked"))
        LocalPickStatus = true;
    let LocalDropStatus = false;
    if ($("#Drop").is(":checked"))
        LocalDropStatus = true;

    let LoadingDate = $("#LoadingDate").val();
    let LoadingTime = $("#LoadingTime").val();
    let LoadingGateInDate = $("#LoadingGateInDate").val();
    let LoadingGateInTime = $("#LoadingGateInTime").val();
    let LoadingGateOutDate = $("#LoadingGateOutDate").val();
    let LoadingGateOutTime = $("#LoadingGateOutTime").val();
    let LoadingTotalDays = $("#LoadingTotalDays").val();
    let LoadingTotalTime = $("#LoadingTotalTime").val();


    let UnLoadingDate = $("#UnLoadingDate").val();
    let UnLoadingTime = $("#UnLoadingTime").val();
    let UnLoadingGateInDate = $("#UnLoadingGateInDate").val();
    let UnLoadingGateInTime = $("#UnLoadingGateInTime").val();
    let UnLoadingGateOutDate = $("#UnLoadingGateOutDate").val();
    let UnLoadingGateOutTime = $("#UnLoadingGateOutTime").val();
    let UnLoadingTotalDays = $("#UnLoadingTotalDays").val();
    let UnLoadingTotalTime = $("#UnLoadingTotalTime").val();

    if (ManualBiltyNo == "" || BiltyDate == "") {
        Message("Required", "Enter all required fields", "error");
        return;
    }

    Universal.ID = ID;
    Universal.AutoBiltyNo = AutoBiltyNo;
    Universal.AutoBiltyNo = AutoBiltyNo;
    Universal.ManualBiltyNo = ManualBiltyNo;
    Universal.BiltyDate = BiltyDate;
    Universal.DeliveryDate = DeliveryDate;
    Universal.CompanyID = CompanyID;
    Universal.Remarks = Remarks;

    Universal.LocationCode = LocationCode;
    Universal.DropLocationCode = DropLocationCode;
    Universal.LocalPickCity = LocalPickCity;
    Universal.LocalPickArea = LocalPickArea;
    Universal.LocalPickAddress = LocalPickAddress;
    Universal.LocalDropCity = LocalDropCity;
    Universal.LocalDropArea = LocalDropArea;
    Universal.LocalDropAddress = LocalDropAddress;
    Universal.LocalPickStatus = LocalPickStatus;
    Universal.LocalDropStatus = LocalDropStatus;

    Universal.Remarks = $("#remarks").find("tbody tr.Allremarks td:eq(0) textarea").val();

    Universal.ContainerQty = $("#Container").find("tfoot tr.grand_total_Container td:eq(1)").text();
    Universal.ContainerWeight = $("#Container").find("tfoot tr.grand_total_Container td:eq(2)").text();
    Universal.ContainerTotalWeight = $("#Container").find("tfoot tr.grand_total_Container td:eq(3)").text();

    Universal.ProductQty = $("#Product").find("tfoot tr.grand_total td:eq(1)").text();
    Universal.ProductWeight = $("#Product").find("tfoot tr.grand_total td:eq(2)").text();
    Universal.ProductTotalWeight = $("#Product").find("tfoot tr.grand_total td:eq(3)").text();

    Universal.TotalAdditionalFreight = $("#FreightCharges").find("tfoot tr.grand_total_Freight td:eq(0)").text();
    Universal.TotalWeighbridge = $("#FreightCharges").find("tfoot tr.grand_total_Freight td:eq(1)").text();
    Universal.TotalLabour = $("#FreightCharges").find("tfoot tr.grand_total_Freight td:eq(2)").text();
    Universal.TotalFreight = $("#FreightCharges").find("tfoot tr.grand_total_Freight td:eq(3)").text();

    Universal.LoadingDate = LoadingDate; 
    Universal.LoadingTime = LoadingTime; 
    Universal.LoadingGateInDate = LoadingGateInDate; 
    Universal.LoadingGateInTime = LoadingGateInTime; 
    Universal.LoadingGateOutDate = LoadingGateOutDate; 
    Universal.LoadingGateOutTime = LoadingGateOutTime; 
    Universal.LoadingTotalDays = LoadingTotalDays; 
    Universal.LoadingTotalTime = LoadingTotalTime; 

    Universal.UnLoadingDate = UnLoadingDate;
    Universal.UnLoadingTime = UnLoadingTime;
    Universal.UnLoadingGateInDate = UnLoadingGateInDate;
    Universal.UnLoadingGateInTime = UnLoadingGateInTime;
    Universal.UnLoadingGateOutDate = UnLoadingGateOutDate;
    Universal.UnLoadingGateOutTime = UnLoadingGateOutTime;
    Universal.UnLoadingTotalDays = UnLoadingTotalDays;
    Universal.UnLoadingTotalTime = UnLoadingTotalTime; 

    //Universal.VID = VID;
    //Universal.VehicleTypeID = VehicleTypeID;
    //Universal.RegNo = RegNo;
    //Universal.OwnerContact = OwnerContact;
    //Universal.OwnerNIC = OwnerNIC;
    //Universal.Code = Code;

    var Customers = new Array();
    $("#Customer tbody").each(function () {
        var row = $(this);
        var customer = {};
        customer.ConsignerSenderCustomerCode = row.find("tr.Sender td:eq(0) select option:selected").val();
        customer.ConsignerReceiverCustomerCode = row.find("tr.Receiver td:eq(0) select option:selected").val();
        //customer.ClientBillTo = row.find("td:eq(0) input").val();
        customer.ClientCode = row.find("tr.Client td:eq(0) select option:selected").val();
        customer.BillingType = row.find("tr.Payment td:eq(0) select option:selected").val();
        customer.ShipmentType = row.find("tr.Payment td:eq(1) select option:selected").val();
        Customers.push(customer);
    });

    //var Location = new Array();
    //$("#Location tbody tr").each(function () {
    //    var row = $(this);
    //    var customer = {};
    //    customer.ID = row.find("td:eq(0) input").val();
    //    customer.LocationCode = row.find("td:eq(0) select option:selected").val();
    //    Location.push(customer);
    //});

    var Vehicle = new Array();
    $("#Vehicle tbody tr").each(function () {
        var row = $(this);
        var customer = {};
        customer.ID = row.find("td:eq(0) input").val();
        customer.VehicleID = row.find("td:eq(0) select option:selected").val();
        customer.Qty = row.find("td:eq(1) input").val();
        Vehicle.push(customer);
    });

    //var Details = new Array();
    //$("#VehicleInfoModal tbody tr.Info_Vehicle").each(function () {
    //    var row = $(this);
    //    var v = {};
    //    v.VehicleID = row.find("td:eq(0) input").val();
    //    v.VehicleTypeID = row.find("td:eq(1) select option:selected").val();
    //    v.RegNo = row.find("td:eq(2) input").val();
    //    v.OwnerContact = row.find("td:eq(3) input").val();
    //    v.OwnerNIC = row.find("td:eq(6) input").val();
    //    v.Code = row.find("td:eq(8) input").val();
    //    Details.push(v);
    //});

    var Container = new Array();
    $("#Container tbody tr").each(function () {
        var row = $(this);
        var customer = {};
        customer.ID = row.find("td:eq(0) input").val();
        customer.ContainerID = row.find("td:eq(0) select option:selected").val();
        customer.Qty = row.find("td:eq(1) input").val();
        customer.Weight = row.find("td:eq(2) input").val();
        customer.TotalQty = $("#Container").find("tfoot tr.grand_total_Container td:eq(1)").text();
        customer.TotalWeight = $("#Container").find("tfoot tr.grand_total_Container td:eq(2)").text();
        customer.GrandTotal = $("#Container").find("tfoot tr.grand_total_Container td:eq(3)").text();
        Container.push(customer);
    });

    var Product = new Array();
    $("#Product tbody tr").each(function () {
        var row = $(this);
        var customer = {};
        customer.ID = row.find("td:eq(0) input").val();
        customer.ProductID = row.find("td:eq(0) select option:selected").val();
        customer.Qty = row.find("td:eq(3) input").val();
        customer.Weight = row.find("td:eq(4) input").val();
        customer.WeightQtyTotal = row.find("td:eq(5) input").val();
        customer.TotalQty = $("#Product").find("tfoot tr.grand_total td:eq(1)").text();
        customer.TotalWeight = $("#Product").find("tfoot tr.grand_total td:eq(2)").text();
        customer.GrandTotal = $("#Product").find("tfoot tr.grand_total td:eq(3)").text();
        Product.push(customer);
    });

    var DispatchDocument = new Array();
    $("#Dispatch tbody tr").each(function () {
        var row = $(this);
        var customer = {};
        customer.ID = row.find("td:eq(0) input").val();
        customer.DocumentID = row.find("td:eq(0) select option:selected").val();
        customer.DocumentNo = row.find("td:eq(1) input").val();
        customer.FileName = row.find("td:eq(2) input").val();
        DispatchDocument.push(customer);
    });

    var ReceivingInformation = new Array();
    $("#ReceivingInformation tbody tr").each(function () {
        var row = $(this);
        var customer = {};
        customer.ID = row.find("td:eq(0) input").val();
        customer.EmployeeID = row.find("td:eq(0) select option:selected").val();
        customer.Date = row.find("td:eq(1) input").val();
        customer.Time = row.find("td:eq(2) input").val();
        ReceivingInformation.push(customer);
    });

    var ReceivingDocument = new Array();
    $("#ReceivingDocument tbody tr").each(function () {
        var row = $(this);
        var customer = {};
        customer.ID = row.find("td:eq(0) input").val();
        customer.DocumentID = row.find("td:eq(0) select option:selected").val();
        customer.DocumentNo = row.find("td:eq(1) input").val();
        customer.FileName = row.find("td:eq(2) input").val();
        ReceivingDocument.push(customer);
    });

    var Damage = new Array();
    $("#DamageDetail tbody tr").each(function () {
        var row = $(this);
        var customer = {};
        customer.ID = row.find("td:eq(0) input").val();
        customer.ProductItemID = row.find("td:eq(0) select option:selected").val();
        customer.DamageTypeID = row.find("td:eq(1) select option:selected").val();
        customer.DocumentTypeID = row.find("td:eq(2) select option:selected").val();
        customer.ClaimDamageCost = row.find("td:eq(3) input").val();
        customer.CauseOfDamage = row.find("td:eq(4) input").val();
        Damage.push(customer);
    });

    var Expense = new Array();
    $("#Expenses tbody tr").each(function () {
        var row = $(this);
        var customer = {};
        customer.ID = row.find("td:eq(0) input").val();
        customer.ExpenseID = row.find("td:eq(0) select option:selected").val();
        customer.Account = row.find("td:eq(1) input").val();
        customer.Amount = row.find("td:eq(2) input").val();
        Expense.push(customer);
    });

    var VehicleDetails = new Array();
    $("#modal-addVehicleInfo #VehicleInfoModal tbody tr").each(function () {
        var row = $(this);
        var customer = {};
        //customer.ID = row.find("td:eq(0) input").val();
        customer.VehicleID = $("#Vehicle tbody tr").find("td:eq(0) select").val();
        customer.RegNo = row.find("td:eq(2) input").val();
        customer.Cell = row.find("td:eq(3) input").val();
        customer.DriverName = row.find("td:eq(4) input").val();
        customer.FatherName = row.find("td:eq(5) input").val();
        customer.NIC = row.find("td:eq(6) input").val();
        customer.License = row.find("td:eq(7) input").val();
        customer.Broker = row.find("td:eq(8) input").val();
        VehicleDetails.push(customer);
    });

    var ContainerDetails = new Array();
    $("#modal-addContainerInfo #ContainerInfoModal tbody tr").each(function () {
        var row = $(this);
        var customer = {};
        customer.ContainerTypeID = $("#Container tbody tr").find("td:eq(0) select").val();
        customer.ContainerNo = row.find("td:eq(2) input").val();
        customer.Weight = row.find("td:eq(3) input").val();
        customer.ContainerPick = row.find("td:eq(4) select").val();
        customer.ContainerDrop = row.find("td:eq(5) select").val();
        customer.Vessel = row.find("td:eq(6) input").val();
        customer.Remarks = row.find("td:eq(7) input").val();
        ContainerDetails.push(customer);
    });

    var FreightCharges = new Array();
    $("#FreightCharges tbody tr").each(function () {
        var row = $(this);
        var customer = {};
        customer.ID = row.find("td:eq(0) input").val();
        customer.AdditionalFrieght = row.find("td:eq(0) input").val();
        customer.Weighbridge = row.find("td:eq(1) input").val();
        customer.Labour = row.find("td:eq(2) input").val();
        customer.Freight = row.find("td:eq(3) input").val();
        FreightCharges.push(customer);
    });

    $.ajax({
        url: "../Admin/InsertUniversalBilty",
        type: "POST",
        data: {
            Universal: Universal, Customer: Customers, Location: Universal,
            Vehicle: Vehicle, Container: Container, Product: Product,
            DispatchDocument: DispatchDocument, ReceivingInformation: ReceivingInformation,
            ReceivingDocument: ReceivingDocument,
            DamageDetail: Damage, ExpenseDetail: Expense,
            LoadingDetail: Universal, UnLoadingDetail: Universal,
            VehicleDetails: VehicleDetails, ContainerDetails: ContainerDetails
            //Details: Details
        },
        success: function (r) {
            debugger;
            $("#SaveBtn").attr("disabled", false);
            if (r.status == "OK") {
                Message("Success", "Universal Bilty has been created", "success");
                window.location = "../Admin/UniversalBiltyList";
            }
            else {
                Message("Error", r.Message, "info");
                $("#SaveBtn").attr("disabled", false);
            }
        }
    });
}
            VehicleDetails: VehicleDetails, ContainerDetails: ContainerDetails,
            FreightDetail: FreightCharges
        },
        success: function (r) {
            debugger;
            if (r.status == "OK") {
                Message("Success", "Universal Bilty has been created", "success");
                window.location = "../Admin/UniversalBiltyList";
                enable();
            }
            else {
                Message("Error", r.message, "info");
                enable();
            }
        },
        error: function (e) {
            Message("Error", e.message, "error");
            enable();
        }
    });
}
function disable() {
    $("#SaveBtn").attr("disabled", true);
}
function enable() {
    $("#SaveBtn").attr("disabled", false);
}

function ReloadLocation() {
    debugger;
    $.ajax({
        url: "../Admin/Reload",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#LocationCode').chosen('destroy');
            $('#LocationCode').html('');
            console.log(data);
            $('#LocationCode').append("<option value='0'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Location.length; i++) {
                $('#LocationCode').append("<option value='" + data.Location[i].ID + "'>" + data.Location[i].LocationCode + " | " + data.Location[i].LocationName + "</option>");
            }
            $('#LocationCode').chosen();

            $('#DropLocationCode').chosen('destroy');
            $('#DropLocationCode').html('');
            $('#DropLocationCode').append("<option value='0'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.DropLocation.length; i++) {
                $('#DropLocationCode').append("<option value='" + data.DropLocation[i].ID + "'>" + data.DropLocation[i].LocationCode + " | " + data.DropLocation[i].LocationName + "</option>");
            }
            $('#DropLocationCode').chosen();

            $('#City').chosen('destroy');
            $('#City').html('');
            $('#City').append("<option value='0'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.City.length; i++) {
                $('#City').append("<option value='" + data.City[i].CityID + "'>" + data.City[i].CityCode + " | " + data.City[i].CityName + "</option>");
            }

            $('#Area').chosen('destroy');
            $('#Area').html('');
            $('#Area').append("<option value='0'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Area.length; i++) {
                $('#Area').append("<option value='" + data.Area[i].ID + "'>" + data.Area[i].Code + " | " + data.Area[i].Name + "</option>");
            }

            $('#DropLocCity').chosen('destroy');
            $('#DropLocCity').html('');
            $('#DropLocCity').append("<option value='0'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.City.length; i++) {
                $('#DropLocCity').append("<option value='" + data.City[i].CityID + "'>" + data.City[i].CityCode + " | " + data.City[i].CityName + "</option>");
            }

            $('#DropLocArea').chosen('destroy');
            $('#DropLocArea').html('');
            $('#DropLocArea').append("<option value='0'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Area.length; i++) {
                $('#DropLocArea').append("<option value='" + data.Area[i].ID + "'>" + data.Area[i].Code + " | " + data.Area[i].Name + "</option>");
            }
        }
    });
}

function ReloadCustomer() {
    debugger;
    $.ajax({
        url: "../Admin/Reload",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#ConsignerSenderCustomerCode').chosen('destroy');
            $('#ConsignerSenderCustomerCode').html('');
            console.log(data);
            $('#ConsignerSenderCustomerCode').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Customer.length; i++) {
                $('#ConsignerSenderCustomerCode').append("<option value='" + data.Customer[i].ID + "'>" + data.Customer[i].Code + " | " + data.Customer[i].Name + "</option>");
            }
            $('#ConsignerSenderCustomerCode').chosen();

            $('#ConsignerReceiverCustomerCode').chosen('destroy');
            $('#ConsignerReceiverCustomerCode').html('');
            $('#ConsignerReceiverCustomerCode').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Customer.length; i++) {
                $('#ConsignerReceiverCustomerCode').append("<option value='" + data.Customer[i].ID + "'>" + data.Customer[i].Code + " | " + data.Customer[i].Name + "</option>");
            }
            $('#ConsignerReceiverCustomerCode').chosen();

            $('#ClientCode').chosen('destroy');
            $('#ClientCode').html('');
            console.log(data);
            $('#ClientCode').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Customer.length; i++) {
                $('#ClientCode').append("<option value='" + data.Customer[i].ID + "'>" + data.Customer[i].Code + " | " + data.Customer[i].Name + "</option>");
            }
            $('#ClientCode').chosen();

            $('#ShipmentType').chosen('destroy');
            $('#ShipmentType').html('');
            $('#ShipmentType').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Shipment.length; i++) {
                $('#ShipmentType').append("<option value='" + data.Shipment[i].ID + "'>" + data.Shipment[i].Code + " | " + data.Shipment[i].Name + "</option>");
            }
            $('#ShipmentType').chosen();
        }
    });
}

function ReloadVehicle() {
    debugger;
    $.ajax({
        url: "../Admin/Reload",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#VehicleCode').chosen('destroy');
            $('#VehicleCode').html('');
            console.log(data);
            $('#VehicleCode').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Vehicle.length; i++) {
                $('#VehicleCode').append("<option value='" + data.Vehicle[i].ID + "'>" + data.Vehicle[i].Code + " | " + data.Vehicle[i].Name + "</option>");
            }
            $('#VehicleCode').chosen();
        }
    });
}

function ReloadContainer() {
    debugger;
    $.ajax({
        url: "../Admin/Reload",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#ContainerCode').chosen('destroy');
            $('#ContainerCode').html('');
            console.log(data);
            $('#ContainerCode').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Container.length; i++) {
                $('#ContainerCode').append("<option value='" + data.Container[i].ContainerTypeID + "'>" + data.Container[i].Code + " | " + data.Container[i].ContainerTypeName + "</option>");
            }
            $('#ContainerCode').chosen();
        }
    });
}

function ReloadProduct() {
    debugger;
    $.ajax({
        url: "../Admin/Reload",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#ProductCode').chosen('destroy');
            $('#ProductCode').html('');
            console.log(data);
            $('#ProductCode').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Product.length; i++) {
                $('#ProductCode').append("<option value='" + data.Product[i].ID + "'>" + data.Product[i].Code + " | " + data.Product[i].Name + "</option>");
            }
            $('#ProductCode').chosen();
        }
    });
}

function ReloadDispatch() {
    debugger;
    $.ajax({
        url: "../Admin/Reload",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#DispatchDocumentCode').chosen('destroy');
            $('#DispatchDocumentCode').html('');
            console.log(data);
            $('#DispatchDocumentCode').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Document.length; i++) {
                $('#DispatchDocumentCode').append("<option value='" + data.Document[i].ID + "'>" + data.Document[i].Code + " | " + data.Document[i].Name + "</option>");
            }
            $('#DispatchDocumentCode').chosen();
        }
    });
}

function ReloadReceivingInfo() {
    debugger;
    $.ajax({
        url: "../Admin/Reload",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#EmployeeID').chosen('destroy');
            $('#EmployeeID').html('');
            console.log(data);
            $('#EmployeeID').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Received.length; i++) {
                $('#EmployeeID').append("<option value='" + data.Received[i].EmployeeID + "'>" + data.Received[i].EmployeeCode + " | " + data.Received[i].EmployeeName + "</option>");
            }
            $('#EmployeeID').chosen();
        }
    });
}

function ReloadReceivingDocument() {
    debugger;
    $.ajax({
        url: "../Admin/Reload",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#ReceivingDocumentCode').chosen('destroy');
            $('#ReceivingDocumentCode').html('');
            console.log(data);
            $('#ReceivingDocumentCode').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Document.length; i++) {
                $('#ReceivingDocumentCode').append("<option value='" + data.Document[i].ID + "'>" + data.Document[i].Code + " | " + data.Document[i].Name + "</option>");
            }
            $('#ReceivingDocumentCode').chosen();
        }
    });
}

function ReloadDamage() {
    debugger;
    $.ajax({
        url: "../Admin/Reload",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#DamageTypeID').chosen('destroy');
            $('#DamageTypeID').html('');
            console.log(data);
            $('#DamageTypeID').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Damage.length; i++) {
                $('#DamageTypeID').append("<option value='" + data.Damage[i].DamageTypeID + "'>" + data.Damage[i].Code + " | " + data.Damage[i].Name + "</option>");
            }
            $('#DamageTypeID').chosen();

            $('#ProductItemID').chosen('destroy');
            $('#ProductItemID').html('');
            console.log(data);
            $('#ProductItemID').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Product.length; i++) {
                $('#ProductItemID').append("<option value='" + data.Product[i].ID + "'>" + data.Product[i].Code + " | " + data.Product[i].Name + "</option>");
            }
            $('#ProductItemID').chosen();

            $('#DocumentTypeID').chosen('destroy');
            $('#DocumentTypeID').html('');
            console.log(data);
            $('#DocumentTypeID').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.DocumentType.length; i++) {
                $('#DocumentTypeID').append("<option value='" + data.DocumentType[i].DocumentTypeID + "'>" + data.DocumentType[i].Code + " | " + data.DocumentType[i].Name + "</option>");
            }
            $('#DocumentTypeID').chosen();
        }
    });
}

function ReloadExpense() {
    debugger;
    $.ajax({
        url: "../Admin/Reload",
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#ExpenseID').chosen('destroy');
            $('#ExpenseID').html('');
            console.log(data);
            $('#ExpenseID').append("<option value='--select--'>" + "--Select--" + "</option>");
            for (var i = 0; i < data.Damage.length; i++) {
                $('#ExpenseID').append("<option value='" + data.Expense[i].ID + "'>" + data.Expense[i].Code + " | " + data.Expense[i].Name + "</option>");
            }
            $('#ExpenseID').chosen();
        }
    });
}

function Enabled() {
    debugger;
    let Dropdown = $("#LocationCode").val();
    if (Dropdown == 0) {
        $(".Picktblddl").show();
        $(".Picktbl").hide();
    }
    else {
        $(".Picktblddl").hide();
        $(".Picktbl").show();
    }
};

function EnabledDrop() {
    debugger;
    let Dropdown = $("#DropLocationCode").val();
    if (Dropdown == 0) {
        $(".Droptblddl").show();
        $(".Droptbl").hide();
    }
    else {
        $(".Droptblddl").hide();
        $(".Droptbl").show();
    }
};

function EnabledUpdate() {
    debugger;
    let Dropdown = $("#LocationCode").val();
    if (Dropdown == 0) {
        $(".UpdatePicktblddl").show();
        $(".UpdatePicktbl").hide();
    }
    else {
        $(".UpdatePicktblddl").hide();
        $(".UpdatePicktbl").show();
    }
};

function EnabledDropUpdate() {
    debugger;
    let Dropdown = $("#DropLocationCode").val();
    if (Dropdown != 0) {
        $(".UpdateDroptblddl").hide();
        $(".UpdateDroptbl").show();
    }
    else {
        $(".UpdateDroptbl").hide();
        $(".UpdateDroptblddl").show();
    }
};

//$(document).on("keyup", "input.search", function () {
//    debugger;
//    var _value = $(this).val();
//    if (_value != "") {
//        $.ajax({
//            url: '../Admin/SearchvehicleBilty?_value=' + _value,
//            type: 'get',
//            sucess: function (response) {
//                alert('success');
//                console.log(response);
//                //var row = `<tr><td>${response.RegNo}</td><td>${response.Code}</td></tr>`;
//                //$('#tblSearchvehicle tbody').append(row);
//                //$('#tblSearchvehicle').show();
//            },
//            error: function () {
//                alert("Request time out");
//            }
//        });
//    }
//})

function LoadingReport() {
    debugger;
    let LoadingDate = $("#LoadingDate").val();
    let LoadingGateOutDate = $("#LoadingGateOutDate").val();
    var VLoadingDate = new Date(LoadingDate);
    var VLoadingGateOutDate = new Date(LoadingGateOutDate);
    var Subtract = VLoadingGateOutDate.getTime() - VLoadingDate.getTime();
    var TotalDays = Subtract / (1000 * 60 * 60 * 24);
    $("#LoadingTotalDays").val(TotalDays);
}

function UnLoadingReport() {
    debugger;
    let UnLoadingDate = $("#UnLoadingDate").val();
    let UnLoadingGateOutDate = $("#UnLoadingGateOutDate").val();
    var VUnLoadingDate = new Date(UnLoadingDate);
    var VUnLoadingGateOutDate = new Date(UnLoadingGateOutDate);
    var Subtract = VUnLoadingGateOutDate.getTime() - VUnLoadingDate.getTime();
    var TotalDays = Subtract / (1000 * 60 * 60 * 24);
    $("#UnLoadingTotalDays").val(TotalDays);
}

function LoadingGetTime() {
    var hours = parseInt($("#LoadingGateOutTime").val().split(':')[0], 10) - parseInt($("#LoadingTime").val().split(':')[0], 10);
    if (hours < 0) hours = 24 + hours;
    $("#LoadingTotalTime").val(hours);
}

function UnLoadingGetTime() {
    var hours = parseInt($("#UnLoadingGateOutTime").val().split(':')[0], 10) - parseInt($("#UnLoadingTime").val().split(':')[0], 10);
    if (hours < 0) hours = 24 + hours;
    $("#UnLoadingTotalTime").val(hours);
}

function SaveInfoVehicle() {
    debugger;
    var Details = new Array();
    $("#VehicleInfoModal tbody tr.Info_Vehicle").each(function () {
        var row = $(this);
        var v = {};
        //v.VehicleTypeID = row.find("td:eq(1) input").val();
        v.RegNo = row.find("td:eq(2) select option:selected").val();
        v.Cell = row.find("td:eq(3) input").val();
        v.DriverName = row.find("td:eq(4) input").val();
        v.FatherName = row.find("td:eq(5) input").val();
        v.NIC = row.find("td:eq(6) input").val();
        v.License = row.find("td:eq(7) input").val();
        v.Broker = row.find("td:eq(8) input").val();
        Details.push(v);
    });
    $.ajax({
        url: "../Admin/AddVehicleInfo",
        type: "POST",
        data: { Details: Details },
        success: function (r) {
            Message("Success", "Vehicle Information has been saved successfully", "success");
            $('#modal-addVehicleInfo').modal('hide');
            $('#VehicleInfoModal tbody').html('');
        },
        error: function () {
            Message("Error", "An error", "error");
        }
    })
}

function SaveInfoContainer() {
    debugger;
    var Details = new Array();
    $("#ContainerInfoModal tbody tr.Info_Container").each(function () {
        var row = $(this);
        var v = {};
        v.ContainerTypeID = row.find("td:eq(1) select option:selected").val();
        v.ContainerNo = row.find("td:eq(2) input").val();
        v.Weight = row.find("td:eq(3) input").val();
        v.ContainerPick = row.find("td:eq(4) select option:selected").val();
        v.ContainerDrop = row.find("td:eq(5) select option:selected").val();
        v.Vessel = row.find("td:eq(6) input").val();
        v.Remarks = row.find("td:eq(7) input").val();
        Details.push(v);
    });
    $.ajax({
        url: "../Admin/AddContainerInfo",
        type: "POST",
        data: { Details: Details },
        success: function (r) {
            Message("Success", "Container Information has been saved successfully", "success");
            $('#modal-addContainerInfo').modal('hide');
            $('#ContainerInfoModal tbody').html('');
        },
        error: function () {
            Message("Error", "An error", "error");
        }
    })
}

function GrandtotalVehicleQty(q, totalColumnIndex, type) {
    let grTotal = 0;
    for (var i = 0; i < $(q).find("tbody tr").length; i++) {

        grTotal += parseFloat($(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim() == "" ? 0 : $(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim());
    }
    if (type == "Qty") {
        $(q).find("tr.grand_total_Vehicle td:eq(1)").text(grTotal);
    }
}

function Grandtotal(q, totalColumnIndex, type) {
    let grTotal = 0;
    for (var i = 0; i < $(q).find("tbody tr").length; i++) {

        grTotal += parseFloat($(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim() == "" ? 0 : $(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim());
    }
    if (type == "Qty") {
        $(q).find("tr.grand_total td:eq(1)").text(grTotal);
    }
    else if (type == "Weight") {
        $(q).find("tr.grand_total td:eq(2)").text(grTotal);
    }
    else if (type == "TotalWeight") {
        $(q).find("tr.grand_total td:eq(3)").text(grTotal);
    }
}

function GrandtotalContainer(q, totalColumnIndex, type) {
    let grTotal = 0;
    for (var i = 0; i < $(q).find("tbody tr").length; i++) {

        grTotal += parseFloat($(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim() == "" ? 0 : $(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim());
    }
    if (type == "Qty") {
        $(q).find("tr.grand_total_Container td:eq(1)").text(grTotal);
    }
    else if (type == "Weight") {
        $(q).find("tr.grand_total_Container td:eq(2)").text(grTotal);
    }
    else if (type == "TotalWeight") {
        $(q).find("tr.grand_total_Container td:eq(3)").text(grTotal);
    }
}

function GrandtotalFrieghtDetails(q, totalColumnIndex, type) {
    let grTotal = 0;
    for (var i = 0; i < $(q).find("tbody tr").length; i++) {

        grTotal += parseFloat($(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim() == "" ? 0 : $(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim());
    }
    if (type == "Additional") {
        $(q).find("tr.grand_total_Freight td:eq(0)").text(grTotal);
    }
    else if (type == "Weighbridge") {
        $(q).find("tr.grand_total_Freight td:eq(1)").text(grTotal);
    }
    else if (type == "Labour") {
        $(q).find("tr.grand_total_Freight td:eq(2)").text(grTotal);
    }
    else if (type == "Freight") {
        $(q).find("tr.grand_total_Freight td:eq(3)").text(grTotal);
    }
}

//function AddInfoContainer() {
//    let Qty = $("#ContainerQuantity").val();
//    let Code = $("#ContainerCode option:selected").text();
//    for (var i = 0; i < Qty.length; i++) {
//        var row = `<tr class='Info_Container'>
//            <td style='padding:2px;'><input type='hidden' id='ID' name='ID'></td>
//            <td style='padding:2px;'>
//            <input type='text' value='${Code}' disabled>
//            </td>
//            <td style='padding:2px;width:100px;'><input type='text' id='Container' name='Container'></td>
//            <td style='padding:2px;width:50px;'><input type='text' id='Weight' name='Weight'></td>
//                <td style='padding:2px;'><select id="EmptyContainerPick">
//            <option>--Select--</option>
//                @if (ViewBag.PickLocation != null)
//                {
//                    foreach (var pro in ViewBag.PickLocation)
//                    {
//                        <option value="@pro.Value">@pro.Text</option>
//                    }
//                }
//            </select></td>
//                <td style='padding:2px;'><select id="EmptyContainerDrop">
//            <option>--Select--</option>
//                @if (ViewBag.DropLocation != null)
//                {
//                    foreach (var pro in ViewBag.DropLocation)
//                    {
//                        <option value="@pro.Value">@pro.Text</option>
//                    }
//                }
//            </select>
//            </td>
//            <td style='padding:2px;width:80px;'><input type='text' id='Vessel'></td>
//            <td style='padding:2px;width:80px;'><input type='text'id='Remarks'></td>
//                </tr>`;
//        $('#ContainerInfoModal tbody').append(row);
//    }
//}

//function AddInfoVehicle() {
//    debugger;
//    let Qty = $("#Vehicle tbody tr td:eq(1) input").val();
//    let Code = $("#Vehicle tbody tr td:eq(0) select option:selected").text();
//    //let Qty = $("#Qty").val();
//    //let Code = $("#VehicleCode option:selected").text();

//    for (var i = 0; i < Qty.length; i++) {
//        var row = `<tr class='Info_Vehicle'>
//        <td><input type='hidden' id='VehicleID' name='VehicleID'></td>
//        <td style='padding:2px;'>
//        <input type='text' value='${Code}' disabled>
//        </td>

//        <td style='padding:2px;'><input type='text' id='RegNo' name='RegNo' style='width:150px;'></td>
//        <td style='padding:2px;'><input type='text' id='Cell' maxlength='12' name='Cell' style='width:120px;'></td>
//        <td style='padding:2px;'><input type='text' id='DriverName' style='width:120px;'></td>
//        <td style='padding:2px;'><input type='text' id='FatherName' style='width:120px;'></td>
//        <td style='padding:2px;'><input type='text' class='NIC' style='width:120px;'placeholder='99999-9999999-9' maxlength='15'></td>
//        <td style='padding:2px;'><input type='text'id='License' style='width:120px;'></td>
//        <td style='padding:2px;'><input type='text' id='Broker' style='width:120px;'></td>
//        </tr>`;
//        $('#VehicleInfoModal tbody').append(row);
//    }
//}

//function InfoVehicle() {
//    debugger;
//    let Code = $("#VehicleCode option:selected").text();
//    let Qty = $("#Qty").val();

//    if (Code == "" || Qty == "") {
//        alert("Vehicle not selected & Qty not fill");
//    }
//    else {
//        $(".TEST").find("tbody").append(`<tr style='font-weight:bold;'><td id='VCode'><i class='fa fa-exclamation-triangle text-danger'></i> ${Code}</td><td id='VQty'>${Qty}</td></tr>`);
//    }
//}
//function InfoContainer() {
//    debugger;
//    let Code = $("#ContainerCode option:selected").text();
//    let Qty = $("#ContainerQuantity").val();

//    if (Code == "" || Qty == "") {
//        alert("Container not selected & Qty not fill");
//    }
//    else {
//        $(".ContainerTEST").find("tbody").append(`<tr style='font-weight:bold;'><td id='CCode'><i class='fa fa-exclamation-triangle text-danger'></i> ${Code}</td><td id='CQty'>${Qty}</td></tr>`);
//    }
//}
//function ContainerQuantityAdd(btn) {
//    var $rooms = $(btn).prev();
//    var a = $rooms.val();

//    a++;
//    $("#Containersubs").prop("disabled", !a);
//    $rooms.val(a);

//    $("#Containersubs").prop("disabled", !$rooms.val());
//}

//function ContainerQuantityLess(btn) {
//    var $rooms = $(btn).next();
//    var b = $rooms.val();
//    if (b >= 1) {
//        b--;
//        $rooms.val(b);
//    }
//    else {
//        $("#Containersubs").prop("disabled", true);
//    }
//}

//function QuantityAdd(btn) {
//    var $rooms = $(btn).prev();
//    var a = $rooms.val();

//    a++;
//    $("#subs").prop("disabled", !a);
//    $rooms.val(a);

//    $("#subs").prop("disabled", !$rooms.val());
//}

//function QuantityLess(btn) {
//    var $rooms = $(btn).next();
//    var b = $rooms.val();
//    if (b >= 1) {
//        b--;
//        $rooms.val(b);
//    }
//    else {
//        $("#subs").prop("disabled", true);
//    }
//}

//function ContainerQuantityAdd() {
//    var $rooms = $("#ContainerQuantity");
//    var a = $rooms.val();

//    a++;
//    $("#Containersubs").prop("disabled", !a);
//    $rooms.val(a);

//    $("#subs").prop("disabled", !$("#ContainerQuantity").val());
//}

//function ContainerQuantityLess() {
//    var $rooms = $("#ContainerQuantity");
//    var b = $rooms.val();
//    if (b >= 1) {
//        b--;
//        $rooms.val(b);
//    }
//    else {
//        $("#Containersubs").prop("disabled", true);
//    }
//}