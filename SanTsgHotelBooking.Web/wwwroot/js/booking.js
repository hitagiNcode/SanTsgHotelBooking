var offerId;


$(document).ready(function () {
    //offerId = $("#offerId").text();
    //AjaxInit();
});

function AjaxInit() {
    $.ajax({
        url: '/Booking/BeginTransaction/',
        headers: {
            "RequestVerificationToken":
                $('input[name="__RequestVerificationToken"]').val()
        },
        data: { "offerId": offerId },
        type: "POST",
        dataType: "json",
        async: true,
        success: function (data) {
            alert("first step done");
        },
        error: function (xhr, textStatus, error) {
            alert(xhr.statusText);
        },
        failure: function (response) {
            alert("failure " + response.responseText);
        }
    });
}