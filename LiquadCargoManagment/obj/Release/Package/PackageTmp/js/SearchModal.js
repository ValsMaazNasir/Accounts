
function DateToFrom() {
    $("#DateTo").datepicker({
        altField: "#DateTo",
        altFormat: "d-M-y",
        changeMonth: true,
        changeYear: true,
        yearRange: "1947:5000"
    });
    $("#DateFrom").datepicker({
        altField: "#DateFrom",
        altFormat: "d-M-y",
        changeMonth: true,
        changeYear: true,
        yearRange: "1947:5000"
    });
    $("#DeliveryDate").datepicker({
        altField: "#DeliveryDate",
        altFormat: "d-M-y",
        changeMonth: true,
        changeYear: true,
        yearRange: "1947:5000"
    });
}