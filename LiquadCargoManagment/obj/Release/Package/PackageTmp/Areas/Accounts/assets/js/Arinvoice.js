function AddRow(A) {
    debugger;
    var parent = $(A).parent("td").parent("tr").html();
    let v = $("#GlTable tbody tr").length - 1;
    parent = parent.replace("btn-primary", "btn-danger").replace("fa-plus", "fa-trash").replace("AddRow", "remove").replace("ARInvoiceDetails[0]", "ARInvoiceDetails[" + $("#GlTable tbody tr").length + "]").replace("ARInvoiceDetails[0]", "ARInvoiceDetails[" + $("#GlTable tbody tr").length + "]").replace("ARInvoiceDetails[0]", "ARInvoiceDetails[" + $("#GlTable tbody tr").length + "]").replace("ARInvoiceDetails[0]", "ARInvoiceDetails[" + $("#GlTable tbody tr").length + "]").replace("ARInvoiceDetails[0]", "ARInvoiceDetails[" + $("#GlTable tbody tr").length + "]").replace("ARInvoiceDetails[0]", "ARInvoiceDetails[" + $("#GlTable tbody tr").length + "]").replace("ARInvoiceDetails[0]", "ARInvoiceDetails[" + $("#GlTable tbody tr").length + "]").replace("ARInvoiceDetails[0]", "ARInvoiceDetails[" + $("#GlTable tbody tr").length + "]").replace("ARInvoiceDetails[0]", "ARInvoiceDetails[" + $("#GlTable tbody tr").length + "]");
    $("#GlTable").find("tbody").append(`<tr>${parent}</tr>`);
}

function remove(e) {
    $(e).parent("td").parent("tr").fadeOut(300, function () { $(this).remove(); });
}

$("table").on("keyup", "input", function () {
    debugger;
    var tableRow = $(this).closest("tr");
    var Quantity = Number(tableRow.find("#Quantity").val());
    var Price = Number(tableRow.find("#Price").val());
    var DiscountRate = Number(tableRow.find("#DiscountRate").val());
    var TaxRate = Number(tableRow.find("#TaxRate").val());
    var Grand = Line + Tax - Discount;
    var Amount = Quantity * Price;
    var DiscountAmount = Amount * DiscountRate / 100;
    var PriceAfterDiscount = Amount - DiscountAmount;
    var TaxAmount = PriceAfterDiscount * TaxRate / 100;
    var LineTotal = PriceAfterDiscount + TaxAmount;
    tableRow.find("#DiscAmount").val(DiscountAmount.toFixed(2));
    tableRow.find("#PriceADisc").val(PriceAfterDiscount.toFixed(2));
    tableRow.find("#TaxAmount").val(TaxAmount.toFixed(2));
    tableRow.find("#LineTotal").val(LineTotal.toFixed(2));

});
function Grandtotal(q, totalColumnIndex, type) {
    let grTotal = 0;
    for (var i = 0; i < $(q).find("tbody tr").length; i++) {

        grTotal += parseFloat($(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim() == "" ? 0 : $(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim());
    }
    if (type == "Discount") {
        $(q).find("tfoot tr.TotalDiscount td:eq(7)").text(grTotal);
    }
    else if (type == "Tax") {
        $(q).find("tfoot tr.TotalTax td:eq(7)").text(grTotal);
    }
    else if (type == "Line") {
        $(q).find("tfoot tr.Total td:eq(7)").text(grTotal);
    }

    else if (type == "Debit") {
        $(q).find("tr.grand_total td:eq(5)").text(grTotal);
    }
}