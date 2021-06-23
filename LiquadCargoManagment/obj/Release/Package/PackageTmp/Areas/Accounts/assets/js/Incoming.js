function Customer() {
    $("#drop").hide();
    $("#Payment").show();
    $("#Payment1").show();
}
function ModalHide() {
    $("#modal-add").modal('hide');
}
function AddRow(A) {
    debugger;
    var parent = $(A).parent("td").parent("tr").html();
    parent = parent.replace("btn-primary", "btn-danger").replace("fa-plus", "fa-trash").replace("AddRow", "remove");
    $("#GlTable").find("tbody").append(`<tr>${parent}</tr>`);
}
function remove(e) {
    $(e).parent("td").parent("tr").fadeOut(300, function () { $(this).remove(); });
}
function Grandtotal(q, totalColumnIndex, type) {
    debugger;
    let grTotal = 0;
    for (var i = 0; i < $(q).find("tbody tr").length; i++) {

        grTotal += parseFloat($(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim() == "" ? 0 : $(q).find(`tbody tr:eq(${i}) td:eq(${totalColumnIndex}) input`).val().trim());
    }
    if (type == "Debit") {
        $(q).find("tfoot tr.Total td:eq(2)").text(grTotal);
    }
    else if (type == "Credit") {
        $(q).find("tfoot tr.Total td:eq(3)").text(grTotal);
    }
    else if (type == "Amount") {
        $(q).find("tr.grand_total td:eq(0)").text(grTotal);
    }
    else if (type == "Amount") {
        $(q).find("tr.grand_total td:eq(5)").text(grTotal);
    }

}