var hotelId;

$(document).ready(function () {
    hotelId = $("#hotelID").text();
   
    loadOfferDataTable();
});

function loadOfferDataTable() {
    hotelDataTable = $('#priceOfferData').DataTable({
        "lenghtChange": false,
        "language": {
            loadingRecords: "Please wait your offers are loading....."
        },
        "paginate": false,
        "filter": false,
        "info": true,
        "stateSave": true,
        "ajax": {
            "url": "/Search/GetCertainHotelPrice/",
            "data": { "hotelId": hotelId, "adultNumber": 1 },
            "headers": { RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() },
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { data: "offers[0].night", width: "5%" },
            { data: "offers[0].checkIn", width: "5%" },
            {
                data: "offers[0].rooms",
                render: function (data, type, row) {
                    return row.offers[0].rooms[0].roomName + '<br>(' + row.offers[0].rooms[0].boardName + ')';
                },
                width: "15%"
            },
            {
                data: "offers[0].price.amount",
                render: function (data, type, row) {
                    return row.offers[0].price.amount + '<br>(' + row.offers[0].price.currency + ')';
                },
                width: "5%"
            },
            {
                render: function (data, type, row) {
                    return '<div class="" role="group">' +
                        '<a href="/Booking?offerId=' + row.offers[0].offerId + '"  class="btn btn-primary mt-1"> <i class="bi bi-book"></i>Book Now</a>'
                        + ' </div>';
                },
                width: "5%"
            }
        ],
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
    });
}
